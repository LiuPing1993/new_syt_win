namespace qgj
{
    partial class OtherSetControl
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
            this.lblPaipaiInfo = new System.Windows.Forms.Label();
            this.lblPaipai = new System.Windows.Forms.Label();
            this.PaipaiselectC = new qgj.selectControl();
            this.SuspendLayout();
            // 
            // lblPaipaiInfo
            // 
            this.lblPaipaiInfo.AutoSize = true;
            this.lblPaipaiInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblPaipaiInfo.Location = new System.Drawing.Point(155, 175);
            this.lblPaipaiInfo.Name = "lblPaipaiInfo";
            this.lblPaipaiInfo.Size = new System.Drawing.Size(161, 12);
            this.lblPaipaiInfo.TabIndex = 22;
            this.lblPaipaiInfo.Text = "可能需要重新启动程序后生效";
            this.lblPaipaiInfo.DoubleClick += new System.EventHandler(this.lblPaipaiInfo_DoubleClick);
            // 
            // lblPaipai
            // 
            this.lblPaipai.AutoSize = true;
            this.lblPaipai.Location = new System.Drawing.Point(61, 175);
            this.lblPaipai.Name = "lblPaipai";
            this.lblPaipai.Size = new System.Drawing.Size(77, 12);
            this.lblPaipai.TabIndex = 21;
            this.lblPaipai.Text = "使用派派小盒";
            this.lblPaipai.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PaipaiselectC_MouseUp);
            // 
            // PaipaiselectC
            // 
            this.PaipaiselectC.Location = new System.Drawing.Point(35, 174);
            this.PaipaiselectC.Name = "PaipaiselectC";
            this.PaipaiselectC.Size = new System.Drawing.Size(15, 15);
            this.PaipaiselectC.TabIndex = 20;
            this.PaipaiselectC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PaipaiselectC_MouseUp);
            // 
            // OtherSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPaipaiInfo);
            this.Controls.Add(this.lblPaipai);
            this.Controls.Add(this.PaipaiselectC);
            this.Name = "OtherSetControl";
            this.Size = new System.Drawing.Size(608, 439);
            this.Load += new System.EventHandler(this.OtherSetControl_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OtherSetControl_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPaipaiInfo;
        private System.Windows.Forms.Label lblPaipai;
        private selectControl PaipaiselectC;

    }
}
