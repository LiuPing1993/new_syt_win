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
    public partial class scancodeControl : UserControl
    {
        PictureBox pbxQrcode = new PictureBox();

        confirmcancelControl cancelC = new confirmcancelControl("取消");
        printControl printC = new printControl("打印");

        precreateSuccess precreateS;
        querySuccess queryS;
        errorClass errorC;

        string sTitle = "扫码收款";
        string sMoneyTip = "应收金额";
        string sTip = "请用户使用支付宝或微信扫描并付款";
        string sMoney = "";

        public delegate void addDelegate();
        public addDelegate d;
        Thread t;
        bool IsThreadRun = false;

        int iHttpResult = 0;
        string sHttpResult = "";
        string sDiscountName = "";
        string sDiscountMoney = "";
        string sErrorMsg1 = "";
        string sErrorMsg2 = "";

        Image imageQrcode;
        public scancodeControl()
        {
            InitializeComponent();

            cancelC.Location = new Point(180, 275);
            cancelC.MouseUp += new MouseEventHandler(cancel_Click);
            this.Controls.Add(cancelC);

            printC.Location = new Point(260, 275);
            printC.MouseUp += new MouseEventHandler(print_Click);
            this.Controls.Add(printC);

            pbxQrcode.SetBounds((ClientRectangle.Width - 150) / 2, (ClientRectangle.Height - 150) / 2 - 15, 150, 150);
            //qrcodePictureBox.BackgroundImageLayout = ImageLayout.Tile;
            pbxQrcode.BackColor = Defcolor.MainBackColor;
            this.Controls.Add(pbxQrcode);
            this.BackColor = Defcolor.MainBackColor;

            d = new addDelegate(fnScanTradeOver);

        }
        private void scancodeControl_Load(object sender, EventArgs e)
        {
            fnPrecreateAction();
            if (iHttpResult == 1)
            {
                errorinformationForm _infoF = new errorinformationForm("失败", sHttpResult);
                _infoF.StartPosition = FormStartPosition.CenterParent;
                _infoF.TopMost = true;
                _infoF.ShowDialog();
            }
            else
            {
                try
                {
                    zxingClass _zxingC = new zxingClass(150, 150);
                    imageQrcode = _zxingC.getQrcode(precreateS.data.qr_code.ToString());
                    pbxQrcode.BackgroundImage = imageQrcode;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString());
                }
                fnScanQuery();
            }
            if (((cashierForm)Parent).IsStore)
            {
                sMoney = UserClass.storeInfoC.getstoremoney();
            }
            else
            {
                sMoney = UserClass.orderInfoC.getShowReceipt();
            }
        }
        private void fnScanQuery()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnScanQueryAction);
                thread.IsBackground = true;
                thread.Start();
                t = thread;
            }
        }
        private void fnPrecreateAction()
        {
            try
            {
                fnPrecreateHttp();
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
        }
        private void fnScanQueryAction()
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
                    _urlC.addParameter("order_no", precreateS.data.order_no.ToString());
                }
                else
                {
                    _urlC = new UrlClass(Url.query);
                    _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                    _urlC.addParameter("token", UserClass.Token);
                    _urlC.addParameter("order_no", precreateS.data.order_no.ToString());
                }
                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                while (true)
                {
                    Thread.Sleep(1000);
                    if (fnScanQueryHttp(_sRequestUrl))
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sErrorMsg1 = e.ToString();
            }
            Invoke(d);
            IsThreadRun = false;
            t.Abort();
        }
        private void fnScanTradeOver()
        {
            try
            {
                //pictureClass.Delay(1000);

                if (iHttpResult == 0)
                {
                    if (!((cashierForm)Parent).IsStore)
                    {
                        print(true);
                    }
                    ((cashierForm)Parent).payresultC.fnSetParams(queryS.data.pay_type.ToString(), sDiscountName, 0, "+" + queryS.data.receipt_fee, sDiscountMoney);
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
                //PublicMethods.WriteLog(e);
                errorinformationForm _errorF = new errorinformationForm("错误(扫码支付)", e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterScreen;
                _errorF.ShowDialog();
                Refresh();
            }
        }
        private bool fnPrecreateHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC;
            if (((cashierForm)Parent).IsStore)
            {
                _urlC = new UrlClass(Url.storeprecreate);
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
            }
            else
            {
                string _sCoupon = UserClass.orderInfoC.coupon;
                if (_sCoupon != "")
                {
                    _sCoupon = _sCoupon.Remove(_sCoupon.Length - 1);
                }
                _urlC = new UrlClass(Url.precreate);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);
                _urlC.addParameter("total_amount", UserClass.orderInfoC.getMoney());
                _urlC.addParameter("undiscount_amount", UserClass.orderInfoC.getNotDiscount());
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
                precreateS = (precreateSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(precreateSuccess));
                if (precreateS.data.third_code == "FAIL")
                {
                    iHttpResult = 1;
                    sHttpResult = precreateS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = precreateS.data.third_msg;
                    return false;
                }
                else
                {
                    return true;
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
        private bool fnScanQueryHttp(string _requesturl)
        {

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_requesturl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                queryS = (querySuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(querySuccess));
                if (queryS.data.third_code == "FAIL")
                {
                    iHttpResult = 1;
                    sHttpResult = queryS.data.third_msg;
                    //errormsg1 = p.errMsg;
                    sErrorMsg2 = queryS.data.third_msg;
                    return false;
                }
                else if (queryS.data.third_code == "SUCCESS")
                {
                    iHttpResult = 0;
                    sHttpResult = "支付成功";
                    if (queryS.data.pay_type.IndexOf("支付宝") != -1)
                    {
                        sDiscountName = "支付宝优惠";
                        sDiscountMoney = queryS.data.alipay_discount_money;
                    }
                    else
                    {
                        sDiscountName = "微信优惠";
                        sDiscountMoney = queryS.data.wechat_discount_money;
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
        private void scancodeControl_Paint(object sender, PaintEventArgs e)
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
            strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 40);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = String.Format(sMoney);
            sizeF = e.Graphics.MeasureString(sMoneyTip, myFont);
            strPoint = new PointF(strPoint.X + sizeF.Width, 40);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), strPoint);

            myFont = new Font(UserClass.fontName, 12);
            strLine = String.Format(sTip);
            sizeF = e.Graphics.MeasureString(sTip, myFont);
            strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, pbxQrcode.Bounds.Bottom + 15);
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
                //print_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void cancel_Click(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                ((cashierForm)Parent).Dispose();
                return;
            }
            stopqueryForm stopquery = new stopqueryForm();
            stopquery.TopMost = true;
            stopquery.StartPosition = FormStartPosition.Manual;
            int x = ((cashierForm)Parent).Location.X + (((cashierForm)Parent).Size.Width - stopquery.Size.Width) / 2;
            int y = ((cashierForm)Parent).Location.Y + (((cashierForm)Parent).Size.Height - stopquery.Size.Height) / 2 + 50;
            stopquery.Location = new Point(x, y);
            stopquery.ShowDialog();
            if (stopquery.DialogResult == DialogResult.OK)
            {
                if (IsThreadRun)
                {
                    t.Abort();
                }
                ((cashierForm)Parent).DialogResult = DialogResult.Cancel;
                ((cashierForm)Parent).Dispose();
            }
            else
            {
                Refresh();
            }
        }
        private void print_Click(object sender, MouseEventArgs e)
        {
            try
            {
                qrpayprintClass _qrpayprintC = new qrpayprintClass(false);
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                _qrpayprintC.IshandPrint = true;
                _qrpayprintC.STRmerchantname = precreateS.data.merchant_name;
                _qrpayprintC.STRstorename = precreateS.data.store_name;
                _qrpayprintC.STRterminalnum = _lcc.readfromConfig();
                _qrpayprintC.STRemployee = precreateS.data.employee_name;
                _qrpayprintC.STRordertime = DateTime.Now.ToString();
                _qrpayprintC.STRordermoney = precreateS.data.total_money;
                _qrpayprintC.STRpaymoney = precreateS.data.pay_money;
                _qrpayprintC.qrcodeImage = imageQrcode;
                _qrpayprintC.PrintNow();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
        }
        public void print(bool _ismerchant)
        {
            try
            {
                tradeprintClass _tradeprintC = new tradeprintClass(_ismerchant);
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                _tradeprintC.STRmerchantname = queryS.data.merchant_name;
                _tradeprintC.STRstorename = queryS.data.store_name;
                _tradeprintC.STRterminalnum = _lcc.readfromConfig();
                _tradeprintC.STRemployee = queryS.data.employee_name;
                _tradeprintC.STRordernum = queryS.data.order_no;
                _tradeprintC.STRpaytype = queryS.data.pay_type;
                _tradeprintC.STRorderstatus = queryS.data.order_status;
                _tradeprintC.STRpayaccount = queryS.data.pay_account;
                _tradeprintC.STRtotalmoney = queryS.data.total_money;
                _tradeprintC.STRundiscountmoney = queryS.data.undiscount_money;
                _tradeprintC.STRdiscountmoney = PublicMethods.discountmoneyFormater(queryS.data.member_discount_money, queryS.data.coupon_money, queryS.data.alipay_merchant_discount);
                _tradeprintC.STRpaymoney = PublicMethods.cashierPrintPaymoney(queryS.data.pay_money, queryS.data.alipay_merchant_discount);
                _tradeprintC.STRalipaydiscountmoney = queryS.data.alipay_discount_money;
                _tradeprintC.STRwechatdiscountmoney = queryS.data.wechat_discount_money;
                _tradeprintC.STRreceipt = queryS.data.receipt_fee;
                _tradeprintC.STRpaytime = queryS.data.pay_time;
                _tradeprintC.STRtradenum = queryS.data.flow_no;
                _tradeprintC.STRbanktradenum = queryS.data.bank_trade_no;
                _tradeprintC.STRpaytradenum = queryS.data.trade_no;
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
    public class precreateData
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
        public string undiscountable_money { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 二维码地址
        /// </summary>
        public string qr_code { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string terminal_sn { get; set; }
    }

    public class precreateSuccess
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
        /// 数据内容
        /// </summary>
        public precreateData data { get; set; }
    }

    public class queryData
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
        /// 下单时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 优惠券相关优惠的金额
        /// </summary>
        public string coupon_money { get; set; }
        /// <summary>
        /// 会员折扣优惠的金额
        /// </summary>
        public string member_discount_money { get; set; }
        /// <summary>
        /// 能否退款
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
        /// 支付时间
        /// </summary>
        public string pay_time { get; set; }
        /// <summary>
        /// 交易号
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
        /// 实付金额
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public string receipt_fee { get; set; }
        /// <summary>
        /// 退款列表
        /// </summary>
        public List<Refund_listItem> refund_list { get; set; }
    }

    public class querySuccess
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
        /// 数据
        /// </summary>
        public queryData data { get; set; }
    }
}
