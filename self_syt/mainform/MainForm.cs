using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace self_syt.mainform
{
    public partial class MainForm : SkinMain
    {
        public mainform.MainBaseControl mainBaseC = new MainBaseControl();
        private Point mouseOffset;
        private bool isMouseDown = false;
        public MainForm()
        {
            InitializeComponent();
            Init();
            StartPosition = FormStartPosition.CenterScreen;

            #region 早期设置窗体圆角的属性
            //this.BackColor = Color.Black;
            //this.TransparencyKey = Color.Black;
            //FormBorderStyle = FormBorderStyle.None;
            #endregion

            this.Controls.Add(mainBaseC);

            BaseModel.mainFormModel = this;
            untils.UcomUntils.newFastLnk();

        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        public void CloseForm()
        {
            Application.Exit();
        }

        public void Frm_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;
            //点击窗体时，记录鼠标位置，启动移动
            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine("鼠标得左键按下的时候...");
                xOffset = -e.X;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }
        public void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                //移动的位置计算
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        public void Frm_MouseUp(object sender, MouseEventArgs e)
        {
            // 修改鼠标状态isMouseDown的值
            // 确保只有鼠标左键按下并移动时，才移动窗体
            if (e.Button == MouseButtons.Left)
            {
                //松开鼠标时，停止移动
                isMouseDown = false;
                //Top高度小于0的时候，等于0
                if (this.Top < 0)
                {
                    this.Top = 0;
                }
            }
        }

        private void Init()
        {
            BaseValue.drawFormatTitle.Alignment = StringAlignment.Center;
            BaseValue.drawFormatTitle.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatLeft.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeft.LineAlignment = StringAlignment.Near;

            BaseValue.drawFormatRight.Alignment = StringAlignment.Far;
            BaseValue.drawFormatRight.LineAlignment = StringAlignment.Near;

            BaseValue.drawFormatLeftMid.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeftMid.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatLeftMid.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeftMid.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatRightMid.Alignment = StringAlignment.Far;
            BaseValue.drawFormatRightMid.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatRightFar.Alignment = StringAlignment.Far;
            BaseValue.drawFormatRightFar.LineAlignment = StringAlignment.Far;

            BaseValue.drawFormatLeftFar.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeftFar.LineAlignment = StringAlignment.Far;

            BaseValue.drawFormatLeftFar.Alignment = StringAlignment.Far;
            BaseValue.drawFormatLeftFar.LineAlignment = StringAlignment.Center;
        }
    }
}
