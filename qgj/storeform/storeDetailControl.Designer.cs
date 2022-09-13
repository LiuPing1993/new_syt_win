namespace qgj
{
    partial class storeDetailControl
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
            this.paytypePanel = new System.Windows.Forms.Panel();
            this.paytypePic = new System.Windows.Forms.PictureBox();
            this.paytypeLabel = new System.Windows.Forms.Label();
            this.operationPanel = new System.Windows.Forms.Panel();
            this.operationLabel = new System.Windows.Forms.Label();
            this.storetimePanel = new System.Windows.Forms.Panel();
            this.storetimeLabel = new System.Windows.Forms.Label();
            this.personPanel = new System.Windows.Forms.Panel();
            this.personLabel = new System.Windows.Forms.Label();
            this.storemoneyPanel = new System.Windows.Forms.Panel();
            this.storemoneyLabel = new System.Windows.Forms.Label();
            this.storetypePanel = new System.Windows.Forms.Panel();
            this.storetypeLabel = new System.Windows.Forms.Label();
            this.membernamePanel = new System.Windows.Forms.Panel();
            this.membernameLabel = new System.Windows.Forms.Label();
            this.membernumPanel = new System.Windows.Forms.Panel();
            this.membernumLabel = new System.Windows.Forms.Label();
            this.numPanel = new System.Windows.Forms.Panel();
            this.numLabel = new System.Windows.Forms.Label();
            this.storelistPanel = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.paytypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paytypePic)).BeginInit();
            this.operationPanel.SuspendLayout();
            this.storetimePanel.SuspendLayout();
            this.personPanel.SuspendLayout();
            this.storemoneyPanel.SuspendLayout();
            this.storetypePanel.SuspendLayout();
            this.membernamePanel.SuspendLayout();
            this.membernumPanel.SuspendLayout();
            this.numPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // waitinfoLabel
            // 
            this.waitinfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.waitinfoLabel.Location = new System.Drawing.Point(667, 12);
            this.waitinfoLabel.Name = "waitinfoLabel";
            this.waitinfoLabel.Size = new System.Drawing.Size(93, 24);
            this.waitinfoLabel.TabIndex = 43;
            this.waitinfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // paytypePanel
            // 
            this.paytypePanel.Controls.Add(this.paytypePic);
            this.paytypePanel.Controls.Add(this.paytypeLabel);
            this.paytypePanel.Location = new System.Drawing.Point(444, 48);
            this.paytypePanel.Name = "paytypePanel";
            this.paytypePanel.Size = new System.Drawing.Size(66, 30);
            this.paytypePanel.TabIndex = 38;
            this.paytypePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            this.paytypePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fnPayTypeSelectShow);
            // 
            // paytypePic
            // 
            this.paytypePic.BackgroundImage = global::qgj.Properties.Resources.down;
            this.paytypePic.Location = new System.Drawing.Point(50, 10);
            this.paytypePic.Name = "paytypePic";
            this.paytypePic.Size = new System.Drawing.Size(10, 8);
            this.paytypePic.TabIndex = 1;
            this.paytypePic.TabStop = false;
            this.paytypePic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fnPayTypeSelectShow);
            // 
            // paytypeLabel
            // 
            this.paytypeLabel.Location = new System.Drawing.Point(1, 1);
            this.paytypeLabel.Name = "paytypeLabel";
            this.paytypeLabel.Size = new System.Drawing.Size(45, 28);
            this.paytypeLabel.TabIndex = 0;
            this.paytypeLabel.Text = "类型";
            this.paytypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.paytypeLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fnPayTypeSelectShow);
            // 
            // operationPanel
            // 
            this.operationPanel.Controls.Add(this.operationLabel);
            this.operationPanel.Location = new System.Drawing.Point(716, 48);
            this.operationPanel.Name = "operationPanel";
            this.operationPanel.Size = new System.Drawing.Size(55, 30);
            this.operationPanel.TabIndex = 42;
            this.operationPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // operationLabel
            // 
            this.operationLabel.AutoSize = true;
            this.operationLabel.Location = new System.Drawing.Point(15, 8);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(29, 12);
            this.operationLabel.TabIndex = 0;
            this.operationLabel.Text = "操作";
            // 
            // storetimePanel
            // 
            this.storetimePanel.Controls.Add(this.storetimeLabel);
            this.storetimePanel.Location = new System.Drawing.Point(586, 48);
            this.storetimePanel.Name = "storetimePanel";
            this.storetimePanel.Size = new System.Drawing.Size(130, 30);
            this.storetimePanel.TabIndex = 40;
            this.storetimePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // storetimeLabel
            // 
            this.storetimeLabel.AutoSize = true;
            this.storetimeLabel.Location = new System.Drawing.Point(38, 8);
            this.storetimeLabel.Name = "storetimeLabel";
            this.storetimeLabel.Size = new System.Drawing.Size(53, 12);
            this.storetimeLabel.TabIndex = 0;
            this.storetimeLabel.Text = "交易时间";
            // 
            // personPanel
            // 
            this.personPanel.Controls.Add(this.personLabel);
            this.personPanel.Location = new System.Drawing.Point(510, 48);
            this.personPanel.Name = "personPanel";
            this.personPanel.Size = new System.Drawing.Size(76, 30);
            this.personPanel.TabIndex = 41;
            this.personPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.AutoSize = true;
            this.personLabel.Location = new System.Drawing.Point(17, 8);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(41, 12);
            this.personLabel.TabIndex = 0;
            this.personLabel.Text = "操作员";
            // 
            // storemoneyPanel
            // 
            this.storemoneyPanel.Controls.Add(this.storemoneyLabel);
            this.storemoneyPanel.Location = new System.Drawing.Point(356, 48);
            this.storemoneyPanel.Name = "storemoneyPanel";
            this.storemoneyPanel.Size = new System.Drawing.Size(88, 30);
            this.storemoneyPanel.TabIndex = 37;
            this.storemoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // storemoneyLabel
            // 
            this.storemoneyLabel.AutoSize = true;
            this.storemoneyLabel.Location = new System.Drawing.Point(16, 8);
            this.storemoneyLabel.Name = "storemoneyLabel";
            this.storemoneyLabel.Size = new System.Drawing.Size(53, 12);
            this.storemoneyLabel.TabIndex = 0;
            this.storemoneyLabel.Text = "实收金额";
            // 
            // storetypePanel
            // 
            this.storetypePanel.Controls.Add(this.storetypeLabel);
            this.storetypePanel.Location = new System.Drawing.Point(226, 48);
            this.storetypePanel.Name = "storetypePanel";
            this.storetypePanel.Size = new System.Drawing.Size(130, 30);
            this.storetypePanel.TabIndex = 39;
            this.storetypePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // storetypeLabel
            // 
            this.storetypeLabel.AutoSize = true;
            this.storetypeLabel.Location = new System.Drawing.Point(37, 8);
            this.storetypeLabel.Name = "storetypeLabel";
            this.storetypeLabel.Size = new System.Drawing.Size(53, 12);
            this.storetypeLabel.TabIndex = 0;
            this.storetypeLabel.Text = "储值活动";
            // 
            // membernamePanel
            // 
            this.membernamePanel.Controls.Add(this.membernameLabel);
            this.membernamePanel.Location = new System.Drawing.Point(148, 48);
            this.membernamePanel.Name = "membernamePanel";
            this.membernamePanel.Size = new System.Drawing.Size(78, 30);
            this.membernamePanel.TabIndex = 35;
            this.membernamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // membernameLabel
            // 
            this.membernameLabel.AutoSize = true;
            this.membernameLabel.Location = new System.Drawing.Point(23, 8);
            this.membernameLabel.Name = "membernameLabel";
            this.membernameLabel.Size = new System.Drawing.Size(29, 12);
            this.membernameLabel.TabIndex = 1;
            this.membernameLabel.Text = "姓名";
            // 
            // membernumPanel
            // 
            this.membernumPanel.Controls.Add(this.membernumLabel);
            this.membernumPanel.Location = new System.Drawing.Point(48, 48);
            this.membernumPanel.Name = "membernumPanel";
            this.membernumPanel.Size = new System.Drawing.Size(100, 30);
            this.membernumPanel.TabIndex = 36;
            this.membernumPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // membernumLabel
            // 
            this.membernumLabel.AutoSize = true;
            this.membernumLabel.Location = new System.Drawing.Point(25, 8);
            this.membernumLabel.Name = "membernumLabel";
            this.membernumLabel.Size = new System.Drawing.Size(53, 12);
            this.membernumLabel.TabIndex = 0;
            this.membernumLabel.Text = "会员账号";
            // 
            // numPanel
            // 
            this.numPanel.BackColor = System.Drawing.SystemColors.Control;
            this.numPanel.Controls.Add(this.numLabel);
            this.numPanel.Location = new System.Drawing.Point(12, 48);
            this.numPanel.Name = "numPanel";
            this.numPanel.Size = new System.Drawing.Size(36, 30);
            this.numPanel.TabIndex = 34;
            this.numPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // numLabel
            // 
            this.numLabel.AutoSize = true;
            this.numLabel.Location = new System.Drawing.Point(4, 8);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(29, 12);
            this.numLabel.TabIndex = 0;
            this.numLabel.Text = "序号";
            // 
            // storelistPanel
            // 
            this.storelistPanel.AutoScroll = true;
            this.storelistPanel.Location = new System.Drawing.Point(12, 78);
            this.storelistPanel.Name = "storelistPanel";
            this.storelistPanel.Size = new System.Drawing.Size(760, 300);
            this.storelistPanel.TabIndex = 33;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 9F);
            this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 11F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(126, 24);
            this.dateTimePicker1.TabIndex = 31;
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
            this.dateTimePicker2.TabIndex = 32;
            this.dateTimePicker2.TabStop = false;
            this.dateTimePicker2.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // storeDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.waitinfoLabel);
            this.Controls.Add(this.paytypePanel);
            this.Controls.Add(this.operationPanel);
            this.Controls.Add(this.storetimePanel);
            this.Controls.Add(this.personPanel);
            this.Controls.Add(this.storemoneyPanel);
            this.Controls.Add(this.storetypePanel);
            this.Controls.Add(this.membernamePanel);
            this.Controls.Add(this.membernumPanel);
            this.Controls.Add(this.numPanel);
            this.Controls.Add(this.storelistPanel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateTimePicker2);
            this.Name = "storeDetailControl";
            this.Size = new System.Drawing.Size(780, 465);
            this.Load += new System.EventHandler(this.storeDetailControl_Load);
            this.VisibleChanged += new System.EventHandler(this.storeDetailControl_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.storeDetailControl_Paint);
            this.paytypePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paytypePic)).EndInit();
            this.operationPanel.ResumeLayout(false);
            this.operationPanel.PerformLayout();
            this.storetimePanel.ResumeLayout(false);
            this.storetimePanel.PerformLayout();
            this.personPanel.ResumeLayout(false);
            this.personPanel.PerformLayout();
            this.storemoneyPanel.ResumeLayout(false);
            this.storemoneyPanel.PerformLayout();
            this.storetypePanel.ResumeLayout(false);
            this.storetypePanel.PerformLayout();
            this.membernamePanel.ResumeLayout(false);
            this.membernamePanel.PerformLayout();
            this.membernumPanel.ResumeLayout(false);
            this.membernumPanel.PerformLayout();
            this.numPanel.ResumeLayout(false);
            this.numPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label waitinfoLabel;
        private System.Windows.Forms.Panel paytypePanel;
        public System.Windows.Forms.Label paytypeLabel;
        private System.Windows.Forms.Panel operationPanel;
        private System.Windows.Forms.Label operationLabel;
        private System.Windows.Forms.Panel storetimePanel;
        private System.Windows.Forms.Label storetimeLabel;
        private System.Windows.Forms.Panel personPanel;
        private System.Windows.Forms.Label personLabel;
        private System.Windows.Forms.Panel storemoneyPanel;
        private System.Windows.Forms.Label storemoneyLabel;
        private System.Windows.Forms.Panel storetypePanel;
        private System.Windows.Forms.Label storetypeLabel;
        private System.Windows.Forms.Panel membernamePanel;
        private System.Windows.Forms.Panel membernumPanel;
        private System.Windows.Forms.Label membernumLabel;
        private System.Windows.Forms.Panel numPanel;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Panel storelistPanel;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label membernameLabel;
        private System.Windows.Forms.PictureBox paytypePic;

    }
}
