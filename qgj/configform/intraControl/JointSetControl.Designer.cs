namespace qgj
{
    partial class JointSetControl
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
            this.lblAutoGet = new System.Windows.Forms.Label();
            this.lblAutoWay = new System.Windows.Forms.Label();
            this.cbbAutoWay = new System.Windows.Forms.ComboBox();
            this.lblFastPay = new System.Windows.Forms.Label();
            this.cbbFastPay = new System.Windows.Forms.ComboBox();
            this.cust_detail = new System.Windows.Forms.Label();
            this.autoWriteLabel = new System.Windows.Forms.Label();
            this.scanComfirmlabel = new System.Windows.Forms.Label();
            this.scanComfirmSelectC = new qgj.selectControl();
            this.autoWriteSelectC = new qgj.selectControl();
            this.autoMouseSetC = new qgj.AutoMouseSetControl();
            this.AutoGetSelectC = new qgj.selectControl();
            this.PicAreaSelectC = new qgj.PicAreaSelectControl();
            this.HotKeySetC = new qgj.HotKeySetControl();
            this.onlyBarLabel = new System.Windows.Forms.Label();
            this.onlyBarSelectC = new qgj.selectControl();
            this.allscanLabel = new System.Windows.Forms.Label();
            this.allscanSelectC = new qgj.selectControl();
            this.cbxBefore = new System.Windows.Forms.ComboBox();
            this.beforelabel = new System.Windows.Forms.Label();
            this.beforetimeTbx = new System.Windows.Forms.TextBox();
            this.delayLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAutoGet
            // 
            this.lblAutoGet.AutoSize = true;
            this.lblAutoGet.Location = new System.Drawing.Point(67, 28);
            this.lblAutoGet.Name = "lblAutoGet";
            this.lblAutoGet.Size = new System.Drawing.Size(101, 12);
            this.lblAutoGet.TabIndex = 14;
            this.lblAutoGet.Text = "启用金额自动识别";
            this.lblAutoGet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autoget_MouseUp);
            // 
            // lblAutoWay
            // 
            this.lblAutoWay.AutoSize = true;
            this.lblAutoWay.Location = new System.Drawing.Point(200, 28);
            this.lblAutoWay.Name = "lblAutoWay";
            this.lblAutoWay.Size = new System.Drawing.Size(53, 12);
            this.lblAutoWay.TabIndex = 15;
            this.lblAutoWay.Text = "识别方式";
            // 
            // cbbAutoWay
            // 
            this.cbbAutoWay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbAutoWay.FormattingEnabled = true;
            this.cbbAutoWay.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cbbAutoWay.Items.AddRange(new object[] {
            "图像识别",
            "文本框识别",
            "智能图像识别",
            "剪切板模式",
            "虚拟串口"});
            this.cbbAutoWay.Location = new System.Drawing.Point(266, 25);
            this.cbbAutoWay.Name = "cbbAutoWay";
            this.cbbAutoWay.Size = new System.Drawing.Size(121, 20);
            this.cbbAutoWay.TabIndex = 16;
            this.cbbAutoWay.TabStop = false;
            this.cbbAutoWay.SelectedIndexChanged += new System.EventHandler(this.cbbWay_SelectedIndexChanged);
            // 
            // lblFastPay
            // 
            this.lblFastPay.AutoSize = true;
            this.lblFastPay.Location = new System.Drawing.Point(39, 252);
            this.lblFastPay.Name = "lblFastPay";
            this.lblFastPay.Size = new System.Drawing.Size(137, 12);
            this.lblFastPay.TabIndex = 43;
            this.lblFastPay.Text = "识别后自动唤起支付方式";
            // 
            // cbbFastPay
            // 
            this.cbbFastPay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFastPay.FormattingEnabled = true;
            this.cbbFastPay.Items.AddRange(new object[] {
            "无",
            "条码支付",
            "扫码支付",
            "银联记账",
            "现金记账"});
            this.cbbFastPay.Location = new System.Drawing.Point(224, 249);
            this.cbbFastPay.Name = "cbbFastPay";
            this.cbbFastPay.Size = new System.Drawing.Size(121, 20);
            this.cbbFastPay.TabIndex = 42;
            this.cbbFastPay.TabStop = false;
            this.cbbFastPay.SelectedIndexChanged += new System.EventHandler(this.cbbFastPay_SelectedIndexChanged);
            // 
            // cust_detail
            // 
            this.cust_detail.AutoSize = true;
            this.cust_detail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cust_detail.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cust_detail.Location = new System.Drawing.Point(484, 30);
            this.cust_detail.Name = "cust_detail";
            this.cust_detail.Size = new System.Drawing.Size(53, 12);
            this.cust_detail.TabIndex = 46;
            this.cust_detail.Text = "详细设置";
            this.cust_detail.Click += new System.EventHandler(this.lblLPTtest_Click);
            // 
            // autoWriteLabel
            // 
            this.autoWriteLabel.AutoSize = true;
            this.autoWriteLabel.Location = new System.Drawing.Point(425, 30);
            this.autoWriteLabel.Name = "autoWriteLabel";
            this.autoWriteLabel.Size = new System.Drawing.Size(53, 12);
            this.autoWriteLabel.TabIndex = 48;
            this.autoWriteLabel.Text = "实时获取";
            this.autoWriteLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autoWrite_MouseUp);
            // 
            // scanComfirmlabel
            // 
            this.scanComfirmlabel.AutoSize = true;
            this.scanComfirmlabel.Location = new System.Drawing.Point(301, 52);
            this.scanComfirmlabel.Name = "scanComfirmlabel";
            this.scanComfirmlabel.Size = new System.Drawing.Size(65, 12);
            this.scanComfirmlabel.TabIndex = 50;
            this.scanComfirmlabel.Text = "扫码后确认";
            this.scanComfirmlabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scanComfirmSelectC_MouseUp);
            // 
            // scanComfirmSelectC
            // 
            this.scanComfirmSelectC.Location = new System.Drawing.Point(275, 51);
            this.scanComfirmSelectC.Name = "scanComfirmSelectC";
            this.scanComfirmSelectC.Size = new System.Drawing.Size(15, 15);
            this.scanComfirmSelectC.TabIndex = 49;
            this.scanComfirmSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.scanComfirmSelectC_MouseUp);
            // 
            // autoWriteSelectC
            // 
            this.autoWriteSelectC.Location = new System.Drawing.Point(402, 28);
            this.autoWriteSelectC.Name = "autoWriteSelectC";
            this.autoWriteSelectC.Size = new System.Drawing.Size(15, 15);
            this.autoWriteSelectC.TabIndex = 47;
            this.autoWriteSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autoWrite_MouseUp);
            // 
            // autoMouseSetC
            // 
            this.autoMouseSetC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.autoMouseSetC.Location = new System.Drawing.Point(35, 272);
            this.autoMouseSetC.Name = "autoMouseSetC";
            this.autoMouseSetC.Size = new System.Drawing.Size(430, 150);
            this.autoMouseSetC.TabIndex = 20;
            // 
            // AutoGetSelectC
            // 
            this.AutoGetSelectC.Location = new System.Drawing.Point(41, 27);
            this.AutoGetSelectC.Name = "AutoGetSelectC";
            this.AutoGetSelectC.Size = new System.Drawing.Size(15, 15);
            this.AutoGetSelectC.TabIndex = 13;
            this.AutoGetSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.autoget_MouseUp);
            // 
            // PicAreaSelectC
            // 
            this.PicAreaSelectC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.PicAreaSelectC.Location = new System.Drawing.Point(35, 177);
            this.PicAreaSelectC.Name = "PicAreaSelectC";
            this.PicAreaSelectC.Size = new System.Drawing.Size(420, 70);
            this.PicAreaSelectC.TabIndex = 1;
            // 
            // HotKeySetC
            // 
            this.HotKeySetC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.HotKeySetC.Location = new System.Drawing.Point(35, 106);
            this.HotKeySetC.Name = "HotKeySetC";
            this.HotKeySetC.Size = new System.Drawing.Size(420, 70);
            this.HotKeySetC.TabIndex = 0;
            // 
            // onlyBarLabel
            // 
            this.onlyBarLabel.AutoSize = true;
            this.onlyBarLabel.Location = new System.Drawing.Point(67, 52);
            this.onlyBarLabel.Name = "onlyBarLabel";
            this.onlyBarLabel.Size = new System.Drawing.Size(65, 12);
            this.onlyBarLabel.TabIndex = 52;
            this.onlyBarLabel.Text = "仅使用条码";
            this.onlyBarLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onlyBarSelectC_MouseUp);
            // 
            // onlyBarSelectC
            // 
            this.onlyBarSelectC.Location = new System.Drawing.Point(41, 51);
            this.onlyBarSelectC.Name = "onlyBarSelectC";
            this.onlyBarSelectC.Size = new System.Drawing.Size(15, 15);
            this.onlyBarSelectC.TabIndex = 51;
            this.onlyBarSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onlyBarSelectC_MouseUp);
            // 
            // allscanLabel
            // 
            this.allscanLabel.AutoSize = true;
            this.allscanLabel.Location = new System.Drawing.Point(175, 52);
            this.allscanLabel.Name = "allscanLabel";
            this.allscanLabel.Size = new System.Drawing.Size(89, 12);
            this.allscanLabel.TabIndex = 54;
            this.allscanLabel.Text = "实时获取付款码";
            this.allscanLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.allscanSelectC_MouseUp);
            // 
            // allscanSelectC
            // 
            this.allscanSelectC.Location = new System.Drawing.Point(149, 51);
            this.allscanSelectC.Name = "allscanSelectC";
            this.allscanSelectC.Size = new System.Drawing.Size(15, 15);
            this.allscanSelectC.TabIndex = 53;
            this.allscanSelectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.allscanSelectC_MouseUp);
            // 
            // cbxBefore
            // 
            this.cbxBefore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBefore.FormattingEnabled = true;
            this.cbxBefore.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cbxBefore.Items.AddRange(new object[] {
            "无",
            "发送快捷键",
            "鼠标点击"});
            this.cbxBefore.Location = new System.Drawing.Point(132, 78);
            this.cbxBefore.Name = "cbxBefore";
            this.cbxBefore.Size = new System.Drawing.Size(121, 20);
            this.cbxBefore.TabIndex = 56;
            this.cbxBefore.TabStop = false;
            this.cbxBefore.SelectedIndexChanged += new System.EventHandler(this.cbxBefore_SelectedIndexChanged);
            // 
            // beforelabel
            // 
            this.beforelabel.AutoSize = true;
            this.beforelabel.Location = new System.Drawing.Point(40, 81);
            this.beforelabel.Name = "beforelabel";
            this.beforelabel.Size = new System.Drawing.Size(89, 12);
            this.beforelabel.TabIndex = 55;
            this.beforelabel.Text = "获取付款金额前";
            // 
            // beforetimeTbx
            // 
            this.beforetimeTbx.Location = new System.Drawing.Point(402, 77);
            this.beforetimeTbx.Name = "beforetimeTbx";
            this.beforetimeTbx.Size = new System.Drawing.Size(53, 21);
            this.beforetimeTbx.TabIndex = 57;
            this.beforetimeTbx.TextChanged += new System.EventHandler(this.beforetimeTbx_TextChanged);
            this.beforetimeTbx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.beforetimeTbx_KeyPress);
            // 
            // delayLabel
            // 
            this.delayLabel.AutoSize = true;
            this.delayLabel.Location = new System.Drawing.Point(367, 81);
            this.delayLabel.Name = "delayLabel";
            this.delayLabel.Size = new System.Drawing.Size(29, 12);
            this.delayLabel.TabIndex = 58;
            this.delayLabel.Text = "延时";
            // 
            // JointSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.delayLabel);
            this.Controls.Add(this.beforetimeTbx);
            this.Controls.Add(this.cbxBefore);
            this.Controls.Add(this.beforelabel);
            this.Controls.Add(this.allscanLabel);
            this.Controls.Add(this.allscanSelectC);
            this.Controls.Add(this.onlyBarLabel);
            this.Controls.Add(this.onlyBarSelectC);
            this.Controls.Add(this.scanComfirmlabel);
            this.Controls.Add(this.scanComfirmSelectC);
            this.Controls.Add(this.autoWriteLabel);
            this.Controls.Add(this.autoWriteSelectC);
            this.Controls.Add(this.cust_detail);
            this.Controls.Add(this.lblFastPay);
            this.Controls.Add(this.cbbFastPay);
            this.Controls.Add(this.autoMouseSetC);
            this.Controls.Add(this.cbbAutoWay);
            this.Controls.Add(this.lblAutoWay);
            this.Controls.Add(this.lblAutoGet);
            this.Controls.Add(this.AutoGetSelectC);
            this.Controls.Add(this.PicAreaSelectC);
            this.Controls.Add(this.HotKeySetC);
            this.Name = "JointSetControl";
            this.Size = new System.Drawing.Size(608, 439);
            this.Load += new System.EventHandler(this.JointSetControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.JointSetControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HotKeySetControl HotKeySetC;
        private PicAreaSelectControl PicAreaSelectC;
        private System.Windows.Forms.Label lblAutoGet;
        private selectControl AutoGetSelectC;
        private System.Windows.Forms.Label lblAutoWay;
        private System.Windows.Forms.ComboBox cbbAutoWay;
        public AutoMouseSetControl autoMouseSetC;
        private System.Windows.Forms.Label lblFastPay;
        private System.Windows.Forms.ComboBox cbbFastPay;
        private System.Windows.Forms.Label cust_detail;
        private System.Windows.Forms.Label autoWriteLabel;
        private selectControl autoWriteSelectC;
        private System.Windows.Forms.Label scanComfirmlabel;
        private selectControl scanComfirmSelectC;
        private System.Windows.Forms.Label onlyBarLabel;
        private selectControl onlyBarSelectC;
        private System.Windows.Forms.Label allscanLabel;
        private selectControl allscanSelectC;
        private System.Windows.Forms.ComboBox cbxBefore;
        private System.Windows.Forms.Label beforelabel;
        private System.Windows.Forms.TextBox beforetimeTbx;
        private System.Windows.Forms.Label delayLabel;
    }
}
