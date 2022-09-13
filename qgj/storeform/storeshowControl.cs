using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class storeshowControl : UserControl
    {
        string sShowStoreMoney = "";
        public storeshowControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void storeshowControl_Load(object sender, EventArgs e)
        {
            BackColor = Defcolor.MainBackColor;
        }
        public void fnSetShowStoreMoney(string _strshowmoney)
        {
            sShowStoreMoney = _strshowmoney == "" ? _strshowmoney : PublicMethods.moneyFormater(_strshowmoney);
            UserClass.storeInfoC.setstoremoney(sShowStoreMoney);
            Refresh();
        }
        private void storeshowControl_Paint(object sender, PaintEventArgs e)
        {
            string _sReceipt = UserClass.storeInfoC.getstoremoney();
            if (_sReceipt == "")
            {
                _sReceipt = "0.00";
            }
            Font showFont = new Font(UserClass.fontName, 12);
            Font showmoenyFont = new Font(UserClass.fontName, 12, FontStyle.Bold);
            string strLine = String.Format("应收 " + _sReceipt);
            SizeF sizeF = e.Graphics.MeasureString("应收 " + _sReceipt, showFont);
            SizeF sizeFdefstr = e.Graphics.MeasureString("应收 ", showFont);
            PointF point1 = new PointF(e.ClipRectangle.Width - sizeF.Width - 5, e.ClipRectangle.Height - sizeF.Height - (e.ClipRectangle.Height - sizeF.Height) / 2);
            PointF point2 = new PointF(e.ClipRectangle.Width - sizeF.Width + sizeFdefstr.Width - 5, e.ClipRectangle.Height - sizeF.Height - (e.ClipRectangle.Height - sizeF.Height) / 2);
            e.Graphics.DrawString("应收 ", showFont, new SolidBrush(Defcolor.FontGrayColor), point1);
            e.Graphics.DrawString(_sReceipt, showmoenyFont, new SolidBrush(Defcolor.FontRadColor), point2);
        }
    }
}
