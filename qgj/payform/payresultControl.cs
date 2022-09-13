using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class payresultControl : UserControl
    {
        string sPayname = "";
        string sDiscounName = "";
        int iResult = 0;
        string sContent1;
        string sContent2;

        confirmcancelControl confirmC = new confirmcancelControl("确认");
        PictureBox pbxInfo = new PictureBox();
        Rectangle rectContet2 = new Rectangle(5, 210, 500, 55);

        int countdow = 3;
        public payresultControl()
        {
            InitializeComponent();

            BackColor = Color.FromArgb(245, 245, 245);

            pbxInfo.SetBounds(220, 70, 70, 70);
            Controls.Add(pbxInfo);
        }

        private void payresultControl_Load(object sender, EventArgs e)
        {
            confirmC.Size = new System.Drawing.Size(100, 36);
            confirmC.Location = new Point((ClientRectangle.Width - confirmC.Width) / 2, 275);
            confirmC.MouseUp += new MouseEventHandler(confirm_MouseUp);
            Controls.Add(confirmC);
        }

        /// <summary>
        /// 设置显示参数
        /// </summary>
        /// <param name="_name1"> 支付名称 </param>
        /// <param name="_name2"> 优惠名称 </param>
        /// <param name="_result"> 是否成功 </param>
        /// <param name="_content1"> 成功：实收金额 失败：错误提示 </param>
        /// <param name="_content2"> 成功：优惠金额 失败：三方返回错误信息 </param>
        public void fnSetParams(string _name1, string _name2, int _result, string _content1, string _content2)
        {
            sPayname = _name1;
            sDiscounName = _name2;
            iResult = _result;
            sContent1 = PublicMethods.errormsgFormater(_content1);
            sContent2 = PublicMethods.errormsgFormater(_content2);
            if (iResult == 0)
            {
                pbxInfo.Image = Properties.Resources.success;
                PublicMethods.gfnVoiceToInform();
            }
            else
            {
                pbxInfo.Image = Properties.Resources.fail;
            }
        }
        private void confirm_MouseUp(object sender, MouseEventArgs e)
        {
            if (iResult == 0)
            {
                ((cashierForm)Parent).fnPrint(false);
                ((cashierForm)Parent).DialogResult = DialogResult.OK;
                timer.Dispose();
                ((cashierForm)Parent).Dispose();
            }
            else
            {
                ((cashierForm)Parent).DialogResult = DialogResult.No;
                timer.Dispose();
                ((cashierForm)Parent).Dispose();
            }
        }
        private void payresultControl_Paint(object sender, PaintEventArgs e)
        {

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            Font myFont = new Font(UserClass.fontName, 12);
            string strLine = String.Format("收款结果");
            PointF strPoint = new PointF(20, 20);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            
            if(iResult == 0)
            {

                myFont = new Font(UserClass.fontName, 10);
                strLine = String.Format("收款成功");
                SizeF sizeF = e.Graphics.MeasureString(strLine, myFont);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 150);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);

                //
                string payname = String.Format(sPayname + "  ");
                SizeF paynameF = e.Graphics.MeasureString(payname, myFont);

                Font moneyFont = new System.Drawing.Font(UserClass.fontName, 18);
                string paymoney = String.Format(sContent1);
                SizeF paymoneyF = e.Graphics.MeasureString(paymoney, moneyFont);

                float paynameX = (e.ClipRectangle.Width - (paynameF.Width + paymoneyF.Width)) / 2;
                float paymoneyX = paynameX + paynameF.Width;

                PointF paynamePoint = new PointF(paynameX, 180);
                PointF paymoneyPoint = new PointF(paymoneyX, 170);
                e.Graphics.DrawString(payname, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), paynamePoint);
                e.Graphics.DrawString(paymoney, moneyFont, new SolidBrush(Color.Black), paymoneyPoint);

                //
                if (sContent2 != "") 
                {
                    payname = String.Format(sDiscounName + "  ");
                    paynameF = e.Graphics.MeasureString(payname, myFont);

                    moneyFont = new System.Drawing.Font(UserClass.fontName, 9);
                    paymoney = String.Format(sContent2);
                    paymoneyF = e.Graphics.MeasureString(paymoney, moneyFont);

                    paynameX = (e.ClipRectangle.Width - (paynameF.Width + paymoneyF.Width)) / 2;
                    paymoneyX = paynameX + paynameF.Width;

                    paynamePoint = new PointF(paynameX, 210);
                    paymoneyPoint = new PointF(paymoneyX, 211);
                    e.Graphics.DrawString(payname, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), paynamePoint);
                    e.Graphics.DrawString(paymoney, moneyFont, new SolidBrush(Color.RosyBrown), paymoneyPoint);
                }

            }
            else
            {
                myFont = new Font(UserClass.fontName, 12);
                strLine = String.Format("收款失败");
                SizeF sizeF = e.Graphics.MeasureString(strLine, myFont);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 150);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);

                myFont = new Font(UserClass.fontName, 10);
                strLine = String.Format("原因 " + sContent1);
                sizeF = e.Graphics.MeasureString(strLine, myFont);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 192);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);

                //User.drawFormatTitle.LineAlignment = StringAlignment.Near;
                //User.drawFormatTitle.Alignment = StringAlignment.Center;
                strLine = String.Format(sContent2);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectContet2, User.drawFormatTitle);
            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                confirm_MouseUp(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void start_countdown()
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (countdow == 0)
            {
                timer.Stop();
                confirm_MouseUp(null, null);
            }
            else if (countdow > 0) 
            {
                confirmC.sInfo = string.Format("确认({0})", countdow.ToString());
                confirmC.Refresh();
                countdow--;
            }
            else
            {
                timer.Stop();
            }
        }

    }
}
