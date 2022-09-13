using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class couponUseSuccessForm : Form
    {

        int iResult = 0;
        string sContent;
        confirmcancelControl confirmC = new confirmcancelControl("确认");
        PictureBox pbxInfo = new PictureBox();
        public couponUseSuccessForm(int result,string content)
        {
            InitializeComponent();
            iResult = result;
            sContent = content;
        }

        private void couponUseSuccessForm_Load(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(245, 245, 245);

            confirmC.Location = new Point((ClientRectangle.Width - confirmC.Width) / 2, 275);
            confirmC.MouseUp += new MouseEventHandler(confirm_MouseUp);
            Controls.Add(confirmC);

            pbxInfo.SetBounds(220, 70, 70, 70);
            Controls.Add(pbxInfo);

            if (iResult == 0)
            {
                pbxInfo.Image = Properties.Resources.success;
            }
            else
            {
                pbxInfo.Image = Properties.Resources.fail;
            }

        }
        private void confirm_MouseUp(object sender, MouseEventArgs e)
        {
            this.Dispose();
        }
        private void couponUseSuccessForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
               Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid
           );

            Font myFont = new Font(UserClass.fontName, 12);
            string strLine = String.Format("核销结果");
            PointF strPoint = new PointF(20, 20);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            if (iResult == 0)
            {

                myFont = new Font(UserClass.fontName, 10);
                strLine = String.Format("核销成功");
                SizeF sizeF = e.Graphics.MeasureString(strLine, myFont);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 150);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);

            }
            else
            {
                myFont = new Font(UserClass.fontName, 12);
                strLine = String.Format("核销失败");
                SizeF sizeF = e.Graphics.MeasureString(strLine, myFont);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 150);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);

                myFont = new Font(UserClass.fontName, 10);
                strLine = String.Format("原因 " + sContent);
                sizeF = e.Graphics.MeasureString(strLine, myFont);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 192);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);
            }

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                confirm_MouseUp(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
