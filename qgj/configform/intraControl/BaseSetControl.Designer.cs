namespace qgj
{
    partial class BaseSetControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbbDrivePrint = new System.Windows.Forms.ComboBox();
            this.lblAutoRun = new System.Windows.Forms.Label();
            this.lblDrivePrint = new System.Windows.Forms.Label();
            this.lblEmptyLine = new System.Windows.Forms.Label();
            this.nEmptyLine = new System.Windows.Forms.NumericUpDown();
            this.lblPrintMode = new System.Windows.Forms.Label();
            this.cbbPrintMode = new System.Windows.Forms.ComboBox();
            this.lblLPTPrint = new System.Windows.Forms.Label();
            this.cbbLPT = new System.Windows.Forms.ComboBox();
            this.lblExit = new System.Windows.Forms.Label();
            this.lblSuccessMini = new System.Windows.Forms.Label();
            this.lblUsekeyBoard = new System.Windows.Forms.Label();
            this.lblFastPay = new System.Windows.Forms.Label();
            this.cbbFastPay = new System.Windows.Forms.ComboBox();
            this.lblKeyBoardInfo = new System.Windows.Forms.Label();
            this.lblSuspended = new System.Windows.Forms.Label();
            this.lblLPTtest = new System.Windows.Forms.Label();
            this.lblVoiceToInform = new System.Windows.Forms.Label();
            this.cbbVoiceSet = new System.Windows.Forms.ComboBox();
            this.lblkeyboard = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblencoding = new System.Windows.Forms.Label();
            this.cbbEncoding = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAutoPrint = new System.Windows.Forms.Label();
            this.lblbold = new System.Windows.Forms.Label();
            this.lblPageWide = new System.Windows.Forms.Label();
            this.cbbPageWide = new System.Windows.Forms.ComboBox();
            this.cbbPrintCompatible = new System.Windows.Forms.ComboBox();
            this.boldSelectC = new qgj.selectControl();
            this.autoPrintSelectC = new qgj.selectControl();
            this.keyboardSelectC = new qgj.selectControl();
            this.proxySetControl1 = new qgj.ProxySetControl();
            this.voicetoinformselectC = new qgj.selectControl();
            this.suspendedselectC = new qgj.selectControl();
            this.usekeyboardselectC = new qgj.selectControl();
            this.successminiselectC = new qgj.selectControl();
            this.exitselectC = new qgj.selectControl();
            this.lptprintselectC = new qgj.selectControl();
            this.driveprintselectC = new qgj.selectControl();
            this.autorunselectC = new qgj.selectControl();
            this.label3 = new System.Windows.Forms.Label();
            this.lblmin = new System.Windows.Forms.Label();
            this.minselectC = new qgj.selectControl();
            ((System.ComponentModel.ISupportInitialize)(this.nEmptyLine)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbDrivePrint
            // 
            this.cbbDrivePrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDrivePrint.FormattingEnabled = true;
            this.cbbDrivePrint.Location = new System.Drawing.Point(162, 46);
            this.cbbDrivePrint.Name = "cbbDrivePrint";
            this.cbbDrivePrint.Size = new System.Drawing.Size(111, 20);
            this.cbbDrivePrint.TabIndex = 9;
            this.cbbDrivePrint.TabStop = false;
            this.cbbDrivePrint.SelectedIndexChanged += new System.EventHandler(this.cbbDrivePrint_SelectedIndexChanged);
            // 
            // lblAutoRun
            // 
            this.lblAutoRun.AutoSize = true;
            this.lblAutoRun.Location = new System.Drawing.Point(69, 18);
            this.lblAutoRun.Name = "lblAutoRun";
            this.lblAutoRun.Size = new System.Drawing.Size(77, 12);
            this.lblAutoRun.TabIndex = 12;
            this.lblAutoRun.Text = "开机自动运行";
            this.lblAutoRun.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autorun_MouseUp);
            // 
            // lblDrivePrint
            // 
            this.lblDrivePrint.AutoSize = true;
            this.lblDrivePrint.Location = new System.Drawing.Point(69, 50);
            this.lblDrivePrint.Name = "lblDrivePrint";
            this.lblDrivePrint.Size = new System.Drawing.Size(77, 12);
            this.lblDrivePrint.TabIndex = 13;
            this.lblDrivePrint.Text = "开启驱动打印";
            this.lblDrivePrint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.driveprint_MouseUp);
            // 
            // lblEmptyLine
            // 
            this.lblEmptyLine.AutoSize = true;
            this.lblEmptyLine.Location = new System.Drawing.Point(383, 83);
            this.lblEmptyLine.Name = "lblEmptyLine";
            this.lblEmptyLine.Size = new System.Drawing.Size(137, 12);
            this.lblEmptyLine.TabIndex = 29;
            this.lblEmptyLine.Text = "票尾空行数(仅端口打印)";
            // 
            // nEmptyLine
            // 
            this.nEmptyLine.Location = new System.Drawing.Point(522, 79);
            this.nEmptyLine.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nEmptyLine.Name = "nEmptyLine";
            this.nEmptyLine.Size = new System.Drawing.Size(62, 21);
            this.nEmptyLine.TabIndex = 28;
            this.nEmptyLine.TabStop = false;
            this.nEmptyLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nEmptyLine.ValueChanged += new System.EventHandler(this.nEmptyLine_ValueChanged);
            // 
            // lblPrintMode
            // 
            this.lblPrintMode.AutoSize = true;
            this.lblPrintMode.Location = new System.Drawing.Point(383, 50);
            this.lblPrintMode.Name = "lblPrintMode";
            this.lblPrintMode.Size = new System.Drawing.Size(53, 12);
            this.lblPrintMode.TabIndex = 27;
            this.lblPrintMode.Text = "打印模式";
            // 
            // cbbPrintMode
            // 
            this.cbbPrintMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrintMode.FormattingEnabled = true;
            this.cbbPrintMode.Items.AddRange(new object[] {
            "默认 (打印两联)",
            "仅打印顾客存根",
            "仅打印商户存根"});
            this.cbbPrintMode.Location = new System.Drawing.Point(463, 47);
            this.cbbPrintMode.Name = "cbbPrintMode";
            this.cbbPrintMode.Size = new System.Drawing.Size(121, 20);
            this.cbbPrintMode.TabIndex = 26;
            this.cbbPrintMode.TabStop = false;
            this.cbbPrintMode.SelectedValueChanged += new System.EventHandler(this.cbbPrintMode_SelectedValueChanged);
            // 
            // lblLPTPrint
            // 
            this.lblLPTPrint.AutoSize = true;
            this.lblLPTPrint.Location = new System.Drawing.Point(69, 82);
            this.lblLPTPrint.Name = "lblLPTPrint";
            this.lblLPTPrint.Size = new System.Drawing.Size(77, 12);
            this.lblLPTPrint.TabIndex = 32;
            this.lblLPTPrint.Text = "开启端口打印";
            this.lblLPTPrint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblLPTPrint_MouseUp);
            // 
            // cbbLPT
            // 
            this.cbbLPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLPT.FormattingEnabled = true;
            this.cbbLPT.Items.AddRange(new object[] {
            "LPT1",
            "LPT2",
            "LPT3",
            "USB001",
            "USB002"});
            this.cbbLPT.Location = new System.Drawing.Point(162, 78);
            this.cbbLPT.Name = "cbbLPT";
            this.cbbLPT.Size = new System.Drawing.Size(111, 20);
            this.cbbLPT.TabIndex = 30;
            this.cbbLPT.TabStop = false;
            this.cbbLPT.SelectedIndexChanged += new System.EventHandler(this.cbbLPT_SelectedIndexChanged);
            // 
            // lblExit
            // 
            this.lblExit.AutoSize = true;
            this.lblExit.Location = new System.Drawing.Point(134, 177);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(53, 12);
            this.lblExit.TabIndex = 34;
            this.lblExit.Text = "直接退出";
            this.lblExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.exit_MouseUp);
            // 
            // lblSuccessMini
            // 
            this.lblSuccessMini.AutoSize = true;
            this.lblSuccessMini.Location = new System.Drawing.Point(69, 209);
            this.lblSuccessMini.Name = "lblSuccessMini";
            this.lblSuccessMini.Size = new System.Drawing.Size(77, 12);
            this.lblSuccessMini.TabIndex = 37;
            this.lblSuccessMini.Text = "收款后最小化";
            this.lblSuccessMini.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mini_MouseUp);
            // 
            // lblUsekeyBoard
            // 
            this.lblUsekeyBoard.AutoSize = true;
            this.lblUsekeyBoard.Location = new System.Drawing.Point(69, 240);
            this.lblUsekeyBoard.Name = "lblUsekeyBoard";
            this.lblUsekeyBoard.Size = new System.Drawing.Size(77, 12);
            this.lblUsekeyBoard.TabIndex = 39;
            this.lblUsekeyBoard.Text = "启用键盘操作";
            this.lblUsekeyBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.usekeyboard_MouseUp);
            // 
            // lblFastPay
            // 
            this.lblFastPay.AutoSize = true;
            this.lblFastPay.Location = new System.Drawing.Point(32, 277);
            this.lblFastPay.Name = "lblFastPay";
            this.lblFastPay.Size = new System.Drawing.Size(149, 12);
            this.lblFastPay.TabIndex = 41;
            this.lblFastPay.Text = "收银界面回车唤起支付方式";
            // 
            // cbbFastPay
            // 
            this.cbbFastPay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFastPay.FormattingEnabled = true;
            this.cbbFastPay.Items.AddRange(new object[] {
            "无",
            "条码支付",
            "扫码支付"});
            this.cbbFastPay.Location = new System.Drawing.Point(217, 274);
            this.cbbFastPay.Name = "cbbFastPay";
            this.cbbFastPay.Size = new System.Drawing.Size(121, 20);
            this.cbbFastPay.TabIndex = 40;
            this.cbbFastPay.TabStop = false;
            this.cbbFastPay.SelectedIndexChanged += new System.EventHandler(this.cbbFastPay_SelectedIndexChanged);
            // 
            // lblKeyBoardInfo
            // 
            this.lblKeyBoardInfo.AutoSize = true;
            this.lblKeyBoardInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblKeyBoardInfo.Location = new System.Drawing.Point(162, 240);
            this.lblKeyBoardInfo.Name = "lblKeyBoardInfo";
            this.lblKeyBoardInfo.Size = new System.Drawing.Size(377, 12);
            this.lblKeyBoardInfo.TabIndex = 42;
            this.lblKeyBoardInfo.Text = "方向键进行选择，“空格”= 点击，“ENTER”= 确定，“ESC”= 取消";
            // 
            // lblSuspended
            // 
            this.lblSuspended.AutoSize = true;
            this.lblSuspended.Location = new System.Drawing.Point(69, 311);
            this.lblSuspended.Name = "lblSuspended";
            this.lblSuspended.Size = new System.Drawing.Size(65, 12);
            this.lblSuspended.TabIndex = 44;
            this.lblSuspended.Text = "开启悬浮窗";
            this.lblSuspended.MouseUp += new System.Windows.Forms.MouseEventHandler(this.suspended_MouseUp);
            // 
            // lblLPTtest
            // 
            this.lblLPTtest.AutoSize = true;
            this.lblLPTtest.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLPTtest.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblLPTtest.Location = new System.Drawing.Point(284, 83);
            this.lblLPTtest.Name = "lblLPTtest";
            this.lblLPTtest.Size = new System.Drawing.Size(77, 12);
            this.lblLPTtest.TabIndex = 45;
            this.lblLPTtest.Text = "端口打印测试";
            this.lblLPTtest.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblLPTtest_MouseUp);
            // 
            // lblVoiceToInform
            // 
            this.lblVoiceToInform.AutoSize = true;
            this.lblVoiceToInform.Location = new System.Drawing.Point(69, 344);
            this.lblVoiceToInform.Name = "lblVoiceToInform";
            this.lblVoiceToInform.Size = new System.Drawing.Size(113, 12);
            this.lblVoiceToInform.TabIndex = 47;
            this.lblVoiceToInform.Text = "开启收款成功提示音";
            this.lblVoiceToInform.MouseUp += new System.Windows.Forms.MouseEventHandler(this.voicetoinformselectC_MouseUp);
            // 
            // cbbVoiceSet
            // 
            this.cbbVoiceSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbVoiceSet.FormattingEnabled = true;
            this.cbbVoiceSet.Items.AddRange(new object[] {
            "提示音一",
            "提示音二",
            "提示音三",
            "提示音四"});
            this.cbbVoiceSet.Location = new System.Drawing.Point(217, 341);
            this.cbbVoiceSet.Name = "cbbVoiceSet";
            this.cbbVoiceSet.Size = new System.Drawing.Size(121, 20);
            this.cbbVoiceSet.TabIndex = 48;
            this.cbbVoiceSet.TabStop = false;
            this.cbbVoiceSet.SelectedIndexChanged += new System.EventHandler(this.cbbVoiceSet_SelectedIndexChanged);
            // 
            // lblkeyboard
            // 
            this.lblkeyboard.AutoSize = true;
            this.lblkeyboard.Location = new System.Drawing.Point(249, 311);
            this.lblkeyboard.Name = "lblkeyboard";
            this.lblkeyboard.Size = new System.Drawing.Size(77, 12);
            this.lblkeyboard.TabIndex = 51;
            this.lblkeyboard.Text = "开启系统键盘";
            this.lblkeyboard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.keyboardSelectC_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(332, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "自动开启系统辅助键盘（暂只支持32位系统）";
            // 
            // lblencoding
            // 
            this.lblencoding.AutoSize = true;
            this.lblencoding.Location = new System.Drawing.Point(69, 113);
            this.lblencoding.Name = "lblencoding";
            this.lblencoding.Size = new System.Drawing.Size(77, 12);
            this.lblencoding.TabIndex = 54;
            this.lblencoding.Text = "端口打印编码";
            // 
            // cbbEncoding
            // 
            this.cbbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbEncoding.FormattingEnabled = true;
            this.cbbEncoding.Items.AddRange(new object[] {
            "ANSI",
            "Unicode",
            "ASCII",
            "UTF8",
            "UTF32",
            "UTF7",
            "BigEndianUnicode"});
            this.cbbEncoding.Location = new System.Drawing.Point(162, 109);
            this.cbbEncoding.Name = "cbbEncoding";
            this.cbbEncoding.Size = new System.Drawing.Size(111, 20);
            this.cbbEncoding.TabIndex = 53;
            this.cbbEncoding.TabStop = false;
            this.cbbEncoding.SelectedValueChanged += new System.EventHandler(this.cbbEncoding_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(162, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 57;
            this.label2.Text = "收款成功后自动打印小票";
            // 
            // lblAutoPrint
            // 
            this.lblAutoPrint.AutoSize = true;
            this.lblAutoPrint.Location = new System.Drawing.Point(69, 145);
            this.lblAutoPrint.Name = "lblAutoPrint";
            this.lblAutoPrint.Size = new System.Drawing.Size(53, 12);
            this.lblAutoPrint.TabIndex = 56;
            this.lblAutoPrint.Text = "自动打印";
            this.lblAutoPrint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autoprint_MouseUp);
            // 
            // lblbold
            // 
            this.lblbold.AutoSize = true;
            this.lblbold.Location = new System.Drawing.Point(406, 146);
            this.lblbold.Name = "lblbold";
            this.lblbold.Size = new System.Drawing.Size(53, 12);
            this.lblbold.TabIndex = 59;
            this.lblbold.Text = "加粗打印";
            this.lblbold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.boldSelectC_MouseUp);
            // 
            // lblPageWide
            // 
            this.lblPageWide.AutoSize = true;
            this.lblPageWide.Location = new System.Drawing.Point(383, 113);
            this.lblPageWide.Name = "lblPageWide";
            this.lblPageWide.Size = new System.Drawing.Size(53, 12);
            this.lblPageWide.TabIndex = 62;
            this.lblPageWide.Text = "打印纸宽";
            // 
            // cbbPageWide
            // 
            this.cbbPageWide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPageWide.FormattingEnabled = true;
            this.cbbPageWide.Items.AddRange(new object[] {
            "50",
            "80"});
            this.cbbPageWide.Location = new System.Drawing.Point(464, 110);
            this.cbbPageWide.Name = "cbbPageWide";
            this.cbbPageWide.Size = new System.Drawing.Size(121, 20);
            this.cbbPageWide.TabIndex = 61;
            this.cbbPageWide.TabStop = false;
            this.cbbPageWide.SelectedValueChanged += new System.EventHandler(this.cbbPageWide_SelectedValueChanged);
            // 
            // cbbPrintCompatible
            // 
            this.cbbPrintCompatible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrintCompatible.FormattingEnabled = true;
            this.cbbPrintCompatible.Items.AddRange(new object[] {
            "默认模式",
            "兼容模式1",
            "兼容模式2"});
            this.cbbPrintCompatible.Location = new System.Drawing.Point(279, 46);
            this.cbbPrintCompatible.Name = "cbbPrintCompatible";
            this.cbbPrintCompatible.Size = new System.Drawing.Size(98, 20);
            this.cbbPrintCompatible.TabIndex = 63;
            this.cbbPrintCompatible.TabStop = false;
            this.cbbPrintCompatible.SelectedIndexChanged += new System.EventHandler(this.cbbPrintCompatible_SelectedIndexChanged);
            // 
            // boldSelectC
            // 
            this.boldSelectC.Location = new System.Drawing.Point(385, 144);
            this.boldSelectC.Name = "boldSelectC";
            this.boldSelectC.Size = new System.Drawing.Size(15, 15);
            this.boldSelectC.TabIndex = 60;
            this.boldSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.boldSelectC_MouseUp);
            // 
            // autoPrintSelectC
            // 
            this.autoPrintSelectC.Location = new System.Drawing.Point(35, 143);
            this.autoPrintSelectC.Name = "autoPrintSelectC";
            this.autoPrintSelectC.Size = new System.Drawing.Size(15, 15);
            this.autoPrintSelectC.TabIndex = 55;
            this.autoPrintSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autoprint_MouseUp);
            // 
            // keyboardSelectC
            // 
            this.keyboardSelectC.Location = new System.Drawing.Point(215, 309);
            this.keyboardSelectC.Name = "keyboardSelectC";
            this.keyboardSelectC.Size = new System.Drawing.Size(15, 15);
            this.keyboardSelectC.TabIndex = 50;
            this.keyboardSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.keyboardSelectC_MouseUp);
            // 
            // proxySetControl1
            // 
            this.proxySetControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.proxySetControl1.Location = new System.Drawing.Point(24, 367);
            this.proxySetControl1.Name = "proxySetControl1";
            this.proxySetControl1.Size = new System.Drawing.Size(380, 60);
            this.proxySetControl1.TabIndex = 49;
            // 
            // voicetoinformselectC
            // 
            this.voicetoinformselectC.Location = new System.Drawing.Point(35, 342);
            this.voicetoinformselectC.Name = "voicetoinformselectC";
            this.voicetoinformselectC.Size = new System.Drawing.Size(15, 15);
            this.voicetoinformselectC.TabIndex = 46;
            this.voicetoinformselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.voicetoinformselectC_MouseUp);
            // 
            // suspendedselectC
            // 
            this.suspendedselectC.Location = new System.Drawing.Point(35, 309);
            this.suspendedselectC.Name = "suspendedselectC";
            this.suspendedselectC.Size = new System.Drawing.Size(15, 15);
            this.suspendedselectC.TabIndex = 43;
            this.suspendedselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.suspended_MouseUp);
            // 
            // usekeyboardselectC
            // 
            this.usekeyboardselectC.Location = new System.Drawing.Point(35, 238);
            this.usekeyboardselectC.Name = "usekeyboardselectC";
            this.usekeyboardselectC.Size = new System.Drawing.Size(15, 15);
            this.usekeyboardselectC.TabIndex = 38;
            this.usekeyboardselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.usekeyboard_MouseUp);
            // 
            // successminiselectC
            // 
            this.successminiselectC.Location = new System.Drawing.Point(35, 207);
            this.successminiselectC.Name = "successminiselectC";
            this.successminiselectC.Size = new System.Drawing.Size(15, 15);
            this.successminiselectC.TabIndex = 36;
            this.successminiselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mini_MouseUp);
            // 
            // exitselectC
            // 
            this.exitselectC.Location = new System.Drawing.Point(113, 175);
            this.exitselectC.Name = "exitselectC";
            this.exitselectC.Size = new System.Drawing.Size(15, 15);
            this.exitselectC.TabIndex = 33;
            this.exitselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.exit_MouseUp);
            // 
            // lptprintselectC
            // 
            this.lptprintselectC.Location = new System.Drawing.Point(35, 80);
            this.lptprintselectC.Name = "lptprintselectC";
            this.lptprintselectC.Size = new System.Drawing.Size(15, 15);
            this.lptprintselectC.TabIndex = 31;
            this.lptprintselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblLPTPrint_MouseUp);
            // 
            // driveprintselectC
            // 
            this.driveprintselectC.Location = new System.Drawing.Point(35, 48);
            this.driveprintselectC.Name = "driveprintselectC";
            this.driveprintselectC.Size = new System.Drawing.Size(15, 15);
            this.driveprintselectC.TabIndex = 11;
            this.driveprintselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.driveprint_MouseUp);
            // 
            // autorunselectC
            // 
            this.autorunselectC.Location = new System.Drawing.Point(35, 17);
            this.autorunselectC.Name = "autorunselectC";
            this.autorunselectC.Size = new System.Drawing.Size(15, 15);
            this.autorunselectC.TabIndex = 10;
            this.autorunselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autorun_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 64;
            this.label3.Text = "点击关闭";
            // 
            // lblmin
            // 
            this.lblmin.AutoSize = true;
            this.lblmin.Location = new System.Drawing.Point(215, 178);
            this.lblmin.Name = "lblmin";
            this.lblmin.Size = new System.Drawing.Size(41, 12);
            this.lblmin.TabIndex = 66;
            this.lblmin.Text = "最小化";
            this.lblmin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.exitMin_MouseUp);
            // 
            // minselectC
            // 
            this.minselectC.Location = new System.Drawing.Point(194, 176);
            this.minselectC.Name = "minselectC";
            this.minselectC.Size = new System.Drawing.Size(15, 15);
            this.minselectC.TabIndex = 65;
            this.minselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.exitMin_MouseUp);
            // 
            // BaseSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblmin);
            this.Controls.Add(this.minselectC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbPrintCompatible);
            this.Controls.Add(this.lblPageWide);
            this.Controls.Add(this.cbbPageWide);
            this.Controls.Add(this.boldSelectC);
            this.Controls.Add(this.lblbold);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAutoPrint);
            this.Controls.Add(this.autoPrintSelectC);
            this.Controls.Add(this.lblencoding);
            this.Controls.Add(this.cbbEncoding);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblkeyboard);
            this.Controls.Add(this.keyboardSelectC);
            this.Controls.Add(this.proxySetControl1);
            this.Controls.Add(this.cbbVoiceSet);
            this.Controls.Add(this.lblVoiceToInform);
            this.Controls.Add(this.voicetoinformselectC);
            this.Controls.Add(this.lblLPTtest);
            this.Controls.Add(this.lblSuspended);
            this.Controls.Add(this.suspendedselectC);
            this.Controls.Add(this.lblKeyBoardInfo);
            this.Controls.Add(this.lblFastPay);
            this.Controls.Add(this.cbbFastPay);
            this.Controls.Add(this.lblUsekeyBoard);
            this.Controls.Add(this.usekeyboardselectC);
            this.Controls.Add(this.lblSuccessMini);
            this.Controls.Add(this.successminiselectC);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.exitselectC);
            this.Controls.Add(this.lblLPTPrint);
            this.Controls.Add(this.lptprintselectC);
            this.Controls.Add(this.cbbLPT);
            this.Controls.Add(this.lblEmptyLine);
            this.Controls.Add(this.nEmptyLine);
            this.Controls.Add(this.lblPrintMode);
            this.Controls.Add(this.cbbPrintMode);
            this.Controls.Add(this.lblDrivePrint);
            this.Controls.Add(this.lblAutoRun);
            this.Controls.Add(this.driveprintselectC);
            this.Controls.Add(this.autorunselectC);
            this.Controls.Add(this.cbbDrivePrint);
            this.Name = "BaseSetControl";
            this.Size = new System.Drawing.Size(608, 439);
            this.Load += new System.EventHandler(this.BaseSetControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nEmptyLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbDrivePrint;
        private selectControl autorunselectC;
        private selectControl driveprintselectC;
        private System.Windows.Forms.Label lblAutoRun;
        private System.Windows.Forms.Label lblDrivePrint;
        private System.Windows.Forms.Label lblEmptyLine;
        private System.Windows.Forms.NumericUpDown nEmptyLine;
        private System.Windows.Forms.Label lblPrintMode;
        private System.Windows.Forms.ComboBox cbbPrintMode;
        private System.Windows.Forms.Label lblLPTPrint;
        private selectControl lptprintselectC;
        private System.Windows.Forms.ComboBox cbbLPT;
        private System.Windows.Forms.Label lblExit;
        private selectControl exitselectC;
        private System.Windows.Forms.Label lblSuccessMini;
        private selectControl successminiselectC;
        private System.Windows.Forms.Label lblUsekeyBoard;
        private selectControl usekeyboardselectC;
        private System.Windows.Forms.Label lblFastPay;
        private System.Windows.Forms.ComboBox cbbFastPay;
        private System.Windows.Forms.Label lblKeyBoardInfo;
        private System.Windows.Forms.Label lblSuspended;
        private selectControl suspendedselectC;
        private System.Windows.Forms.Label lblLPTtest;
        private System.Windows.Forms.Label lblVoiceToInform;
        private selectControl voicetoinformselectC;
        private System.Windows.Forms.ComboBox cbbVoiceSet;
        private ProxySetControl proxySetControl1;
        private System.Windows.Forms.Label lblkeyboard;
        private selectControl keyboardSelectC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblencoding;
        private System.Windows.Forms.ComboBox cbbEncoding;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAutoPrint;
        private selectControl autoPrintSelectC;
        private System.Windows.Forms.Label lblbold;
        private selectControl boldSelectC;
        private System.Windows.Forms.Label lblPageWide;
        private System.Windows.Forms.ComboBox cbbPageWide;
        private System.Windows.Forms.ComboBox cbbPrintCompatible;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblmin;
        private selectControl minselectC;
    }
}
