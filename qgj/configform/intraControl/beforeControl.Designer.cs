namespace qgj
{
    partial class beforeControl
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
            this.SuspendLayout();
            // 
            // beforeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "beforeControl";
            this.Size = new System.Drawing.Size(75, 20);
            this.Load += new System.EventHandler(this.beforeControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.beforeControl_Paint);
            this.Leave += new System.EventHandler(this.beforeControl_Leave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.beforeControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
