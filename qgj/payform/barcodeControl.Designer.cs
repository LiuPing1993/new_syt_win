namespace qgj
{
    partial class barcodeControl
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
            this.paystatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // paystatusLabel
            // 
            this.paystatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.paystatusLabel.Location = new System.Drawing.Point(3, 180);
            this.paystatusLabel.Name = "paystatusLabel";
            this.paystatusLabel.Size = new System.Drawing.Size(504, 23);
            this.paystatusLabel.TabIndex = 0;
            this.paystatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // barcodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paystatusLabel);
            this.Name = "barcodeControl";
            this.Size = new System.Drawing.Size(510, 330);
            this.Load += new System.EventHandler(this.barcodeControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.barcodeControl_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label paystatusLabel;
    }
}
