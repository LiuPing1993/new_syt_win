namespace qgj
{
    partial class orderlistdetailControl
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
            this.ordernumLabel = new System.Windows.Forms.Label();
            this.paytypeLabel = new System.Windows.Forms.Label();
            this.ordermoneyLabel = new System.Windows.Forms.Label();
            this.detailmoneyLabel = new System.Windows.Forms.Label();
            this.orderstatusLabel = new System.Windows.Forms.Label();
            this.personLabel = new System.Windows.Forms.Label();
            this.ordertimeLabel = new System.Windows.Forms.Label();
            this.refundLabel = new System.Windows.Forms.Label();
            this.detailLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // numLabel
            // 
            this.numLabel.BackColor = System.Drawing.SystemColors.Control;
            this.numLabel.Location = new System.Drawing.Point(0, 0);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(36, 30);
            this.numLabel.TabIndex = 2;
            this.numLabel.Text = "序号";
            this.numLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.numLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // ordernumLabel
            // 
            this.ordernumLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ordernumLabel.Location = new System.Drawing.Point(36, 0);
            this.ordernumLabel.Name = "ordernumLabel";
            this.ordernumLabel.Size = new System.Drawing.Size(140, 30);
            this.ordernumLabel.TabIndex = 3;
            this.ordernumLabel.Text = "订单号";
            this.ordernumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ordernumLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            this.ordernumLabel.DoubleClick += new System.EventHandler(this.ordernumLabel_DoubleClick);
            // 
            // paytypeLabel
            // 
            this.paytypeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.paytypeLabel.Location = new System.Drawing.Point(176, 0);
            this.paytypeLabel.Name = "paytypeLabel";
            this.paytypeLabel.Size = new System.Drawing.Size(70, 30);
            this.paytypeLabel.TabIndex = 4;
            this.paytypeLabel.Text = "类型";
            this.paytypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.paytypeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // ordermoneyLabel
            // 
            this.ordermoneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ordermoneyLabel.Location = new System.Drawing.Point(246, 0);
            this.ordermoneyLabel.Name = "ordermoneyLabel";
            this.ordermoneyLabel.Size = new System.Drawing.Size(75, 30);
            this.ordermoneyLabel.TabIndex = 5;
            this.ordermoneyLabel.Text = "订单金额";
            this.ordermoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ordermoneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // detailmoneyLabel
            // 
            this.detailmoneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.detailmoneyLabel.Location = new System.Drawing.Point(321, 0);
            this.detailmoneyLabel.Name = "detailmoneyLabel";
            this.detailmoneyLabel.Size = new System.Drawing.Size(73, 30);
            this.detailmoneyLabel.TabIndex = 6;
            this.detailmoneyLabel.Text = "流水";
            this.detailmoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.detailmoneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // orderstatusLabel
            // 
            this.orderstatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.orderstatusLabel.Location = new System.Drawing.Point(394, 0);
            this.orderstatusLabel.Name = "orderstatusLabel";
            this.orderstatusLabel.Size = new System.Drawing.Size(66, 30);
            this.orderstatusLabel.TabIndex = 7;
            this.orderstatusLabel.Text = "状态";
            this.orderstatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.orderstatusLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.BackColor = System.Drawing.SystemColors.Control;
            this.personLabel.Location = new System.Drawing.Point(460, 0);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(83, 30);
            this.personLabel.TabIndex = 8;
            this.personLabel.Text = "操作员";
            this.personLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.personLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // ordertimeLabel
            // 
            this.ordertimeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ordertimeLabel.Location = new System.Drawing.Point(543, 0);
            this.ordertimeLabel.Name = "ordertimeLabel";
            this.ordertimeLabel.Size = new System.Drawing.Size(130, 30);
            this.ordertimeLabel.TabIndex = 9;
            this.ordertimeLabel.Text = "2017-02-28 10:20:20";
            this.ordertimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ordertimeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // refundLabel
            // 
            this.refundLabel.BackColor = System.Drawing.SystemColors.Control;
            this.refundLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.refundLabel.Location = new System.Drawing.Point(673, 0);
            this.refundLabel.Name = "refundLabel";
            this.refundLabel.Size = new System.Drawing.Size(35, 30);
            this.refundLabel.TabIndex = 10;
            this.refundLabel.Text = "退款";
            this.refundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.refundLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.refundLabel_Paint);
            this.refundLabel.MouseEnter += new System.EventHandler(this.operation_MouseEnter);
            this.refundLabel.MouseLeave += new System.EventHandler(this.operation_MouseLeave);
            // 
            // detailLabel
            // 
            this.detailLabel.BackColor = System.Drawing.SystemColors.Control;
            this.detailLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.detailLabel.Location = new System.Drawing.Point(708, 0);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(35, 30);
            this.detailLabel.TabIndex = 11;
            this.detailLabel.Text = "详情";
            this.detailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.detailLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.detailLabel_Paint);
            this.detailLabel.MouseEnter += new System.EventHandler(this.operation_MouseEnter);
            this.detailLabel.MouseLeave += new System.EventHandler(this.operation_MouseLeave);
            // 
            // orderlistdetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.refundLabel);
            this.Controls.Add(this.ordertimeLabel);
            this.Controls.Add(this.personLabel);
            this.Controls.Add(this.orderstatusLabel);
            this.Controls.Add(this.detailmoneyLabel);
            this.Controls.Add(this.ordermoneyLabel);
            this.Controls.Add(this.paytypeLabel);
            this.Controls.Add(this.ordernumLabel);
            this.Controls.Add(this.numLabel);
            this.Name = "orderlistdetailControl";
            this.Size = new System.Drawing.Size(743, 30);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label numLabel;
        public System.Windows.Forms.Label ordernumLabel;
        public System.Windows.Forms.Label paytypeLabel;
        public System.Windows.Forms.Label ordermoneyLabel;
        public System.Windows.Forms.Label detailmoneyLabel;
        public System.Windows.Forms.Label orderstatusLabel;
        public System.Windows.Forms.Label personLabel;
        public System.Windows.Forms.Label ordertimeLabel;
        public System.Windows.Forms.Label refundLabel;
        public System.Windows.Forms.Label detailLabel;
    }
}
