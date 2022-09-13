namespace qgj
{
    partial class couponUseItemControl
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
            this.detailLabel = new System.Windows.Forms.Label();
            this.personLabel = new System.Windows.Forms.Label();
            this.nicknameLabel = new System.Windows.Forms.Label();
            this.orderreceiptLabel = new System.Windows.Forms.Label();
            this.ordermoneyLabel = new System.Windows.Forms.Label();
            this.usetypeLabel = new System.Windows.Forms.Label();
            this.couponnameLabel = new System.Windows.Forms.Label();
            this.coupontypeLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // detailLabel
            // 
            this.detailLabel.BackColor = System.Drawing.SystemColors.Control;
            this.detailLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.detailLabel.Location = new System.Drawing.Point(675, 0);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(69, 30);
            this.detailLabel.TabIndex = 28;
            this.detailLabel.Text = "详情";
            this.detailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.detailLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.detailLabel_Paint);
            this.detailLabel.MouseEnter += new System.EventHandler(this.detailLabel_MouseEnter);
            this.detailLabel.MouseLeave += new System.EventHandler(this.detailLabel_MouseLeave);
            // 
            // personLabel
            // 
            this.personLabel.BackColor = System.Drawing.SystemColors.Control;
            this.personLabel.Location = new System.Drawing.Point(592, 0);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(83, 30);
            this.personLabel.TabIndex = 26;
            this.personLabel.Text = "操作员";
            this.personLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.personLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // nicknameLabel
            // 
            this.nicknameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.nicknameLabel.Location = new System.Drawing.Point(526, 0);
            this.nicknameLabel.Name = "nicknameLabel";
            this.nicknameLabel.Size = new System.Drawing.Size(66, 30);
            this.nicknameLabel.TabIndex = 25;
            this.nicknameLabel.Text = "这是一个昵称";
            this.nicknameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nicknameLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // orderreceiptLabel
            // 
            this.orderreceiptLabel.BackColor = System.Drawing.SystemColors.Control;
            this.orderreceiptLabel.Location = new System.Drawing.Point(451, 0);
            this.orderreceiptLabel.Name = "orderreceiptLabel";
            this.orderreceiptLabel.Size = new System.Drawing.Size(75, 30);
            this.orderreceiptLabel.TabIndex = 24;
            this.orderreceiptLabel.Text = "实收金额";
            this.orderreceiptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.orderreceiptLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // ordermoneyLabel
            // 
            this.ordermoneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ordermoneyLabel.Location = new System.Drawing.Point(376, 0);
            this.ordermoneyLabel.Name = "ordermoneyLabel";
            this.ordermoneyLabel.Size = new System.Drawing.Size(75, 30);
            this.ordermoneyLabel.TabIndex = 23;
            this.ordermoneyLabel.Text = "订单金额";
            this.ordermoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ordermoneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // usetypeLabel
            // 
            this.usetypeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.usetypeLabel.Location = new System.Drawing.Point(306, 0);
            this.usetypeLabel.Name = "usetypeLabel";
            this.usetypeLabel.Size = new System.Drawing.Size(70, 30);
            this.usetypeLabel.TabIndex = 22;
            this.usetypeLabel.Text = "直接核销";
            this.usetypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.usetypeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // couponnameLabel
            // 
            this.couponnameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.couponnameLabel.Location = new System.Drawing.Point(205, 0);
            this.couponnameLabel.Name = "couponnameLabel";
            this.couponnameLabel.Size = new System.Drawing.Size(100, 30);
            this.couponnameLabel.TabIndex = 21;
            this.couponnameLabel.Text = "这是一张优惠券";
            this.couponnameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.couponnameLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            this.couponnameLabel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ordernumLabel_DoubleClick);
            // 
            // coupontypeLabel
            // 
            this.coupontypeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.coupontypeLabel.Location = new System.Drawing.Point(130, 0);
            this.coupontypeLabel.Name = "coupontypeLabel";
            this.coupontypeLabel.Size = new System.Drawing.Size(75, 30);
            this.coupontypeLabel.TabIndex = 20;
            this.coupontypeLabel.Text = "兑换券";
            this.coupontypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coupontypeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(130, 30);
            this.timeLabel.TabIndex = 19;
            this.timeLabel.Text = "2017-02-28 10:20:20";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.timeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // couponUseItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.personLabel);
            this.Controls.Add(this.nicknameLabel);
            this.Controls.Add(this.orderreceiptLabel);
            this.Controls.Add(this.ordermoneyLabel);
            this.Controls.Add(this.usetypeLabel);
            this.Controls.Add(this.couponnameLabel);
            this.Controls.Add(this.coupontypeLabel);
            this.Controls.Add(this.timeLabel);
            this.Name = "couponUseItemControl";
            this.Size = new System.Drawing.Size(745, 30);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label detailLabel;
        public System.Windows.Forms.Label personLabel;
        public System.Windows.Forms.Label nicknameLabel;
        public System.Windows.Forms.Label orderreceiptLabel;
        public System.Windows.Forms.Label ordermoneyLabel;
        public System.Windows.Forms.Label usetypeLabel;
        public System.Windows.Forms.Label couponnameLabel;
        public System.Windows.Forms.Label coupontypeLabel;
        public System.Windows.Forms.Label timeLabel;
    }
}
