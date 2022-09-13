namespace qgj
{
    partial class PicDetailSetControl
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
            this.lblInvInfo = new System.Windows.Forms.Label();
            this.lblTIP2 = new System.Windows.Forms.Label();
            this.lblTIP1 = new System.Windows.Forms.Label();
            this.lblZoom = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.lblZoomInfo = new System.Windows.Forms.Label();
            this.hScrollZoom = new System.Windows.Forms.HScrollBar();
            this.lblValueInfo = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.hScrollValue = new System.Windows.Forms.HScrollBar();
            this.PICBwork = new System.Windows.Forms.PictureBox();
            this.lblResultInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PICBwork)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInvInfo
            // 
            this.lblInvInfo.AutoSize = true;
            this.lblInvInfo.Location = new System.Drawing.Point(11, 233);
            this.lblInvInfo.Name = "lblInvInfo";
            this.lblInvInfo.Size = new System.Drawing.Size(65, 12);
            this.lblInvInfo.TabIndex = 46;
            this.lblInvInfo.Text = "白字启用：";
            // 
            // lblTIP2
            // 
            this.lblTIP2.AutoSize = true;
            this.lblTIP2.ForeColor = System.Drawing.Color.Gray;
            this.lblTIP2.Location = new System.Drawing.Point(11, 259);
            this.lblTIP2.Name = "lblTIP2";
            this.lblTIP2.Size = new System.Drawing.Size(233, 12);
            this.lblTIP2.TabIndex = 45;
            this.lblTIP2.Text = "如金额数字是【白色的字】，请启用该选项";
            // 
            // lblTIP1
            // 
            this.lblTIP1.AutoSize = true;
            this.lblTIP1.ForeColor = System.Drawing.Color.Gray;
            this.lblTIP1.Location = new System.Drawing.Point(11, 197);
            this.lblTIP1.Name = "lblTIP1";
            this.lblTIP1.Size = new System.Drawing.Size(305, 12);
            this.lblTIP1.TabIndex = 44;
            this.lblTIP1.Text = "默认放大倍数为1，如无法识别，可尝试修改倍数，如1.1";
            // 
            // lblZoom
            // 
            this.lblZoom.Location = new System.Drawing.Point(401, 168);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(23, 12);
            this.lblZoom.TabIndex = 43;
            this.lblZoom.Text = "1";
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(401, 128);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(23, 12);
            this.lblValue.TabIndex = 42;
            this.lblValue.Text = "120";
            // 
            // lblZoomInfo
            // 
            this.lblZoomInfo.AutoSize = true;
            this.lblZoomInfo.Location = new System.Drawing.Point(11, 168);
            this.lblZoomInfo.Name = "lblZoomInfo";
            this.lblZoomInfo.Size = new System.Drawing.Size(41, 12);
            this.lblZoomInfo.TabIndex = 39;
            this.lblZoomInfo.Text = "倍率：";
            // 
            // hScrollZoom
            // 
            this.hScrollZoom.LargeChange = 1;
            this.hScrollZoom.Location = new System.Drawing.Point(56, 166);
            this.hScrollZoom.Maximum = 15;
            this.hScrollZoom.Minimum = 5;
            this.hScrollZoom.Name = "hScrollZoom";
            this.hScrollZoom.Size = new System.Drawing.Size(326, 17);
            this.hScrollZoom.TabIndex = 38;
            this.hScrollZoom.Value = 5;
            this.hScrollZoom.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollZoom_Scroll);
            // 
            // lblValueInfo
            // 
            this.lblValueInfo.AutoSize = true;
            this.lblValueInfo.Location = new System.Drawing.Point(11, 128);
            this.lblValueInfo.Name = "lblValueInfo";
            this.lblValueInfo.Size = new System.Drawing.Size(41, 12);
            this.lblValueInfo.TabIndex = 37;
            this.lblValueInfo.Text = "阈值：";
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("宋体", 12F);
            this.lblResult.ForeColor = System.Drawing.Color.DimGray;
            this.lblResult.Location = new System.Drawing.Point(303, 45);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(162, 16);
            this.lblResult.TabIndex = 36;
            this.lblResult.Text = "识别内容";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hScrollValue
            // 
            this.hScrollValue.LargeChange = 5;
            this.hScrollValue.Location = new System.Drawing.Point(55, 126);
            this.hScrollValue.Maximum = 259;
            this.hScrollValue.Minimum = 5;
            this.hScrollValue.Name = "hScrollValue";
            this.hScrollValue.Size = new System.Drawing.Size(326, 19);
            this.hScrollValue.SmallChange = 5;
            this.hScrollValue.TabIndex = 35;
            this.hScrollValue.Value = 5;
            this.hScrollValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollValue_Scroll);
            // 
            // PICBwork
            // 
            this.PICBwork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PICBwork.Location = new System.Drawing.Point(13, 13);
            this.PICBwork.Name = "PICBwork";
            this.PICBwork.Size = new System.Drawing.Size(215, 95);
            this.PICBwork.TabIndex = 34;
            this.PICBwork.TabStop = false;
            // 
            // lblResultInfo
            // 
            this.lblResultInfo.AutoSize = true;
            this.lblResultInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblResultInfo.Location = new System.Drawing.Point(251, 49);
            this.lblResultInfo.Name = "lblResultInfo";
            this.lblResultInfo.Size = new System.Drawing.Size(65, 12);
            this.lblResultInfo.TabIndex = 47;
            this.lblResultInfo.Text = "识别结果：";
            // 
            // PicDetailSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblResultInfo);
            this.Controls.Add(this.lblInvInfo);
            this.Controls.Add(this.lblTIP2);
            this.Controls.Add(this.lblTIP1);
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblZoomInfo);
            this.Controls.Add(this.hScrollZoom);
            this.Controls.Add(this.lblValueInfo);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.hScrollValue);
            this.Controls.Add(this.PICBwork);
            this.Name = "PicDetailSetControl";
            this.Size = new System.Drawing.Size(515, 300);
            this.Load += new System.EventHandler(this.PicDetailSetControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PicDetailSetControl_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.PICBwork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInvInfo;
        private System.Windows.Forms.Label lblTIP2;
        private System.Windows.Forms.Label lblTIP1;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblZoomInfo;
        private System.Windows.Forms.HScrollBar hScrollZoom;
        private System.Windows.Forms.Label lblValueInfo;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.HScrollBar hScrollValue;
        private System.Windows.Forms.PictureBox PICBwork;
        private System.Windows.Forms.Label lblResultInfo;
    }
}
