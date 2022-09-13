namespace qgj
{
    partial class orderListItemControl
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
            this.ordertimeLabel = new System.Windows.Forms.Label();
            this.ordernumLabel = new System.Windows.Forms.Label();
            this.paytypeLabel = new System.Windows.Forms.Label();
            this.ordermoneyLabel = new System.Windows.Forms.Label();
            this.orderreceiptLabel = new System.Windows.Forms.Label();
            this.orderstatusLabel = new System.Windows.Forms.Label();
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
            this.numLabel.TabIndex = 3;
            this.numLabel.Text = "序号";
            this.numLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.numLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // ordertimeLabel
            // 
            this.ordertimeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ordertimeLabel.Location = new System.Drawing.Point(36, 0);
            this.ordertimeLabel.Name = "ordertimeLabel";
            this.ordertimeLabel.Size = new System.Drawing.Size(130, 30);
            this.ordertimeLabel.TabIndex = 10;
            this.ordertimeLabel.Text = "2017-02-28 10:20:20";
            this.ordertimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ordertimeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // ordernumLabel
            // 
            this.ordernumLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ordernumLabel.Location = new System.Drawing.Point(166, 0);
            this.ordernumLabel.Name = "ordernumLabel";
            this.ordernumLabel.Size = new System.Drawing.Size(140, 30);
            this.ordernumLabel.TabIndex = 11;
            this.ordernumLabel.Text = "订单号";
            this.ordernumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ordernumLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            this.ordernumLabel.DoubleClick += new System.EventHandler(this.ordernumLabel_DoubleClick);
            // 
            // paytypeLabel
            // 
            this.paytypeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.paytypeLabel.Location = new System.Drawing.Point(306, 0);
            this.paytypeLabel.Name = "paytypeLabel";
            this.paytypeLabel.Size = new System.Drawing.Size(70, 30);
            this.paytypeLabel.TabIndex = 12;
            this.paytypeLabel.Text = "类型";
            this.paytypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.paytypeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // ordermoneyLabel
            // 
            this.ordermoneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ordermoneyLabel.Location = new System.Drawing.Point(376, 0);
            this.ordermoneyLabel.Name = "ordermoneyLabel";
            this.ordermoneyLabel.Size = new System.Drawing.Size(75, 30);
            this.ordermoneyLabel.TabIndex = 13;
            this.ordermoneyLabel.Text = "订单金额";
            this.ordermoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ordermoneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // orderreceiptLabel
            // 
            this.orderreceiptLabel.BackColor = System.Drawing.SystemColors.Control;
            this.orderreceiptLabel.Location = new System.Drawing.Point(451, 0);
            this.orderreceiptLabel.Name = "orderreceiptLabel";
            this.orderreceiptLabel.Size = new System.Drawing.Size(75, 30);
            this.orderreceiptLabel.TabIndex = 14;
            this.orderreceiptLabel.Text = "实收金额";
            this.orderreceiptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.orderreceiptLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // orderstatusLabel
            // 
            this.orderstatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.orderstatusLabel.Location = new System.Drawing.Point(526, 0);
            this.orderstatusLabel.Name = "orderstatusLabel";
            this.orderstatusLabel.Size = new System.Drawing.Size(66, 30);
            this.orderstatusLabel.TabIndex = 15;
            this.orderstatusLabel.Text = "状态";
            this.orderstatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.orderstatusLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.BackColor = System.Drawing.SystemColors.Control;
            this.personLabel.Location = new System.Drawing.Point(592, 0);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(83, 30);
            this.personLabel.TabIndex = 16;
            this.personLabel.Text = "操作员";
            this.personLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.personLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // detailLabel
            // 
            this.detailLabel.BackColor = System.Drawing.SystemColors.Control;
            this.detailLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.detailLabel.Location = new System.Drawing.Point(710, 0);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(35, 30);
            this.detailLabel.TabIndex = 18;
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
            this.refundLabel.Location = new System.Drawing.Point(675, 0);
            this.refundLabel.Name = "refundLabel";
            this.refundLabel.Size = new System.Drawing.Size(35, 30);
            this.refundLabel.TabIndex = 17;
            this.refundLabel.Text = "退款";
            this.refundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.refundLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.refundLabel_Paint);
            this.refundLabel.MouseEnter += new System.EventHandler(this.operation_MouseEnter);
            this.refundLabel.MouseLeave += new System.EventHandler(this.operation_MouseLeave);
            // 
            // orderListItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.refundLabel);
            this.Controls.Add(this.personLabel);
            this.Controls.Add(this.orderstatusLabel);
            this.Controls.Add(this.orderreceiptLabel);
            this.Controls.Add(this.ordermoneyLabel);
            this.Controls.Add(this.paytypeLabel);
            this.Controls.Add(this.ordernumLabel);
            this.Controls.Add(this.ordertimeLabel);
            this.Controls.Add(this.numLabel);
            this.Name = "orderListItemControl";
            this.Size = new System.Drawing.Size(745, 30);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label numLabel;
        public System.Windows.Forms.Label ordertimeLabel;
        public System.Windows.Forms.Label ordernumLabel;
        public System.Windows.Forms.Label paytypeLabel;
        public System.Windows.Forms.Label ordermoneyLabel;
        public System.Windows.Forms.Label orderreceiptLabel;
        public System.Windows.Forms.Label orderstatusLabel;
        public System.Windows.Forms.Label personLabel;
        public System.Windows.Forms.Label detailLabel;
        public System.Windows.Forms.Label refundLabel;
    }
}
