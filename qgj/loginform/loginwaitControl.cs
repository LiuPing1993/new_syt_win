using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class loginwaitControl : UserControl
    {
        int iTemp = 0;
        string sTemp = "";
        public string sInfo = "登陆中";
        public loginwaitControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }

        private void loginwaitControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid
            );

            e.Graphics.FillRectangle(new SolidBrush(Defcolor.MainRadColor), new Rectangle(0, 0, e.ClipRectangle.Width, 45));
            e.Graphics.DrawLine(new Pen(Defcolor.MainBackColor, 2), new Point(466, 10), new Point(486, 30));
            e.Graphics.DrawLine(new Pen(Defcolor.MainBackColor, 2), new Point(466, 30), new Point(486, 10));

            Font _myFont = new Font(UserClass.fontName, 12);
            SizeF _sizeF = e.Graphics.MeasureString(sTemp, _myFont);
            string _strLine = String.Format(sTemp);
            PointF _strPoint = new PointF(200, 175);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _strPoint);
            e.Graphics.DrawImage(Properties.Resources.qxtitle, 10, 8, 105, 27);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (iTemp)
            {
                case 0: sTemp = sInfo + ""; iTemp = 1; break;
                case 1: sTemp = sInfo + "."; iTemp = 2; break;
                case 2: sTemp = sInfo +".."; iTemp = 3; break;
                default: sTemp = sInfo +"..."; iTemp = 0; break;
            }
            Refresh();
        }

        private void loginwaitControl_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
