namespace qgj
{
    partial class flowdetailitemControl
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.contantLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(17, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(35, 12);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "title";
            // 
            // contantLabel
            // 
            this.contantLabel.AutoSize = true;
            this.contantLabel.Location = new System.Drawing.Point(185, 9);
            this.contantLabel.Name = "contantLabel";
            this.contantLabel.Size = new System.Drawing.Size(47, 12);
            this.contantLabel.TabIndex = 1;
            this.contantLabel.Text = "contant";
            // 
            // flowdetailitemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.contantLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "flowdetailitemControl";
            this.Size = new System.Drawing.Size(690, 30);
            this.Load += new System.EventHandler(this.flowdetailitemControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.flowdetailitemControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label contantLabel;

    }
}
