namespace qgj
{
    partial class storeMemberInfoControl
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
            this.avatarPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.avatarPic)).BeginInit();
            this.SuspendLayout();
            // 
            // avatarPic
            // 
            this.avatarPic.Location = new System.Drawing.Point(5, 7);
            this.avatarPic.Name = "avatarPic";
            this.avatarPic.Size = new System.Drawing.Size(55, 55);
            this.avatarPic.TabIndex = 0;
            this.avatarPic.TabStop = false;
            // 
            // storeMemberInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.avatarPic);
            this.Name = "storeMemberInfoControl";
            this.Size = new System.Drawing.Size(320, 68);
            this.Load += new System.EventHandler(this.storeMemberInfoControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.storeMemberInfoControl_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.avatarPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox avatarPic;
    }
}
