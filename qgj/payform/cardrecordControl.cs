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
    public partial class cardrecordControl : UserControl
    {
        string sTitle = "银联记账";
        string sMoneyTip = "应收金额";
        string sTip = "请使用POS机为顾客刷卡，再点确认";
        string sMoney = "";

        string sDiscountName = "";
        //string sDiscountMoney = "";
        string sErrorMsg1 = "";
        string sErrorMsg2 = "";
        cashcardSuccess cashcardS;
        errorClass errorC;

        public delegate void addDelegate();
        public addDelegate d;
        Thread t;
        bool IsThreadRun = false;

        int iHttpResult = 0;
        string sHttpResult = "";

        confirmcancelControl cancelC = new confirmcancelControl("取消");
        confirmcancelControl confirmC = new confirmcancelControl("确认");

        public cardrecordControl()
        {
            InitializeComponent();

            cancelC.Size = new System.Drawing.Size(100, 36);
            cancelC.Location = new Point(145, 275);
            cancelC.MouseUp += new MouseEventHandler(cancel_Click);
            this.Controls.Add(cancelC);

            confirmC.Size = new System.Drawing.Size(100, 36);
            confirmC.Location = new Point(275, 275);
            confirmC.MouseUp += new MouseEventHandler(confirm_Click);
            this.Controls.Add(confirmC);

            d = new addDelegate(fnCardPayOver);
        }
        private void cardrecordControl_Load(object sender, EventArgs e)
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
        private void cardrecordControl_Paint(object sender, PaintEventArgs e)
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
            if (IsThreadRun)
            {
                t.Abort();
            }
            Parent.Dispose();
        }
        private void confirm_Click(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnCardPayAction);
                thread.IsBackground = true;
                thread.Start();
                t = thread;
            }
        }
        public void fnCardPayAction()
        {
            try
            {
                fnCardPayHttp();
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sErrorMsg2 = e.ToString();
            }
            Invoke(d);
            //IsThreadRun = false;
            t.Abort();
        }
        private void fnCardPayOver()
        {
            try
            {
                //pictureClass.Delay(1000);

                if (iHttpResult == 0)
                {
                    if (!((cashierForm)Parent).IsStore)
                    {
                        fnPrint(true);
                    }
                    ((cashierForm)Parent).payresultC.fnSetParams(cashcardS.data.pay_type.ToString(), "", 0, "+" + cashcardS.data.receipt_fee, "");
                    ((cashierForm)Parent).fnShowResult(this);
                }
                else
                {
                    ((cashierForm)Parent).payresultC.fnSetParams("", sDiscountName, 1, sErrorMsg1, sErrorMsg2);
                    ((cashierForm)Parent).fnShowResult(this);
                }
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("错误(记账)", e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }

        }
        private bool fnCardPayHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC;
            if (((cashierForm)Parent).IsStore)
            {
                _urlC = new UrlClass(Url.storecashcard);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);
                _urlC.addParameter("account", UserClass.storeInfoC.code);
                if (UserClass.storeInfoC.storetype == "0")
                {
                    _urlC.addParameter("recharge_money", UserClass.storeInfoC.getstoremoney());
                }
                else
                {
                    _urlC.addParameter("activity_id", UserClass.storeInfoC.storetype);
                }
                _urlC.addParameter("pay_channel", "3");
            }
            else
            {
                string _sCoupon = UserClass.orderInfoC.coupon;
                if (_sCoupon != "")
                {
                    _sCoupon = _sCoupon.Remove(_sCoupon.Length - 1);
                }
                _urlC = new UrlClass(Url.cashcard);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);
                _urlC.addParameter("total_amount", UserClass.orderInfoC.getMoney());
                _urlC.addParameter("pay_channel", "3");
                _urlC.addParameter("undiscount_amount", UserClass.orderInfoC.getNotDiscount());
                _urlC.addParameter("coupon_id_list", _sCoupon);
                _urlC.addParameter("code", UserClass.orderInfoC.member);
                if (UserClass.orderInfoC.UseMemberDiscount)
                {
                    _urlC.addParameter("if_member_discount", "1");
                }
                else
                {
                    _urlC.addParameter("if_member_discount", "2");
                }
            }
            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                cashcardS = (cashcardSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(cashcardSuccess));
                if (cashcardS.data.third_code != "SUCCESS")
                {
                    iHttpResult = 1;
                    sHttpResult = cashcardS.data.third_msg;
                    sErrorMsg1 = cashcardS.errMsg;
                    sErrorMsg2 = cashcardS.data.third_msg;
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
        public void fnPrint(bool _ismerchant)
        {
            try
            {
                tradeprintClass _tradeprintC = new tradeprintClass(_ismerchant);
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                _tradeprintC.STRmerchantname = cashcardS.data.merchant_name;
                _tradeprintC.STRstorename = cashcardS.data.store_name;
                _tradeprintC.STRterminalnum = _lcc.readfromConfig();
                _tradeprintC.STRemployee = cashcardS.data.employee_name;
                _tradeprintC.STRordernum = cashcardS.data.order_no;
                _tradeprintC.STRpaytype = cashcardS.data.pay_type;
                _tradeprintC.STRorderstatus = cashcardS.data.order_status;
                _tradeprintC.STRpayaccount = cashcardS.data.pay_account;
                _tradeprintC.STRtotalmoney = cashcardS.data.total_money;
                _tradeprintC.STRundiscountmoney = cashcardS.data.undiscount_money;
                _tradeprintC.STRdiscountmoney = PublicMethods.discountmoneyFormater(cashcardS.data.member_discount_money, cashcardS.data.coupon_money, "0");
                _tradeprintC.STRpaymoney = cashcardS.data.pay_money;
                _tradeprintC.STRalipaydiscountmoney = "";
                _tradeprintC.STRreceipt = cashcardS.data.receipt_fee;
                _tradeprintC.STRpaytime = cashcardS.data.pay_time;
                _tradeprintC.STRtradenum = cashcardS.data.flow_no;
                _tradeprintC.STRbanktradenum = cashcardS.data.bank_trade_no;
                _tradeprintC.STRpaytradenum = cashcardS.data.trade_no;
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
    public class cashcardData
    {
        /// <summary>
        /// 
        /// </summary>
        public string third_code { get; set; }
        /// <summary>
        /// 龙虾店
        /// </summary>
        public string merchant_name { get; set; }
        /// <summary>
        /// 一分店
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 李四
        /// </summary>
        public string employee_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_no { get; set; }
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
        public string alipay_merchant_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receipt_fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string create_time { get; set; }
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
        public string third_msg { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 优惠券优惠
        /// </summary>
        public string coupon_money { get; set; }
        /// <summary>
        /// 会员折扣
        /// </summary>
        public string member_discount_money { get; set; }

    }
    public class cashcardSuccess
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
        public cashcardData data { get; set; }
    }
}
