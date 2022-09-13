namespace qgj
{
    partial class detailControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(detailControl));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.operationPanel = new System.Windows.Forms.Panel();
            this.operationLabel = new System.Windows.Forms.Label();
            this.ordertimePanel = new System.Windows.Forms.Panel();
            this.ordertimeLabel = new System.Windows.Forms.Label();
            this.personPanel = new System.Windows.Forms.Panel();
            this.personPicbox = new System.Windows.Forms.PictureBox();
            this.personLabel = new System.Windows.Forms.Label();
            this.detailmoneyPanel = new System.Windows.Forms.Panel();
            this.detailmoneyPicbox = new System.Windows.Forms.PictureBox();
            this.detailmoneyLabel = new System.Windows.Forms.Label();
            this.ordermoneyPanel = new System.Windows.Forms.Panel();
            this.ordermoneyLabel = new System.Windows.Forms.Label();
            this.paytypePanel = new System.Windows.Forms.Panel();
            this.paytpePicbox = new System.Windows.Forms.PictureBox();
            this.paytyppeLabel = new System.Windows.Forms.Label();
            this.ordernumPanel = new System.Windows.Forms.Panel();
            this.ordernumLabel = new System.Windows.Forms.Label();
            this.numPanel = new System.Windows.Forms.Panel();
            this.numLabel = new System.Windows.Forms.Label();
            this.orderlistPanel = new System.Windows.Forms.Panel();
            this.orderstatusPanel = new System.Windows.Forms.Panel();
            this.paystatusPicbox = new System.Windows.Forms.PictureBox();
            this.orderstatusLabel = new System.Windows.Forms.Label();
            this.waitinfoLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.operationPanel.SuspendLayout();
            this.ordertimePanel.SuspendLayout();
            this.personPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personPicbox)).BeginInit();
            this.detailmoneyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailmoneyPicbox)).BeginInit();
            this.ordermoneyPanel.SuspendLayout();
            this.paytypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paytpePicbox)).BeginInit();
            this.ordernumPanel.SuspendLayout();
            this.numPanel.SuspendLayout();
            this.orderstatusPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paystatusPicbox)).BeginInit();
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
            this.dateTimePicker1.TabIndex = 7;
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
            this.dateTimePicker2.TabIndex = 8;
            this.dateTimePicker2.TabStop = false;
            this.dateTimePicker2.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dateTimePicker2.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // operationPanel
            // 
            this.operationPanel.Controls.Add(this.operationLabel);
            this.operationPanel.Location = new System.Drawing.Point(685, 48);
            this.operationPanel.Name = "operationPanel";
            this.operationPanel.Size = new System.Drawing.Size(70, 30);
            this.operationPanel.TabIndex = 29;
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
            // ordertimePanel
            // 
            this.ordertimePanel.Controls.Add(this.ordertimeLabel);
            this.ordertimePanel.Location = new System.Drawing.Point(555, 48);
            this.ordertimePanel.Name = "ordertimePanel";
            this.ordertimePanel.Size = new System.Drawing.Size(130, 30);
            this.ordertimePanel.TabIndex = 27;
            this.ordertimePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // ordertimeLabel
            // 
            this.ordertimeLabel.AutoSize = true;
            this.ordertimeLabel.Location = new System.Drawing.Point(31, 8);
            this.ordertimeLabel.Name = "ordertimeLabel";
            this.ordertimeLabel.Size = new System.Drawing.Size(53, 12);
            this.ordertimeLabel.TabIndex = 0;
            this.ordertimeLabel.Text = "交易时间";
            // 
            // personPanel
            // 
            this.personPanel.Controls.Add(this.personPicbox);
            this.personPanel.Controls.Add(this.personLabel);
            this.personPanel.Location = new System.Drawing.Point(472, 48);
            this.personPanel.Name = "personPanel";
            this.personPanel.Size = new System.Drawing.Size(83, 30);
            this.personPanel.TabIndex = 28;
            this.personPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // personPicbox
            // 
            this.personPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("personPicbox.BackgroundImage")));
            this.personPicbox.Location = new System.Drawing.Point(63, 12);
            this.personPicbox.Name = "personPicbox";
            this.personPicbox.Size = new System.Drawing.Size(10, 8);
            this.personPicbox.TabIndex = 19;
            this.personPicbox.TabStop = false;
            // 
            // personLabel
            // 
            this.personLabel.AutoSize = true;
            this.personLabel.Location = new System.Drawing.Point(16, 8);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(41, 12);
            this.personLabel.TabIndex = 0;
            this.personLabel.Text = "操作员";
            // 
            // detailmoneyPanel
            // 
            this.detailmoneyPanel.Controls.Add(this.detailmoneyPicbox);
            this.detailmoneyPanel.Controls.Add(this.detailmoneyLabel);
            this.detailmoneyPanel.Location = new System.Drawing.Point(333, 48);
            this.detailmoneyPanel.Name = "detailmoneyPanel";
            this.detailmoneyPanel.Size = new System.Drawing.Size(73, 30);
            this.detailmoneyPanel.TabIndex = 25;
            this.detailmoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            this.detailmoneyPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moneytypeSelectShow);
            // 
            // detailmoneyPicbox
            // 
            this.detailmoneyPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("detailmoneyPicbox.BackgroundImage")));
            this.detailmoneyPicbox.Location = new System.Drawing.Point(50, 12);
            this.detailmoneyPicbox.Name = "detailmoneyPicbox";
            this.detailmoneyPicbox.Size = new System.Drawing.Size(10, 8);
            this.detailmoneyPicbox.TabIndex = 17;
            this.detailmoneyPicbox.TabStop = false;
            this.detailmoneyPicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moneytypeSelectShow);
            // 
            // detailmoneyLabel
            // 
            this.detailmoneyLabel.AutoSize = true;
            this.detailmoneyLabel.Location = new System.Drawing.Point(15, 8);
            this.detailmoneyLabel.Name = "detailmoneyLabel";
            this.detailmoneyLabel.Size = new System.Drawing.Size(29, 12);
            this.detailmoneyLabel.TabIndex = 0;
            this.detailmoneyLabel.Text = "流水";
            this.detailmoneyLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moneytypeSelectShow);
            // 
            // ordermoneyPanel
            // 
            this.ordermoneyPanel.Controls.Add(this.ordermoneyLabel);
            this.ordermoneyPanel.Location = new System.Drawing.Point(258, 48);
            this.ordermoneyPanel.Name = "ordermoneyPanel";
            this.ordermoneyPanel.Size = new System.Drawing.Size(75, 30);
            this.ordermoneyPanel.TabIndex = 26;
            this.ordermoneyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // ordermoneyLabel
            // 
            this.ordermoneyLabel.AutoSize = true;
            this.ordermoneyLabel.Location = new System.Drawing.Point(12, 8);
            this.ordermoneyLabel.Name = "ordermoneyLabel";
            this.ordermoneyLabel.Size = new System.Drawing.Size(53, 12);
            this.ordermoneyLabel.TabIndex = 0;
            this.ordermoneyLabel.Text = "订单金额";
            // 
            // paytypePanel
            // 
            this.paytypePanel.Controls.Add(this.paytpePicbox);
            this.paytypePanel.Controls.Add(this.paytyppeLabel);
            this.paytypePanel.Location = new System.Drawing.Point(188, 48);
            this.paytypePanel.Name = "paytypePanel";
            this.paytypePanel.Size = new System.Drawing.Size(70, 30);
            this.paytypePanel.TabIndex = 23;
            this.paytypePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            this.paytypePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // paytpePicbox
            // 
            this.paytpePicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paytpePicbox.BackgroundImage")));
            this.paytpePicbox.Location = new System.Drawing.Point(52, 11);
            this.paytpePicbox.Name = "paytpePicbox";
            this.paytpePicbox.Size = new System.Drawing.Size(10, 8);
            this.paytpePicbox.TabIndex = 16;
            this.paytpePicbox.TabStop = false;
            this.paytpePicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // paytyppeLabel
            // 
            this.paytyppeLabel.Location = new System.Drawing.Point(3, 1);
            this.paytyppeLabel.Name = "paytyppeLabel";
            this.paytyppeLabel.Size = new System.Drawing.Size(43, 26);
            this.paytyppeLabel.TabIndex = 0;
            this.paytyppeLabel.Text = "类型";
            this.paytyppeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.paytyppeLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paytypeSelectShow);
            // 
            // ordernumPanel
            // 
            this.ordernumPanel.Controls.Add(this.ordernumLabel);
            this.ordernumPanel.Location = new System.Drawing.Point(48, 48);
            this.ordernumPanel.Name = "ordernumPanel";
            this.ordernumPanel.Size = new System.Drawing.Size(140, 30);
            this.ordernumPanel.TabIndex = 24;
            this.ordernumPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            // 
            // ordernumLabel
            // 
            this.ordernumLabel.AutoSize = true;
            this.ordernumLabel.Location = new System.Drawing.Point(51, 8);
            this.ordernumLabel.Name = "ordernumLabel";
            this.ordernumLabel.Size = new System.Drawing.Size(41, 12);
            this.ordernumLabel.TabIndex = 0;
            this.ordernumLabel.Text = "订单号";
            // 
            // numPanel
            // 
            this.numPanel.BackColor = System.Drawing.SystemColors.Control;
            this.numPanel.Controls.Add(this.numLabel);
            this.numPanel.Location = new System.Drawing.Point(12, 48);
            this.numPanel.Name = "numPanel";
            this.numPanel.Size = new System.Drawing.Size(36, 30);
            this.numPanel.TabIndex = 22;
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
            this.orderlistPanel.TabIndex = 21;
            // 
            // orderstatusPanel
            // 
            this.orderstatusPanel.Controls.Add(this.paystatusPicbox);
            this.orderstatusPanel.Controls.Add(this.orderstatusLabel);
            this.orderstatusPanel.Location = new System.Drawing.Point(406, 48);
            this.orderstatusPanel.Name = "orderstatusPanel";
            this.orderstatusPanel.Size = new System.Drawing.Size(66, 30);
            this.orderstatusPanel.TabIndex = 26;
            this.orderstatusPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.numPanel_Paint);
            this.orderstatusPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orderstatusSelectShow);
            // 
            // paystatusPicbox
            // 
            this.paystatusPicbox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("paystatusPicbox.BackgroundImage")));
            this.paystatusPicbox.Location = new System.Drawing.Point(50, 11);
            this.paystatusPicbox.Name = "paystatusPicbox";
            this.paystatusPicbox.Size = new System.Drawing.Size(10, 8);
            this.paystatusPicbox.TabIndex = 18;
            this.paystatusPicbox.TabStop = false;
            this.paystatusPicbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orderstatusSelectShow);
            // 
            // orderstatusLabel
            // 
            this.orderstatusLabel.Location = new System.Drawing.Point(1, 1);
            this.orderstatusLabel.Name = "orderstatusLabel";
            this.orderstatusLabel.Size = new System.Drawing.Size(49, 28);
            this.orderstatusLabel.TabIndex = 0;
            this.orderstatusLabel.Text = "状态";
            this.orderstatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.orderstatusLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orderstatusSelectShow);
            // 
            // waitinfoLabel
            // 
            this.waitinfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.waitinfoLabel.Location = new System.Drawing.Point(666, 12);
            this.waitinfoLabel.Name = "waitinfoLabel";
            this.waitinfoLabel.Size = new System.Drawing.Size(89, 24);
            this.waitinfoLabel.TabIndex = 30;
            this.waitinfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer
            // 
            this.timer.Interval = 250;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // detailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.waitinfoLabel);
            this.Controls.Add(this.orderstatusPanel);
            this.Controls.Add(this.operationPanel);
            this.Controls.Add(this.ordertimePanel);
            this.Controls.Add(this.personPanel);
            this.Controls.Add(this.detailmoneyPanel);
            this.Controls.Add(this.ordermoneyPanel);
            this.Controls.Add(this.paytypePanel);
            this.Controls.Add(this.ordernumPanel);
            this.Controls.Add(this.numPanel);
            this.Controls.Add(this.orderlistPanel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dateTimePicker2);
            this.Name = "detailControl";
            this.Size = new System.Drawing.Size(780, 465);
            this.Load += new System.EventHandler(this.detailControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.detailControl_Paint);
            this.operationPanel.ResumeLayout(false);
            this.operationPanel.PerformLayout();
            this.ordertimePanel.ResumeLayout(false);
            this.ordertimePanel.PerformLayout();
            this.personPanel.ResumeLayout(false);
            this.personPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personPicbox)).EndInit();
            this.detailmoneyPanel.ResumeLayout(false);
            this.detailmoneyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailmoneyPicbox)).EndInit();
            this.ordermoneyPanel.ResumeLayout(false);
            this.ordermoneyPanel.PerformLayout();
            this.paytypePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paytpePicbox)).EndInit();
            this.ordernumPanel.ResumeLayout(false);
            this.ordernumPanel.PerformLayout();
            this.numPanel.ResumeLayout(false);
            this.numPanel.PerformLayout();
            this.orderstatusPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paystatusPicbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Panel operationPanel;
        private System.Windows.Forms.Label operationLabel;
        private System.Windows.Forms.Panel ordertimePanel;
        private System.Windows.Forms.Label ordertimeLabel;
        private System.Windows.Forms.Panel personPanel;
        private System.Windows.Forms.Label personLabel;
        private System.Windows.Forms.Panel detailmoneyPanel;
        private System.Windows.Forms.Label detailmoneyLabel;
        private System.Windows.Forms.Panel ordermoneyPanel;
        private System.Windows.Forms.Label ordermoneyLabel;
        private System.Windows.Forms.Panel paytypePanel;
        private System.Windows.Forms.Label paytyppeLabel;
        private System.Windows.Forms.Panel ordernumPanel;
        private System.Windows.Forms.Label ordernumLabel;
        private System.Windows.Forms.Panel numPanel;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Panel orderlistPanel;
        private System.Windows.Forms.Panel orderstatusPanel;
        private System.Windows.Forms.Label orderstatusLabel;
        private System.Windows.Forms.PictureBox paytpePicbox;
        private System.Windows.Forms.PictureBox personPicbox;
        private System.Windows.Forms.PictureBox detailmoneyPicbox;
        private System.Windows.Forms.PictureBox paystatusPicbox;
        private System.Windows.Forms.Label waitinfoLabel;
        private System.Windows.Forms.Timer timer;
    }
}
