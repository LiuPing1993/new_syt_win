using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class refunddetailControl : UserControl
    {
        string sContent1 = "";
        string sContent2 = "";
        string sContent3 = "";
        string sContent4 = "";

        public string sTradeNo1 = "";
        public string sTradeNo2 = "";
        public string sTradeNo3 = "";

        int iHeight = 65;

        bool IsRefund = true;

        Rectangle rectContent1 = new Rectangle(0, 0, 350, 20);
        Rectangle rectContent2 = new Rectangle(0, 20, 350, 20);

        public refunddetailControl(string _content1, string _content2, string _content3, string _content4,bool _isrefund)
        {
            sContent1 = _content1;
            sContent2 = _content2;
            sContent3 = _content3;
            sContent4 = _content4;
            IsRefund = _isrefund;

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;
        }

        public void setHeight()
        {
            iHeight += sTradeNo1 != "" ? 20 : 0;
            iHeight += sTradeNo2 != "" ? 20 : 0;
            iHeight += sTradeNo3 != "" ? 20 : 0;
            iHeight += sContent4 != "" ? 20 : 0;
            this.Size = new Size(350, iHeight);
        }

        public int getHeight()
        {
            return iHeight;
        }

        private void refunddetailControl_Paint(object sender, PaintEventArgs e)
        {
            string strLine;
            Font myFont = new System.Drawing.Font(UserClass.fontName, 9);
            StringFormat drawFormatLeft = new StringFormat();
            StringFormat drawFormatRight = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            drawFormatLeft.LineAlignment = StringAlignment.Center;
            drawFormatRight.Alignment = StringAlignment.Far;
            drawFormatRight.LineAlignment = StringAlignment.Center;

            strLine = String.Format(sContent1);
            e.Graphics.DrawString(strLine, myFont, Brushes.Black, rectContent1, drawFormatLeft);
            if (IsRefund)
            {
                strLine = String.Format(sContent2);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), rectContent1, drawFormatRight);
            }
            else
            {
                strLine = String.Format("+" + sContent2);
                e.Graphics.DrawString(strLine, myFont, Brushes.Black, rectContent1, drawFormatRight);
            }

            int y_pos = 20;
            if (sContent4 != "")
            {
                strLine = String.Format(sContent4);
                e.Graphics.DrawString(strLine, myFont, Brushes.Gray, new Rectangle(0, 20, 350, 20), drawFormatRight);
                y_pos += 20;
            }

            if (sTradeNo1 != "")
            {
                strLine = String.Format("系统流水号：" + sTradeNo1);
                e.Graphics.DrawString(strLine, myFont, Brushes.Gray, new Rectangle(0, y_pos, 350, 20), drawFormatLeft);
                y_pos += 20;
            }
            if (sTradeNo2 != "")
            {
                strLine = String.Format("银行流水号：" + sTradeNo2);
                e.Graphics.DrawString(strLine, myFont, Brushes.Gray, new Rectangle(0, y_pos, 350, 20), drawFormatLeft);
                y_pos += 20;
            }
            if (sTradeNo3 != "")
            {
                strLine = String.Format("支付流水号：" + sTradeNo3);
                e.Graphics.DrawString(strLine, myFont, Brushes.Gray, new Rectangle(0, y_pos, 350, 20), drawFormatLeft);
                y_pos += 20;
            }

            strLine = String.Format(sContent3);
            e.Graphics.DrawString(strLine, myFont, Brushes.Gray, new Rectangle(0, y_pos, 350, 20), drawFormatLeft);

            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(0, iHeight - 15), new Point(350, iHeight - 15));
        }
    }
}
