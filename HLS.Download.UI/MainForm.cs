using FlyVR.Aria2;
using HLS.Download.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HLS.Download.UI
{
    public partial class MainForm : Form
    {
        private Aria2c mAria2c;
        private decimal? mSelectedBandwidth = null;
        private Dictionary<string, string> mUrlAndNameMap = new Dictionary<string, string>();
        private Dictionary<string, string> mUrlAndDownloadDirMap = new Dictionary<string, string>();
        private Dictionary<string, string> mPidAndUrlMap = new Dictionary<string, string>();

        public MainForm()
        {
            InitializeComponent();
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
            WriteLog(TAG, "执行中");
            try
            {
                //防止没启动Aria2
                if (mAria2c == null)
                    btnStartAria2_Click(btnStartAria2, null);

                mUrlAndNameMap.Clear();
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
                    mPidAndUrlMap.Add(mAria2c.AddUri(url, "", mUrlAndDownloadDirMap[url]), url);
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
            WriteLog(TAG, "执行中");
            try
            {
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
                    var url = new Uri(baseUri, nextPlaylist.URI).AbsoluteUri;
                    var gid = mAria2c.AddUri(url, "", dir);
                    WriteLog(TAG, string.Format("下载指定码率:任务ID={0}", gid));
                }
                else
                {
                    WriteLog(TAG, String.Format("需下载的视频流切片块数量={0}", r.Parts.Length));
                    foreach (var p in r.Parts)
                    {
                        var url = new Uri(baseUri, p.Path).AbsoluteUri;
                        mAria2c.AddUri(url, "", dir);
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
            }
        }

        private void btnKillAllAria2_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            var TAG = "杀掉所有Aria2进程";
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
            //以下两个WebUI是一样的。只是语言不同，且都不够强大。
            //中文：http://aria2c.com/
            //原版：http://binux.github.io/yaaw/demo/

            //更强大的UI，支持查看和设置各种选项。
            Process.Start("http://ariang.mayswind.net/latest/");

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
            Debug.Write(tag);
            Debug.Write("：");
            Debug.WriteLine(info);
#endif
            txbLog.AppendText(tag);
            txbLog.AppendText("：");
            txbLog.AppendText(info);
            txbLog.AppendText(Environment.NewLine);
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
    }
}
