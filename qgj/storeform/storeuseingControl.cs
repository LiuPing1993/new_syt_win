using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace qgj
{
    public partial class storeuseingControl : UserControl
    {
        string sTitle = "储值支付";
        string sMoneyTip = "应收金额";
        string sTip = "确认使用储值进行支付？";
        string sMoney = "";

        string sDiscountName = "";
        //string sDiscountMoney = "";
        string sErrorMsg1 = "";
        string sErrorMsg2 = "";
        storeSuccess storeS;
        errorClass errorC;

        public delegate void addDelegate();
        public addDelegate d;
        Thread t1;
        Thread t2;
        bool IsThreadRun = false;
        bool IsQuery = false;

        int iHttpResult = 0;
        string sHttpResult = "";

        confirmcancelControl cancelC = new confirmcancelControl("取消");
        confirmcancelControl confirmC = new confirmcancelControl("确认");
        public storeuseingControl()
        {
            InitializeComponent();

            cancelC.Location = new Point(180, 275);
            cancelC.MouseUp += new MouseEventHandler(cancel_Click);
            this.Controls.Add(cancelC);

            confirmC.Location = new Point(260, 275);
            confirmC.MouseUp += new MouseEventHandler(confirm_Click);
            this.Controls.Add(confirmC);
            this.BackColor = Defcolor.MainBackColor;

            d = new addDelegate(fnStorePayOver);

        }

        private void storeuseingControl_Load(object sender, EventArgs e)
        {
            if (((cashierForm)Parent).IsStore)
            {
                sMoney = UserClass.storeInfoC.getstoremoney();
            }
            else
            {
                sMoney = UserClass.orderInfoC.getShowReceipt();
            }
        }

        private void storeuseingControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            Font myFont = new Font(UserClass.fontName, 12);
            string strLine = String.Format(sTitle);
            PointF strPoint = new PointF(20, 20);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            myFont = new Font(UserClass.fontName, 12, FontStyle.Bold);
            SizeF sizeF = e.Graphics.MeasureString(sMoneyTip + " " + sMoney, myFont);
            strLine = String.Format(sMoneyTip);
            strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 100);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = String.Format(sMoney);
            sizeF = e.Graphics.MeasureString(sMoneyTip, myFont);
            strPoint = new PointF(strPoint.X + sizeF.Width, 100);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), strPoint);

            myFont = new Font(UserClass.fontName, 12);
            strLine = String.Format(sTip);
            sizeF = e.Graphics.MeasureString(sTip, myFont);
            strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                cancel_Click(null, null);
                return true;
            }
            if (keyData == Keys.Enter)
            {
                confirm_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void cancel_Click(object sender, MouseEventArgs e)
        {
            if (IsQuery)
            {
                stopqueryForm _stopquery = new stopqueryForm();
                _stopquery.TopMost = true;
                _stopquery.StartPosition = FormStartPosition.Manual;
                int _x = ((cashierForm)Parent).Location.X + (((cashierForm)Parent).Size.Width - _stopquery.Size.Width) / 2;
                int _y = ((cashierForm)Parent).Location.Y + (((cashierForm)Parent).Size.Height - _stopquery.Size.Height) / 2 + 50;
                _stopquery.Location = new Point(_x, _y);
                _stopquery.ShowDialog();
                if (_stopquery.DialogResult == DialogResult.OK)
                {
                    if (IsQuery)
                    {
                        t2.Abort();
                    }
                    ((cashierForm)Parent).DialogResult = DialogResult.Cancel;
                    ((cashierForm)Parent).Dispose();
                }
                else
                {
                    Refresh();
                }
            }
            else
            {
                if (IsThreadRun)
                {
                    t1.Abort();
                }
                ((cashierForm)Parent).Dispose();
            }
        }
        private void confirm_Click(object sender, MouseEventArgs e)
        {
            if (IsQuery)
            {
                return;
            }
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnStorePayAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }
        public void fnStorePayAction()
        {
            try
            {
                fnStorePayHttp();
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sErrorMsg2 = e.Message;
            }
            Invoke(d);
            IsThreadRun = false;
            t1.Abort();
        }
        private void fnStorePayOver()
        {
            try
            {
                //pictureClass.Delay(1000);

                if (iHttpResult == 0)
                {
                    fnPrint(true);
                    ((cashierForm)Parent).payresultC.fnSetParams(storeS.data.pay_type.ToString(), sDiscountName, 0, "+" + storeS.data.receipt_fee, "");
                    ((cashierForm)Parent).fnShowResult(this);
                }
                else if (iHttpResult == 1)
                {
                    ((cashierForm)Parent).payresultC.fnSetParams("", sDiscountName, 1, sErrorMsg1, sErrorMsg2);
                    ((cashierForm)Parent).fnShowResult(this);
                }
                else if (iHttpResult == 2)
                {
                    sHttpResult = "未完成支付";
                    sTip = "等待用户确认储值支付。";
                    confirmC.Visible = false;
                    cancelC.Location = new Point(220, 275);
                    Refresh();
                    fnStoreQuery();
                }
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("错误(储值)", e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterScreen;
                _errorF.ShowDialog();
                Refresh();
            }
        }
        private void fnStoreQuery()
        {
            if (!IsQuery)
            {
                IsQuery = true;
                Thread thread = new Thread(fnStoreQueryAction);
                thread.IsBackground = true;
                thread.Start();
                t2 = thread;
            }
        }
        private void fnStoreQueryAction()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.query);
                    _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                    _urlC.addParameter("token", UserClass.Token);
                    _urlC.addParameter("order_no", storeS.data.order_no.ToString());

                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                while (true)
                {
                    Thread.Sleep(1000);
                    if (fnStoreQueryHttp(_sRequestUrl))
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sErrorMsg1 = e.Message;
            }
            Invoke(d);
            IsQuery = false;
            t2.Abort();
        }
        private bool fnStorePayHttp()
        {

            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.store);
            string coupon = UserClass.orderInfoC.coupon;
            if (coupon != "")
            {
                coupon = coupon.Remove(coupon.Length - 1);
            }
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("total_amount", UserClass.orderInfoC.getMoney());
            _urlC.addParameter("undiscount_amount", UserClass.orderInfoC.getNotDiscount());
            _urlC.addParameter("code", UserClass.orderInfoC.member);
            _urlC.addParameter("coupon_id_list", coupon);
            if (UserClass.orderInfoC.UseMemberDiscount)
            {
                _urlC.addParameter("if_member_discount", "1");
            }
            else
            {
                _urlC.addParameter("if_member_discount", "2");
            }
            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                storeS = (storeSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(storeSuccess));
                if (storeS.data.third_code == "FAIL")
                {
                    iHttpResult = 1;
                    sHttpResult = storeS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = storeS.data.third_msg;
                    return false;
                }
                else if (storeS.data.third_code == "INPROCESS")
                {
                    iHttpResult = 2;
                    sHttpResult = storeS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = storeS.data.third_msg;
                    return false;
                }
                else
                {
                    iHttpResult = 0;
                    sHttpResult = "支付成功";
                }
                return true;
            }
            else
            {
                errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sErrorMsg1 = errorC.errMsg;
                sErrorMsg2 = errorC.errMsg;
                return false;
            }
        }
        private bool fnStoreQueryHttp(string _requesturl)
        {
            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_requesturl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                storeS = (storeSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(storeSuccess));
                if (storeS.data.third_code == "FAIL")
                {
                    iHttpResult = 1;
                    sHttpResult = storeS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = storeS.data.third_msg;
                    return false;
                }
                else if (storeS.data.third_code == "SUCCESS")
                {
                    iHttpResult = 0;
                    sHttpResult = "支付成功";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = errorC.errMsg;
                sErrorMsg1 = errorC.errMsg;
                return false;
            }
        }
        public void fnPrint(bool _ismerchant)
        {
            try
            {
                tradeprintClass _tradeprintC = new tradeprintClass(_ismerchant);
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                _tradeprintC.STRmerchantname = storeS.data.merchant_name;
                _tradeprintC.STRstorename = storeS.data.store_name;
                _tradeprintC.STRterminalnum = _lcc.readfromConfig();
                _tradeprintC.STRemployee = storeS.data.employee_name;
                _tradeprintC.STRordernum = storeS.data.order_no;
                _tradeprintC.STRpaytype = storeS.data.pay_type;
                _tradeprintC.STRorderstatus = storeS.data.order_status;
                _tradeprintC.STRpayaccount = storeS.data.pay_account;
                _tradeprintC.STRtotalmoney = storeS.data.total_money;
                _tradeprintC.STRundiscountmoney = storeS.data.undiscount_money;
                _tradeprintC.STRdiscountmoney = PublicMethods.discountmoneyFormater(storeS.data.member_discount_money, storeS.data.coupon_money, storeS.data.alipay_merchant_discount);
                _tradeprintC.STRpaymoney = storeS.data.pay_money;
                _tradeprintC.STRalipaydiscountmoney = storeS.data.alipay_discount_money;
                _tradeprintC.STRwechatdiscountmoney = storeS.data.wechat_discount_money;
                _tradeprintC.STRreceipt = storeS.data.receipt_fee;
                _tradeprintC.STRpaytime = storeS.data.pay_time;
                _tradeprintC.STRtradenum = storeS.data.flow_no;
                _tradeprintC.STRbanktradenum = storeS.data.bank_trade_no;
                _tradeprintC.STRpaytradenum = storeS.data.trade_no;
                _tradeprintC.PrintNow();
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("提示", "打印故障：" + e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
        }
    }
    public class storeData
    {
        /// <summary>
        /// 三方返回错误码
        /// </summary>
        public string third_code { get; set; }
        /// <summary>
        /// 三方返回错误信息
        /// </summary>
        public string third_msg { get; set; }
        /// <summary>
        /// 商户名
        /// </summary>
        public string merchant_name { get; set; }
        /// <summary>
        /// 门店名
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string employee_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 
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
        public string flow_no { get; set; }
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
        public List<string> refund_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receipt_fee { get; set; }
    }

    public class storeSuccess
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
        public storeData data { get; set; }
    }
}
