using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class CouponUseControl : UserControl
    {
        string sInfo = "";
        Color colorLine = Defcolor.ButtonLineRadColor;
        Color colorFill = Defcolor.MainRadColor;
        public CouponUseControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sInfo = "直接核销";
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }

        private void CouponUseControl_Paint(object sender, PaintEventArgs e)
        {
            Font Font = new Font(UserClass.fontName, 10);
            SizeF sizeF = e.Graphics.MeasureString(sInfo, Font);
            string strLine = String.Format(sInfo);
            PointF strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2);
            PublicMethods.FillRoundRectangle(e.ClipRectangle, e.Graphics, 10, colorFill);
            PublicMethods.FrameRoundRectangle(e.ClipRectangle, e.Graphics, 10, colorLine);
            e.Graphics.DrawString(strLine, Font, new SolidBrush(Color.White), strPoint);
        }

        private void CouponUseControl_MouseDown(object sender, MouseEventArgs e)
        {
            colorFill = Defcolor.TopButtonRadColorMouseDown;
            Refresh();
        }

        private void CouponUseControl_MouseEnter(object sender, EventArgs e)
        {
            colorFill = Defcolor.TopButtonRadColorMouseIn;
            Refresh();
        }

        private void CouponUseControl_MouseLeave(object sender, EventArgs e)
        {
            colorFill = Defcolor.MainRadColor;
            Refresh();
        }

        private void CouponUseControl_MouseUp(object sender, MouseEventArgs e)
        {
            colorFill = Defcolor.MainRadColor;
            Refresh();
        }
    }
}
