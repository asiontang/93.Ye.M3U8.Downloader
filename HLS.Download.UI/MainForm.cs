using FlyVR.Aria2;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HLS.Download.UI
{
    public partial class MainForm : Form
    {
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
            try
            {
                var urls = getDownloadUrls();
                foreach (var url in urls)
                {
                    Debug.WriteLine("url：");
                    Debug.WriteLine(url);
                    var task = HLS.Download.Models.HLSStream.Open(url);
                    Debug.WriteLine("Result：");
                    Debug.WriteLine(task.Result);

                }
            }
            finally
            {
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

            btnStartAria2.Enabled = !Aria2cRuntime.IsLoaded;
            WriteLog(TAG, "检测到 Aria2 状态为" + (!btnStartAria2.Enabled ? "【已启动】" : "[未启动]"));

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

        private void btnKillAllAria2_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            var TAG = "杀掉所有Aria2进程";
            WriteLog(TAG, "执行中");
            try
            {
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
            Process.Start("http://aria2c.com/");
        }

        private void WriteLog(String tag, String info)
        {
#if DEBUG
            Debug.Write(tag);
            Debug.Write("：");
            Debug.Write(info);
            Debug.WriteLine(info);
#endif

            txbLog.AppendText(tag);
            txbLog.AppendText("：");
            txbLog.AppendText(info);
            txbLog.AppendText(Environment.NewLine);
        }
    }
}
