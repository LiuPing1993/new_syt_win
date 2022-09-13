namespace qgj
{
    partial class koubeiUseItemControl
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
            this.timeLabel = new System.Windows.Forms.Label();
            this.couponnameLabel = new System.Windows.Forms.Label();
            this.couponmoneyLabel = new System.Windows.Forms.Label();
            this.merchantdisLabel = new System.Windows.Forms.Label();
            this.orderreceiptLabel = new System.Windows.Forms.Label();
            this.personLabel = new System.Windows.Forms.Label();
            this.detailLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(130, 30);
            this.timeLabel.TabIndex = 20;
            this.timeLabel.Text = "2017-02-28 10:20:20";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.timeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.otherLabel_Paint);
            // 
            // couponnameLabel
            // 
            this.couponnameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.couponnameLabel.Location = new System.Drawing.Point(130, 0);
            this.couponnameLabel.Name = "couponnameLabel";
            this.couponnameLabel.Size = new System.Drawing.Size(200, 30);
            this.couponnameLabel.TabIndex = 22;
            this.couponnameLabel.Text = "这是一张优惠券";
            this.couponnameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.couponnameLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.otherLabel_Paint);
            this.couponnameLabel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nameLabel_DoubleClick);
            // 
            // couponmoneyLabel
            // 
            this.couponmoneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.couponmoneyLabel.Location = new System.Drawing.Point(330, 0);
            this.couponmoneyLabel.Name = "couponmoneyLabel";
            this.couponmoneyLabel.Size = new System.Drawing.Size(75, 30);
            this.couponmoneyLabel.TabIndex = 24;
            this.couponmoneyLabel.Text = "券金额";
            this.couponmoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.couponmoneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.otherLabel_Paint);
            // 
            // merchantdisLabel
            // 
            this.merchantdisLabel.BackColor = System.Drawing.SystemColors.Control;
            this.merchantdisLabel.Location = new System.Drawing.Point(405, 0);
            this.merchantdisLabel.Name = "merchantdisLabel";
            this.merchantdisLabel.Size = new System.Drawing.Size(75, 30);
            this.merchantdisLabel.TabIndex = 25;
            this.merchantdisLabel.Text = "商家优惠";
            this.merchantdisLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.merchantdisLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.otherLabel_Paint);
            // 
            // orderreceiptLabel
            // 
            this.orderreceiptLabel.BackColor = System.Drawing.SystemColors.Control;
            this.orderreceiptLabel.Location = new System.Drawing.Point(480, 0);
            this.orderreceiptLabel.Name = "orderreceiptLabel";
            this.orderreceiptLabel.Size = new System.Drawing.Size(75, 30);
            this.orderreceiptLabel.TabIndex = 26;
            this.orderreceiptLabel.Text = "实收金额";
            this.orderreceiptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.orderreceiptLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.otherLabel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.BackColor = System.Drawing.SystemColors.Control;
            this.personLabel.Location = new System.Drawing.Point(555, 0);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(115, 30);
            this.personLabel.TabIndex = 27;
            this.personLabel.Text = "操作员";
            this.personLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.personLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.otherLabel_Paint);
            // 
            // detailLabel
            // 
            this.detailLabel.BackColor = System.Drawing.SystemColors.Control;
            this.detailLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.detailLabel.Location = new System.Drawing.Point(670, 0);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(75, 30);
            this.detailLabel.TabIndex = 29;
            this.detailLabel.Text = "详情";
            this.detailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.detailLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.detailLabel_Paint);
            this.detailLabel.MouseEnter += new System.EventHandler(this.detailLabel_MouseEnter);
            this.detailLabel.MouseLeave += new System.EventHandler(this.detailLabel_MouseLeave);
            // 
            // koubeiUseItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.personLabel);
            this.Controls.Add(this.orderreceiptLabel);
            this.Controls.Add(this.merchantdisLabel);
            this.Controls.Add(this.couponmoneyLabel);
            this.Controls.Add(this.couponnameLabel);
            this.Controls.Add(this.timeLabel);
            this.Name = "koubeiUseItemControl";
            this.Size = new System.Drawing.Size(745, 30);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label timeLabel;
        public System.Windows.Forms.Label couponnameLabel;
        public System.Windows.Forms.Label couponmoneyLabel;
        public System.Windows.Forms.Label merchantdisLabel;
        public System.Windows.Forms.Label orderreceiptLabel;
        public System.Windows.Forms.Label personLabel;
        public System.Windows.Forms.Label detailLabel;
    }
}
