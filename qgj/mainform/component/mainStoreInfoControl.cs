using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class mainStoreInfoControl : UserControl
    {
        bool IsSelect = false;
        //bool IsIn = false;

        public string sStore = "";
        string sTitle = "储值余额";
        public mainStoreInfoControl(string _store)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            if (_store == "")
            {
                _store = "0.00";
            }
            sStore = _store;

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;
        }

        public void setSelectStatus(bool _isselect)
        {
            IsSelect = _isselect;
        }
        public Point getLocation()
        {
            return PointToScreen(Location);
        }
        private void mainStoreInfoControl_MouseUp(object sender, MouseEventArgs e)
        {
            //return;
            //IsSelect = IsSelect ? false : true;
            //Refresh();
        }

        private void mainStoreInfoControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            PublicMethods.FillRoundRectangle(e.ClipRectangle, e.Graphics, 10, Color.White);
            Font _myFont = new Font(UserClass.fontName, 10);
            string _strLine = String.Format(sTitle);
            PointF _strPoint = new PointF(10, 10);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _strPoint);

            _myFont = new Font(UserClass.fontName, 16);
            _strLine = String.Format(sStore);
            _strPoint = new PointF(10, 45);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.Gray), _strPoint);

            //if(IsIn)
            //{
            //    float[] dashValues = { 2, 3 };
            //    Pen blackPen = new Pen(Defcolor.MainRadColor, 2);
            //    blackPen.DashPattern = dashValues;
            //    e.Graphics.DrawRectangle(blackPen, e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            //}
            if(IsSelect)
            {
                Pen _myPen = new Pen(new SolidBrush(Defcolor.MainRadColor), 2);
                e.Graphics.DrawEllipse(_myPen, 124, 50, 20, 20);
                e.Graphics.DrawLine(_myPen, new Point(127, 60), new Point(132, 65));
                e.Graphics.DrawLine(_myPen, new Point(140, 55), new Point(132, 65));
            }
        }

        private void mainStoreInfoControl_MouseEnter(object sender, EventArgs e)
        {
            //IsIn = true;
            //Refresh();
        }

        private void mainStoreInfoControl_MouseLeave(object sender, EventArgs e)
        {
            //IsIn = false;
            //Refresh();
        }
    }
}
