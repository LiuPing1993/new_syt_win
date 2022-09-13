namespace qgj
{
    partial class flowtypeSelectControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(1, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 35);
            this.label3.TabIndex = 5;
            this.label3.Text = "全部";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 35);
            this.label2.TabIndex = 4;
            this.label2.Text = "门店储值";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(1, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "门店交易";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // flowtypeSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "flowtypeSelectControl";
            this.Size = new System.Drawing.Size(70, 109);
            this.Load += new System.EventHandler(this.flowtypeSelectControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
    }
}
