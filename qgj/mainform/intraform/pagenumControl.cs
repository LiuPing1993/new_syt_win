using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class pagenumControl : UserControl
    {

        Rectangle rectUpPage = new Rectangle(5, 5, 30, 30);
        Rectangle rectPageNumShow = new Rectangle(36, 5, 60, 30);
        Rectangle rectDownPage = new Rectangle(92, 5, 30, 30);
        Rectangle rectPageNumInput = new Rectangle(125, 5, 60, 30);
        Rectangle rectPageButton = new Rectangle(188, 5, 60, 30);

        TextBox tbxPagNum = new TextBox();

        int iAllPageNum = 1;
        int iNowPageNum = 1;
        Color colorAllowClick = Defcolor.FontLiteGrayColor;
        Color colorNotAllowClick = Defcolor.MainGrayLineColor;
        Point[] pointUP = new Point[3];
        Point[] pointDown = new Point[3];

        listType listtype = listType.orderlist;
        public pagenumControl(listType _type = listType.orderlist)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            listtype = _type;

            pointUP[0] = new Point(17, 20);
            pointUP[1] = new Point(25, 13);
            pointUP[2] = new Point(25, 27);

            pointDown[0] = new Point(102, 13);
            pointDown[1] = new Point(102, 27);
            pointDown[2] = new Point(110, 20);

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;
            tbxPagNum.BackColor = Defcolor.MainBackColor;
            tbxPagNum.ForeColor = Defcolor.FontGrayColor;
            tbxPagNum.ImeMode = System.Windows.Forms.ImeMode.Off;
            tbxPagNum.BorderStyle = BorderStyle.None;
            tbxPagNum.TextAlign = HorizontalAlignment.Right;
            tbxPagNum.Font = new System.Drawing.Font(UserClass.fontName, 11);
            tbxPagNum.SetBounds(140, 10, 30, 30);
         
            Controls.Add(tbxPagNum);
        }
        private void pagenumControl_Load(object sender, EventArgs e) { }
        public void fnSetAllPage(string _insert)
        {
            int _iTemp = 0;
            try
            {
                _iTemp = Convert.ToInt32(_insert);
            }
            catch
            {
                _iTemp = 0;
            }
            iAllPageNum = _iTemp;
            iNowPageNum = 1;
            Refresh();
        }

        public void setTotalPage(string total,string each)
        {
            int temp_page = 0;
            try
            {
                temp_page = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(total) / Convert.ToDouble(each)));
            }
            catch
            {
                temp_page = 1;
            }
            iAllPageNum = temp_page;
            iNowPageNum = 1;
        }

        public void fnNextPage()
        {
            if (iNowPageNum == iAllPageNum)
            {
                return;
            }

            iNowPageNum += 1;
            Refresh();
            fnPageChange();
        }
        public void fnUpPage()
        {
            if (iNowPageNum == 1)
            {
                return;
            }

            iNowPageNum -= 1;
            Refresh();
            fnPageChange();
        }
        public void fnJumpPage()
        {
            int _iTemp = 1;
            try
            {
                _iTemp = Convert.ToInt32(tbxPagNum.Text.ToString());
            }
            catch
            {
                _iTemp = 1;
            }
            if (_iTemp > iAllPageNum || _iTemp < 1)
            {
                tbxPagNum.Text = "";
                return;
            }
            else
            {
                iNowPageNum = _iTemp;
                Refresh();
                fnPageChange();
            }
            tbxPagNum.Text = "";
        }
        private void fnPageChange()
        {
            try
            {
                if (listtype == listType.orderlist)
                {
                    ((detailControl)Parent).sPageNum = iNowPageNum.ToString();
                    ((detailControl)Parent).fnTradePageLoad();
                }
                else if (listtype == listType.storelist)
                {
                    ((storeDetailControl)Parent).sPageNum = iNowPageNum.ToString();
                    ((storeDetailControl)Parent).fnStorePageLoad();
                }
                else if (listtype == listType.flowlist)
                {
                    ((flowListControl)Parent).sPageNum = iNowPageNum.ToString();
                    ((flowListControl)Parent).flowPageLoad();
                }
                else if (listtype == listType.neworderlist)
                {
                    ((orderListControl)Parent).sPageNum = iNowPageNum.ToString();
                    ((orderListControl)Parent).orderlistPageLoad();
                }
                else if (listtype == listType.couponlist)
                {
                    ((couponUseListControl)Parent).sPageNum = iNowPageNum.ToString();
                    ((couponUseListControl)Parent).listPageLoad();
                }
                else if (listtype == listType.koubeilist)
                {
                    ((koubeiUseListControl)Parent).sPageNum = iNowPageNum.ToString();
                    ((koubeiUseListControl)Parent).listPageLoad();
                }
                
            }
            catch { }
            
        }
        private void pagenumControl_Paint(object sender, PaintEventArgs e)
        {
            PublicMethods.FrameRoundRectangle(new Rectangle(5, 5, 37, 37), e.Graphics, 7, Defcolor.MainGrayLineColor);
            PublicMethods.FrameRoundRectangle(new Rectangle(92, 5, 122, 37), e.Graphics, 7, Defcolor.MainGrayLineColor);
            PublicMethods.FrameRoundRectangle(new Rectangle(123, 5, 187, 37), e.Graphics, 7, Defcolor.MainGrayLineColor);
            PublicMethods.FrameRoundRectangle(new Rectangle(188, 5, 248, 37), e.Graphics, 7, Defcolor.MainGrayLineColor);

            if (iNowPageNum == 1)
            {
                e.Graphics.DrawPolygon(new Pen(colorNotAllowClick), pointUP);
                e.Graphics.FillPolygon(new SolidBrush(colorNotAllowClick), pointUP);
            }
            else
            {
                e.Graphics.DrawPolygon(new Pen(colorAllowClick), pointUP);
                e.Graphics.FillPolygon(new SolidBrush(colorAllowClick), pointUP);
            }
            if (iNowPageNum == iAllPageNum)
            {
                e.Graphics.DrawPolygon(new Pen(colorNotAllowClick), pointDown);
                e.Graphics.FillPolygon(new SolidBrush(colorNotAllowClick), pointDown);
            }
            else
            {
                e.Graphics.DrawPolygon(new Pen(colorAllowClick), pointDown);
                e.Graphics.FillPolygon(new SolidBrush(colorAllowClick), pointDown);
            }

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            Font myFont = new Font(UserClass.fontName, 11);
            string strLine = string.Format("跳转");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectPageButton, drawFormat);

            myFont = new Font(UserClass.fontName, 9);
            strLine = string.Format(iNowPageNum + "/" + iAllPageNum);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectPageNumShow, drawFormat);
        }
        private void pagenumControl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (listtype == listType.orderlist)
                {
                    if (((detailControl)Parent).IsThreadRun)
                    {
                        return;
                    }
                }
                else if (listtype == listType.storelist)
                {
                    if (((storeDetailControl)Parent).IsThreadRun)
                    {
                        return;
                    }
                }
                else if (listtype == listType.flowlist)
                {
                    if (((flowListControl)Parent).IsThreadRun)
                    {
                        return;
                    }
                }
                else if (listtype == listType.neworderlist)
                {
                    if (((orderListControl)Parent).IsThreadRun)
                    {
                        return;
                    }
                }
                else if (listtype == listType.couponlist)
                {
                    if (((couponUseListControl)Parent).IsThreadRun)
                    {
                        return;
                    }
                }
                else if (listtype == listType.koubeilist)
                {
                    if (((koubeiUseListControl)Parent).IsThreadRun)
                    {
                        return;
                    }
                }

                if (rectUpPage.Contains(e.Location))
                {
                    fnUpPage();
                }
                else if (rectDownPage.Contains(e.Location))
                {
                    fnNextPage();
                }
                else if (rectPageNumInput.Contains(e.Location))
                {
                    tbxPagNum.Focus();
                }
                else if (rectPageButton.Contains(e.Location))
                {
                    fnJumpPage();
                }
                else
                {
                    return;
                }
            }
            catch { }
        }
    }
}
