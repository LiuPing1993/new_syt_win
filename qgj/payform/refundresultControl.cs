using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class refundresultControl : UserControl
    {
        string sPayType;
        string sMoney;
        string sContent1;
        string sContent2;
        bool IsSuccess;

        confirmcancelControl confirmC = new confirmcancelControl("确认");
        PictureBox pbxInfo = new PictureBox();

        Rectangle rectContent = new Rectangle(5, 210, 500, 50);
        public refundresultControl(bool _issuccess,string _paytype,string _money,string _content1,string _content2)
        {
            IsSuccess = _issuccess;
            sPayType = _paytype;
            sMoney = _money;
            sContent1 = _content1;
            sContent2 = _content2;

            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
            confirmC.Location = new Point(220, 280);
            confirmC.MouseUp += new MouseEventHandler(confirm_MouseUp);
            Controls.Add(confirmC);

            pbxInfo.SetBounds(220, 70, 70, 70);
            Controls.Add(pbxInfo);
        }
        private void refundresultControl_Load(object sender, EventArgs e)
        {
            if (IsSuccess)
            {
                pbxInfo.Image = Properties.Resources.success;
            }
            else
            {
                pbxInfo.Image = Properties.Resources.fail;
            }
        }
        private void refundresultControl_Paint(object sender, PaintEventArgs e)
        {

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            Font myFont = new Font(UserClass.fontName, 11);
            string strLine = String.Format("退款结果");
            PointF strPoint = new PointF(20, 20);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            if (IsSuccess)
            {
                myFont = new Font(UserClass.fontName, 9);
                strLine = String.Format("退款成功");
                strPoint = new PointF(225, 150);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

                string payname = String.Format(sPayType + "  ");
                SizeF paynameF = e.Graphics.MeasureString(payname, myFont);

                Font moneyFont = new System.Drawing.Font(UserClass.fontName, 18);
                string paymoney = String.Format(sMoney);
                SizeF paymoneyF = e.Graphics.MeasureString(paymoney, moneyFont);

                float paynameX = (e.ClipRectangle.Width - (paynameF.Width + paymoneyF.Width)) / 2;
                float paymoneyX = paynameX + paynameF.Width;

                PointF paynamePoint = new PointF(paynameX, 180);
                PointF paymoneyPoint = new PointF(paymoneyX, 170);
                e.Graphics.DrawString(payname, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), paynamePoint);
                e.Graphics.DrawString(paymoney, moneyFont, new SolidBrush(Color.Black), paymoneyPoint);

            }
            else
            {
                strLine = String.Format("退款失败");
                strPoint = new PointF(225, 150);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

                myFont = new Font(UserClass.fontName, 10);
                strLine = String.Format("原因 " + sContent1);
                SizeF sizeF = e.Graphics.MeasureString(strLine, myFont);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 192);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);

                StringFormat drawFormatTitle = new StringFormat();
                drawFormatTitle.Alignment = StringAlignment.Center;
                drawFormatTitle.LineAlignment = StringAlignment.Near;
                strLine = String.Format(sContent2);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectContent, drawFormatTitle);
            }
        }
        private void confirm_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsSuccess)
            {
                ((refundForm)Parent).fnPrint(false);
                ((refundForm)Parent).DialogResult = DialogResult.OK;
            }
            ((refundForm)Parent).Dispose();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                Console.WriteLine("per refund enter");
                confirm_MouseUp(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
