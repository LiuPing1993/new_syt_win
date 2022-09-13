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
    public partial class flowCollectForm : Form
    {

        string sTitle = "更多汇总";

        string sOrderNum = ""; // 订单号/流水号
        string sType = ""; //流水类型：门店交易、门店储值
        string sPayChannel = ""; // 支付渠道：支付宝、微信、银联……
        string sTradeType = ""; // 流水收支类型

        string sStartTime = "";
        string sEndTime = "";

        string[] title = { "交易金额", "交易笔数", "退款金额", "退款笔数", "优惠金额", "实收金额" };
        string[] name = { "支付宝", "微信", "云闪付", "银联", "现金", "储值" };

        string[][] content;

        confirmcancelControl printC = new confirmcancelControl("打印");
        confirmcancelControl confirmC = new confirmcancelControl("确定");

        int iHttpResult = 0;
        string sHttpResult = "";

        orderCollectSucccess orderCollectS;
        public flowCollectForm(string _orderNum,string _type,string _paychannel,string _tradetype,string _starttime,string _endtime)
        {

            sOrderNum = _orderNum;
            sType = _type;
            sPayChannel = _paychannel;
            sTradeType = _tradetype;
            sStartTime = _starttime;
            sEndTime = _endtime;

            InitializeComponent();

            TopMost = true;
            content = new string[6][];

            for (int i = 0; i < 6; i++)
            {
                content[i] = new string[5];
                for (int j = 0; j < 5; j++)
                {
                    content[i][j] = "0.00";
                }
            }
        }

        private void flowCollectForm_Load(object sender, EventArgs e)
        {
            fnSetCollectMouse();

            printC.Location = new Point(300, 395);
            printC.MouseUp += new MouseEventHandler(print_MouseUp);
            Controls.Add(printC);

            confirmC.Location = new Point(400, 395);
            confirmC.MouseUp += new MouseEventHandler(confirmC_MouseUp);
            Controls.Add(confirmC);

            fnOrderCollectFirstLoadAction();
            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
        }
        private void fnOrderCollectFirstLoadAction()
        {
            try
            {
                if (ordercollecthttp())
                {

                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }

        }
        private bool ordercollecthttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.flowtotalsummary);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sOrderNum != "")
            {
                _urlC.addParameter("order_no", sOrderNum);
            }
            if (sPayChannel != "")
            {
                _urlC.addParameter("pay_channel", sPayChannel);
            }
            if (sTradeType != "")
            {
                _urlC.addParameter("trade_type", sTradeType);
            }
            if (sType != "")
            {
                _urlC.addParameter("type", sType);
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
                orderCollectS = (orderCollectSucccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(orderCollectSucccess));
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
        private void flowCollectForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

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
            strLine = String.Format(orderCollectS.data.date);
            strPoint = new PointF(30, 65);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 100), new Point(470, 100));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 140), new Point(470, 140));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 180), new Point(470, 180));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 220), new Point(470, 220));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 260), new Point(470, 260));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 300), new Point(470, 300));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 340), new Point(470, 340));

            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(30, 100), new Point(30, 340));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(470, 100), new Point(470, 340));

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            int temp = 30;
            foreach (string _s in title)
            {
                temp += 63;
                strLine = String.Format(_s);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(temp, 100, 63, 40), drawFormat);
            }

            temp = 100;
            foreach (string _s in name)
            {
                temp += 40;
                strLine = String.Format(_s);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(30, temp, 63, 40), drawFormat);
            }
            if (iHttpResult == 1)
            {
                return;
            }

            int _x = 93;
            int _y = 140;
            strLine = String.Format(orderCollectS.data.data.alipay.money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.alipay.num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.alipay.refund_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.alipay.refund_num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.alipay.discount_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.alipay.pay_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 63, 40), drawFormat);

            _x = 93;
            _y = 180;
            strLine = String.Format(orderCollectS.data.data.wechat.money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.wechat.num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.wechat.refund_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.wechat.refund_num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.wechat.discount_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.wechat.pay_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 63, 40), drawFormat);

            _x = 93;
            _y = 220;
            strLine = String.Format(orderCollectS.data.data.ysf.money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.ysf.num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.ysf.refund_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.ysf.refund_num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.ysf.discount_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.ysf.pay_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 63, 40), drawFormat);

            _x = 93;
            _y = 260;
            strLine = String.Format(orderCollectS.data.data.unionpay.money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.unionpay.num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.unionpay.refund_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.unionpay.refund_num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.unionpay.discount_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.unionpay.pay_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 63, 40), drawFormat);

            _x = 93;
            _y = 300;
            strLine = String.Format(orderCollectS.data.data.cash.money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.cash.num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.cash.refund_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.cash.refund_num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.cash.discount_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.cash.pay_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 63, 40), drawFormat);

            _x = 93;
            _y = 340;
            strLine = String.Format(orderCollectS.data.data.balance.money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.balance.num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.balance.refund_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.balance.refund_num);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.balance.discount_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(_x, _y, 63, 40), drawFormat);
            _x += 63;
            strLine = String.Format(orderCollectS.data.data.balance.pay_money);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), new Rectangle(_x, _y, 63, 40), drawFormat);
        }
        private void confirmC_MouseUp(object sender, MouseEventArgs e)
        {
            Dispose();
        }
        private void print_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                summaryprintClass summaryprintC = new summaryprintClass();
                summaryprintC.p = orderCollectS;
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

}



