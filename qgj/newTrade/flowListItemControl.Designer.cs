namespace qgj
{
    partial class flowListItemControl
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
            this.numLabel = new System.Windows.Forms.Label();
            this.tradetimeLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.flownoLabel = new System.Windows.Forms.Label();
            this.paychannelLabel = new System.Windows.Forms.Label();
            this.trademoneyLabel = new System.Windows.Forms.Label();
            this.moneyLabel = new System.Windows.Forms.Label();
            this.personLabel = new System.Windows.Forms.Label();
            this.detailLabel = new System.Windows.Forms.Label();
            this.refundLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // numLabel
            // 
            this.numLabel.BackColor = System.Drawing.SystemColors.Control;
            this.numLabel.Location = new System.Drawing.Point(0, 0);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(36, 30);
            this.numLabel.TabIndex = 4;
            this.numLabel.Text = "序号";
            this.numLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.numLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // tradetimeLabel
            // 
            this.tradetimeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.tradetimeLabel.Location = new System.Drawing.Point(36, 0);
            this.tradetimeLabel.Name = "tradetimeLabel";
            this.tradetimeLabel.Size = new System.Drawing.Size(130, 30);
            this.tradetimeLabel.TabIndex = 11;
            this.tradetimeLabel.Text = "2017-02-28 10:20:20";
            this.tradetimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tradetimeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // typeLabel
            // 
            this.typeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.typeLabel.Location = new System.Drawing.Point(166, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(70, 30);
            this.typeLabel.TabIndex = 13;
            this.typeLabel.Text = "类型";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.typeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // flownoLabel
            // 
            this.flownoLabel.BackColor = System.Drawing.SystemColors.Control;
            this.flownoLabel.Font = new System.Drawing.Font("宋体", 7.5F);
            this.flownoLabel.Location = new System.Drawing.Point(236, 0);
            this.flownoLabel.Name = "flownoLabel";
            this.flownoLabel.Size = new System.Drawing.Size(170, 30);
            this.flownoLabel.TabIndex = 14;
            this.flownoLabel.Text = "流水号";
            this.flownoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.flownoLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            this.flownoLabel.DoubleClick += new System.EventHandler(this.ordernumLabel_DoubleClick);
            // 
            // paychannelLabel
            // 
            this.paychannelLabel.BackColor = System.Drawing.SystemColors.Control;
            this.paychannelLabel.Location = new System.Drawing.Point(406, 0);
            this.paychannelLabel.Name = "paychannelLabel";
            this.paychannelLabel.Size = new System.Drawing.Size(73, 30);
            this.paychannelLabel.TabIndex = 15;
            this.paychannelLabel.Text = "支付类型";
            this.paychannelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.paychannelLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // trademoneyLabel
            // 
            this.trademoneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.trademoneyLabel.Location = new System.Drawing.Point(479, 0);
            this.trademoneyLabel.Name = "trademoneyLabel";
            this.trademoneyLabel.Size = new System.Drawing.Size(60, 30);
            this.trademoneyLabel.TabIndex = 16;
            this.trademoneyLabel.Text = "交易金额";
            this.trademoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.trademoneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // moneyLabel
            // 
            this.moneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.moneyLabel.Location = new System.Drawing.Point(539, 0);
            this.moneyLabel.Name = "moneyLabel";
            this.moneyLabel.Size = new System.Drawing.Size(58, 30);
            this.moneyLabel.TabIndex = 17;
            this.moneyLabel.Text = "流水";
            this.moneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.moneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.BackColor = System.Drawing.SystemColors.Control;
            this.personLabel.Location = new System.Drawing.Point(597, 0);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(83, 30);
            this.personLabel.TabIndex = 18;
            this.personLabel.Text = "操作员";
            this.personLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.personLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // detailLabel
            // 
            this.detailLabel.BackColor = System.Drawing.SystemColors.Control;
            this.detailLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.detailLabel.Location = new System.Drawing.Point(712, 0);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(33, 30);
            this.detailLabel.TabIndex = 19;
            this.detailLabel.Text = "详情";
            this.detailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.detailLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.detailLabel_Paint);
            this.detailLabel.MouseEnter += new System.EventHandler(this.operation_MouseEnter);
            this.detailLabel.MouseLeave += new System.EventHandler(this.operation_MouseLeave);
            // 
            // refundLabel
            // 
            this.refundLabel.BackColor = System.Drawing.SystemColors.Control;
            this.refundLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.refundLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refundLabel.Location = new System.Drawing.Point(680, 0);
            this.refundLabel.Name = "refundLabel";
            this.refundLabel.Size = new System.Drawing.Size(32, 30);
            this.refundLabel.TabIndex = 20;
            this.refundLabel.Text = "退款";
            this.refundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refundLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            this.refundLabel.MouseEnter += new System.EventHandler(this.operation_MouseEnter);
            this.refundLabel.MouseLeave += new System.EventHandler(this.operation_MouseLeave);
            // 
            // flowListItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.refundLabel);
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.personLabel);
            this.Controls.Add(this.moneyLabel);
            this.Controls.Add(this.trademoneyLabel);
            this.Controls.Add(this.paychannelLabel);
            this.Controls.Add(this.flownoLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.tradetimeLabel);
            this.Controls.Add(this.numLabel);
            this.Name = "flowListItemControl";
            this.Size = new System.Drawing.Size(746, 30);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label numLabel;
        public System.Windows.Forms.Label tradetimeLabel;
        public System.Windows.Forms.Label typeLabel;
        public System.Windows.Forms.Label flownoLabel;
        public System.Windows.Forms.Label paychannelLabel;
        public System.Windows.Forms.Label trademoneyLabel;
        public System.Windows.Forms.Label moneyLabel;
        public System.Windows.Forms.Label personLabel;
        public System.Windows.Forms.Label detailLabel;
        public System.Windows.Forms.Label refundLabel;
    }
}
