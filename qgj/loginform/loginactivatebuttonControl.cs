using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class loginactivatebuttonControl : UserControl
    {
        string sInfo = "";
        Color colorLine = Defcolor.ButtonLineRadColor;
        Color colorFill = Defcolor.MainRadColor;
        public loginactivatebuttonControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sInfo = "登        录";
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }
        public loginactivatebuttonControl(string insertinfo)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            sInfo = insertinfo;
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }

        private void loginactivateControl_Paint(object sender, PaintEventArgs e)
        {
            Font Font = new Font(UserClass.fontName, 14);
            SizeF sizeF = e.Graphics.MeasureString(sInfo, Font);
            string strLine = String.Format(sInfo);
            PointF strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2);
            PublicMethods.FillRoundRectangle(e.ClipRectangle, e.Graphics, 10, colorFill);
            PublicMethods.FrameRoundRectangle(e.ClipRectangle, e.Graphics, 10, colorLine);
            e.Graphics.DrawString(strLine, Font, new SolidBrush(Color.White), strPoint);

        }

        private void loginactivateControl_MouseEnter(object sender, EventArgs e)
        {
            colorFill = Defcolor.TopButtonRadColorMouseIn;
            Refresh();
        }

        private void loginactivateControl_MouseDown(object sender, MouseEventArgs e)
        {
            colorFill = Defcolor.TopButtonRadColorMouseDown;
            Refresh();
        }

        private void loginactivateControl_MouseUp(object sender, MouseEventArgs e)
        {
            colorFill = Defcolor.MainRadColor;
            Refresh();
        }

        private void loginactivateControl_MouseLeave(object sender, EventArgs e)
        {
            colorFill = Defcolor.MainRadColor;
            Refresh();
        }
    }
}
