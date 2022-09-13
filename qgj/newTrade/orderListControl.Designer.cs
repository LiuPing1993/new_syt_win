namespace qgj
{
    partial class orderListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(orderListControl));
            this.waitinfoLabel = new System.Windows.Forms.Label();
            this.receiptmoneyPanel = new System.Windows.Forms.Panel();
            this.receiptmoneyLabel = new System.Windows.Forms.Label();
            this.operationPanel = new System.Windows.Forms.Panel();
            this.operationLabel = new System.Windows.Forms.Label();
            this.paersonPanel = new System.Windows.Forms.Panel();
            this.personLabel = new System.Windows.Forms.Label();
            this.orderstatusPanel = new System.Windows.Forms.Panel();
            this.orderstatusPicbox = new System.Windows.Forms.PictureBox();
            this.orderstatusLabel = new System.Windows.Forms.Label();
            this.ordermoneyPanel = new System.Windows.Forms.Panel();
            this.ordermoneyLabel = new System.Windows.Forms.Label();
            this.paychannelPanel = new System.Windows.Forms.Panel();
            this.paychannelPicbox = new System.Windows.Forms.PictureBox();
            this.paychannelLabel = new System.Windows.Forms.Label();
            this.ordernumPanel = new System.Windows.Forms.Panel();
            this.ordernumLabel = new System.Windows.Forms.Label();
            this.createtimePanel = new System.Windows.Forms.Panel();
            this.createtimeLabel = new System.Windows.Forms.Label();
            this.numPanel = new System.Windows.Forms.Panel();
            this.numLabel = new System.Windows.Forms.Label();
            this.orderlistPanel = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.receiptmoneyPanel.SuspendLayout();
            this.operationPanel.SuspendLayout();
            this.paersonPanel.SuspendLayout();
            this.orderstatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderstatusPicbox)).BeginInit();
            this.ordermoneyPanel.SuspendLayout();
            this.paychannelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paychannelPicbox)).BeginInit();
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
            this.waitinfoLabel.TabIndex = 43;
            this.waitinfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // receiptmoneyPanel
            // 
            this.receiptmoneyPanel.Controls.Add(this.receiptmoneyLabel);
            this.receiptmoneyPanel.Location = new System.Drawing.Point(463, 48);
            this.receiptmoneyPanel.Name = "receiptmoneyPanel";
            this.receiptmoneyPanel.Size = new System.Drawing.Size(75, 30);
            this.receiptmoneyPanel.TabIndex = 38;
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
            this.operationPanel.TabIndex = 42;
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
            this.paersonPanel.TabIndex = 40;
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
            this.orderstatusPanel.Controls.Add(this.orderstatusPicbox);
            this.orderstatusPanel.Controls.Add(this.orderstatusLabel);
            this.orderstatusPanel.Location = new System.Drawing.Point(538, 48);
            this.orderstatusPanel.Name = "orderstatusPanel";
            this.orderstatusPanel.Size = new System.Drawing.Size(66, 30);
            this.orderstatusPanel.TabIndex = 41;
            this.orderstatusPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            this.orderstatusPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orderstatusSelectShow);
            // 
            // orderstatusPicbox
            // 
            this.orderstatusPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("orderstatusPicbox.BackgroundImage")));
            this.orderstatusPicbox.Location = new System.Drawing.Point(54, 12);
            this.orderstatusPicbox.Name = "orderstatusPicbox";
            this.orderstatusPicbox.Size = new System.Drawing.Size(10, 8);
            this.orderstatusPicbox.TabIndex = 19;
            this.orderstatusPicbox.TabStop = false;
            this.orderstatusPicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orderstatusSelectShow);
            // 
            // orderstatusLabel
            // 
            this.orderstatusLabel.Location = new System.Drawing.Point(2, 3);
            this.orderstatusLabel.Name = "orderstatusLabel";
            this.orderstatusLabel.Size = new System.Drawing.Size(58, 24);
            this.orderstatusLabel.TabIndex = 0;
            this.orderstatusLabel.Text = "订单状态";
            this.orderstatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.orderstatusLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orderstatusSelectShow);
            // 
            // ordermoneyPanel
            // 
            this.ordermoneyPanel.Controls.Add(this.ordermoneyLabel);
            this.ordermoneyPanel.Location = new System.Drawing.Point(388, 48);
            this.ordermoneyPanel.Name = "ordermoneyPanel";
            this.ordermoneyPanel.Size = new System.Drawing.Size(75, 30);
            this.ordermoneyPanel.TabIndex = 37;
            this.ordermoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // ordermoneyLabel
            // 
            this.ordermoneyLabel.AutoSize = true;
            this.ordermoneyLabel.Location = new System.Drawing.Point(12, 9);
            this.ordermoneyLabel.Name = "ordermoneyLabel";
            this.ordermoneyLabel.Size = new System.Drawing.Size(53, 12);
            this.ordermoneyLabel.TabIndex = 0;
            this.ordermoneyLabel.Text = "订单金额";
            // 
            // paychannelPanel
            // 
            this.paychannelPanel.Controls.Add(this.paychannelPicbox);
            this.paychannelPanel.Controls.Add(this.paychannelLabel);
            this.paychannelPanel.Location = new System.Drawing.Point(318, 48);
            this.paychannelPanel.Name = "paychannelPanel";
            this.paychannelPanel.Size = new System.Drawing.Size(70, 30);
            this.paychannelPanel.TabIndex = 39;
            this.paychannelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            this.paychannelPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // paychannelPicbox
            // 
            this.paychannelPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paychannelPicbox.BackgroundImage")));
            this.paychannelPicbox.Location = new System.Drawing.Point(54, 11);
            this.paychannelPicbox.Name = "paychannelPicbox";
            this.paychannelPicbox.Size = new System.Drawing.Size(10, 8);
            this.paychannelPicbox.TabIndex = 18;
            this.paychannelPicbox.TabStop = false;
            this.paychannelPicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // paychannelLabel
            // 
            this.paychannelLabel.Location = new System.Drawing.Point(5, 3);
            this.paychannelLabel.Name = "paychannelLabel";
            this.paychannelLabel.Size = new System.Drawing.Size(51, 24);
            this.paychannelLabel.TabIndex = 0;
            this.paychannelLabel.Text = "类型";
            this.paychannelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.paychannelLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // ordernumPanel
            // 
            this.ordernumPanel.Controls.Add(this.ordernumLabel);
            this.ordernumPanel.Location = new System.Drawing.Point(178, 48);
            this.ordernumPanel.Name = "ordernumPanel";
            this.ordernumPanel.Size = new System.Drawing.Size(140, 30);
            this.ordernumPanel.TabIndex = 35;
            this.ordernumPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // ordernumLabel
            // 
            this.ordernumLabel.Location = new System.Drawing.Point(29, 3);
            this.ordernumLabel.Name = "ordernumLabel";
            this.ordernumLabel.Size = new System.Drawing.Size(83, 25);
            this.ordernumLabel.TabIndex = 0;
            this.ordernumLabel.Text = "订单号";
            this.ordernumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // createtimePanel
            // 
            this.createtimePanel.Controls.Add(this.createtimeLabel);
            this.createtimePanel.Location = new System.Drawing.Point(48, 48);
            this.createtimePanel.Name = "createtimePanel";
            this.createtimePanel.Size = new System.Drawing.Size(130, 30);
            this.createtimePanel.TabIndex = 36;
            this.createtimePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // createtimeLabel
            // 
            this.createtimeLabel.AutoSize = true;
            this.createtimeLabel.Location = new System.Drawing.Point(39, 9);
            this.createtimeLabel.Name = "createtimeLabel";
            this.createtimeLabel.Size = new System.Drawing.Size(53, 12);
            this.createtimeLabel.TabIndex = 0;
            this.createtimeLabel.Text = "下单时间";
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
            // orderlistPanel
            // 
            this.orderlistPanel.AutoScroll = true;
            this.orderlistPanel.Location = new System.Drawing.Point(12, 78);
            this.orderlistPanel.Name = "orderlistPanel";
            this.orderlistPanel.Size = new System.Drawing.Size(760, 300);
            this.orderlistPanel.TabIndex = 33;
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
            // orderListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Controls.Add(this.orderlistPanel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateTimePicker2);
            this.Name = "orderListControl";
            this.Size = new System.Drawing.Size(780, 465);
            this.Load += new System.EventHandler(this.orderListControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.orderListControl_Paint);
            this.receiptmoneyPanel.ResumeLayout(false);
            this.receiptmoneyPanel.PerformLayout();
            this.operationPanel.ResumeLayout(false);
            this.operationPanel.PerformLayout();
            this.paersonPanel.ResumeLayout(false);
            this.paersonPanel.PerformLayout();
            this.orderstatusPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.orderstatusPicbox)).EndInit();
            this.ordermoneyPanel.ResumeLayout(false);
            this.ordermoneyPanel.PerformLayout();
            this.paychannelPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paychannelPicbox)).EndInit();
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
        private System.Windows.Forms.PictureBox orderstatusPicbox;
        private System.Windows.Forms.Label orderstatusLabel;
        private System.Windows.Forms.Panel ordermoneyPanel;
        private System.Windows.Forms.Label ordermoneyLabel;
        private System.Windows.Forms.Panel paychannelPanel;
        private System.Windows.Forms.Label paychannelLabel;
        private System.Windows.Forms.Panel ordernumPanel;
        private System.Windows.Forms.Label ordernumLabel;
        private System.Windows.Forms.Panel createtimePanel;
        private System.Windows.Forms.Label createtimeLabel;
        private System.Windows.Forms.Panel numPanel;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Panel orderlistPanel;
        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.PictureBox paychannelPicbox;
        private System.Windows.Forms.Timer timer;
    }
}
