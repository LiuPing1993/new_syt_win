using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace qgj
{
    public partial class refundForm : Form
    {
        string sTitle = "退款";
        string sOrderNum = "";
        string sOrderMoney = "";
        string sPayType = "";
        string sReceipt = "";
        bool IsNeedPwd = false;

        confirmcancelControl confirmC = new confirmcancelControl("确认");
        confirmcancelControl cancelC = new confirmcancelControl("取消");

        watermarkTextBox tbxRefundMony = new watermarkTextBox();
        watermarkTextBox tbxPassWord = new watermarkTextBox();

        refundresultControl refundresultC;
        orderdetailSuccess orderdetailS;
        refundSuccess refundS;

        public delegate void addDelegate();
        public addDelegate d;
        Thread t;
        bool IsThreadRun = false;

        int iHttpResult = 0;
        string sHttpResult = "";

        public refundForm(string _ordernum)
        {
            sOrderNum = _ordernum;
            if (fnOrderDetailHttp())
            {
                sOrderMoney = orderdetailS.data.total_money;
                sPayType = orderdetailS.data.pay_type;
                sReceipt = orderdetailS.data.receipt_fee;
                if (!fnRefundPwdNeedAction())
                {
                    IsNeedPwd = false;
                }
                else
                {
                    IsNeedPwd = true;
                }
            }
            InitializeComponent();

            confirmC.Location = new Point(260, 280);
            confirmC.MouseUp += new MouseEventHandler(confirm_MouseUp);
            Controls.Add(confirmC);

            cancelC.Location = new Point(180, 280);
            cancelC.MouseUp += new MouseEventHandler(cancel_MouseUp);
            Controls.Add(cancelC);

            tbxRefundMony.BorderStyle = BorderStyle.None;
            tbxRefundMony.SetBounds(140, 165, 280, 30);
            Controls.Add(tbxRefundMony);

            tbxPassWord.BorderStyle = BorderStyle.None;
            tbxPassWord.PasswordChar = '*';
            tbxPassWord.UseSystemPasswordChar = true;
            tbxPassWord.SetBounds(140, 215, 280, 30);
            Controls.Add(tbxPassWord);

            d = new addDelegate(fnRefundOver);
            BackColor = Defcolor.MainBackColor;
        }
        public refundForm(orderdetailSuccess _orderdetailS)
        {
            orderdetailS = _orderdetailS;
            sOrderNum = orderdetailS.data.order_no;
            sOrderMoney = orderdetailS.data.total_money;
            sPayType = orderdetailS.data.pay_type;
            sReceipt = orderdetailS.data.receipt_fee;
            if (!fnRefundPwdNeedAction())
            {
                IsNeedPwd = false;
            }
            else
            {
                IsNeedPwd = true;
            }
            InitializeComponent();

            confirmC.Location = new Point(260, 280);
            confirmC.MouseUp += new MouseEventHandler(confirm_MouseUp);
            Controls.Add(confirmC);

            cancelC.Location = new Point(180, 280);
            cancelC.MouseUp += new MouseEventHandler(cancel_MouseUp);
            Controls.Add(cancelC);

            tbxRefundMony.BorderStyle = BorderStyle.None;
            tbxRefundMony.SetBounds(140, 165, 280, 30);
            Controls.Add(tbxRefundMony);

            tbxPassWord.BorderStyle = BorderStyle.None;
            tbxPassWord.PasswordChar = '*';
            tbxPassWord.UseSystemPasswordChar = true;
            tbxPassWord.SetBounds(140, 215, 280, 30);
            Controls.Add(tbxPassWord);

            d = new addDelegate(fnRefundOver);
            BackColor = Defcolor.MainBackColor;
        }
        private void refundForm_Load(object sender, EventArgs e)
        {
            if (iHttpResult == 1)
            {
                refundresultC = new refundresultControl(false, sPayType, "", "服务出错", sHttpResult);
                refundresultC.Location = new Point(0, 0);
                Controls.Add(refundresultC);
                refundresultC.Show();
                refundresultC.BringToFront();
            }
            else
            {
                if (!IsNeedPwd)
                {
                    tbxPassWord.Hide();
                }
                tbxRefundMony.Text = orderdetailS.data.receipt_fee.ToString();
            }
            
        }
        private void confirm_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsNeedPwd)
            {
                if (tbxRefundMony.Text == "" || tbxPassWord.Text == "")
                {
                    return;
                }
            }
            else
            {
                if (tbxRefundMony.Text == "")
                {
                    return;
                }
            }
            try
            {
                if (!(Convert.ToDouble(tbxRefundMony.Text) > Convert.ToDouble(sReceipt)))
                {
                    if (!IsThreadRun)
                    {
                        IsThreadRun = true;
                        Thread thread = new Thread(fnRefundAction);
                        thread.IsBackground = true;
                        thread.Start();
                        t = thread;
                    }
                }
            }
            catch { return; }
        }
        private void cancel_MouseUp(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            if (IsThreadRun)
            {
                t.Abort();
            }
            Dispose();
        }
        public void fnRefundAction()
        {
            try
            {
                fnRefundHttp(tbxRefundMony.Text, tbxPassWord.Text);
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
            Invoke(d);
            IsThreadRun = true;
            t.Abort();
        }
        public bool fnRefundPwdNeedAction()
        {
            try
            {
                if (fnRefundPwdNeedHttp())
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }
        private void fnRefundOver()
        {
            try
            {
                //pictureClass.Delay(1000);

                if (iHttpResult == 0)
                {
                    fnPrint(true);
                    refundresultC = new refundresultControl(true, sPayType, refundS.data.refund_money.ToString(), "", "");
                    refundresultC.Location = new Point(0, 0);
                    Controls.Add(refundresultC);
                    
                    refundresultC.Show();
                    refundresultC.BringToFront();
                    refundresultC.Focus();
                }
                else
                {
                    refundresultC = new refundresultControl(false, sPayType, "", "服务出错", sHttpResult);
                    refundresultC.Location = new Point(0, 0);
                    Controls.Add(refundresultC);

                    refundresultC.Show();
                    refundresultC.BringToFront();
                    refundresultC.Focus();
                }
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("错误(退款)", e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Application.DoEvents();
                Refresh();
            }
        }
        private bool fnRefundHttp(string _refundmoney, string _password)
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.refund);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("order_no", sOrderNum);
            _urlC.addParameter("refund_money", _refundmoney);
            if (IsNeedPwd)
            {
                //_urlC.addParameter("admin_pwd", PublicMethods.md5(_password).ToLower());
                _urlC.addParameter("admin_pwd", _password);
            }
            else
            {
                _urlC.addParameter("admin_pwd", "");
            }

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);
            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                refundS = (refundSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(refundSuccess));
                iHttpResult = 0;
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
        private bool fnOrderDetailHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.orderdetail);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("order_no", sOrderNum);

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                orderdetailS = (orderdetailSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(orderdetailSuccess));
                iHttpResult = 0; ;
                sHttpResult = "请求成功";
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
        private bool fnRefundPwdNeedHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.refundpwdshow);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("order_no", sOrderNum);

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);
            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                refundpwdneedSuccess _refundpwdneedS = (refundpwdneedSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(refundpwdneedSuccess));
                if (_refundpwdneedS.data.verify_result == "need")
                {
                    iHttpResult = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = _errorC.errMsg;
                return false;
            }
        }
        private void refundForm_Paint(object sender, PaintEventArgs e)
        {

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            Font myFont = new Font(UserClass.fontName, 9);
            string strLine = String.Format(sTitle);
            PointF strPoint = new PointF(20, 20);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format("订单号");
            strPoint = new PointF(60, 50);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format(sOrderNum);
            strPoint = new PointF(133, 50);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format("订单金额");
            strPoint = new PointF(60, 75);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format(sOrderMoney);
            strPoint = new PointF(133, 75);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format("支付方式");
            strPoint = new PointF(60, 100);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format(sPayType);
            strPoint = new PointF(133, 100);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format("实收金额");
            strPoint = new PointF(60, 125);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = string.Format(sReceipt);
            strPoint = new PointF(133, 125);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);



            strLine = string.Format("退款金额");
            strPoint = new PointF(60, 165);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            PublicMethods.FillRoundRectangle(new Rectangle(133, 155, 433, 190), e.Graphics, 7, Color.White);
            PublicMethods.FrameRoundRectangle(new Rectangle(133, 155, 433, 190), e.Graphics, 7, Defcolor.MainGrayLineColor);

            if (IsNeedPwd)
            {
                strLine = string.Format("退款密码");
                strPoint = new PointF(60, 213);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

                PublicMethods.FillRoundRectangle(new Rectangle(133, 205, 433, 240), e.Graphics, 7, Color.White);
                PublicMethods.FrameRoundRectangle(new Rectangle(133, 205, 433, 240), e.Graphics, 7, Defcolor.MainGrayLineColor);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                Console.WriteLine("per refund enter");
                confirm_MouseUp(null, null);
            }
            else if (keyData == Keys.Tab)
            {
                if (!IsNeedPwd)
                {
                    tbxRefundMony.Focus();
                }
                else
                {
                    if (tbxRefundMony.Focused)
                    {
                        tbxPassWord.Focus();
                    }
                    else
                    {
                        tbxRefundMony.Focus();
                    }
                }
            }
            else if(keyData == Keys.Escape)
            {
                cancel_MouseUp(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void fnPrint(bool _ismerchant)
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                refundprintClass _refundprintC = new refundprintClass(_ismerchant);
                _refundprintC.STRmerchantname = refundS.data.merchant_name;
                _refundprintC.STRstorename = refundS.data.store_name;
                _refundprintC.STRterminalnum = _lcc.readfromConfig();
                _refundprintC.STRemployee = refundS.data.employee_name;
                _refundprintC.STRordernum = refundS.data.order_no;
                _refundprintC.STRpaytype = refundS.data.pay_type;
                _refundprintC.STRorderstatus = refundS.data.order_status;
                _refundprintC.STRpayaccount = refundS.data.pay_account;
                _refundprintC.STRtotalmoney = refundS.data.total_money;
                _refundprintC.STRpaymoney = refundS.data.pay_money;
                _refundprintC.STRrefundmoney = refundS.data.refund_money;
                _refundprintC.STRreceipt = refundS.data.receipt_fee;
                _refundprintC.STRrefundtime = refundS.data.refund_time;
                _refundprintC.STRtradenum = refundS.data.refund_flow_no;
                //_refundprintC.STRbanktradenum = refundS.data.bank_trade_no;
                //_refundprintC.STRpaytradenum = refundS.data.trade_no;
                _refundprintC.PrintNow();
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("提示", "打印故障："+e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
        }
    }
    public class refundData
    {
        /// <summary>
        /// 
        /// </summary>
        public string third_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string third_msg { get; set; }
        /// <summary>
        /// 测试商户
        /// </summary>
        public string merchant_name { get; set; }
        /// <summary>
        /// 海底捞
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 李四
        /// </summary>
        public string employee_name { get; set; }
        /// <summary>
        /// 现金记账
        /// </summary>
        public string pay_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 已部分退款
        /// </summary>
        public string order_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string total_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string undiscount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coupon_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string member_discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminal_sn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refund_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alipay_merchant_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alipay_discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wechat_discount_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 系统流水号
        /// </summary>
        public string refund_flow_no { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public string bank_trade_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Refund_listItem> refund_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receipt_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refund_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refund_money { get; set; }
    }
    public class refundSuccess
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
        public refundData data { get; set; }
    }
    public class refundpwdneedData
    {
        /// <summary>
        /// 
        /// </summary>
        public string verify_result { get; set; }
    }
    public class refundpwdneedSuccess
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
        public refundpwdneedData data { get; set; }
    }
}
