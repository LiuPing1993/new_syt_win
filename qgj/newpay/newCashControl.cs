using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj.newpay
{
    public partial class newCashControl : baseRequestControl
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

        cashierForm cashierF;
        toPay t_pay;
        toCreate t_create;
        bool start_pay = false;
        public newCashControl(cashierForm cashierF)
        {
            this.cashierF = cashierF;

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
        }

        private void newCashControl_Load(object sender, EventArgs e)
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

        private void newCashControl_Paint(object sender, PaintEventArgs e)
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
                else if (sInputMoney == "0.0" && keytemp == "0")
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
            if (start_pay) return;
            if (!IsThreadRun)
            {

                if (!moneycheck())
                {
                    using (errorinformationForm _errorF = new errorinformationForm("计算提示", "收款金额不能大于应收金额+100元"))
                    {
                        _errorF.TopMost = true;
                        _errorF.StartPosition = FormStartPosition.CenterParent;
                        _errorF.ShowDialog();
                    }
                    Refresh();
                    return;
                }
                create();
            }
        }

        //开始创建订单
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
                    if (t_pay.iRHttp == 0)
                    {
                        //todo 现金记账 支付接口
                    }
                    else
                    {
                        cashierF.payresultC.fnSetParams("",
                           "",
                           1,
                           "创建订单失败",
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
    }
}
