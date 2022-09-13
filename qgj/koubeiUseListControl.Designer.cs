namespace qgj
{
    partial class koubeiUseListControl
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.couponNameLabel = new System.Windows.Forms.Label();
            this.ordermoneyLabel = new System.Windows.Forms.Label();
            this.nicknameLabel = new System.Windows.Forms.Label();
            this.waitinfoLabel = new System.Windows.Forms.Label();
            this.ordermoneyPanel = new System.Windows.Forms.Panel();
            this.personLabel = new System.Windows.Forms.Label();
            this.paersonPanel = new System.Windows.Forms.Panel();
            this.operationLabel = new System.Windows.Forms.Label();
            this.operationPanel = new System.Windows.Forms.Panel();
            this.couponnamePanel = new System.Windows.Forms.Panel();
            this.receiptmoneyLabel = new System.Windows.Forms.Label();
            this.receiptmoneyPanel = new System.Windows.Forms.Panel();
            this.orderstatusPanel = new System.Windows.Forms.Panel();
            this.timePanel = new System.Windows.Forms.Panel();
            this.listPanel = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.ordermoneyPanel.SuspendLayout();
            this.paersonPanel.SuspendLayout();
            this.operationPanel.SuspendLayout();
            this.couponnamePanel.SuspendLayout();
            this.receiptmoneyPanel.SuspendLayout();
            this.orderstatusPanel.SuspendLayout();
            this.timePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
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
            // couponNameLabel
            // 
            this.couponNameLabel.Location = new System.Drawing.Point(2, 2);
            this.couponNameLabel.Name = "couponNameLabel";
            this.couponNameLabel.Size = new System.Drawing.Size(195, 25);
            this.couponNameLabel.TabIndex = 0;
            this.couponNameLabel.Text = "券名称";
            this.couponNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ordermoneyLabel
            // 
            this.ordermoneyLabel.AutoSize = true;
            this.ordermoneyLabel.Location = new System.Drawing.Point(18, 9);
            this.ordermoneyLabel.Name = "ordermoneyLabel";
            this.ordermoneyLabel.Size = new System.Drawing.Size(41, 12);
            this.ordermoneyLabel.TabIndex = 0;
            this.ordermoneyLabel.Text = "券金额";
            // 
            // nicknameLabel
            // 
            this.nicknameLabel.Location = new System.Drawing.Point(3, 3);
            this.nicknameLabel.Name = "nicknameLabel";
            this.nicknameLabel.Size = new System.Drawing.Size(69, 24);
            this.nicknameLabel.TabIndex = 0;
            this.nicknameLabel.Text = "实收金额";
            this.nicknameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // waitinfoLabel
            // 
            this.waitinfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.waitinfoLabel.Location = new System.Drawing.Point(666, 12);
            this.waitinfoLabel.Name = "waitinfoLabel";
            this.waitinfoLabel.Size = new System.Drawing.Size(89, 24);
            this.waitinfoLabel.TabIndex = 70;
            this.waitinfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ordermoneyPanel
            // 
            this.ordermoneyPanel.Controls.Add(this.ordermoneyLabel);
            this.ordermoneyPanel.Location = new System.Drawing.Point(342, 48);
            this.ordermoneyPanel.Name = "ordermoneyPanel";
            this.ordermoneyPanel.Size = new System.Drawing.Size(75, 30);
            this.ordermoneyPanel.TabIndex = 64;
            this.ordermoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.AutoSize = true;
            this.personLabel.Location = new System.Drawing.Point(40, 9);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(41, 12);
            this.personLabel.TabIndex = 0;
            this.personLabel.Text = "操作员";
            // 
            // paersonPanel
            // 
            this.paersonPanel.Controls.Add(this.personLabel);
            this.paersonPanel.Location = new System.Drawing.Point(567, 48);
            this.paersonPanel.Name = "paersonPanel";
            this.paersonPanel.Size = new System.Drawing.Size(115, 30);
            this.paersonPanel.TabIndex = 67;
            this.paersonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // operationLabel
            // 
            this.operationLabel.AutoSize = true;
            this.operationLabel.Location = new System.Drawing.Point(25, 8);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(29, 12);
            this.operationLabel.TabIndex = 0;
            this.operationLabel.Text = "操作";
            // 
            // operationPanel
            // 
            this.operationPanel.Controls.Add(this.operationLabel);
            this.operationPanel.Location = new System.Drawing.Point(681, 48);
            this.operationPanel.Name = "operationPanel";
            this.operationPanel.Size = new System.Drawing.Size(76, 30);
            this.operationPanel.TabIndex = 69;
            this.operationPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // couponnamePanel
            // 
            this.couponnamePanel.Controls.Add(this.couponNameLabel);
            this.couponnamePanel.Location = new System.Drawing.Point(142, 48);
            this.couponnamePanel.Name = "couponnamePanel";
            this.couponnamePanel.Size = new System.Drawing.Size(200, 30);
            this.couponnamePanel.TabIndex = 62;
            this.couponnamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // receiptmoneyLabel
            // 
            this.receiptmoneyLabel.AutoSize = true;
            this.receiptmoneyLabel.Location = new System.Drawing.Point(11, 9);
            this.receiptmoneyLabel.Name = "receiptmoneyLabel";
            this.receiptmoneyLabel.Size = new System.Drawing.Size(53, 12);
            this.receiptmoneyLabel.TabIndex = 0;
            this.receiptmoneyLabel.Text = "商家优惠";
            this.receiptmoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // receiptmoneyPanel
            // 
            this.receiptmoneyPanel.Controls.Add(this.receiptmoneyLabel);
            this.receiptmoneyPanel.Location = new System.Drawing.Point(417, 48);
            this.receiptmoneyPanel.Name = "receiptmoneyPanel";
            this.receiptmoneyPanel.Size = new System.Drawing.Size(75, 30);
            this.receiptmoneyPanel.TabIndex = 65;
            this.receiptmoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // orderstatusPanel
            // 
            this.orderstatusPanel.Controls.Add(this.nicknameLabel);
            this.orderstatusPanel.Location = new System.Drawing.Point(492, 48);
            this.orderstatusPanel.Name = "orderstatusPanel";
            this.orderstatusPanel.Size = new System.Drawing.Size(75, 30);
            this.orderstatusPanel.TabIndex = 68;
            this.orderstatusPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // timePanel
            // 
            this.timePanel.BackColor = System.Drawing.SystemColors.Control;
            this.timePanel.Controls.Add(this.timeLabel);
            this.timePanel.Location = new System.Drawing.Point(12, 48);
            this.timePanel.Name = "timePanel";
            this.timePanel.Size = new System.Drawing.Size(130, 30);
            this.timePanel.TabIndex = 61;
            this.timePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // listPanel
            // 
            this.listPanel.AutoScroll = true;
            this.listPanel.Location = new System.Drawing.Point(12, 78);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(760, 300);
            this.listPanel.TabIndex = 60;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 9F);
            this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 11F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(126, 24);
            this.dateTimePicker1.TabIndex = 58;
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
            this.dateTimePicker2.TabIndex = 59;
            this.dateTimePicker2.TabStop = false;
            this.dateTimePicker2.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // koubeiUseListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.waitinfoLabel);
            this.Controls.Add(this.ordermoneyPanel);
            this.Controls.Add(this.paersonPanel);
            this.Controls.Add(this.operationPanel);
            this.Controls.Add(this.couponnamePanel);
            this.Controls.Add(this.receiptmoneyPanel);
            this.Controls.Add(this.orderstatusPanel);
            this.Controls.Add(this.timePanel);
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateTimePicker2);
            this.Name = "koubeiUseListControl";
            this.Size = new System.Drawing.Size(780, 465);
            this.Load += new System.EventHandler(this.koubeiUseListControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.koubeiUseListControl_Paint);
            this.ordermoneyPanel.ResumeLayout(false);
            this.ordermoneyPanel.PerformLayout();
            this.paersonPanel.ResumeLayout(false);
            this.paersonPanel.PerformLayout();
            this.operationPanel.ResumeLayout(false);
            this.operationPanel.PerformLayout();
            this.couponnamePanel.ResumeLayout(false);
            this.receiptmoneyPanel.ResumeLayout(false);
            this.receiptmoneyPanel.PerformLayout();
            this.orderstatusPanel.ResumeLayout(false);
            this.timePanel.ResumeLayout(false);
            this.timePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label couponNameLabel;
        private System.Windows.Forms.Label ordermoneyLabel;
        private System.Windows.Forms.Label nicknameLabel;
        private System.Windows.Forms.Label waitinfoLabel;
        private System.Windows.Forms.Panel ordermoneyPanel;
        private System.Windows.Forms.Label personLabel;
        private System.Windows.Forms.Panel paersonPanel;
        private System.Windows.Forms.Label operationLabel;
        private System.Windows.Forms.Panel operationPanel;
        private System.Windows.Forms.Panel couponnamePanel;
        private System.Windows.Forms.Label receiptmoneyLabel;
        private System.Windows.Forms.Panel receiptmoneyPanel;
        private System.Windows.Forms.Panel orderstatusPanel;
        private System.Windows.Forms.Panel timePanel;
        private System.Windows.Forms.Panel listPanel;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
    }
}
