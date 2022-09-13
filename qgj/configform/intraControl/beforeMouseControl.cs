using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class beforeMouseControl : UserControl
    {
        Rectangle rectMouseShow;
        public beforeMouseControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void beforeMouseControl_Load(object sender, EventArgs e)
        {
            rectMouseShow = new Rectangle(0, 0, Width, Height);
        }

        private void beforeMouseControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (rectMouseShow.Contains(e.Location))
            {
                JointSetControl joinC = (JointSetControl)(Parent);
                joinC.fnAutoAreaSelect("mouse");
                Console.WriteLine("选择区域");
            }
        }

        private void beforeMouseControl_Paint(object sender, PaintEventArgs e)
        {
            StringFormat _drawFormat = new StringFormat();
            _drawFormat.Alignment = StringAlignment.Center;
            _drawFormat.LineAlignment = StringAlignment.Center;

            Font _myFont = new Font(UserClass.fontName, 9);
            if (is_set_area())
            {
                e.Graphics.DrawString("已设置", _myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectMouseShow, _drawFormat);
            }
            else
            {
                e.Graphics.DrawString("选择区域", _myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectMouseShow, _drawFormat);
            }

            PublicMethods.FrameRoundRectangle(rectMouseShow, e.Graphics, 7, Defcolor.MainGrayLineColor);
        }

        public bool is_set_area()
        {
            loadconfigClass lcc = new loadconfigClass("step_area_mouse");
            if (lcc.readfromConfig() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
