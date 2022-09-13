using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj.newpay
{
    public partial class newBarPayControl : baseRequestControl
    {
        string sTitle = "条码收款";
        string sTip = "请扫描用户付款码";
        string sBarCode = "";
        confirmcancelControl cancelC = new confirmcancelControl("取消");
        confirmcancelControl confirmC = new confirmcancelControl("确认");
        barinputControl barinputC = new barinputControl();
        string outside_code = "";
        public bool is_store = false;
        cashierForm cashierF;

        toPay t_pay;
        toQuery t_query;
        toCreate t_create;

        queryData printData;

        bool start_pay = false;
        public newBarPayControl(cashierForm cashierF ,string code = "")
        {
            outside_code = code;
            this.cashierF = cashierF;

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
        }

        private void newBarPayControl_Load(object sender, EventArgs e)
        {
            if (outside_code != "")
            {
                barinputC.fnInputBarcode(outside_code);
                confirm_Click(null, null);
            }
        }

        private void newBarPayControl_Paint(object sender, PaintEventArgs e)
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

        private void confirm_Click(object sender, MouseEventArgs e)
        {
            if (barinputC.fnReturnBarcode() == "" || barinputC.fnReturnBarcode() == "付款码")
            {
                return;
            }
            if (start_pay) return;
            create();
        }

        private void cancel_Click(object sender, MouseEventArgs e)
        {
            try
            {
                if (IsThreadRun)
                {
                    stopqueryForm stopqueryF = new stopqueryForm();
                    stopqueryF.TopMost = true;
                    stopqueryF.StartPosition = FormStartPosition.CenterScreen;
                    stopqueryF.ShowDialog();
                    if (stopqueryF.DialogResult == DialogResult.OK)
                    {
                        base.t.Abort();
                        cashierF.DialogResult = DialogResult.Cancel;
                        cashierF.Dispose();
                    }
                    else
                    {
                        Refresh();
                    }
                }
                else
                {
                    cashierF.Dispose();
                }
            }
            catch { }
            
        }

        public void create()
        {
            if (!IsThreadRun)
            {
                t_create = new toCreate();
                t_create.trade_money = yuan2cent(UserClass.orderInfoC.getMoney());
                t_create.undiscount_money = yuan2cent(UserClass.orderInfoC.getNotDiscount());
                d = new addDelegate(result_create);
                start(t_create);
                start_pay = true;
            }
        }

        public void result_create()
        {
            try
            {
                if (iRThread == 0)
                {
                    if (t_create.iRHttp == 0)
                    {
                        pay(t_create.resultS.data.order.order_no, t_create.resultS.data.order.trade_money, t_create.resultS.data.collection_account);
                    }
                    else
                    {
                        cashierF.payresultC.fnSetParams("",
                           "",
                           1,
                           "创建订单失败",
                           t_create.sRHttp
                           );
                        cashierF.fnShowResult(this);
                    }
                }
                else if (iRThread == 1)
                {
                    cashierF.payresultC.fnSetParams("",
                            "",
                            1,
                            "创建订单失败",
                            sRThread
                            );
                    cashierF.fnShowResult(this);
                }
            }
            catch (Exception e)
            {
                cashierF.payresultC.fnSetParams("",
                            "",
                            1,
                            "创建订单失败",
                            "内部错误"
                            );
                cashierF.fnShowResult(this);
                PublicMethods.WriteLog(e);
            }
        }

        public void pay(string order_no, string money, string collection_account)
        {
            if (!IsThreadRun)
            {
                t_pay = new toPay();
                t_pay.auth_code = barinputC.fnReturnBarcode();
                t_pay.out_order_no = order_no;
                t_pay.collection_account = collection_account;
                t_pay.pay_money = money;
                d = new addDelegate(result_pay);
                start(t_pay);
            }
        }
        public void result_pay()
        {
            try
            {
                if (iRThread == 0)
                {
                    if (t_pay.iRHttp == 0)
                    {
                        string pay_channel_name = getPayChannelName(t_pay.resultS.data.pay_channel, t_pay.resultS.data.pay_channel_type);

                        //查询
                        query(t_pay.resultS.data.out_order_no);
                        
                    }
                    else
                    {
                        cashierF.payresultC.fnSetParams("",
                           "",
                           1,
                           "交易失败",
                           t_pay.sRHttp
                           );
                        cashierF.fnShowResult(this);
                    }
                }
                else if (iRThread == 1)
                {
                    cashierF.payresultC.fnSetParams("",
                            "",
                            1,
                            "交易失败",
                            sRThread
                            );
                    cashierF.fnShowResult(this);
                }
            }
            catch (Exception e)
            {
                cashierF.payresultC.fnSetParams("",
                            "",
                            1,
                            "交易失败",
                            "内部错误"
                            );
                cashierF.fnShowResult(this);
                PublicMethods.WriteLog(e);
            }
        }

        public void query(string order_no)
        {
            if (!IsThreadRun)
            {
                paystatusLabel.Text = "等待用户确认支付";
                t_query = new toQuery(order_no);
                d = new addDelegate(query_success);
                start(t_query);
            }
        }

        public void query_success()
        {
            try
            {
                if (iRThread == 0)
                {
                    if (t_query.iRHttp == 0)
                    {
                        paystatusLabel.Text = "";
                        cashierF.payresultC.fnSetParams(
                            getPayChannelName(t_query.resultS.data.pay_channel, t_query.resultS.data.pay_channel_type),
                            t_query.discount_name,
                            0,
                            "+" + cent2yuan(t_query.resultS.data.trade_money),
                           cent2yuan(t_query.discount_money)
                            );
                        //查询到支付成功 并 打印
                        printData = t_query.resultS.data;
                        fnPrint(true);
                        cashierF.fnShowResult(this);
                    }
                    else
                    {
                        cashierF.payresultC.fnSetParams("", 
                            "",
                            1,
                            "查询失败",
                            t_query.sRHttp
                            );
                        cashierF.fnShowResult(this);
                    }
                }
                else if (iRThread == 1)
                {
                    cashierF.payresultC.fnSetParams("",
                            "",
                            1,
                            "交易失败",
                            sRThread
                            );
                    cashierF.fnShowResult(this);
                }
            }
            catch (Exception e)
            {
                cashierF.payresultC.fnSetParams("",
                            "",
                            1,
                            "交易失败",
                            "内部错误"
                            );
                cashierF.fnShowResult(this);
                PublicMethods.WriteLog(e);
            }
        }

        public void fnPrint(bool _ismerchant)
        {
            try
            {
                tradeprintClass printC = new tradeprintClass(_ismerchant);
                printC.STRmerchantname = BaseValue.merchant_name;
                printC.STRstorename = BaseValue.store_name;
                printC.STRterminalnum = "";
                printC.STRemployee = BaseValue.employee_name;
                printC.STRordernum = printData.order_no;
                printC.STRpaytype = getPayChannelName(printData.pay_channel, printData.pay_channel_type);
                printC.STRorderstatus = printData.order_status == "2" ? "交易成功" : "交易未完成";
                printC.STRpayaccount = "";
                printC.STRtotalmoney = t_create.trade_money;
                printC.STRundiscountmoney = t_create.undiscount_money;
                printC.STRdiscountmoney = countDiscountMoney(t_create.resultS.data.order.member_discount_money, t_create.resultS.data.order.coupon_discount_money, printData.merchant_discount_money);
                printC.STRpaymoney = printData.trade_money;
                printC.STRalipaydiscountmoney = printData.pay_channel == "1" ? cent2yuan(printData.discount_money) : "";
                printC.STRwechatdiscountmoney = printData.pay_channel == "2" ? cent2yuan(printData.discount_money) : "";
                printC.STRreceipt = printData.trade_money;
                printC.STRpaytime = printData.create_time;
                printC.STRtradenum = "";// printData.flow_no;
                printC.STRbanktradenum = "";
                printC.STRpaytradenum = printData.out_trade_no;
                printC.PrintNow();
            }
            catch (Exception e)
            {
                errorinformationForm errorF = new errorinformationForm("提示", "打印故障：" + e.ToString());
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterParent;
                errorF.ShowDialog();
                Refresh();
            }
        }

    }
}
