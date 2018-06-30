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

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            var settings = new Aria2cSettings();
            settings.Aria2Path = Path.Combine(Environment.CurrentDirectory, "Aria2\\aria2c.exe");
            settings.Aria2Host = "localhost";
            settings.Aria2Port = 6800;
            Aria2cRuntime.Load(settings);
            try
            {
                var urls = txbUrls.Text.Split(Environment.NewLine.ToArray());
                foreach (var url in urls)
                {
                    Debug.WriteLine("url:");
                    Debug.WriteLine(url);
                    var task = HLS.Download.Models.HLSStream.Open(url);
                    Debug.WriteLine("Result:");
                    Debug.WriteLine(task.Result);

                }
            }
            finally
            {
                Aria2cRuntime.ShutDown();
            }
        }
    }
}
