namespace qgj
{
    partial class storelistdetailControl
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
            this.membernumLabel = new System.Windows.Forms.Label();
            this.membernameLabel = new System.Windows.Forms.Label();
            this.storetypeLabel = new System.Windows.Forms.Label();
            this.storemoneyLabel = new System.Windows.Forms.Label();
            this.paytypeLabel = new System.Windows.Forms.Label();
            this.personLabel = new System.Windows.Forms.Label();
            this.storetimeLabel = new System.Windows.Forms.Label();
            this.operationLabel = new System.Windows.Forms.Label();
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
            // membernumLabel
            // 
            this.membernumLabel.BackColor = System.Drawing.SystemColors.Control;
            this.membernumLabel.Location = new System.Drawing.Point(36, 0);
            this.membernumLabel.Name = "membernumLabel";
            this.membernumLabel.Size = new System.Drawing.Size(100, 30);
            this.membernumLabel.TabIndex = 4;
            this.membernumLabel.Text = "会员账号";
            this.membernumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.membernumLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            this.membernumLabel.DoubleClick += new System.EventHandler(this.membernumLabel_DoubleClick);
            // 
            // membernameLabel
            // 
            this.membernameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.membernameLabel.Location = new System.Drawing.Point(136, 0);
            this.membernameLabel.Name = "membernameLabel";
            this.membernameLabel.Size = new System.Drawing.Size(78, 30);
            this.membernameLabel.TabIndex = 5;
            this.membernameLabel.Text = "姓名";
            this.membernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.membernameLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // storetypeLabel
            // 
            this.storetypeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.storetypeLabel.Location = new System.Drawing.Point(214, 0);
            this.storetypeLabel.Name = "storetypeLabel";
            this.storetypeLabel.Size = new System.Drawing.Size(130, 30);
            this.storetypeLabel.TabIndex = 6;
            this.storetypeLabel.Text = "储值活动";
            this.storetypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.storetypeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // storemoneyLabel
            // 
            this.storemoneyLabel.BackColor = System.Drawing.SystemColors.Control;
            this.storemoneyLabel.Location = new System.Drawing.Point(344, 0);
            this.storemoneyLabel.Name = "storemoneyLabel";
            this.storemoneyLabel.Size = new System.Drawing.Size(88, 30);
            this.storemoneyLabel.TabIndex = 7;
            this.storemoneyLabel.Text = "实收金额";
            this.storemoneyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.storemoneyLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // paytypeLabel
            // 
            this.paytypeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.paytypeLabel.Location = new System.Drawing.Point(432, 0);
            this.paytypeLabel.Name = "paytypeLabel";
            this.paytypeLabel.Size = new System.Drawing.Size(66, 30);
            this.paytypeLabel.TabIndex = 8;
            this.paytypeLabel.Text = "类型";
            this.paytypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.paytypeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // personLabel
            // 
            this.personLabel.BackColor = System.Drawing.SystemColors.Control;
            this.personLabel.Location = new System.Drawing.Point(498, 0);
            this.personLabel.Name = "personLabel";
            this.personLabel.Size = new System.Drawing.Size(76, 30);
            this.personLabel.TabIndex = 9;
            this.personLabel.Text = "操作员";
            this.personLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.personLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // storetimeLabel
            // 
            this.storetimeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.storetimeLabel.Location = new System.Drawing.Point(574, 0);
            this.storetimeLabel.Name = "storetimeLabel";
            this.storetimeLabel.Size = new System.Drawing.Size(130, 30);
            this.storetimeLabel.TabIndex = 10;
            this.storetimeLabel.Text = "时间";
            this.storetimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.storetimeLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.numLabel_Paint);
            // 
            // operationLabel
            // 
            this.operationLabel.BackColor = System.Drawing.SystemColors.Control;
            this.operationLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.operationLabel.Location = new System.Drawing.Point(704, 0);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(55, 30);
            this.operationLabel.TabIndex = 11;
            this.operationLabel.Text = "操作";
            this.operationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.operationLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.detailLabel_Paint);
            this.operationLabel.MouseEnter += new System.EventHandler(this.operation_MouseEnter);
            this.operationLabel.MouseLeave += new System.EventHandler(this.operation_MouseLeave);
            // 
            // storelistdetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.operationLabel);
            this.Controls.Add(this.storetimeLabel);
            this.Controls.Add(this.personLabel);
            this.Controls.Add(this.paytypeLabel);
            this.Controls.Add(this.storemoneyLabel);
            this.Controls.Add(this.storetypeLabel);
            this.Controls.Add(this.membernameLabel);
            this.Controls.Add(this.membernumLabel);
            this.Controls.Add(this.numLabel);
            this.Name = "storelistdetailControl";
            this.Size = new System.Drawing.Size(760, 30);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label numLabel;
        public System.Windows.Forms.Label membernumLabel;
        public System.Windows.Forms.Label membernameLabel;
        public System.Windows.Forms.Label storetypeLabel;
        public System.Windows.Forms.Label storemoneyLabel;
        public System.Windows.Forms.Label paytypeLabel;
        public System.Windows.Forms.Label personLabel;
        public System.Windows.Forms.Label storetimeLabel;
        public System.Windows.Forms.Label operationLabel;
    }
}
