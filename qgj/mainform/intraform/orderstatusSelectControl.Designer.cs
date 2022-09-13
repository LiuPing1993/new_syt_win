namespace qgj
{
    partial class orderstatusSelectControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "未付款";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "已付款";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 35);
            this.label3.TabIndex = 2;
            this.label3.Text = "已退款";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 35);
            this.label4.TabIndex = 3;
            this.label4.Text = "已部分退款";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label4.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 35);
            this.label5.TabIndex = 4;
            this.label5.Text = "全部";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label5.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // orderstatusSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "orderstatusSelectControl";
            this.Size = new System.Drawing.Size(67, 175);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
    }
}
