namespace HLS.Download.UI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDoIt = new System.Windows.Forms.Button();
            this.txbUrls = new System.Windows.Forms.TextBox();
            this.txbLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnKillAllAria2 = new System.Windows.Forms.Button();
            this.btnStartAria2 = new System.Windows.Forms.Button();
            this.btnOpenAria2WebUI = new System.Windows.Forms.Button();
            this.btnOpenDownloadDir = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDoIt);
            this.groupBox1.Controls.Add(this.txbUrls);
            this.groupBox1.Location = new System.Drawing.Point(12, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "地址";
            // 
            // btnDoIt
            // 
            this.btnDoIt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoIt.Location = new System.Drawing.Point(399, 20);
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size(43, 53);
            this.btnDoIt.TabIndex = 1;
            this.btnDoIt.Text = "下载";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler(this.btnDoIt_Click);
            // 
            // txbUrls
            // 
            this.txbUrls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbUrls.Location = new System.Drawing.Point(6, 20);
            this.txbUrls.Multiline = true;
            this.txbUrls.Name = "txbUrls";
            this.txbUrls.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbUrls.Size = new System.Drawing.Size(386, 53);
            this.txbUrls.TabIndex = 0;
            this.txbUrls.Text = "https://video.fjhps.com/20180303/E1okxWlJ/index.m3u8\r\nhttps://video.fjhps.com/201" +
    "80303/hD8sDBV1/index.m3u8";
            this.txbUrls.TextChanged += new System.EventHandler(this.txbUrls_TextChanged);
            // 
            // txbLog
            // 
            this.txbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbLog.Location = new System.Drawing.Point(12, 160);
            this.txbLog.Multiline = true;
            this.txbLog.Name = "txbLog";
            this.txbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbLog.Size = new System.Drawing.Size(448, 129);
            this.txbLog.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnOpenDownloadDir);
            this.groupBox2.Controls.Add(this.btnKillAllAria2);
            this.groupBox2.Controls.Add(this.btnStartAria2);
            this.groupBox2.Controls.Add(this.btnOpenAria2WebUI);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 66);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Aria2控制台:";
            // 
            // btnKillAllAria2
            // 
            this.btnKillAllAria2.Location = new System.Drawing.Point(346, 20);
            this.btnKillAllAria2.Name = "btnKillAllAria2";
            this.btnKillAllAria2.Size = new System.Drawing.Size(96, 36);
            this.btnKillAllAria2.TabIndex = 2;
            this.btnKillAllAria2.Text = "杀掉所有进程";
            this.btnKillAllAria2.UseVisualStyleBackColor = true;
            this.btnKillAllAria2.Click += new System.EventHandler(this.btnKillAllAria2_Click);
            // 
            // btnStartAria2
            // 
            this.btnStartAria2.Location = new System.Drawing.Point(6, 20);
            this.btnStartAria2.Name = "btnStartAria2";
            this.btnStartAria2.Size = new System.Drawing.Size(96, 36);
            this.btnStartAria2.TabIndex = 2;
            this.btnStartAria2.Text = "启动";
            this.btnStartAria2.UseVisualStyleBackColor = true;
            this.btnStartAria2.Click += new System.EventHandler(this.btnStartAria2_Click);
            // 
            // btnOpenAria2WebUI
            // 
            this.btnOpenAria2WebUI.Location = new System.Drawing.Point(108, 20);
            this.btnOpenAria2WebUI.Name = "btnOpenAria2WebUI";
            this.btnOpenAria2WebUI.Size = new System.Drawing.Size(96, 36);
            this.btnOpenAria2WebUI.TabIndex = 1;
            this.btnOpenAria2WebUI.Text = "打开WebUI";
            this.btnOpenAria2WebUI.UseVisualStyleBackColor = true;
            this.btnOpenAria2WebUI.Click += new System.EventHandler(this.btnOpenAria2WebUI_Click);
            // 
            // btnOpenDownloadDir
            // 
            this.btnOpenDownloadDir.Location = new System.Drawing.Point(210, 20);
            this.btnOpenDownloadDir.Name = "btnOpenDownloadDir";
            this.btnOpenDownloadDir.Size = new System.Drawing.Size(96, 36);
            this.btnOpenDownloadDir.TabIndex = 3;
            this.btnOpenDownloadDir.Text = "打开下载目录";
            this.btnOpenDownloadDir.UseVisualStyleBackColor = true;
            this.btnOpenDownloadDir.Click += new System.EventHandler(this.btnOpenDownloadDir_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 301);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txbLog);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDoIt;
        private System.Windows.Forms.TextBox txbUrls;
        private System.Windows.Forms.TextBox txbLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOpenAria2WebUI;
        private System.Windows.Forms.Button btnStartAria2;
        private System.Windows.Forms.Button btnKillAllAria2;
        private System.Windows.Forms.Button btnOpenDownloadDir;
    }
}

