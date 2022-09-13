namespace qgj
{
    partial class couponUseListControl
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
            this.components = new System.ComponentModel.Container();
            this.waitinfoLabel = new System.Windows.Forms.Label();
            this.receiptmoneyPanel = new System.Windows.Forms.Panel();
            this.receiptmoneyLabel = new System.Windows.Forms.Label();
            this.operationPanel = new System.Windows.Forms.Panel();
            this.operationLabel = new System.Windows.Forms.Label();
            this.paersonPanel = new System.Windows.Forms.Panel();
            this.personLabel = new System.Windows.Forms.Label();
            this.orderstatusPanel = new System.Windows.Forms.Panel();
            this.nicknameLabel = new System.Windows.Forms.Label();
            this.ordermoneyPanel = new System.Windows.Forms.Panel();
            this.ordermoneyLabel = new System.Windows.Forms.Label();
            this.paychannelPanel = new System.Windows.Forms.Panel();
            this.useTypeLabel = new System.Windows.Forms.Label();
            this.ordernumPanel = new System.Windows.Forms.Panel();
            this.couponNameLabel = new System.Windows.Forms.Label();
            this.createtimePanel = new System.Windows.Forms.Panel();
            this.couponTypeLabel = new System.Windows.Forms.Label();
            this.numPanel = new System.Windows.Forms.Panel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.listPanel = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.ccbCouponType = new System.Windows.Forms.ComboBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.receiptmoneyPanel.SuspendLayout();
            this.operationPanel.SuspendLayout();
            this.paersonPanel.SuspendLayout();
            this.orderstatusPanel.SuspendLayout();
            this.ordermoneyPanel.SuspendLayout();
            this.paychannelPanel.SuspendLayout();
            this.ordernumPanel.SuspendLayout();
            this.createtimePanel.SuspendLayout();
            this.numPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // waitinfoLabel
            // 
            this.waitinfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.waitinfoLabel.Location = new System.Drawing.Point(666, 12);
            this.waitinfoLabel.Name = "waitinfoLabel";
            this.waitinfoLabel.Size = new System.Drawing.Size(89, 24);
            this.waitinfoLabel.TabIndex = 56;
            this.waitinfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // receiptmoneyPanel
            // 
            this.receiptmoneyPanel.Controls.Add(this.receiptmoneyLabel);
            this.receiptmoneyPanel.Location = new System.Drawing.Point(463, 48);
            this.receiptmoneyPanel.Name = "receiptmoneyPanel";
            this.receiptmoneyPanel.Size = new System.Drawing.Size(75, 30);
            this.receiptmoneyPanel.TabIndex = 51;
            this.receiptmoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // receiptmoneyLabel
            // 
            this.receiptmoneyLabel.AutoSize = true;
            this.receiptmoneyLabel.Location = new System.Drawing.Point(11, 9);
            this.receiptmoneyLabel.Name = "receiptmoneyLabel";
            this.receiptmoneyLabel.Size = new System.Drawing.Size(53, 12);
            this.receiptmoneyLabel.TabIndex = 0;
            this.receiptmoneyLabel.Text = "实收金额";
            this.receiptmoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // operationPanel
            // 
            this.operationPanel.Controls.Add(this.operationLabel);
            this.operationPanel.Location = new System.Drawing.Point(687, 48);
            this.operationPanel.Name = "operationPanel";
            this.operationPanel.Size = new System.Drawing.Size(70, 30);
            this.operationPanel.TabIndex = 55;
            this.operationPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // operationLabel
            // 
            this.operationLabel.AutoSize = true;
            this.operationLabel.Location = new System.Drawing.Point(21, 8);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(29, 12);
            this.operationLabel.TabIndex = 0;
            this.operationLabel.Text = "操作";
            // 
            // paersonPanel
            // 
            this.paersonPanel.Controls.Add(this.personLabel);
            this.paersonPanel.Location = new System.Drawing.Point(604, 48);
            this.paersonPanel.Name = "paersonPanel";
            this.paersonPanel.Size = new System.Drawing.Size(83, 30);
            this.paersonPanel.TabIndex = 53;
            this.paersonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.AutoSize = true;
            this.personLabel.Location = new System.Drawing.Point(21, 9);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(41, 12);
            this.personLabel.TabIndex = 0;
            this.personLabel.Text = "操作员";
            // 
            // orderstatusPanel
            // 
            this.orderstatusPanel.Controls.Add(this.nicknameLabel);
            this.orderstatusPanel.Location = new System.Drawing.Point(538, 48);
            this.orderstatusPanel.Name = "orderstatusPanel";
            this.orderstatusPanel.Size = new System.Drawing.Size(66, 30);
            this.orderstatusPanel.TabIndex = 54;
            this.orderstatusPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // nicknameLabel
            // 
            this.nicknameLabel.Location = new System.Drawing.Point(2, 3);
            this.nicknameLabel.Name = "nicknameLabel";
            this.nicknameLabel.Size = new System.Drawing.Size(58, 24);
            this.nicknameLabel.TabIndex = 0;
            this.nicknameLabel.Text = "用户昵称";
            this.nicknameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ordermoneyPanel
            // 
            this.ordermoneyPanel.Controls.Add(this.ordermoneyLabel);
            this.ordermoneyPanel.Location = new System.Drawing.Point(388, 48);
            this.ordermoneyPanel.Name = "ordermoneyPanel";
            this.ordermoneyPanel.Size = new System.Drawing.Size(75, 30);
            this.ordermoneyPanel.TabIndex = 50;
            this.ordermoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // ordermoneyLabel
            // 
            this.ordermoneyLabel.AutoSize = true;
            this.ordermoneyLabel.Location = new System.Drawing.Point(11, 9);
            this.ordermoneyLabel.Name = "ordermoneyLabel";
            this.ordermoneyLabel.Size = new System.Drawing.Size(53, 12);
            this.ordermoneyLabel.TabIndex = 0;
            this.ordermoneyLabel.Text = "订单金额";
            // 
            // paychannelPanel
            // 
            this.paychannelPanel.Controls.Add(this.useTypeLabel);
            this.paychannelPanel.Location = new System.Drawing.Point(317, 48);
            this.paychannelPanel.Name = "paychannelPanel";
            this.paychannelPanel.Size = new System.Drawing.Size(71, 30);
            this.paychannelPanel.TabIndex = 52;
            this.paychannelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // useTypeLabel
            // 
            this.useTypeLabel.Location = new System.Drawing.Point(6, 3);
            this.useTypeLabel.Name = "useTypeLabel";
            this.useTypeLabel.Size = new System.Drawing.Size(58, 24);
            this.useTypeLabel.TabIndex = 0;
            this.useTypeLabel.Text = "核销方式";
            this.useTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ordernumPanel
            // 
            this.ordernumPanel.Controls.Add(this.couponNameLabel);
            this.ordernumPanel.Location = new System.Drawing.Point(217, 48);
            this.ordernumPanel.Name = "ordernumPanel";
            this.ordernumPanel.Size = new System.Drawing.Size(100, 30);
            this.ordernumPanel.TabIndex = 48;
            this.ordernumPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // couponNameLabel
            // 
            this.couponNameLabel.Location = new System.Drawing.Point(9, 2);
            this.couponNameLabel.Name = "couponNameLabel";
            this.couponNameLabel.Size = new System.Drawing.Size(83, 25);
            this.couponNameLabel.TabIndex = 0;
            this.couponNameLabel.Text = "券名称";
            this.couponNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // createtimePanel
            // 
            this.createtimePanel.Controls.Add(this.couponTypeLabel);
            this.createtimePanel.Location = new System.Drawing.Point(142, 48);
            this.createtimePanel.Name = "createtimePanel";
            this.createtimePanel.Size = new System.Drawing.Size(75, 30);
            this.createtimePanel.TabIndex = 49;
            this.createtimePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // couponTypeLabel
            // 
            this.couponTypeLabel.AutoSize = true;
            this.couponTypeLabel.Location = new System.Drawing.Point(17, 9);
            this.couponTypeLabel.Name = "couponTypeLabel";
            this.couponTypeLabel.Size = new System.Drawing.Size(41, 12);
            this.couponTypeLabel.TabIndex = 0;
            this.couponTypeLabel.Text = "券类型";
            // 
            // numPanel
            // 
            this.numPanel.BackColor = System.Drawing.SystemColors.Control;
            this.numPanel.Controls.Add(this.timeLabel);
            this.numPanel.Location = new System.Drawing.Point(12, 48);
            this.numPanel.Name = "numPanel";
            this.numPanel.Size = new System.Drawing.Size(130, 30);
            this.numPanel.TabIndex = 47;
            this.numPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(50, 9);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(29, 12);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "时间";
            // 
            // listPanel
            // 
            this.listPanel.AutoScroll = true;
            this.listPanel.Location = new System.Drawing.Point(12, 78);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(760, 300);
            this.listPanel.TabIndex = 46;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 9F);
            this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 11F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(126, 24);
            this.dateTimePicker1.TabIndex = 44;
            this.dateTimePicker1.TabStop = false;
            this.dateTimePicker1.Value = new System.DateTime(2015, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker1.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            this.dateTimePicker1.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("宋体", 11F);
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(145, 12);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(122, 24);
            this.dateTimePicker2.TabIndex = 45;
            this.dateTimePicker2.TabStop = false;
            this.dateTimePicker2.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // ccbCouponType
            // 
            this.ccbCouponType.FormattingEnabled = true;
            this.ccbCouponType.Items.AddRange(new object[] {
            "全部券类型",
            "代金券",
            "折扣券",
            "兑换券"});
            this.ccbCouponType.Location = new System.Drawing.Point(273, 15);
            this.ccbCouponType.Name = "ccbCouponType";
            this.ccbCouponType.Size = new System.Drawing.Size(76, 20);
            this.ccbCouponType.TabIndex = 57;
            this.ccbCouponType.TabStop = false;
            this.ccbCouponType.SelectedIndexChanged += new System.EventHandler(this.ccbCouponType_SelectedIndexChanged);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // couponUseListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ccbCouponType);
            this.Controls.Add(this.waitinfoLabel);
            this.Controls.Add(this.receiptmoneyPanel);
            this.Controls.Add(this.operationPanel);
            this.Controls.Add(this.paersonPanel);
            this.Controls.Add(this.orderstatusPanel);
            this.Controls.Add(this.ordermoneyPanel);
            this.Controls.Add(this.paychannelPanel);
            this.Controls.Add(this.ordernumPanel);
            this.Controls.Add(this.createtimePanel);
            this.Controls.Add(this.numPanel);
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateTimePicker2);
            this.Name = "couponUseListControl";
            this.Size = new System.Drawing.Size(780, 465);
            this.Load += new System.EventHandler(this.couponUseListControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.couponUseListControl_Paint);
            this.receiptmoneyPanel.ResumeLayout(false);
            this.receiptmoneyPanel.PerformLayout();
            this.operationPanel.ResumeLayout(false);
            this.operationPanel.PerformLayout();
            this.paersonPanel.ResumeLayout(false);
            this.paersonPanel.PerformLayout();
            this.orderstatusPanel.ResumeLayout(false);
            this.ordermoneyPanel.ResumeLayout(false);
            this.ordermoneyPanel.PerformLayout();
            this.paychannelPanel.ResumeLayout(false);
            this.ordernumPanel.ResumeLayout(false);
            this.createtimePanel.ResumeLayout(false);
            this.createtimePanel.PerformLayout();
            this.numPanel.ResumeLayout(false);
            this.numPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label waitinfoLabel;
        private System.Windows.Forms.Panel receiptmoneyPanel;
        private System.Windows.Forms.Label receiptmoneyLabel;
        private System.Windows.Forms.Panel operationPanel;
        private System.Windows.Forms.Label operationLabel;
        private System.Windows.Forms.Panel paersonPanel;
        private System.Windows.Forms.Label personLabel;
        private System.Windows.Forms.Panel orderstatusPanel;
        private System.Windows.Forms.Label nicknameLabel;
        private System.Windows.Forms.Panel ordermoneyPanel;
        private System.Windows.Forms.Label ordermoneyLabel;
        private System.Windows.Forms.Panel paychannelPanel;
        private System.Windows.Forms.Label useTypeLabel;
        private System.Windows.Forms.Panel ordernumPanel;
        private System.Windows.Forms.Label couponNameLabel;
        private System.Windows.Forms.Panel createtimePanel;
        private System.Windows.Forms.Label couponTypeLabel;
        private System.Windows.Forms.Panel numPanel;
        private System.Windows.Forms.Panel listPanel;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.ComboBox ccbCouponType;
        private System.Windows.Forms.Timer timer;
    }
}
