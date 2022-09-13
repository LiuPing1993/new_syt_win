using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class paywayControl : UserControl
    {
        PayWay payway = PayWay.barcodePay;
        string sPayWayTitle = "条码收款";
        PictureBox pbxPayWayLogo = new PictureBox();

        Color colorString = Defcolor.FontGrayColor;

        bool IsStore = false;

        public paywayControl(PayWay _selected, bool _isstore = false)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            payway = _selected;
            IsStore = _isstore;
            InitializeComponent();
            this.BackColor = Defcolor.BackColor;
        }

        private void paywayControl_Load(object sender, EventArgs e)
        {
            //paywaytitle = pubFunc.GetEnumDesc(payway);
            if(IsStore)
            {
                pbxPayWayLogo.SetBounds(30, 1, 40, 40);
            }
            else
            {
                pbxPayWayLogo.SetBounds(20, 1, 40, 40);
            }
            pbxPayWayLogo.BackgroundImageLayout = ImageLayout.Zoom;
            pbxPayWayLogo.MouseUp += new MouseEventHandler(paywayControl_MouseUp);
            pbxPayWayLogo.MouseEnter += new EventHandler(paywayControl_MouseEnter);
            pbxPayWayLogo.MouseDown += new MouseEventHandler(paywayControl_MouseDown);
            pbxPayWayLogo.MouseLeave += new EventHandler(paywayControl_MouseLeave);
            switch (payway)
            {
                case PayWay.barcodePay: sPayWayTitle = "条码收款"; pbxPayWayLogo.BackgroundImage = Properties.Resources.bar; break;
                case PayWay.qrcodePay: sPayWayTitle = "扫码收款"; pbxPayWayLogo.BackgroundImage = Properties.Resources.scan; break;
                case PayWay.cardPay: sPayWayTitle = "银联记账"; pbxPayWayLogo.BackgroundImage = Properties.Resources.card; break;
                case PayWay.cashPay: sPayWayTitle = "现金记账"; pbxPayWayLogo.BackgroundImage = Properties.Resources.cash; break;
                case PayWay.storePay: sPayWayTitle = "储值收款"; pbxPayWayLogo.BackgroundImage = Properties.Resources.store; break;
                default: break;
            }
            this.Controls.Add(pbxPayWayLogo);
        }

        private void paywayControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
            Font numPanelFont = new Font(UserClass.fontName, 10);
            SizeF sizeF = e.Graphics.MeasureString(sPayWayTitle, numPanelFont);
            string strLine = String.Format(sPayWayTitle);
            PointF strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2 + 15);
            e.Graphics.DrawString(strLine, numPanelFont, new SolidBrush(colorString), strPoint);
        }

        private void paywayControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (Parent.GetType() == typeof(gatherControl))
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("code:" + UserClass.orderInfoC.member);
                Console.WriteLine("使用会员优惠:" + UserClass.orderInfoC.UseMemberDiscount.ToString());
                Console.WriteLine("coupon:" + UserClass.orderInfoC.coupon);
                Console.WriteLine("total:" + UserClass.orderInfoC.getMoney());
                Console.WriteLine("undiscount:" + UserClass.orderInfoC.getNotDiscount());
                Console.WriteLine("-----------------------------------------------");
                fnNormalPay((gatherControl)Parent, e);
            }
            else if (Parent.GetType() == typeof(storeControl))
            {
                storepay((storeControl)Parent, e);
            }
        }

        public void fnFastPay(string code = "")
        {
            Console.WriteLine("支付渠道" + payway.ToString());
            gatherControl _gc = (gatherControl)Parent;
            UserClass.orderInfoC.payway = payway;
            string _tempmoney = UserClass.orderInfoC.getMoney();

            if (_tempmoney == "" || _tempmoney == "0.00")
            {
                _gc.fnSetFocus();
                return;
            }
            if (!_gc.fnInsertDiscountCheck(UserClass.orderInfoC.getMoney(), UserClass.orderInfoC.getNotDiscount()))
            {
                _gc.fnSetFocus();
                return;
            }
            if (UserClass.orderInfoC.getShowReceipt() == "" || UserClass.orderInfoC.getShowReceipt() == "0.00")
            {
                _gc.fnSetFocus();
                return;
            }
            cashierForm _cashierF = new cashierForm(payway);
            _cashierF.code = code;
            _cashierF.StartPosition = FormStartPosition.CenterScreen;
            _cashierF.ShowDialog();
            if (_cashierF.DialogResult == DialogResult.OK)
            {
                //支付完成
                _gc.fnReload();
            }
            ((main)_gc.Parent).gatherC.barPayC.code = "";
            ((main)_gc.Parent).gatherC.reloadPayWay(true);
            //((main)_gc.Parent).Refresh();
        }
        public void fnFastPayStore()
        {
            storeControl _sc = (storeControl)Parent;
            if (UserClass.storeInfoC.code == "" && _sc.panelMemberInfo.Controls.Count > 0)
            {
                return;
            }
            if (UserClass.storeInfoC.storetype == "")
            {
                return;
            }
            if (UserClass.storeInfoC.getstoremoney() == "" || UserClass.storeInfoC.getstoremoney() == "0.00")
            {
                return;
            }
            UserClass.storeInfoC.payway = payway;
            if (payway == PayWay.storePay)
            {
                return;
            }
            cashierForm _cashierF = new cashierForm(payway, _isstore: true);
            _cashierF.StartPosition = FormStartPosition.CenterParent;
            _cashierF.ShowDialog();
            if (_cashierF.DialogResult == DialogResult.OK)
            {
                _sc.fnReload();
            }
            ((main)_sc.Parent).Refresh();

        }
        private void paywayControl_MouseDown(object sender, MouseEventArgs e)
        {
            colorString = Defcolor.FontGrayColor;
            Refresh();
        }

        private void paywayControl_MouseEnter(object sender, EventArgs e)
        {
            colorString = Color.FromArgb(180, 180, 180);
            Refresh();
        }

        private void paywayControl_MouseLeave(object sender, EventArgs e)
        {
            colorString = Defcolor.FontGrayColor;
            Refresh();
        }

        private void fnNormalPay(gatherControl _gc, MouseEventArgs e)
        {
            BackColor = Defcolor.PayWayBackColor;
            Application.DoEvents();
            if (ClientRectangle.Contains(e.Location))
            {
                UserClass.orderInfoC.payway = payway;
                string _tempmoney = UserClass.orderInfoC.getMoney();
                if (_tempmoney == "" || _tempmoney == "0.00")
                {
                    if (payway != PayWay.cashPay)
                    {
                        _gc.fnSetFocus();
                        return;
                    }
                    if (payway == PayWay.cashPay && UserClass.orderInfoC.coupon == "")
                    {
                        _gc.fnSetFocus();
                        return;
                    }
                }
                if (!_gc.fnInsertDiscountCheck(UserClass.orderInfoC.getMoney(), UserClass.orderInfoC.getNotDiscount()))
                {
                    if (payway != PayWay.cashPay)
                    {
                        _gc.fnSetFocus();
                        return;
                    }
                }
                if (payway == PayWay.storePay)
                {
                    if (UserClass.orderInfoC.member == "")
                    {
                        _gc.fnSetFocus();
                        return;
                    }
                    try
                    {
                        double _dTempReceipt = Convert.ToDouble(UserClass.orderInfoC.getShowReceipt());
                        double _dTempStore = Convert.ToDouble(_gc.mainStoreInfoC.sStore);
                        if (_dTempReceipt > _dTempStore)
                        {
                            errorinformationForm _errorF = new errorinformationForm("提示", "储值余额不足");
                            _errorF.TopMost = true;
                            _errorF.StartPosition = FormStartPosition.CenterParent;
                            _errorF.ShowDialog();
                            _gc.fnSetFocus();
                            return;
                        }
                    }
                    catch
                    {
                        _gc.fnSetFocus();
                        return;
                    }
                }
                if (payway == PayWay.barcodePay || payway == PayWay.qrcodePay || payway == PayWay.storePay)
                {
                    if (UserClass.orderInfoC.getShowReceipt() == "" || UserClass.orderInfoC.getShowReceipt() == "0.00")
                    {
                        _gc.fnSetFocus();
                        return;
                    }
                }
                cashierForm _cashierF = new cashierForm(payway);
                _cashierF.StartPosition = FormStartPosition.CenterParent;
                _cashierF.ShowDialog();
                if (_cashierF.DialogResult == DialogResult.OK)
                {
                    _gc.fnReload();
                }
                else if (_cashierF.DialogResult == DialogResult.Cancel)
                {
                    _gc.fnClearMemberCouponSelected();
                }
                ((main)_gc.Parent).gatherC.barPayC.code = "";
                ((main)_gc.Parent).gatherC.reloadPayWay(true);
                //((main)_gc.Parent).Refresh();
            }
        }
        private void storepay(storeControl _sc, MouseEventArgs e)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine("code:" + UserClass.storeInfoC.code);
            Console.WriteLine("storetype:" + UserClass.storeInfoC.storetype);
            Console.WriteLine("money:" + UserClass.storeInfoC.getstoremoney());
            Console.WriteLine("payway:" + payway);
            Console.WriteLine("------------------------------------");

            if (UserClass.storeInfoC.code == "" && _sc.panelMemberInfo.Controls.Count > 0)
            {
                return;
            }
            if (UserClass.storeInfoC.storetype == "")
            {
                return;
            }
            if (UserClass.storeInfoC.getstoremoney() == "" || UserClass.storeInfoC.getstoremoney() == "0.00")
            {
                return;
            }
            BackColor = Defcolor.PayWayBackColor;
            Application.DoEvents();
            if (ClientRectangle.Contains(e.Location))
            {
                UserClass.storeInfoC.payway = payway;
                if (payway == PayWay.storePay)
                {
                    return;
                }
                cashierForm _cashierF = new cashierForm(payway, _isstore: true);
                _cashierF.StartPosition = FormStartPosition.CenterParent;
                _cashierF.ShowDialog();
                if (_cashierF.DialogResult == DialogResult.OK)
                {
                    _sc.fnReload();
                }
                ((main)_sc.Parent).Refresh();
            }
        }
    }
}
