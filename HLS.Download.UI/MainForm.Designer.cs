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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbbUserAgent = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDoIt = new System.Windows.Forms.Button();
            this.txbUrls = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txbCustomBandwidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbSelectBandWidthCustom = new System.Windows.Forms.RadioButton();
            this.rdbSelectBandWidthMin = new System.Windows.Forms.RadioButton();
            this.rdbSelectBandWidthMax = new System.Windows.Forms.RadioButton();
            this.txbLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txbAria2GlobalInfo = new System.Windows.Forms.TextBox();
            this.btnUnPauseAll = new System.Windows.Forms.Button();
            this.btnPauseAll = new System.Windows.Forms.Button();
            this.btnSetDownloadLocation = new System.Windows.Forms.Button();
            this.btnOpenDownloadDir = new System.Windows.Forms.Button();
            this.btnKillAllAria2 = new System.Windows.Forms.Button();
            this.btnStartAria2 = new System.Windows.Forms.Button();
            this.btnOpenAria2WebUI = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnMerge = new System.Windows.Forms.Button();
            this.txbMergeCMD = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDelSession = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnDoIt);
            this.groupBox1.Controls.Add(this.txbUrls);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下载控制台:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbbUserAgent);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(6, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(510, 30);
            this.panel2.TabIndex = 11;
            // 
            // cbbUserAgent
            // 
            this.cbbUserAgent.FormattingEnabled = true;
            this.cbbUserAgent.Items.AddRange(new object[] {
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like" +
                " Gecko) Version/7.0.3 Safari/7046A194A",
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) C" +
                "hrome/64.0.3282.140 Safari/537.36",
            "Mozilla/5.0 (Linux; U; Android 2.3.5; zh-cn; HTC_IncredibleS_S710e Build/GRJ90) A" +
                "ppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1",
            "Mozilla/5.0 (compatible, MSIE 11, Windows NT 6.3; Trident/7.0; rv:11.0) like Geck" +
                "o",
            "Mozilla/4.0 (compatible; MSIE 6.0b; Windows NT 5.1)",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTM" +
                "L, like Gecko) Version/11.0 Mobile/15A356 Safari/604.1",
            "",
            "",
            "TIP:",
            "常用UserAgent查看地址：",
            "https://developers.whatismybrowser.com/useragents/explore/operating_platform/ipho" +
                "ne/",
            "http://www.useragentstring.com/pages/useragentstring.php"});
            this.cbbUserAgent.Location = new System.Drawing.Point(65, 3);
            this.cbbUserAgent.Name = "cbbUserAgent";
            this.cbbUserAgent.Size = new System.Drawing.Size(439, 20);
            this.cbbUserAgent.TabIndex = 11;
            this.cbbUserAgent.Text = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) C" +
    "hrome/64.0.3282.140 Safari/537.36";
            this.cbbUserAgent.SelectedIndexChanged += new System.EventHandler(this.cbbUserAgent_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "UserAgent:";
            this.toolTip1.SetToolTip(this.label4, "Set user agent for HTTP(S) downloads. Default: aria2/$VERSION, $VERSION is replac" +
        "ed by package version.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "示例:ABC视频 http://a.cn/c/d.m3u8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "正确格式:文件名 空格 网址\r\n";
            // 
            // btnDoIt
            // 
            this.btnDoIt.Location = new System.Drawing.Point(465, 104);
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.Size = new System.Drawing.Size(43, 59);
            this.btnDoIt.TabIndex = 1;
            this.btnDoIt.Text = "下载";
            this.btnDoIt.UseVisualStyleBackColor = true;
            this.btnDoIt.Click += new System.EventHandler(this.btnDoIt_Click);
            // 
            // txbUrls
            // 
            this.txbUrls.Location = new System.Drawing.Point(6, 104);
            this.txbUrls.Multiline = true;
            this.txbUrls.Name = "txbUrls";
            this.txbUrls.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbUrls.Size = new System.Drawing.Size(454, 59);
            this.txbUrls.TabIndex = 0;
            this.txbUrls.Text = "02 https://video.fjhps.com/20180303/E1okxWlJ/index.m3u8\r\n03 https://video.fjhps.c" +
    "om/20180303/hD8sDBV1/index.m3u8";
            this.txbUrls.TextChanged += new System.EventHandler(this.txbUrls_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbCustomBandwidth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.rdbSelectBandWidthCustom);
            this.panel1.Controls.Add(this.rdbSelectBandWidthMin);
            this.panel1.Controls.Add(this.rdbSelectBandWidthMax);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 30);
            this.panel1.TabIndex = 6;
            // 
            // txbCustomBandwidth
            // 
            this.txbCustomBandwidth.Location = new System.Drawing.Point(275, 3);
            this.txbCustomBandwidth.Name = "txbCustomBandwidth";
            this.txbCustomBandwidth.Size = new System.Drawing.Size(229, 21);
            this.txbCustomBandwidth.TabIndex = 6;
            this.txbCustomBandwidth.Text = "000000";
            this.toolTip1.SetToolTip(this.txbCustomBandwidth, "当只有一种码率时，此设置会被忽略。");
            this.txbCustomBandwidth.WordWrap = false;
            this.txbCustomBandwidth.TextChanged += new System.EventHandler(this.txbCustomBandwidth_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "多码率选择策略:";
            this.toolTip1.SetToolTip(this.label3, "当只有一种码率时，此设置会被忽略。");
            // 
            // rdbSelectBandWidthCustom
            // 
            this.rdbSelectBandWidthCustom.AutoSize = true;
            this.rdbSelectBandWidthCustom.Checked = true;
            this.rdbSelectBandWidthCustom.Location = new System.Drawing.Point(210, 6);
            this.rdbSelectBandWidthCustom.Name = "rdbSelectBandWidthCustom";
            this.rdbSelectBandWidthCustom.Size = new System.Drawing.Size(59, 16);
            this.rdbSelectBandWidthCustom.TabIndex = 7;
            this.rdbSelectBandWidthCustom.TabStop = true;
            this.rdbSelectBandWidthCustom.Text = "自定义";
            this.toolTip1.SetToolTip(this.rdbSelectBandWidthCustom, "当只有一种码率时，此设置会被忽略。");
            this.rdbSelectBandWidthCustom.UseVisualStyleBackColor = true;
            this.rdbSelectBandWidthCustom.CheckedChanged += new System.EventHandler(this.rdbSelectBandWidthCustom_CheckedChanged);
            // 
            // rdbSelectBandWidthMin
            // 
            this.rdbSelectBandWidthMin.AutoSize = true;
            this.rdbSelectBandWidthMin.Location = new System.Drawing.Point(157, 6);
            this.rdbSelectBandWidthMin.Name = "rdbSelectBandWidthMin";
            this.rdbSelectBandWidthMin.Size = new System.Drawing.Size(47, 16);
            this.rdbSelectBandWidthMin.TabIndex = 8;
            this.rdbSelectBandWidthMin.TabStop = true;
            this.rdbSelectBandWidthMin.Text = "最小";
            this.toolTip1.SetToolTip(this.rdbSelectBandWidthMin, "当只有一种码率时，此设置会被忽略。");
            this.rdbSelectBandWidthMin.UseVisualStyleBackColor = true;
            this.rdbSelectBandWidthMin.CheckedChanged += new System.EventHandler(this.rdbSelectBandWidthMin_CheckedChanged);
            // 
            // rdbSelectBandWidthMax
            // 
            this.rdbSelectBandWidthMax.AutoSize = true;
            this.rdbSelectBandWidthMax.Location = new System.Drawing.Point(104, 6);
            this.rdbSelectBandWidthMax.Name = "rdbSelectBandWidthMax";
            this.rdbSelectBandWidthMax.Size = new System.Drawing.Size(47, 16);
            this.rdbSelectBandWidthMax.TabIndex = 9;
            this.rdbSelectBandWidthMax.TabStop = true;
            this.rdbSelectBandWidthMax.Text = "最大";
            this.toolTip1.SetToolTip(this.rdbSelectBandWidthMax, "当只有一种码率时，此设置会被忽略。");
            this.rdbSelectBandWidthMax.UseVisualStyleBackColor = true;
            this.rdbSelectBandWidthMax.CheckedChanged += new System.EventHandler(this.rdbSelectBandWidthMax_CheckedChanged);
            // 
            // txbLog
            // 
            this.txbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbLog.Location = new System.Drawing.Point(6, 15);
            this.txbLog.Multiline = true;
            this.txbLog.Name = "txbLog";
            this.txbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbLog.Size = new System.Drawing.Size(432, 571);
            this.txbLog.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDelSession);
            this.groupBox2.Controls.Add(this.txbAria2GlobalInfo);
            this.groupBox2.Controls.Add(this.btnUnPauseAll);
            this.groupBox2.Controls.Add(this.btnPauseAll);
            this.groupBox2.Controls.Add(this.btnSetDownloadLocation);
            this.groupBox2.Controls.Add(this.btnOpenDownloadDir);
            this.groupBox2.Controls.Add(this.btnKillAllAria2);
            this.groupBox2.Controls.Add(this.btnStartAria2);
            this.groupBox2.Controls.Add(this.btnOpenAria2WebUI);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 127);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Aria2控制台:";
            // 
            // txbAria2GlobalInfo
            // 
            this.txbAria2GlobalInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbAria2GlobalInfo.Enabled = false;
            this.txbAria2GlobalInfo.Location = new System.Drawing.Point(6, 100);
            this.txbAria2GlobalInfo.Name = "txbAria2GlobalInfo";
            this.txbAria2GlobalInfo.ReadOnly = true;
            this.txbAria2GlobalInfo.Size = new System.Drawing.Size(504, 21);
            this.txbAria2GlobalInfo.TabIndex = 7;
            // 
            // btnUnPauseAll
            // 
            this.btnUnPauseAll.Location = new System.Drawing.Point(108, 58);
            this.btnUnPauseAll.Name = "btnUnPauseAll";
            this.btnUnPauseAll.Size = new System.Drawing.Size(96, 36);
            this.btnUnPauseAll.TabIndex = 6;
            this.btnUnPauseAll.Text = "全部开始下载";
            this.btnUnPauseAll.UseVisualStyleBackColor = true;
            this.btnUnPauseAll.Click += new System.EventHandler(this.btnUnPauseAll_Click);
            // 
            // btnPauseAll
            // 
            this.btnPauseAll.Location = new System.Drawing.Point(6, 58);
            this.btnPauseAll.Name = "btnPauseAll";
            this.btnPauseAll.Size = new System.Drawing.Size(96, 36);
            this.btnPauseAll.TabIndex = 5;
            this.btnPauseAll.Text = "全部暂停下载";
            this.btnPauseAll.UseVisualStyleBackColor = true;
            this.btnPauseAll.Click += new System.EventHandler(this.btnPauseAll_Click);
            // 
            // btnSetDownloadLocation
            // 
            this.btnSetDownloadLocation.Location = new System.Drawing.Point(312, 16);
            this.btnSetDownloadLocation.Name = "btnSetDownloadLocation";
            this.btnSetDownloadLocation.Size = new System.Drawing.Size(96, 36);
            this.btnSetDownloadLocation.TabIndex = 4;
            this.btnSetDownloadLocation.Text = "设置下载目录";
            this.btnSetDownloadLocation.UseVisualStyleBackColor = true;
            this.btnSetDownloadLocation.Click += new System.EventHandler(this.btnSetDownloadLocation_Click);
            // 
            // btnOpenDownloadDir
            // 
            this.btnOpenDownloadDir.Location = new System.Drawing.Point(210, 16);
            this.btnOpenDownloadDir.Name = "btnOpenDownloadDir";
            this.btnOpenDownloadDir.Size = new System.Drawing.Size(96, 36);
            this.btnOpenDownloadDir.TabIndex = 3;
            this.btnOpenDownloadDir.Text = "打开下载目录";
            this.btnOpenDownloadDir.UseVisualStyleBackColor = true;
            this.btnOpenDownloadDir.Click += new System.EventHandler(this.btnOpenDownloadDir_Click);
            // 
            // btnKillAllAria2
            // 
            this.btnKillAllAria2.Location = new System.Drawing.Point(414, 16);
            this.btnKillAllAria2.Name = "btnKillAllAria2";
            this.btnKillAllAria2.Size = new System.Drawing.Size(96, 36);
            this.btnKillAllAria2.TabIndex = 2;
            this.btnKillAllAria2.Text = "杀掉所有进程";
            this.btnKillAllAria2.UseVisualStyleBackColor = true;
            this.btnKillAllAria2.Click += new System.EventHandler(this.btnKillAllAria2_Click);
            // 
            // btnStartAria2
            // 
            this.btnStartAria2.Location = new System.Drawing.Point(6, 16);
            this.btnStartAria2.Name = "btnStartAria2";
            this.btnStartAria2.Size = new System.Drawing.Size(96, 36);
            this.btnStartAria2.TabIndex = 2;
            this.btnStartAria2.Text = "启动";
            this.btnStartAria2.UseVisualStyleBackColor = true;
            this.btnStartAria2.Click += new System.EventHandler(this.btnStartAria2_Click);
            // 
            // btnOpenAria2WebUI
            // 
            this.btnOpenAria2WebUI.Location = new System.Drawing.Point(108, 16);
            this.btnOpenAria2WebUI.Name = "btnOpenAria2WebUI";
            this.btnOpenAria2WebUI.Size = new System.Drawing.Size(96, 36);
            this.btnOpenAria2WebUI.TabIndex = 1;
            this.btnOpenAria2WebUI.Text = "打开WebUI";
            this.btnOpenAria2WebUI.UseVisualStyleBackColor = true;
            this.btnOpenAria2WebUI.Click += new System.EventHandler(this.btnOpenAria2WebUI_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnMerge);
            this.groupBox3.Controls.Add(this.txbMergeCMD);
            this.groupBox3.Location = new System.Drawing.Point(12, 325);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(516, 279);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "合并控制台:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 96);
            this.label5.TabIndex = 3;
            this.label5.Text = "{ffmpeg}\r\n    会自动替换为本程序目录下的exe所在路径\r\n\r\n{本地TS列表.m3u8}\r\n    会自动替换为本地路径的切片列表文件\r\n\r\n{文件" +
    "名}\r\n    会自动替换为下载时设置的值";
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(465, 116);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(43, 154);
            this.btnMerge.TabIndex = 1;
            this.btnMerge.Text = "手工合并";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // txbMergeCMD
            // 
            this.txbMergeCMD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMergeCMD.Location = new System.Drawing.Point(5, 116);
            this.txbMergeCMD.Multiline = true;
            this.txbMergeCMD.Name = "txbMergeCMD";
            this.txbMergeCMD.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbMergeCMD.Size = new System.Drawing.Size(454, 154);
            this.txbMergeCMD.TabIndex = 0;
            this.txbMergeCMD.Text = resources.GetString("txbMergeCMD.Text");
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txbLog);
            this.groupBox4.Location = new System.Drawing.Point(534, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(446, 592);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "日志信息:";
            // 
            // btnDelSession
            // 
            this.btnDelSession.Location = new System.Drawing.Point(210, 58);
            this.btnDelSession.Name = "btnDelSession";
            this.btnDelSession.Size = new System.Drawing.Size(96, 36);
            this.btnDelSession.TabIndex = 8;
            this.btnDelSession.Text = "清空下载记录";
            this.toolTip1.SetToolTip(this.btnDelSession, "删除 aria2c.session文件");
            this.btnDelSession.UseVisualStyleBackColor = true;
            this.btnDelSession.Click += new System.EventHandler(this.btnDelSession_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 613);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.TextBox txbMergeCMD;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSetDownloadLocation;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txbCustomBandwidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbSelectBandWidthCustom;
        private System.Windows.Forms.RadioButton rdbSelectBandWidthMin;
        private System.Windows.Forms.RadioButton rdbSelectBandWidthMax;
        private System.Windows.Forms.Button btnUnPauseAll;
        private System.Windows.Forms.Button btnPauseAll;
        private System.Windows.Forms.TextBox txbAria2GlobalInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbbUserAgent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDelSession;
    }
}

