using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class moneyshowControl : UserControl
    {
        string sShowGatherMoney = "";
        string sShowDiscountMoney = "";
        Rectangle rectShowGatherMoney = new Rectangle(168, 0, 168, 48);

        public StringFormat drawFormatLeft = new StringFormat();
        public StringFormat drawFormatRight = new StringFormat();
        public moneyshowControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void moneyshowControl_Load(object sender, EventArgs e)
        {
            BackColor = Defcolor.MainBackColor;
        }

        public void fnSetShowGatherMoney(string _strshowmoney)
        {
            sShowGatherMoney = _strshowmoney == "" ? _strshowmoney : PublicMethods.moneyFormater(_strshowmoney);
            UserClass.orderInfoC.setMoney(sShowGatherMoney);
            UserClass.orderInfoC.setShowReceipt(sShowGatherMoney);
            Refresh();
        }
        public void fnSetShowDiscountMoney(string _strshowmoney)
        {
            sShowDiscountMoney = _strshowmoney == "" ? _strshowmoney : PublicMethods.moneyFormater(_strshowmoney);
            UserClass.orderInfoC.setShowDiscount(sShowDiscountMoney);
            Refresh();
        }
        private void moneyshowControl_Paint(object sender, PaintEventArgs e)
        {
            //if (STRshowgatermoney == "")
            //{
            //    STRshowgatermoney = "0.00";
            //}
            //Font showFont = new Font(UserClass.fontName, 12);
            //Font showmoenyFont = new Font(UserClass.fontName, 12, FontStyle.Bold);
            //string strLine = String.Format("应收 " + STRshowgatermoney);
            //SizeF sizeF = e.Graphics.MeasureString("应收 " + STRshowgatermoney, showFont);
            //SizeF sizeFdefstr = e.Graphics.MeasureString("应收 ", showFont);
            //PointF point1 = new PointF(e.ClipRectangle.Width - sizeF.Width - 5, e.ClipRectangle.Height - sizeF.Height - (e.ClipRectangle.Height - sizeF.Height) / 2);
            //PointF point2 = new PointF(e.ClipRectangle.Width - sizeF.Width + sizeFdefstr.Width - 5, e.ClipRectangle.Height - sizeF.Height - (e.ClipRectangle.Height - sizeF.Height) / 2);
            //e.Graphics.DrawString("应收 ", showFont, new SolidBrush(Defcolor.FontGrayColor), point1);
            //e.Graphics.DrawString(STRshowgatermoney, showmoenyFont, new SolidBrush(Defcolor.FontRadColor), point2);

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            string _sReceipt = UserClass.orderInfoC.getShowReceipt();
            string _sDiscount = UserClass.orderInfoC.getShowDiscount();
            if (_sReceipt == "")
            {
                _sReceipt = "0.00";
            }
            Font _showFont = new Font(UserClass.fontName, 12);
            Font _showmoenyFont = new Font(UserClass.fontName, 12, FontStyle.Bold);
            string _strLine = String.Format("应收 " + _sReceipt);
            SizeF _sizeF = e.Graphics.MeasureString("应收 " + _sReceipt, _showFont);
            SizeF _sizeFdefstr = e.Graphics.MeasureString("应收 ", _showFont);
            PointF _point1 = new PointF(e.ClipRectangle.Width - _sizeF.Width - 5, e.ClipRectangle.Height - _sizeF.Height - (e.ClipRectangle.Height - _sizeF.Height) / 2);
            PointF _point2 = new PointF(e.ClipRectangle.Width - _sizeF.Width + _sizeFdefstr.Width - 5, e.ClipRectangle.Height - _sizeF.Height - (e.ClipRectangle.Height - _sizeF.Height) / 2);
            e.Graphics.DrawString("应收 ", _showFont, new SolidBrush(Defcolor.FontGrayColor), _point1);
            e.Graphics.DrawString(_sReceipt, _showmoenyFont, new SolidBrush(Defcolor.FontRadColor), _point2);

            if (_sDiscount != "" && _sDiscount != "0.00")
            {
                _strLine = String.Format("优惠 " + _sDiscount);
                _sizeF = e.Graphics.MeasureString("优惠 " + _sDiscount, _showFont);
                _sizeFdefstr = e.Graphics.MeasureString("优惠 ", _showFont);
                _point1 = new PointF(14, e.ClipRectangle.Height - _sizeF.Height - (e.ClipRectangle.Height - _sizeF.Height) / 2);
                _point2 = new PointF(50, e.ClipRectangle.Height - _sizeF.Height - (e.ClipRectangle.Height - _sizeF.Height) / 2);
                e.Graphics.DrawString("优惠 ", _showFont, new SolidBrush(Defcolor.FontGrayColor), _point1);
                e.Graphics.DrawString(_sDiscount, _showmoenyFont, new SolidBrush(Defcolor.FontBlueColor), _point2);
            }
            base.OnPaint(e);
        }
    }
}
