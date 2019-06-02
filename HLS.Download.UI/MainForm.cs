using FlyVR.Aria2;
using HLS.Download.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLS.Download.UI
{
    //================================================================================
    //灵感标记规则：已完成的打√，不准备完成的打×，尚未未处理的打？
    //================================================================================
    //√数量从1K多激增到4K多，而且异常后，停止计时器了还是在执行！
    //？数量多时，加大刷新间隔？可以缓解？
    //×设计新的思路，假如按顺序下载，则只遍历当前下载数量的16*N倍？前方全下载，后面的全是未下载，中间的部分需要更新状态？
    //？对于TS文件来说，C#端不需要监听状态。只要监听m3u8即可？那如何感知到所有文件下载完成了？是否有全局状态感知？再触发合并？
    //×-y 自动覆盖
    //？-report 自动分析日志是否有遗漏的切片或别的错误，在界面增加正则表达式框，符合的行输出到日志列表。
    //？检测report没错误时，自动删除临时文件的功能？
    //√自解压包能压缩到14MB
    //√检测包含 *.TS 所在的 *.m3u8 文件，然后修改路径？还是在解析时顺便修改好另存为一个新的 .m3u8 文件？
    //？根据Map输出友好的文件名提示，而不是任务ID
    //？增加一个删除Setion的功能？
    //×增加一个自动分组的显示方式？看看是否支持 Aria2
    //×缺少时，手工下载 aria2c ,ffmpeg.exe提醒？可以的结构目录树展示？
    //？ff 解压后的路径 怎么设置工作目录？否则无法识别！！
    //？或者清单文件里直接设置 ts 文件的绝对路径？ 导出文件也设置为绝对路径！
    //？需要去掉 / 符号！！！本地路径才识别。或者换为\符号？
    //？可以使用COPY /B *.ts name.mp4 来解决无法通过 ffmpeg 合并的视频文件。如提示 Timestamps are unset in a packet for stream 0
    public partial class MainForm : Form
    {
        private Aria2c mAria2c;
        private decimal? mSelectedBandwidth = null;
        private string mSelectedUserAgent;
        private Dictionary<string, string> mUrlAndNameMap = new Dictionary<string, string>();
        private Dictionary<string, string> mUrlAndDownloadDirMap = new Dictionary<string, string>();
        private Dictionary<string, string> mPidAndUrlMap = new Dictionary<string, string>();

        public MainForm()
        {
            InitializeComponent();
            mSelectedUserAgent = cbbUserAgent.Text;
        }

        private string[] getDownloadUrls()
        {
            return txbUrls.Text.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private void txbUrls_TextChanged(object sender, EventArgs e)
        {
            btnDoIt.Text = string.Format("下载\n({0})", getDownloadUrls().Length);
        }

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            var TAG = "下载";
            WriteLog(null, Environment.NewLine);
            WriteLog(TAG, "执行中");
            try
            {
                //防止没启动Aria2
                if (mAria2c == null)
                    btnStartAria2_Click(btnStartAria2, null);

                mUrlAndNameMap.Clear();
                mUrlAndDownloadDirMap.Clear();
                mPidAndUrlMap.Clear();

                var downloadDir = Aria2cRuntime.DownLoadDirectory;
                List<string> urls = new List<string>();
                foreach (var s in getDownloadUrls())
                {
                    var urlAndName = s.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (urlAndName.Length != 2)
                    {
                        WriteLog(TAG, "以下格式有误");
                        WriteLog(TAG, s);
                        return;
                    }
                    var name = urlAndName[0];
                    var url = urlAndName[1];
                    var dir = Path.Combine(downloadDir, name);//使用文件名作为新目录来临时存储多个切片。
                    Directory.CreateDirectory(dir);

                    urls.Add(url);
                    mUrlAndNameMap.Add(url, name);
                    mUrlAndDownloadDirMap.Add(url, dir);

                    WriteLog(TAG, string.Format("文件名={0} 下载网址={1}", name, url));
                    WriteLog(TAG, string.Format("文件名={0} 下载目录={1}", name, dir));
                }

                foreach (var url in urls)
                    mPidAndUrlMap.Add(mAria2c.AddUri(url, "", mUrlAndDownloadDirMap[url], mSelectedUserAgent), url);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                WriteLog(TAG, "出现未知异常");
                WriteLog(TAG, ex.ToString());
            }
            finally
            {
                ((Button)sender).Enabled = true;

                Cursor.Current = Cursors.Default;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var TAG = "主程序";
            WriteLog(TAG, "启动中");

            var settings = new Aria2cSettings();
            settings.Aria2Path = Path.Combine(Environment.CurrentDirectory, "Aria2\\aria2c.exe");
            settings.Aria2Host = "localhost";
            settings.Aria2Port = 6800;
            Aria2cRuntime.Settings = settings;

            //不再在启动时检测状态，发现即使检测到已经启动，但是后续的别的模块还是依赖于点击界面的启动按钮才有效。
            //因此没有任何检测的意义了。因为都必须要点击至少一次“启动”按钮才能继续使用别的功能。
            //NO:btnStartAria2.Enabled = !Aria2cRuntime.IsLoaded;
            //NO:WriteLog(TAG, "检测到 Aria2 状态为" + (!btnStartAria2.Enabled ? "【已启动】" : "[未启动]"));

            WriteLog(TAG, "启动完毕。");
        }

        private void btnStartAria2_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            var TAG = "启动Aria2";
            WriteLog(null, Environment.NewLine);
            WriteLog(TAG, "执行中");
            try
            {
                //进程遍历所有相同名字进程，假如存在则不再启动。
                var plist = Process.GetProcessesByName("aria2c");
                if (plist.Length == 0)
                    Aria2cRuntime.Start();

                WriteLog(TAG, "执行完毕。");

                WriteLog(TAG, "执行结果：检测中");
                btnStartAria2.Enabled = !Aria2cRuntime.IsLoaded;
                WriteLog(TAG, "执行结果：" + (!btnStartAria2.Enabled ? "【成功】" : "[失败]"));

                {
                    var downloadPath = Path.Combine(Environment.CurrentDirectory, "Download");
                    if (!String.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
                        downloadPath = folderBrowserDialog1.SelectedPath;
                    Directory.CreateDirectory(downloadPath);
                    Aria2cRuntime.DownLoadDirectory = downloadPath;
                    WriteLog(TAG, "设置全局下载目录=" + downloadPath);
                }

                mAria2c = new Aria2c();
                mAria2c.OnGlobalStatusChanged += delegate (object obj, Aria2cGlobalStatEvent gevent)
                {
                    if (txbAria2GlobalInfo.InvokeRequired)
                    {
                        while (!txbAria2GlobalInfo.IsHandleCreated)
                        {
                            //解决窗体关闭时出现“访问已释放句柄“的异常
                            if (txbAria2GlobalInfo.Disposing || txbAria2GlobalInfo.IsDisposed)
                                return;
                        }
                        txbAria2GlobalInfo.Invoke(new MethodInvoker(
                            () =>
                            txbAria2GlobalInfo.Text = string.Format("DownloadSpeed={0:F2}KB/S NumActive={1} NumWaiting={2} NumStopped={3}"
                              , gevent.Stat.DownloadSpeed / 1024d
                              , gevent.Stat.NumActive
                              , gevent.Stat.NumWaiting
                              , gevent.Stat.NumStopped
                              ))
                          );
                        return;
                    }
                };
                mAria2c.OnError += delegate (object obj, Aria2cTaskEvent taskEvent)
                {
                    var TAG2 = "下载状态更新";
                    var uri = taskEvent.Task.Files[0].Uris[0].Uri.ToString();
                    if (taskEvent.Task.ErrorCode != 0)
                    {
                        WriteLog(TAG2, uri);
                        WriteLog(TAG2, string.Format("OnFinish={0}; ErrorCode={1}; ErrorMessage={2}; "
                            , taskEvent.Gid
                            , taskEvent.Task.ErrorCode
                            , taskEvent.Task.ErrorMessage));
                        return;
                    }
                };
                mAria2c.OnFinish += delegate (object obj, Aria2cTaskEvent taskEvent)
                {
                    OnDownloadCompleted(taskEvent);
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                WriteLog(TAG, "出现未知异常");
                WriteLog(TAG, ex.ToString());

                //异常情况下，还是将按钮可用化，方便下次执行。
                ((Button)sender).Enabled = true;
            }
            finally
            {
                //由执行结果决定最终是否可用。
                //((Button)sender).Enabled = true;

                Cursor.Current = Cursors.Default;
            }
        }

        private void OnDownloadCompleted(Aria2cTaskEvent taskEvent)
        {
            var TAG2 = "下载状态更新";
            var uri = taskEvent.Task.Files[0].Uris[0].Uri.ToString();
            if (taskEvent.Task.ErrorCode != 0)
            {
                WriteLog(TAG2, uri);
                WriteLog(TAG2, string.Format("OnFinish={0}; ErrorCode={1}; ErrorMessage={2}; "
                    , taskEvent.Gid
                    , taskEvent.Task.ErrorCode
                    , taskEvent.Task.ErrorMessage));
                return;
            }
            var extension = Path.GetExtension(uri).ToLower();
            switch (extension)
            {
                case ".m3u8":
                    {
                        WriteLog(TAG2, uri);
                        WriteLog(TAG2, string.Format("OnFinish={0}; 下载完毕！", taskEvent.Gid));

                        //当播放序列文件下载完成时。
                        OnDownloadCompletedM3U8(taskEvent);
                    }
                    break;
                case ".ts":
                    {
                        //当视频流切片下载完成时。
                        OnDownloadCompletedTS(taskEvent);
                    }
                    break;
                default:
                    {
                        WriteLog(TAG2, "不受支持的后缀：" + extension);
                        WriteLog(TAG2, uri);
                    }
                    break;
            }
        }

        private void OnDownloadCompletedTS(Aria2cTaskEvent taskEvent)
        {
            try
            {
                //var file = taskEvent.Task.Files[0];
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private void OnDownloadCompletedM3U8(Aria2cTaskEvent taskEvent)
        {
            var TAG = "解析M3U8";
            WriteLog(TAG, "执行中");
            try
            {
                var file = taskEvent.Task.Files[0];
                var dir = Path.GetDirectoryName(file.Path);
                Uri baseUri;
                Uri.TryCreate(file.Uris[0].Uri, UriKind.Absolute, out baseUri);

                var t = HLS.Download.Models.HLSStream.Open(file.Path);
                var r = t.Result;

                //一个M3u8里可能定义了不同码率的新m3u8文件。
                if (r.Playlist.Length > 0)
                {
                    HLSPlaylist nextPlaylist = null;

                    #region 多码率选择策略
                    if (r.Playlist.Length == 1)
                        nextPlaylist = r.Playlist[0];
                    else
                    {
                        if (mSelectedBandwidth == null)
                        {
                            WriteLog(TAG, string.Format("停止下载视频流切片；因为存在多码率，且没有选择多码率下载策略或自定义码率无效:{0}", mSelectedBandwidth));

                            WriteLog(TAG, "支持的码率有：");
                            for (var pi = 0; pi < r.Playlist.Length; pi++)
                            {
                                var p = r.Playlist[pi];
                                WriteLog(TAG, String.Format("{0}:码率={1},分辨率={2}", pi + 1, p.BANDWIDTH, p.RESOLUTION));
                            }
                            return;
                        }
                        //根据多码率下载策略取出需要的码率下载.
                        decimal? lastBandwidth = null;
                        foreach (var p in r.Playlist)
                        {
                            var tmpBand = decimal.Parse(p.BANDWIDTH);
                            if (mSelectedBandwidth == decimal.MaxValue)
                            {
                                if (lastBandwidth == null || tmpBand > lastBandwidth)
                                    nextPlaylist = p;
                                continue;
                            }
                            if (mSelectedBandwidth == decimal.MinValue)
                            {
                                if (lastBandwidth == null || tmpBand < lastBandwidth)
                                    nextPlaylist = p;
                                continue;
                            }
                            if (mSelectedBandwidth == tmpBand)
                            {
                                nextPlaylist = p;
                                //自定义模式，匹配成功直接退出循环。
                                break;
                            }
                        }

                        //自定义模式，匹配失败时，将所有码率输出来，方便定位。
                        if (nextPlaylist == null)
                        {
                            WriteLog(TAG, string.Format("自定义码率匹配失败；自定义码率设置无效:{0}", mSelectedBandwidth));

                            WriteLog(TAG, "支持的码率有：");
                            for (var pi = 0; pi < r.Playlist.Length; pi++)
                            {
                                var p = r.Playlist[pi];
                                WriteLog(TAG, String.Format("{0}:码率={1},分辨率={2}", pi + 1, p.BANDWIDTH, p.RESOLUTION));
                            }
                            return;
                        }
                    }
                    #endregion

                    WriteLog(TAG, String.Format("下载指定码率={0},分辨率={1}", nextPlaylist.BANDWIDTH, nextPlaylist.RESOLUTION));
                    WriteLog(TAG, "下载指定码率:路径=" + nextPlaylist.URI);

                    //当下载了 index.m3u8 后指向了具体码率的 4405kb/hls/index.m3u8 此时因为文件名都是一样的，导致有BUG，会忽略下载。
                    if (Path.GetFileName(baseUri.AbsoluteUri) == Path.GetFileName(nextPlaylist.URI))
                        //于是将老的文件名重命名即可。
                        File.Move(file.Path, file.Path + ".old.m3u8");

                    var url = new Uri(baseUri, nextPlaylist.URI).AbsoluteUri;
                    var gid = mAria2c.AddUri(url, "", dir, mSelectedUserAgent);
                    WriteLog(TAG, string.Format("下载指定码率:任务ID={0}", gid));
                }
                else
                {
                    WriteLog(TAG, String.Format("需下载的视频流切片块数量={0}", r.Parts.Length));
                    foreach (var p in r.Parts)
                    {
                        var url = new Uri(baseUri, p.Path).AbsoluteUri;
                        mAria2c.AddUri(url, "", dir, mSelectedUserAgent);
                    }
                    WriteLog(TAG, String.Format("Aria2正在下载中"));
                }
            }
            catch (Exception ex)
            {
                WriteLog(TAG, "出现未知异常");
                WriteLog(TAG, ex.ToString());
            }
            finally
            {
                WriteLog(TAG, "执行完毕");
            }
        }

        private void btnKillAllAria2_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            var TAG = "杀掉所有Aria2进程";
            WriteLog(null, Environment.NewLine);
            WriteLog(TAG, "执行中");
            try
            {
                mAria2c?.Shutdown();
                mAria2c?.Dispose();
                mAria2c = null;
                Aria2cRuntime.ShutDown();

                //进程遍历所有相同名字进程。
                var plist = Process.GetProcessesByName("aria2c");
                foreach (var p in plist)
                    if (!p.HasExited)
                        p.Kill();

                WriteLog(TAG, string.Format("执行完毕:尝试杀掉{0}个进程。", plist.Length));
            }
            catch (Exception ex)
            {
                WriteLog(TAG, "出现未知异常");
                WriteLog(TAG, ex.ToString());
            }
            finally
            {
                WriteLog(TAG, "检测是否有进程残留中");
                btnStartAria2.Enabled = Process.GetProcessesByName("aria2c").Length == 0;
                WriteLog(TAG, "检测结果：" + (btnStartAria2.Enabled ? "【无残留】" : "[有残留]"));

                Cursor.Current = Cursors.Default;
                ((Button)sender).Enabled = true;
            }
        }

        private void btnOpenAria2WebUI_Click(object sender, EventArgs e)
        {
            //1.以下两个WebUI是一样的。只是语言不同，且都不够强大。
            //  中文：http://aria2c.com/
            //  原版：http://binux.github.io/yaaw/demo/

            //2.更强大的UI，支持查看和设置各种选项。 #!/settings/rpc/set?protocol=http&host=127.0.0.1&port=6800&interface=jsonrpc
            //  http://ariang.mayswind.net/latest/"

            //3.更强大的UI,支持过滤各种状态的任务,方便重新下载失败的任务.
            //  原版地址：https://ziahamza.github.io/webui-aria2/
            //  将其替换为国内的地址，并且修改为默认localhost模式，方便第一次使用。
            Process.Start("https://asiontang.gitee.io/webui-aria2/");
        }

        private void WriteLog(String tag, String info)
        {
            if (txbLog.InvokeRequired)
            {
                while (!txbLog.IsHandleCreated)
                {
                    //解决窗体关闭时出现“访问已释放句柄“的异常
                    if (txbLog.Disposing || txbLog.IsDisposed)
                        return;
                }
                txbLog.Invoke(new MethodInvoker(() => WriteLog(tag, info)));
                return;
            }
#if DEBUG
            if (tag == null)
                Debug.WriteLine(info);
            else
            {
                Debug.Write(tag);
                Debug.Write("：");
                Debug.WriteLine(info);
            }
#endif
            if (tag == null)
                txbLog.AppendText(info);
            else
            {
                txbLog.AppendText(tag);
                txbLog.AppendText("：");
                txbLog.AppendText(info);
                txbLog.AppendText(Environment.NewLine);
            }
        }

        private void btnOpenDownloadDir_Click(object sender, EventArgs e)
        {
            if (mAria2c == null)
                btnStartAria2_Click(btnStartAria2, null);
            Process.Start(Aria2cRuntime.DownLoadDirectory);
        }

        private void btnSetDownloadLocation_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                if (mAria2c != null)
                    Aria2cRuntime.DownLoadDirectory = folderBrowserDialog1.SelectedPath;
                WriteLog(null, Environment.NewLine);
                WriteLog("设置全局下载目录", folderBrowserDialog1.SelectedPath);
            }
        }

        private void rdbSelectBandWidthCustom_CheckedChanged(object sender, EventArgs e)
        {
            txbCustomBandwidth.Enabled = rdbSelectBandWidthCustom.Checked;
        }

        private void rdbSelectBandWidthMax_CheckedChanged(object sender, EventArgs e)
        {
            mSelectedBandwidth = decimal.MaxValue;
        }

        private void rdbSelectBandWidthMin_CheckedChanged(object sender, EventArgs e)
        {
            mSelectedBandwidth = decimal.MinValue;
        }

        private void txbCustomBandwidth_TextChanged(object sender, EventArgs e)
        {
            decimal tmpdecimal;
            decimal.TryParse(txbCustomBandwidth.Text, out tmpdecimal);
            mSelectedBandwidth = tmpdecimal;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            //防止没启动Aria2 获取不到正确的下载目录
            if (mAria2c == null)
                btnStartAria2_Click(btnStartAria2, null);

            ((Button)sender).Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            var TAG = "合并";
            WriteLog(null, Environment.NewLine);
            WriteLog(TAG, "执行中");
            try
            {
                //检测必要的程序都存在
                var ffmpegPath = Path.Combine(Environment.CurrentDirectory, "FFmpeg\\ffmpeg.exe");
                WriteLog(TAG, "FFmpeg所在路径=" + ffmpegPath);
                if (!File.Exists(ffmpegPath))
                {
                    WriteLog(TAG, "检测到该路径的FFmpeg文件不存在！停止执行。");
                    return;
                }

                //获取下载目录
                WriteLog(TAG, "获取获取下载目录中");
                var DownloadDirectory = Aria2cRuntime.DownLoadDirectory;
                WriteLog(TAG, "获取到的下载目录=" + DownloadDirectory);

                //遍历下载目录的所有目录里是否有必要的文件
                WriteLog(TAG, "获取获取下载目录中的所有子目录中");
                var dirs = Directory.GetDirectories(DownloadDirectory);
                WriteLog(TAG, "检测到子目录数量=" + dirs.Length);
                foreach (var dir in dirs)
                {
                    var dirName = Path.GetFileName(dir);
                    WriteLog(TAG, string.Format("子目录{0}：检测是否包含.M3U8文件中", dirName));

                    //获取目录中所有文件
                    var files = Directory.GetFiles(dir);

                    //提取其中的 两种不同的文件 列表。
                    var m3u8List = new List<string>();
                    var tsList = new List<string>();
                    foreach (var file in files)
                    {
                        switch (Path.GetExtension(file).ToLower())
                        {
                            case ".m3u8":
                                m3u8List.Add(file);
                                break;
                            case ".ts":
                                tsList.Add(file);
                                break;
                            default:
                                break;
                        }
                    }
                    if (m3u8List.Count == 0)
                    {
                        WriteLog(TAG, string.Format("子目录{0}：没有检测到.M3U8文件，跳过！", dirName));
                        continue;
                    }

                    //先从 m3u8 文件里提取出 ts 文件列表。
                    foreach (var m3u8File in m3u8List)
                    {
                        //忽略自己生成的新清单文件
                        if (m3u8File.ToLower().EndsWith("_new.m3u8"))
                        {
                            WriteLog(TAG, string.Format("子目录{0}：分析{1}文件结果：此文件是本程序临时生成的TS清单文件，跳过！"
                                , dirName, Path.GetFileName(m3u8File)));
                            continue;
                        }

                        WriteLog(TAG, string.Format("子目录{0}：分析{1}文件中", dirName, Path.GetFileName(m3u8File)));
                        var txt = File.ReadAllText(m3u8File);

                        //检测是否为TS清单文件
                        {
                            if (!txt.Contains(".ts") || !txt.Contains("EXTINF"))
                            {
                                WriteLog(TAG, string.Format("子目录{0}：分析{1}文件结果：此文件不是TS清单文件，跳过！"
                                    , dirName, Path.GetFileName(m3u8File)));
                                continue;
                            }
                        }

                        //计算总的分片数量。
                        {
                            var count1 = getCountOfString(txt, ".ts");
                            var count2 = getCountOfString(txt, "EXTINF");
                            if (count1 != count2)
                            {
                                WriteLog(TAG, string.Format("子目录{0}：分析{1}文件结果：.ts出现次数={2} 与 EXTINF出现次数={3} 不一致，跳过！"
                                    , dirName, Path.GetFileName(m3u8File), count1, count2));
                                continue;
                            }
                            if (tsList.Count != count1)
                            {
                                WriteLog(TAG, string.Format("子目录{0}：分析{1}文件结果：.ts切片理论数量={2} 与 实际数量={3} 不一致，跳过！"
                                    , dirName, Path.GetFileName(m3u8File), count1, tsList.Count));
                                continue;
                            }
                            WriteLog(TAG, string.Format("子目录{0}：分析{1}文件结果：检测到正确数量的.ts切片={2}"
                                , dirName, Path.GetFileName(m3u8File), tsList.Count));
                        }

                        //改造此清单文件形成本地可用的新清单文件
                        var newM3U8File = getNewM3U8File(txt, m3u8File);

                        //拼接最终执行的命令行脚本
                        {
                            var cmdPath = string.Format("{0}\\{1}.bat", Path.GetDirectoryName(newM3U8File), Path.GetFileNameWithoutExtension(newM3U8File));

                            WriteLog(TAG, string.Format("子目录{0}：分析{1}文件结果：生成批处理脚本中={2}"
                                , dirName, Path.GetFileName(m3u8File), Path.GetFileName(cmdPath)));

                            //{ffmpeg} -threads 1 -i {本地TS列表.m3u8} -c copy {文件名}.mkv
                            var cmd = txbMergeCMD.Text;
                            cmd = cmd.Replace("{ffmpeg}", ffmpegPath);
                            cmd = cmd.Replace("{本地TS列表.m3u8}", newM3U8File);
                            cmd = cmd.Replace("{文件名}", dirName);

                            File.WriteAllText(cmdPath, cmd, Encoding.Default);

                            WriteLog(TAG, string.Format("子目录{0}：分析{1}文件结果：执行批处理脚本中={2}"
                                , dirName, Path.GetFileName(m3u8File), Path.GetFileName(cmdPath)));

                            Process.Start(cmdPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(TAG, "出现未知异常");
                WriteLog(TAG, ex.ToString());
            }
            finally
            {
                WriteLog(TAG, "执行完毕");
                Cursor.Current = Cursors.Default;
                ((Button)sender).Enabled = true;
            }
        }

        private string getNewM3U8File(string txt, string oldFile)
        {
            //找到第一个 / 符号
            var sIndex1 = txt.IndexOf("/");

            //假如找不到URI的分隔符 / 则不需要转换了。情况0.
            if (sIndex1 == -1)
                /*--------------------------------------------------
                 * 情况0：
                 * #EXTINF:4.56,
                 * vKOnx78R3460000.ts
                 *--------------------------------------------------*/
                return oldFile;

            var needReplaceString = "";

            //找到 这个符号 之后的 .ts 
            var tsIndex = txt.IndexOf(".ts", sIndex1);

            //再从 tsIndex 往前找 / 符号
            var sIndex2 = txt.LastIndexOf("/", tsIndex);

            //当 sIndex2 = sIndex1 时，说明是情况1. 那么就不需要转换新列表
            if (sIndex1 == sIndex2)
                /*--------------------------------------------------
                 * 情况1：
                 * #EXTINF:4.56,
                 * /vKOnx78R3460000.ts
                 *---------------------------------------------------*/
                needReplaceString = "/";
            else
            {
                //查找第一个 分隔符 左边的换行符
                var newLineIndex = txt.LastIndexOf("\n", sIndex1);

                //假如 左边的换行符 和 分隔符位置一致时
                if (newLineIndex == sIndex1)
                {
                    /*--------------------------------------------------
                     * 情况2：
                     * #EXTINF:4.56,
                     * /20180303/E1okxWlJ/800kb/hls/vKOnx78R3460000.ts
                     *---------------------------------------------------*/

                    //截取两个 / 符号之间的字符串
                    needReplaceString = txt.Substring(sIndex1, sIndex2 - sIndex1 + 1/*+1目的是把/符号去掉！*/);
                }
                else
                {
                    /*--------------------------------------------------
                     * 情况3：
                     * #EXTINF:4.56,
                     * http://a.b.c/d/vKOnx78R3460000.ts
                     *---------------------------------------------------*/

                    //截取两个 / 符号之间的字符串
                    needReplaceString = txt.Substring(newLineIndex + 1/*+1目的是跳过换行符*/, sIndex2 - (newLineIndex + 1) + 1/*+1目的是把/符号去掉！*/);
                }
            }
            //将需要替换的文本都替换为空格。相当于删除掉。只保留后面的文件名 vKOnx78R3460000.ts
            var newText = txt.Replace(needReplaceString, "");

            //将替换好的新清单文本写入新文件
            var newFile = string.Format("{0}\\{1}_new.m3u8", Path.GetDirectoryName(oldFile), Path.GetFileNameWithoutExtension(oldFile));
            File.WriteAllText(newFile, newText);
            return newFile;
        }

        private int getCountOfString(string sourceText, string searchText)
        {
            var count = 0;
            var lastIndex = 0;
            while ((lastIndex = sourceText.IndexOf(searchText, lastIndex)) != -1)
            {
                lastIndex++;
                count++;
            }
            return count;
        }

        private void btnPauseAll_Click(object sender, EventArgs e)
        {
            if (mAria2c == null)
                btnStartAria2_Click(btnStartAria2, null);

            var result = mAria2c.ForcePauseAll();
            var TAG = "全部暂停下载";
            WriteLog(null, Environment.NewLine);
            WriteLog(TAG, "执行完毕");
            WriteLog(TAG, "执行结果=" + result);
        }

        private void btnUnPauseAll_Click(object sender, EventArgs e)
        {
            if (mAria2c == null)
                btnStartAria2_Click(btnStartAria2, null);

            var result = mAria2c.UnPauseAll();
            var TAG = "全部启动下载";
            WriteLog(null, Environment.NewLine);
            WriteLog(TAG, "执行完毕");
            WriteLog(TAG, "执行结果=" + result);
        }

        private void cbbUserAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelectedUserAgent = cbbUserAgent.Text;
        }

        private void btnDelSession_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            var TAG = "清空Aria2下载记录";
            WriteLog(null, Environment.NewLine);
            WriteLog(TAG, "执行中");
            try
            {
                var path = Path.Combine(Environment.CurrentDirectory, "Aria2\\aria2c.session");
                var bakPath = path + "." + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";

                WriteLog(TAG, string.Format("备份文件={0}", bakPath));
                File.Copy(path, bakPath);

                WriteLog(TAG, string.Format("删除文件={0}", path));
                File.Delete(path);

                WriteLog(TAG, string.Format("新建空文件={0}", path));
                File.WriteAllText(path, "");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                WriteLog(TAG, "出现未知异常");
                WriteLog(TAG, ex.ToString());
            }
            finally
            {
                ((Button)sender).Enabled = true;

                Cursor.Current = Cursors.Default;
            }
        }
    }
}
