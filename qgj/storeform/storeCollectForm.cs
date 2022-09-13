using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace qgj
{
    public partial class storeCollectForm : Form
    {
        string sTitle = "储值汇总";

        string sKeyWords = "";
        string sPayType = "";
        string sStartTime = "";
        string sEndTime = "";

        string[] title = { "储值金额", "储值笔数", "优惠金额", "实收金额" };
        string[] name = { "支付宝", "微信", "银联", "现金" };

        string[][] content;

        confirmcancelControl printC = new confirmcancelControl("打印");
        confirmcancelControl confirmC = new confirmcancelControl("确定");

        int iHttpResult = 0;
        string sHttpResult = "";

        rechargeTradeSummarySuccess rechargeSummaryS;

        public storeCollectForm(string _keyword, string _paytype, string _starttime, string _endtime)
        {
            InitializeComponent();

            TopMost = true;
            content = new string[4][];

            for (int i = 0; i < 4; i++)
            {
                content[i] = new string[4];
                for (int j = 0; j < 4; j++)
                {
                    content[i][j] = "0.00";
                }
            }

            sKeyWords = _keyword;
            sPayType = _paytype;
            sStartTime = _starttime;
            sEndTime = _endtime;

        }

        private void storeCollectForm_Load(object sender, EventArgs e)
        {
            fnSetCollectMouse();

            printC.Location = new Point(300, 355);
            printC.MouseUp += new MouseEventHandler(print_MouseUp);
            Controls.Add(printC);

            confirmC.Location = new Point(400, 355);
            confirmC.MouseUp += new MouseEventHandler(confirmC_MouseUp);
            Controls.Add(confirmC);

            fnRechargeSummaryFirstLoadAction();

            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
        }

        private void fnRechargeSummaryFirstLoadAction()
        {
            try
            {
                if (fnRechargeSummaryHttp())
                {

                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }

        }

        private bool fnRechargeSummaryHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.storesummary);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sPayType != "")
            {
                _urlC.addParameter("pay_channel", sPayType);
            }
            if (sKeyWords != "")
            {
                try
                {
                    if (PublicMethods.IsNumeric(sKeyWords))
                    {
                        _urlC.addParameter("code", sKeyWords);
                    }
                    else
                    {
                        _urlC.addParameter("activity_title", sKeyWords);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (sStartTime != "" && sEndTime != "")
            {
                _urlC.addParameter("start_time", sStartTime);
                _urlC.addParameter("end_time", sEndTime);
            }

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                rechargeSummaryS = (rechargeTradeSummarySuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(rechargeTradeSummarySuccess));
                iHttpResult = 0; ;
                sHttpResult = "请求成功";
                return true;
            }
            else
            {
                errorClass errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = errorC.errMsg;
                return false;
            }

        }

        private void storeCollectForm_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
                );

                e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, e.ClipRectangle.Width, 47));

                Font myFont = new Font(UserClass.fontName, 12);
                string strLine = String.Format(sTitle);
                PointF strPoint = new PointF(30, 15);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

                myFont = new Font(UserClass.fontName, 9);
                strLine = String.Format(rechargeSummaryS.data.date);
                strPoint = new PointF(30, 65);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 100), new Point(470, 100));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 145), new Point(470, 145));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 190), new Point(470, 190));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 235), new Point(470, 235));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 280), new Point(470, 280));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 325), new Point(470, 325));

                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 100), new Point(30, 325));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(470, 100), new Point(470, 325));

                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;

                int temp = 30;
                foreach (string _s in title)
                {
                    temp += 82;
                    strLine = String.Format(_s);
                    e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(temp, 100, 82, 50), drawFormat);
                }

                temp = 100;
                foreach (string _s in name)
                {
                    temp += 46;
                    strLine = String.Format(_s);
                    e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(30, temp, 82, 40), drawFormat);
                }
                if (iHttpResult == 1)
                {
                    return;
                }

                int _x = 112;
                int _y = 145;
                strLine = String.Format(rechargeSummaryS.data.data.alipay.money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.alipay.num);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.alipay.discount_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.alipay.pay_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 82, 40), drawFormat);

                _x = 112;
                _y = 190;
                strLine = String.Format(rechargeSummaryS.data.data.wechat.money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.wechat.num);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.wechat.discount_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.wechat.pay_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 82, 40), drawFormat);

                _x = 112;
                _y = 235;
                strLine = String.Format(rechargeSummaryS.data.data.unionpay.money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.unionpay.num);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.unionpay.discount_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.unionpay.pay_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 82, 40), drawFormat);

                _x = 112;
                _y = 280;
                strLine = String.Format(rechargeSummaryS.data.data.cash.money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.cash.num);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.cash.discount_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 82, 40), drawFormat);
                _x += 82;
                strLine = String.Format(rechargeSummaryS.data.data.cash.pay_money);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 82, 40), drawFormat);
            }
            catch { }
        }
        private void print_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                rechargesummaryprintClass summaryprintC = new rechargesummaryprintClass();
                summaryprintC.p = rechargeSummaryS;
                summaryprintC.PrintNow();

            }
            catch (Exception ee)
            {
                errorinformationForm errorF = new errorinformationForm("提示", "打印故障：" + ee.ToString());
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterParent;
                errorF.ShowDialog();
                Refresh();
            }
        }
        private void confirmC_MouseUp(object sender, MouseEventArgs e)
        {
            Dispose();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    Dispose();
                }
                else
                {
                    if (fnCollectMouseControl(keyData) && UserClass.isUseKeyBorad)
                    {
                        return true;
                    }
                }
            }
            catch { }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void fnSetCollectMouse()
        {
            collectFormMouse.Position.Clear();
            collectFormMouse.Position.Add(new Point(320, 360));
            collectFormMouse.Position.Add(new Point(420, 360));
        }
        public bool fnCollectMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right)
            {
                switch (collectFormMouse.pos)
                {
                    case 0: collectFormMouse.pos = 1; break;
                    case 1: collectFormMouse.pos = 0; break;
                    default: collectFormMouse.pos = 1; break;
                }
                collectFormMouse._offsetX = Location.X;
                collectFormMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(collectFormMouse.Position[collectFormMouse.pos].X + collectFormMouse._offsetX, collectFormMouse.Position[collectFormMouse.pos].Y + collectFormMouse._offsetY);
                return true;
            }
            else if (keyData == Keys.Space)
            {
                NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class rechargeTotal
    {
        /// <summary>
        /// 
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_money { get; set; }
    }

    public class rechargeAlipay
    {
        /// <summary>
        /// 
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_money { get; set; }
    }

    public class rechargeWechat
    {
        /// <summary>
        /// 
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_money { get; set; }
    }

    public class rechargeUnionpay
    {
        /// <summary>
        /// 
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_money { get; set; }
    }

    public class rechargeCash
    {
        /// <summary>
        /// 
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_money { get; set; }
    }

    public class DataList
    {
        /// <summary>
        /// 
        /// </summary>
        public rechargeTotal total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public rechargeAlipay alipay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public rechargeWechat wechat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public rechargeUnionpay unionpay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public rechargeCash cash { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 奥斯卡股份
        /// </summary>
        public string merchant_name { get; set; }
        /// <summary>
        /// 亲快科技-九亭分店
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataList data { get; set; }
    }

    public class rechargeTradeSummarySuccess
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
        public Data data { get; set; }
    }
}
