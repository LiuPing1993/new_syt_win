namespace qgj
{
    partial class flowListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flowListControl));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.waitinfoLabel = new System.Windows.Forms.Label();
            this.trademoneyPanel = new System.Windows.Forms.Panel();
            this.trademoneyLabel = new System.Windows.Forms.Label();
            this.operationPanel = new System.Windows.Forms.Panel();
            this.operationLabel = new System.Windows.Forms.Label();
            this.personPanel = new System.Windows.Forms.Panel();
            this.personLabel = new System.Windows.Forms.Label();
            this.moneyPanel = new System.Windows.Forms.Panel();
            this.moneyPicbox = new System.Windows.Forms.PictureBox();
            this.moneyLabel = new System.Windows.Forms.Label();
            this.paychannelPanel = new System.Windows.Forms.Panel();
            this.paychannelPicbox = new System.Windows.Forms.PictureBox();
            this.paychannelLabel = new System.Windows.Forms.Label();
            this.flownoPanel = new System.Windows.Forms.Panel();
            this.flownoLabel = new System.Windows.Forms.Label();
            this.typePanel = new System.Windows.Forms.Panel();
            this.typePicbox = new System.Windows.Forms.PictureBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.tradetimePanel = new System.Windows.Forms.Panel();
            this.tradetimeLabel = new System.Windows.Forms.Label();
            this.numPanel = new System.Windows.Forms.Panel();
            this.numLabel = new System.Windows.Forms.Label();
            this.flowlistPanel = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.trademoneyPanel.SuspendLayout();
            this.operationPanel.SuspendLayout();
            this.personPanel.SuspendLayout();
            this.moneyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moneyPicbox)).BeginInit();
            this.paychannelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paychannelPicbox)).BeginInit();
            this.flownoPanel.SuspendLayout();
            this.typePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typePicbox)).BeginInit();
            this.tradetimePanel.SuspendLayout();
            this.numPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("宋体", 9F);
            this.dateTimePicker1.Font = new System.Drawing.Font("宋体", 11F);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(126, 24);
            this.dateTimePicker1.TabIndex = 9;
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
            this.dateTimePicker2.TabIndex = 10;
            this.dateTimePicker2.TabStop = false;
            this.dateTimePicker2.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // waitinfoLabel
            // 
            this.waitinfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.waitinfoLabel.Location = new System.Drawing.Point(666, 12);
            this.waitinfoLabel.Name = "waitinfoLabel";
            this.waitinfoLabel.Size = new System.Drawing.Size(89, 24);
            this.waitinfoLabel.TabIndex = 31;
            this.waitinfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trademoneyPanel
            // 
            this.trademoneyPanel.Controls.Add(this.trademoneyLabel);
            this.trademoneyPanel.Location = new System.Drawing.Point(491, 48);
            this.trademoneyPanel.Name = "trademoneyPanel";
            this.trademoneyPanel.Size = new System.Drawing.Size(60, 30);
            this.trademoneyPanel.TabIndex = 37;
            this.trademoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // trademoneyLabel
            // 
            this.trademoneyLabel.Location = new System.Drawing.Point(3, 6);
            this.trademoneyLabel.Name = "trademoneyLabel";
            this.trademoneyLabel.Size = new System.Drawing.Size(54, 19);
            this.trademoneyLabel.TabIndex = 0;
            this.trademoneyLabel.Text = "交易金额";
            this.trademoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // operationPanel
            // 
            this.operationPanel.Controls.Add(this.operationLabel);
            this.operationPanel.Location = new System.Drawing.Point(692, 48);
            this.operationPanel.Name = "operationPanel";
            this.operationPanel.Size = new System.Drawing.Size(65, 30);
            this.operationPanel.TabIndex = 41;
            this.operationPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // operationLabel
            // 
            this.operationLabel.AutoSize = true;
            this.operationLabel.Location = new System.Drawing.Point(16, 8);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(29, 12);
            this.operationLabel.TabIndex = 0;
            this.operationLabel.Text = "操作";
            // 
            // personPanel
            // 
            this.personPanel.Controls.Add(this.personLabel);
            this.personPanel.Location = new System.Drawing.Point(609, 48);
            this.personPanel.Name = "personPanel";
            this.personPanel.Size = new System.Drawing.Size(83, 30);
            this.personPanel.TabIndex = 39;
            this.personPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.AutoSize = true;
            this.personLabel.Location = new System.Drawing.Point(21, 8);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(41, 12);
            this.personLabel.TabIndex = 0;
            this.personLabel.Text = "操作员";
            // 
            // moneyPanel
            // 
            this.moneyPanel.Controls.Add(this.moneyPicbox);
            this.moneyPanel.Controls.Add(this.moneyLabel);
            this.moneyPanel.Location = new System.Drawing.Point(551, 48);
            this.moneyPanel.Name = "moneyPanel";
            this.moneyPanel.Size = new System.Drawing.Size(58, 30);
            this.moneyPanel.TabIndex = 40;
            this.moneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.moneyPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moneytypeSelectShow);
            // 
            // moneyPicbox
            // 
            this.moneyPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("moneyPicbox.BackgroundImage")));
            this.moneyPicbox.Location = new System.Drawing.Point(45, 12);
            this.moneyPicbox.Name = "moneyPicbox";
            this.moneyPicbox.Size = new System.Drawing.Size(10, 8);
            this.moneyPicbox.TabIndex = 19;
            this.moneyPicbox.TabStop = false;
            this.moneyPicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moneytypeSelectShow);
            // 
            // moneyLabel
            // 
            this.moneyLabel.AutoSize = true;
            this.moneyLabel.Location = new System.Drawing.Point(10, 8);
            this.moneyLabel.Name = "moneyLabel";
            this.moneyLabel.Size = new System.Drawing.Size(29, 12);
            this.moneyLabel.TabIndex = 0;
            this.moneyLabel.Text = "流水";
            this.moneyLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moneytypeSelectShow);
            // 
            // paychannelPanel
            // 
            this.paychannelPanel.Controls.Add(this.paychannelPicbox);
            this.paychannelPanel.Controls.Add(this.paychannelLabel);
            this.paychannelPanel.Location = new System.Drawing.Point(418, 48);
            this.paychannelPanel.Name = "paychannelPanel";
            this.paychannelPanel.Size = new System.Drawing.Size(73, 30);
            this.paychannelPanel.TabIndex = 36;
            this.paychannelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.paychannelPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // paychannelPicbox
            // 
            this.paychannelPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paychannelPicbox.BackgroundImage")));
            this.paychannelPicbox.Location = new System.Drawing.Point(50, 12);
            this.paychannelPicbox.Name = "paychannelPicbox";
            this.paychannelPicbox.Size = new System.Drawing.Size(10, 8);
            this.paychannelPicbox.TabIndex = 17;
            this.paychannelPicbox.TabStop = false;
            this.paychannelPicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // paychannelLabel
            // 
            this.paychannelLabel.AutoSize = true;
            this.paychannelLabel.Location = new System.Drawing.Point(15, 8);
            this.paychannelLabel.Name = "paychannelLabel";
            this.paychannelLabel.Size = new System.Drawing.Size(29, 12);
            this.paychannelLabel.TabIndex = 0;
            this.paychannelLabel.Text = "支付";
            this.paychannelLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // flownoPanel
            // 
            this.flownoPanel.Controls.Add(this.flownoLabel);
            this.flownoPanel.Location = new System.Drawing.Point(248, 48);
            this.flownoPanel.Name = "flownoPanel";
            this.flownoPanel.Size = new System.Drawing.Size(170, 30);
            this.flownoPanel.TabIndex = 38;
            this.flownoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // flownoLabel
            // 
            this.flownoLabel.AutoSize = true;
            this.flownoLabel.Location = new System.Drawing.Point(67, 8);
            this.flownoLabel.Name = "flownoLabel";
            this.flownoLabel.Size = new System.Drawing.Size(41, 12);
            this.flownoLabel.TabIndex = 0;
            this.flownoLabel.Text = "流水号";
            // 
            // typePanel
            // 
            this.typePanel.Controls.Add(this.typePicbox);
            this.typePanel.Controls.Add(this.typeLabel);
            this.typePanel.Location = new System.Drawing.Point(178, 48);
            this.typePanel.Name = "typePanel";
            this.typePanel.Size = new System.Drawing.Size(70, 30);
            this.typePanel.TabIndex = 34;
            this.typePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.typePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.flowtypeSelectShow);
            // 
            // typePicbox
            // 
            this.typePicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("typePicbox.BackgroundImage")));
            this.typePicbox.Location = new System.Drawing.Point(55, 11);
            this.typePicbox.Name = "typePicbox";
            this.typePicbox.Size = new System.Drawing.Size(10, 8);
            this.typePicbox.TabIndex = 16;
            this.typePicbox.TabStop = false;
            this.typePicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.flowtypeSelectShow);
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(3, 1);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(55, 26);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "类型";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.typeLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.flowtypeSelectShow);
            // 
            // tradetimePanel
            // 
            this.tradetimePanel.Controls.Add(this.tradetimeLabel);
            this.tradetimePanel.Location = new System.Drawing.Point(48, 48);
            this.tradetimePanel.Name = "tradetimePanel";
            this.tradetimePanel.Size = new System.Drawing.Size(130, 30);
            this.tradetimePanel.TabIndex = 35;
            this.tradetimePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // tradetimeLabel
            // 
            this.tradetimeLabel.AutoSize = true;
            this.tradetimeLabel.Location = new System.Drawing.Point(37, 8);
            this.tradetimeLabel.Name = "tradetimeLabel";
            this.tradetimeLabel.Size = new System.Drawing.Size(53, 12);
            this.tradetimeLabel.TabIndex = 0;
            this.tradetimeLabel.Text = "交易时间";
            // 
            // numPanel
            // 
            this.numPanel.BackColor = System.Drawing.SystemColors.Control;
            this.numPanel.Controls.Add(this.numLabel);
            this.numPanel.Location = new System.Drawing.Point(12, 48);
            this.numPanel.Name = "numPanel";
            this.numPanel.Size = new System.Drawing.Size(36, 30);
            this.numPanel.TabIndex = 33;
            this.numPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
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
            // flowlistPanel
            // 
            this.flowlistPanel.AutoScroll = true;
            this.flowlistPanel.Location = new System.Drawing.Point(12, 78);
            this.flowlistPanel.Name = "flowlistPanel";
            this.flowlistPanel.Size = new System.Drawing.Size(760, 300);
            this.flowlistPanel.TabIndex = 32;
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // flowListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trademoneyPanel);
            this.Controls.Add(this.operationPanel);
            this.Controls.Add(this.personPanel);
            this.Controls.Add(this.moneyPanel);
            this.Controls.Add(this.paychannelPanel);
            this.Controls.Add(this.flownoPanel);
            this.Controls.Add(this.typePanel);
            this.Controls.Add(this.tradetimePanel);
            this.Controls.Add(this.numPanel);
            this.Controls.Add(this.flowlistPanel);
            this.Controls.Add(this.waitinfoLabel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateTimePicker2);
            this.Name = "flowListControl";
            this.Size = new System.Drawing.Size(780, 465);
            this.Load += new System.EventHandler(this.flowListControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.flowListControl_Paint);
            this.trademoneyPanel.ResumeLayout(false);
            this.operationPanel.ResumeLayout(false);
            this.operationPanel.PerformLayout();
            this.personPanel.ResumeLayout(false);
            this.personPanel.PerformLayout();
            this.moneyPanel.ResumeLayout(false);
            this.moneyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moneyPicbox)).EndInit();
            this.paychannelPanel.ResumeLayout(false);
            this.paychannelPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paychannelPicbox)).EndInit();
            this.flownoPanel.ResumeLayout(false);
            this.flownoPanel.PerformLayout();
            this.typePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.typePicbox)).EndInit();
            this.tradetimePanel.ResumeLayout(false);
            this.tradetimePanel.PerformLayout();
            this.numPanel.ResumeLayout(false);
            this.numPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label waitinfoLabel;
        private System.Windows.Forms.Panel trademoneyPanel;
        private System.Windows.Forms.Label trademoneyLabel;
        private System.Windows.Forms.Panel operationPanel;
        private System.Windows.Forms.Label operationLabel;
        private System.Windows.Forms.Panel personPanel;
        private System.Windows.Forms.Label personLabel;
        private System.Windows.Forms.Panel moneyPanel;
        private System.Windows.Forms.Label moneyLabel;
        private System.Windows.Forms.Panel paychannelPanel;
        private System.Windows.Forms.PictureBox paychannelPicbox;
        private System.Windows.Forms.Label paychannelLabel;
        private System.Windows.Forms.Panel flownoPanel;
        private System.Windows.Forms.Label flownoLabel;
        private System.Windows.Forms.Panel typePanel;
        private System.Windows.Forms.PictureBox typePicbox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Panel tradetimePanel;
        private System.Windows.Forms.Label tradetimeLabel;
        private System.Windows.Forms.Panel numPanel;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Panel flowlistPanel;
        private System.Windows.Forms.PictureBox moneyPicbox;
        private System.Windows.Forms.Timer timer;
    }
}
