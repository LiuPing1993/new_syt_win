using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;

namespace qgj
{
    public partial class addBalanceForm : Form
    {
        string sTitle = "储值码";
        string sInfo = "请输入储值码";
        string sMemberId = "";

        TextBox tbBalancecode = new TextBox();

        confirmcancelControl confirmC = new confirmcancelControl("兑换");
        confirmcancelControl cancelC = new confirmcancelControl("取消");
        int iHttpResult = 0;
        string sHttpResult = "";

        addbalanceSuccess addbalanceS;

        memberDetailForm memberDetatilF;
        public addBalanceForm(string _memberid, memberDetailForm _memberDetatilF)
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            tbBalancecode.SetBounds(85, 75, 230, 30);
            tbBalancecode.Font = new Font(UserClass.fontName, 12);
            tbBalancecode.ForeColor = Defcolor.FontLiteGrayColor;
            tbBalancecode.ImeMode = System.Windows.Forms.ImeMode.Off;
            tbBalancecode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Controls.Add(tbBalancecode);

            confirmC.Location = new Point(230, 200);
            confirmC.MouseUp += new MouseEventHandler(confirm_click);
            Controls.Add(confirmC);

            cancelC.Location = new Point(100, 200);
            cancelC.MouseUp += new MouseEventHandler(cancel_click);
            Controls.Add(cancelC);

            sMemberId = _memberid;
            memberDetatilF = _memberDetatilF;
        }

        private void addBalanceForm_Load(object sender, EventArgs e)
        {
            keyboardClass.showKeyBoard();
            tbBalancecode.Focus();
        }

        private void addBalanceForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            Font myFont = new Font(UserClass.fontName, 12);
            string strLine = String.Format(sTitle);
            PointF strPoint = new PointF(18, 15);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            PublicMethods.FillRoundRectangle(new Rectangle(80, 70, 330, 105), e.Graphics, 15, Color.White);
            PublicMethods.FrameRoundRectangle(new Rectangle(80, 70, 330, 105), e.Graphics, 15, Color.Gray);

            User.drawFormatTitle.Alignment = StringAlignment.Center;
            User.drawFormatTitle.LineAlignment = StringAlignment.Center;
            strLine = string.Format(sInfo);
            myFont = new Font(UserClass.fontName, 9);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Color.Gray), new Rectangle(10, 120, 380, 30), User.drawFormatTitle);
        }

        private void confirm_click(object sender, MouseEventArgs e)
        {

                if (tbBalancecode.Text.Trim() == "")
                {
                    return;
                }
                if (addbalanceHttp())
                {
                    memberDetatilF.addbalanceSuccess(addbalanceS.data);
                    Dispose();
                }
                else
                {
                    sInfo = sHttpResult;
                    Refresh();
                }
        }
        private void cancel_click(object sender, MouseEventArgs e)
        {
            Dispose();
        }
        private bool addbalanceHttp()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.userechargecode);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("code", tbBalancecode.Text.Trim());
                _urlC.addParameter("member_id", sMemberId);
                _urlC.addParameter("token", UserClass.Token);
                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                HttpClass httpC = new HttpClass();
                string _sRequestMsg = httpC.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + _sRequestMsg);
                if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                {
                    addbalanceS = (addbalanceSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(addbalanceSuccess));
                    iHttpResult = 0;
                    sHttpResult = "成功";
                    return true;
                }
                else
                {
                    errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                    iHttpResult = 1;
                    sHttpResult = _errorC.errMsg;
                    return false;
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.Message.ToString();
                return false;
            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (keyData == Keys.Tab)
                {
                    tbBalancecode.Focus();
                    return true;
                }
                else if (keyData == Keys.Escape)
                {
                    cancel_click(null, null);
                    return true;
                }
                else if (keyData == Keys.Enter)
                {
                    confirm_click(null, null);
                }
                else if (keyData == Keys.Left || keyData == Keys.Down || keyData == Keys.Right || keyData == Keys.Up)
                {
                    tbBalancecode.Focus();
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }

    public class addbalanceSuccess
    {
        /// <summary>
        /// 
        /// </summary>
        public string errCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string data { get; set; }
    }
}
