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
    public partial class barcodeControl : UserControl
    {
        string sTitle = "条码收款";
        string sTip = "请扫描付款码";
        string sBarCode = "";
        string sDiscountName = "";
        string sDiscountMoney = "";
        string sErrorMsg1 = "";
        string sErrorMsg2 = "";

        barpaySuccess barpayS;
        errorClass errorC;

        confirmcancelControl cancelC = new confirmcancelControl("取消");
        confirmcancelControl confirmC = new confirmcancelControl("确认");

        barinputControl barinputC = new barinputControl();

        public delegate void addDelegate();
        public addDelegate d;
        Thread t1;
        Thread t2;
        bool IsThreadRun = false;
        bool IsQuery = false;

        int iHttpResult = 0;
        string sHttpResult = "";

        string outside_code = "";
        public barcodeControl(string code = "")
        {
            outside_code = code;

            InitializeComponent();

            cancelC.Size = new System.Drawing.Size(100, 36);
            cancelC.Location = new Point(145, 265);
            cancelC.MouseUp += new MouseEventHandler(cancel_Click);
            this.Controls.Add(cancelC);

            confirmC.Size = new System.Drawing.Size(100, 36);
            confirmC.Location = new Point(275, 265);
            confirmC.MouseUp += new MouseEventHandler(confirm_Click);
            this.Controls.Add(confirmC);

            barinputC.Location = new Point(100, 100);
            this.Controls.Add(barinputC);
            this.BackColor = Defcolor.MainBackColor;

            d = new addDelegate(fnBarPayOver);
        }
        private void barcodeControl_Load(object sender, EventArgs e) 
        {
            if (outside_code != "")
            {
                barinputC.fnInputBarcode(outside_code);
                confirm_Click(null, null);
            }
        }
        private void barcodeControl_Paint(object sender, PaintEventArgs e)
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

            myFont = new Font(UserClass.fontName, 10);
            SizeF sizeF = e.Graphics.MeasureString(sTip, myFont);
            strLine = String.Format(sTip);
            strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 160);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
        }

        #region 按键
        private bool fnFunctionKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                confirm_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Back)
            {
                if (sBarCode == "")
                {
                    return false;
                }
                else
                {
                    if (sBarCode.Length == 1)
                    {
                        sBarCode = "";
                        barinputC.fnInputBarcode(sBarCode);
                        return true;
                    }
                    sBarCode = sBarCode.Remove(sBarCode.Length - 1, 1);
                    barinputC.fnInputBarcode(sBarCode);
                    return true;
                }
            }
            else if (keyData == Keys.Escape)
            {
                cancel_Click(null, null);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool fnNumberKey(System.Windows.Forms.Keys keyData)
        {
            string _sKeyTemp = PublicMethods.KeyCodeToChar(keyData).ToString();
            if (PublicMethods.IsNumeric(_sKeyTemp))
            {
                sBarCode += _sKeyTemp;
                barinputC.fnInputBarcode(sBarCode);
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (sBarCode.Length + 1 > 25)
                {
                    if (fnFunctionKey(keyData))
                    {
                        return true;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                else
                {
                    if (fnNumberKey(keyData))
                    {
                        return true;
                    }
                    else if (fnFunctionKey(keyData))
                    {
                        return true;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #endregion

        private void cancel_Click(object sender, MouseEventArgs e)
        {
            if (IsQuery)
            {
                stopqueryForm _stopqueryF = new stopqueryForm();
                _stopqueryF.TopMost = true;
                _stopqueryF.StartPosition = FormStartPosition.Manual;
                int _x = ((cashierForm)Parent).Location.X + (((cashierForm)Parent).Size.Width - _stopqueryF.Size.Width) / 2;
                int _y = ((cashierForm)Parent).Location.Y + (((cashierForm)Parent).Size.Height - _stopqueryF.Size.Height) / 2 + 50;
                _stopqueryF.Location = new Point(_x, _y);
                _stopqueryF.ShowDialog();
                if (_stopqueryF.DialogResult == DialogResult.OK)
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
            if (barinputC.fnReturnBarcode() == "" || barinputC.fnReturnBarcode() == "付款码")
            {
                return;
            }
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnBarPayAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }
        public void fnBarPayAction()
        {
            try
            {
                fnBarPayHttp();
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sErrorMsg2 = e.Message;
            }
            Invoke(d);
            //IsThreadRun = false;
            t1.Abort();
        }
        private void fnBarQuery()
        {
            if (!IsQuery)
            {
                IsQuery = true;
                Thread thread = new Thread(fnBarQueryAction);
                thread.IsBackground = true;
                thread.Start();
                t2 = thread;
            }
        }
        private void fnBarQueryAction()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC;
                if (((cashierForm)Parent).IsStore)
                {
                    _urlC = new UrlClass(Url.storequery);
                    _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                    _urlC.addParameter("token", UserClass.Token);
                    _urlC.addParameter("order_no", barpayS.data.order_no.ToString());
                }
                else
                {
                    _urlC = new UrlClass(Url.query);
                    _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                    _urlC.addParameter("token", UserClass.Token);
                    _urlC.addParameter("order_no", barpayS.data.order_no.ToString());
                }
                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);
                while (true)
                {
                    Thread.Sleep(1000);
                    if (fnBarQueryHttp(_sRequestUrl))
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
        private void fnBarPayOver()
        {
            try
            {
                //pictureClass.Delay(1000);
                if (iHttpResult == 0)
                {
                    if (!((cashierForm)Parent).IsStore)//进行储值时不打印小票
                    {
                        fnPrint(true);
                    }
                    ((cashierForm)Parent).payresultC.fnSetParams(barpayS.data.pay_type.ToString(), sDiscountName, 0, "+" + barpayS.data.receipt_fee, sDiscountMoney);
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
                    paystatusLabel.Text = barpayS.data.third_msg.ToString();
                    fnBarQuery();
                }
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("错误(条码支付)", e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterScreen;
                _errorF.ShowDialog();
                Refresh();
            }
        }
        private bool fnBarPayHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC;
            if(((cashierForm)Parent).IsStore)
            {
                _urlC = new UrlClass(Url.storepay);
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
                _urlC.addParameter("auth_code", barinputC.fnReturnBarcode());
            }
            else
            {
                string _sCoupon = UserClass.orderInfoC.coupon;
                if (_sCoupon != "")
                {
                    _sCoupon = _sCoupon.Remove(_sCoupon.Length - 1);
                }
                _urlC = new UrlClass(Url.pay);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);
                _urlC.addParameter("total_amount", UserClass.orderInfoC.getMoney());
                _urlC.addParameter("undiscount_amount", UserClass.orderInfoC.getNotDiscount());
                _urlC.addParameter("auth_code", barinputC.fnReturnBarcode());
                _urlC.addParameter("code", UserClass.orderInfoC.member);
                if (UserClass.orderInfoC.UseMemberDiscount)
                {
                    _urlC.addParameter("if_member_discount", "1");
                }
                else
                {
                    _urlC.addParameter("if_member_discount", "2");
                }
                _urlC.addParameter("coupon_id_list", _sCoupon);
            }
            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                barpayS = (barpaySuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(barpaySuccess));
                if (barpayS.data.third_code == "FAIL")
                {
                    iHttpResult = 1;
                    sHttpResult = barpayS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = barpayS.data.third_msg;
                    return false;
                }
                else if (barpayS.data.third_code == "INPROCESS")
                {
                    iHttpResult = 2;
                    sHttpResult = barpayS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = barpayS.data.third_msg;
                    return false;
                }
                else
                {
                    iHttpResult = 0;
                    sHttpResult = "支付成功";
                    if (barpayS.data.pay_type.IndexOf("支付宝") != -1)
                    {
                        sDiscountName = "支付宝优惠";
                        sDiscountMoney = barpayS.data.alipay_discount_money;
                    }
                    else if (barpayS.data.pay_type.IndexOf("云闪付") != -1)
                    {
                        sDiscountName = "云闪付优惠";
                        sDiscountMoney = barpayS.data.ysf_discount_money;
                    }
                    else 
                    {
                        sDiscountName = "微信优惠";
                        sDiscountMoney = barpayS.data.wechat_discount_money;
                    }
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
        private bool fnBarQueryHttp(string _requesturl)
        {
            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_requesturl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                barpayS = (barpaySuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(barpaySuccess));
                if (barpayS.data.third_code == "FAIL")
                {
                    iHttpResult = 1;
                    sHttpResult = barpayS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = barpayS.data.third_msg;
                    return false;
                }
                else if (barpayS.data.third_code == "SUCCESS")
                {
                    iHttpResult = 0;
                    sHttpResult = "支付成功";
                    if (barpayS.data.pay_type.IndexOf("支付宝") != -1)
                    {
                        sDiscountName = "支付宝优惠";
                        sDiscountMoney = barpayS.data.alipay_discount_money;
                    }
                    else if (barpayS.data.pay_type.IndexOf("云闪付") != -1)
                    {
                        sDiscountName = "云闪付优惠";
                        sDiscountMoney = barpayS.data.ysf_discount_money;
                    }
                    else
                    {
                        sDiscountName = "微信优惠";
                        sDiscountMoney = barpayS.data.wechat_discount_money;
                    }
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
                _tradeprintC.STRmerchantname = barpayS.data.merchant_name;
                _tradeprintC.STRstorename = barpayS.data.store_name;
                _tradeprintC.STRterminalnum = _lcc.readfromConfig();
                _tradeprintC.STRemployee = barpayS.data.employee_name;
                _tradeprintC.STRordernum = barpayS.data.order_no;
                _tradeprintC.STRpaytype = barpayS.data.pay_type;
                _tradeprintC.STRorderstatus = barpayS.data.order_status;
                _tradeprintC.STRpayaccount = barpayS.data.pay_account;
                _tradeprintC.STRtotalmoney = barpayS.data.total_money;
                _tradeprintC.STRundiscountmoney = barpayS.data.undiscount_money;
                _tradeprintC.STRdiscountmoney = PublicMethods.discountmoneyFormater(barpayS.data.member_discount_money, barpayS.data.coupon_money, barpayS.data.alipay_merchant_discount);
                _tradeprintC.STRpaymoney = PublicMethods.cashierPrintPaymoney(barpayS.data.pay_money, barpayS.data.alipay_merchant_discount);//barpayS.data.pay_money;
                _tradeprintC.STRalipaydiscountmoney = barpayS.data.alipay_discount_money;
                _tradeprintC.STRwechatdiscountmoney = barpayS.data.wechat_discount_money;
                _tradeprintC.STRysfdiscountmoney = barpayS.data.ysf_discount_money;
                _tradeprintC.STRreceipt = barpayS.data.receipt_fee;
                _tradeprintC.STRpaytime = barpayS.data.pay_time;
                _tradeprintC.STRtradenum = barpayS.data.flow_no;
                _tradeprintC.STRbanktradenum = barpayS.data.bank_trade_no;
                _tradeprintC.STRpaytradenum = barpayS.data.trade_no;
                //t.PrintView();
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
public class RefundListItem
{
    /// <summary>
    /// 退款金额
    /// </summary>
    public string refund_money { get; set; }
    /// <summary>
    /// 操作员
    /// </summary>
    public string employee_name { get; set; }
    /// <summary>
    /// 退款时间
    /// </summary>
    public string refund_time { get; set; }
}
public class PayData
{
    /// <summary>
    /// 三方错误码
    /// </summary>
    public string third_code { get; set; }
    /// <summary>
    /// 三方错误信息
    /// </summary>
    public string third_msg { get; set; }
    /// <summary>
    /// 商户名称
    /// </summary>
    public string merchant_name { get; set; }
    /// <summary>
    /// 门店名称
    /// </summary>
    public string store_name { get; set; }
    /// <summary>
    /// 操作员
    /// </summary>
    public string employee_name { get; set; }
    /// <summary>
    /// 订单号
    /// </summary>
    public string order_no { get; set; }
    /// <summary>
    /// 订单金额
    /// </summary>
    public string total_money { get; set; }
    /// <summary>
    /// 不打折金额
    /// </summary>
    public string undiscount_money { get; set; }
    /// <summary>
    /// 订单状态
    /// </summary>
    public string order_status { get; set; }
    /// <summary>
    /// 支付类型
    /// </summary>
    public string pay_type { get; set; }
    /// <summary>
    /// 订单创建时间
    /// </summary>
    public string create_time { get; set; }
    /// <summary>
    /// 优惠券优惠金额
    /// </summary>
    public string coupon_money { get; set; }
    /// <summary>
    /// 会员折扣优惠
    /// </summary>
    public string member_discount_money { get; set; }
    /// <summary>
    /// 退款状态（能否退款）
    /// </summary>
    public string refund_status { get; set; }
    /// <summary>
    /// 支付宝商家优惠
    /// </summary>
    public string alipay_merchant_discount { get; set; }
    /// <summary>
    /// 支付宝优惠
    /// </summary>
    public string alipay_discount_money { get; set; }
    /// <summary>
    /// 微信优惠
    /// </summary>
    public string wechat_discount_money { get; set; }
    /// <summary>
    /// 微信优惠
    /// </summary>
    public string ysf_discount_money { get; set; }
    /// <summary>
    /// 支付时间
    /// </summary>
    public string pay_time { get; set; }
    /// <summary>
    /// 第三方交易流水号
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
    /// 支付账号
    /// </summary>
    public string pay_account { get; set; }
    /// <summary>
    /// 支付金额
    /// </summary>
    public string pay_money { get; set; }
    /// <summary>
    /// 实收金额
    /// </summary>
    public string receipt_fee { get; set; }
    /// <summary>
    /// 退款列表
    /// </summary>
    public List <RefundListItem> refund_list { get; set; }
}
public class barpaySuccess
{
    /// <summary>
    /// 错误码
    /// </summary>
    public string errCode { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    public string errMsg { get; set; }
    /// <summary>
    /// 支付返回数据
    /// </summary>
    public PayData data { get; set; }
}
}
