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
    public partial class cashrecordControl : UserControl
    {
        string sTitle = "现金记账";
        string sInputMoney = "";
        string sChangeMoney = "0.00";
        string sMoneyTip = "应收金额";
        string sMoney = "";
        string sTip = "请向顾客收取现金，再点击确定";
        string sChangeTip = "找零金额";

        confirmcancelControl cancelC = new confirmcancelControl("取消");
        confirmcancelControl confirmC = new confirmcancelControl("确认");

        cashiermoneyinputControl cashiermoneyinputC = new cashiermoneyinputControl();

        Label lblChange = new Label();

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

        public cashrecordControl()
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

            cashiermoneyinputC.Location = new Point(100, 100);
            this.Controls.Add(cashiermoneyinputC);

            lblChange.SetBounds(100, 150, 300, 30);
            lblChange.Text = sChangeTip + sChangeMoney;
            lblChange.Font = new Font(UserClass.fontName, 12);
            lblChange.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblChange);

            this.BackColor = Defcolor.MainBackColor;

            d = new addDelegate(fnCashPayOver);
        }

        private void cashrecordControl_Load(object sender, EventArgs e)
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

        private void cashrecordControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
               Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
           );

            Font myFont = new Font(UserClass.fontName, 12);
            string strLine = String.Format(sTitle);
            PointF strPoint = new PointF(20, 30);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            myFont = new Font(UserClass.fontName, 12, FontStyle.Bold);
            SizeF sizeF = e.Graphics.MeasureString(sMoneyTip + " " + sMoney, myFont);
            strLine = String.Format(sMoneyTip);
            strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, 70);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = String.Format(sMoney);
            sizeF = e.Graphics.MeasureString(sMoneyTip, myFont);
            strPoint = new PointF(strPoint.X + sizeF.Width, 70);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), strPoint);

            myFont = new Font(UserClass.fontName, 12);
            strLine = String.Format(sTip);
            sizeF = e.Graphics.MeasureString(sTip, myFont);
            strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2 + 70);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

        }
        private bool fnCashierMoneyInput(System.Windows.Forms.Keys _keyData)
        {
            string keytemp = PublicMethods.KeyCodeToChar(_keyData).ToString();
            if (PublicMethods.moneyCheck(sInputMoney + keytemp))
            {
                if (sInputMoney == "" && (keytemp == "." || keytemp == "0"))
                {
                    keytemp = "0.";
                }
                else if(sInputMoney == "0.0" && keytemp == "0")
                {
                    return false;
                }
                sInputMoney += keytemp;
                cashiermoneyinputC.inputCashiermoney(sInputMoney);
                return true;
            }
            else if (_keyData == Keys.Back)
            {
                if (sInputMoney == "")
                {
                    return false;
                }
                else if (sInputMoney == "0.")
                {
                    sInputMoney = "";
                    cashiermoneyinputC.inputCashiermoney("0.00");
                    return true;
                }
                else
                {
                    if (sInputMoney.Length == 1)
                    {
                        sInputMoney = "";
                        cashiermoneyinputC.inputCashiermoney("0.00");
                        return true;
                    }
                    sInputMoney = sInputMoney.Remove(sInputMoney.Length - 1, 1);
                    cashiermoneyinputC.inputCashiermoney(sInputMoney);
                    return true;
                }
            }
            else if (_keyData == Keys.Enter)
            {
                confirm_Click(null, null);
                return true;
            }
            else if (_keyData == Keys.Escape)
            {
                cancel_Click(null, null);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool moneycheck()
        {
            try
            {
                double inputmoney = Convert.ToDouble(sInputMoney);
                double getmoney = Convert.ToDouble(sMoney) + 100;
                if (inputmoney <= getmoney)
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
        public void fnCalculateAndShow()
        {
            try
            {
                double _dAllMoney;
                if (((cashierForm)Parent).IsStore)
                {
                    _dAllMoney = Convert.ToDouble(UserClass.storeInfoC.getstoremoney());
                }
                else
                {
                    _dAllMoney = Convert.ToDouble(UserClass.orderInfoC.getShowReceipt());
                }
                
                double _dInputMoney = Convert.ToDouble(sInputMoney);
                if (_dInputMoney > _dAllMoney)
                {
                    sChangeMoney = Math.Round(_dInputMoney - _dAllMoney, 2).ToString();
                    
                }
                else
                {
                    sChangeMoney = "0.00";
                }
                lblChange.Text = sChangeTip + PublicMethods.moneyFormater(sChangeMoney);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (fnCashierMoneyInput(keyData))
                {
                    fnCalculateAndShow();
                    return true;
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return base.ProcessCmdKey(ref msg, keyData);
            }
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

                if(!moneycheck())
                {
                    using(errorinformationForm _errorF = new errorinformationForm("计算提示", "收款金额不能大于应收金额+100元"))
                    {
                        _errorF.TopMost = true;
                        _errorF.StartPosition = FormStartPosition.CenterParent;
                        _errorF.ShowDialog();
                    }
                     Refresh();
                    return;
                }

                IsThreadRun = true;
                Thread thread = new Thread(fnCashPayAction);
                thread.IsBackground = true;
                thread.Start();
                t = thread;
            }
        }
        public void fnCashPayAction()
        {
            try
            {
                fnCashPayHttp();
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

        private void fnCashPayOver()
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
                errorinformationForm _errorF = new errorinformationForm("错误(记账2)", e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
        }
        private bool fnCashPayHttp()
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
                _urlC.addParameter("pay_channel", "4");
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
                _urlC.addParameter("pay_channel", "4");
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
                sErrorMsg2 = "";//errorp.errMsg;
                return false;
            }
        }

        public void print(bool _ismerchant)
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
}
