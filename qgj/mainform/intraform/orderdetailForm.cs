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
    public partial class orderdetailForm : Form
    {

        string sTitle = "订单详情";
        string sStore = "";
        string sMerchant = "";
        string sPayAccount = "";
        string sOrderNum = "";
        string sPayTime = "";
        string sTotalMoney = "";
        string sUndiscountMoney = "";
        string sDiscountMoney = "";
        string sPayMoney = "";
        string sReceipt = "";
        string sOrderStatus = "";

        confirmcancelControl printC = new confirmcancelControl("打印");
        confirmcancelControl refundC = new confirmcancelControl("退款");
        confirmcancelControl refrashC = new confirmcancelControl("刷新");

        int iHttpResult = 0;
        string sHttpResult = "";

        orderdetailSuccess orderdetailS;

        Panel panelRefundDetailArea = new Panel();

        bool IsRefund = false;

        public orderdetailForm(string _insert)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sOrderNum = _insert;

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            refundC.Location = new Point(490, 445);
            refundC.MouseUp += new MouseEventHandler(refund_MouseUp);
            Controls.Add(refundC);

            printC.Location = new Point(570, 445);
            printC.MouseUp += new MouseEventHandler(print_MouseUp);
            Controls.Add(printC);

            refrashC.Location = new Point(650, 445);
            refrashC.MouseUp += new MouseEventHandler(refreshorder_MouseUp);
            Controls.Add(refrashC);

            panelRefundDetailArea.AutoScroll = true;
            panelRefundDetailArea.SetBounds(365, 135, 369, 280);
            panelRefundDetailArea.BackColor = Defcolor.MainBackColor;
            Controls.Add(panelRefundDetailArea);
        }
        private void orderdetailForm_Load(object sender, EventArgs e)
        {
            fnOrderDetailFirstLoadAction();

            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
            else
            {
                fnAddRefundlist();
            }
            fnSetDetailMouse();
        }
        private void fnOrderDetailFirstLoadAction()
        {
            try
            {
                if (orderdetailhttp())
                {
                    sStore = orderdetailS.data.store_name;
                    sMerchant = orderdetailS.data.merchant_name;
                    sPayAccount = orderdetailS.data.user_name != null ? orderdetailS.data.user_name : "";
                    sPayTime = orderdetailS.data.pay_time;
                    sTotalMoney = orderdetailS.data.total_money;
                    sUndiscountMoney = orderdetailS.data.undiscount_money;
                    sDiscountMoney = PublicMethods.discountmoneyFormater(orderdetailS.data.member_discount_money, orderdetailS.data.coupon_money, orderdetailS.data.alipay_merchant_discount);
                    sPayMoney = orderdetailS.data.pay_money;
                    sReceipt = orderdetailS.data.receipt_fee;
                    sOrderStatus = orderdetailS.data.order_status;
                    if (orderdetailS.data.refund_status != "OPEN")
                    {
                        refundC.Hide();
                        Refresh();
                    }
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }

        }
        private bool orderdetailhttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.orderdetail);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("order_no", sOrderNum);


            string _RequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _RequestUrl);

            HttpClass _httpC = new HttpClass();
            string _RequestMsg = _httpC.HttpGet(_RequestUrl);
            Console.WriteLine("result:" + _RequestMsg);
            if (_RequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                orderdetailS = (orderdetailSuccess)JsonConvert.DeserializeObject(_RequestMsg, typeof(orderdetailSuccess));
                iHttpResult = 0; ;
                sHttpResult = "请求成功";
                return true;
            }
            else
            {
                errorClass _errorS = (errorClass)JsonConvert.DeserializeObject(_RequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = _errorS.errMsg;
                return false;
            }
        }
        private void orderdetailForm_Paint(object sender, PaintEventArgs e)
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
            PointF strPoint = new PointF(18, 15);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(350, 47), new Point(350, 490));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(20, 167), new Point(330, 167));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(370, 127), new Point(715, 127));

            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(700, 20), new Point(710, 30));
            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(700, 30), new Point(710, 20));

            myFont = new Font(UserClass.fontName, 9);

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Far;
            drawFormat.LineAlignment = StringAlignment.Near;

            strLine = string.Format("商户名称");
            strPoint = new PointF(20, 65);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sMerchant);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, 65, 255, 25), drawFormat);

            strLine = string.Format("门店名称");
            strPoint = new PointF(20, 100);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sStore);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, 100, 255, 25),drawFormat);

            if (sPayAccount != "")
            {
                strLine = string.Format("会员名");
                strPoint = new PointF(20, 135);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
                strLine = string.Format(sPayAccount);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, 135, 255, 25), drawFormat);
            }

            int startY = 185;
            strLine = string.Format("订单号");
            strPoint = new PointF(20, startY);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sOrderNum);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);

            startY += 35;
            strLine = string.Format("下单时间");
            strPoint = new PointF(20, startY);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sPayTime);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);

            startY += 35;
            strLine = string.Format("订单金额");
            strPoint = new PointF(20, startY);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sTotalMoney);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);

            if (sUndiscountMoney != "" && sUndiscountMoney != "0.00")
            {
                startY += 35;
                strLine = string.Format("不打折金额");
                strPoint = new PointF(20, startY);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
                strLine = string.Format(sUndiscountMoney);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);
            }
            if (sDiscountMoney != "" && sDiscountMoney != "0.00")
            {
                startY += 35;
                strLine = string.Format("优惠金额");
                strPoint = new PointF(20, startY);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
                strLine = string.Format(sDiscountMoney);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);
            }
            if (sPayMoney != "" && sPayMoney != "0.00")
            {
                startY += 35;
                strLine = string.Format("应收金额");
                strPoint = new PointF(20, startY);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
                strLine = string.Format(sPayMoney);
                e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);
            }

            strLine = string.Format("实收金额");
            strPoint = new PointF(370, 65);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            myFont = new System.Drawing.Font(UserClass.fontName, 16);
            strLine = string.Format(sReceipt);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Color.Black), new Rectangle(435, 65, 280, 40), drawFormat);

            myFont = new System.Drawing.Font(UserClass.fontName, 9);
            strLine = string.Format(sOrderStatus);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(435, 100, 280, 25), drawFormat);
        }
        private void orderdetailForm_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle _rectClose = new Rectangle(700, 20, 10, 10);
            if (_rectClose.Contains(e.Location))
            {
                if (IsRefund)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                Dispose();
            }
        }
        private void fnAddRefundlist()
        {
            try
            {
                int _iRefundItem = Convert.ToInt32(orderdetailS.data.refund_list.Count);
                string _sTemp = "";

                if (orderdetailS.data.alipay_discount_money != "")
                {
                    _sTemp = "支付宝优惠 " + orderdetailS.data.alipay_discount_money;
                }
                else if (orderdetailS.data.wechat_discount_money != "")
                {
                    _sTemp = "微信优惠 " + orderdetailS.data.wechat_discount_money;
                }
                else if (orderdetailS.data.ysf_discount_money != "")
                {
                    _sTemp = "微信优惠 " + orderdetailS.data.ysf_discount_money;
                }
                refunddetailControl _refundDetailC = new refunddetailControl(orderdetailS.data.pay_time + " " + orderdetailS.data.pay_type, orderdetailS.data.pay_money, "操作员 " + orderdetailS.data.employee_name, _sTemp, false);
                _refundDetailC.Location = new Point(0, 0);
                _refundDetailC.sTradeNo1 = orderdetailS.data.flow_no;
                _refundDetailC.sTradeNo2 = orderdetailS.data.bank_trade_no;
                _refundDetailC.sTradeNo3 = orderdetailS.data.trade_no;
                _refundDetailC.setHeight();
                panelRefundDetailArea.Controls.Add(_refundDetailC);

                refunddetailControl[] refunddetailC = new refunddetailControl[_iRefundItem];

                for (int i = 0; i < _iRefundItem; i++) 
                {
                    refunddetailC[i] = new refunddetailControl(orderdetailS.data.refund_list[i].refund_time + " " + orderdetailS.data.pay_type, orderdetailS.data.refund_list[i].refund_money, "操作员 " + orderdetailS.data.refund_list[i].employee_name, "", true);
                    refunddetailC[i].sTradeNo1 = orderdetailS.data.refund_list[i].flow_no;
                    refunddetailC[i].sTradeNo2 = orderdetailS.data.refund_list[i].bank_trade_no;
                    refunddetailC[i].sTradeNo3 = orderdetailS.data.refund_list[i].trade_no;
                    refunddetailC[i].setHeight();
                    if(i == 0)
                    {
                        refunddetailC[i].Location = new Point(0, _refundDetailC.getHeight());
                    }
                    else
                    {
                        refunddetailC[i].Location = new Point(0, refunddetailC[i - 1].Bottom + 5);
                    }
                    panelRefundDetailArea.Controls.Add(refunddetailC[i]);
                }
            }
            catch {}
        }
        private void refund_MouseUp(object sender, MouseEventArgs e)
        {
            refundForm _refundF = new refundForm(orderdetailS);
            _refundF.TopMost = true;
            _refundF.StartPosition = FormStartPosition.CenterParent;
            _refundF.ShowDialog();
            if (_refundF.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                fnOrderDetailFirstLoadAction();
                fnAddRefundlist();
                IsRefund = true;
                Refresh();
            }
            else
            {
                Refresh();
            }
        }
        private void refreshorder_MouseUp(object sender, MouseEventArgs e)
        {
            fnOrderDetailFirstLoadAction();
            fnAddRefundlist();
            Refresh();
        }
        private void print_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //配置打印内容
                detailprintClass d = new detailprintClass(true);
                loadconfigClass lcc = new loadconfigClass("terminal_sn");
                d.IshandPrint = true;
                d.STRmerchantname = orderdetailS.data.merchant_name;
                d.STRstorename = orderdetailS.data.store_name;
                d.STRterminalnum = lcc.readfromConfig();
                d.STRemployee = orderdetailS.data.employee_name;
                d.STRordernum = orderdetailS.data.order_no;
                d.STRpaytype = orderdetailS.data.pay_type;
                d.STRorderstatus = orderdetailS.data.order_status;
                d.STRpayaccount = orderdetailS.data.pay_account;
                d.STRtotalmoney = orderdetailS.data.total_money;
                d.STRundiscountmoney = orderdetailS.data.undiscount_money;
                d.STRdiscountmoney = PublicMethods.discountmoneyFormater(orderdetailS.data.member_discount_money, orderdetailS.data.coupon_money, orderdetailS.data.alipay_merchant_discount);
                d.STRpaymoney = orderdetailS.data.pay_money;
                d.STRalipaydiscountmoney = orderdetailS.data.alipay_discount_money;
                d.STRwechatdiscountmoney = orderdetailS.data.wechat_discount_money;
                d.STRysfdiscountmoney = orderdetailS.data.ysf_discount_money;
                d.STRreceipt = orderdetailS.data.receipt_fee;
                d.STRpaytime = orderdetailS.data.pay_time;
                d.STRtradenum = orderdetailS.data.flow_no;
                d.STRbanktradenum = orderdetailS.data.bank_trade_no;
                d.STRpaytradenum = orderdetailS.data.trade_no;

                int iTemp = orderdetailS.data.refund_list.Count;
                refunddetailPrint[] r = new refunddetailPrint[iTemp];
                for (int _i = 0; _i < iTemp; _i++)
                {
                    r[_i] = new refunddetailPrint();
                    r[_i].refundmoney = orderdetailS.data.refund_list[_i].refund_money;
                    r[_i].refundtime = orderdetailS.data.refund_list[_i].refund_time;
                }
                d.refundedetailC = r;
                d.STRresiduereceipt = orderdetailS.data.receipt_fee;

                //根据设置进行打印
                loadconfigClass _lc = new loadconfigClass("userprint");
                if(_lc.readfromConfig() != "drive" && _lc.readfromConfig() != "lpt")
                {
                    d.printToFile();
                    errorinformationForm errorF = new errorinformationForm("提示", "成功输出到打印信息文件");
                    errorF.TopMost = true;
                    errorF.StartPosition = FormStartPosition.CenterParent;
                    errorF.ShowDialog();
                    Refresh();
                    return;
                }
                _lc = new loadconfigClass("printmode");
                string _sTip = "打印完成";
                if (_lc.readfromConfig() == "all" || _lc.readfromConfig() == "")
                {
                    _sTip = "商户存根打印完成，确认后打印用户存根";
                }

                d.PrintNow();

                errorinformationForm _errorF = new errorinformationForm("提示", _sTip);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();

                d.IsMerchant = false;
                d.PrintNow();

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
                    if (fnDetailMouseControl(keyData) && UserClass.isUseKeyBorad)
                    {
                        return true;
                    }

                }
            }
            catch { }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void fnSetDetailMouse()
        {
            detailFormMouse.Position.Clear();
            detailFormMouse.Position.Add(new Point(520, 460));
            detailFormMouse.Position.Add(new Point(600, 460));
            detailFormMouse.Position.Add(new Point(680, 460));
        }
        public bool fnDetailMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                #region left
                switch (detailFormMouse.pos)
                {
                    case 0: detailFormMouse.pos = 2; break;
                    case 1: 
                        if (refundC.Visible)
                        {
                            detailFormMouse.pos = 0;
                        }
                        else
                        {
                            detailFormMouse.pos = 2;
                        }
                        break;
                    case 2: detailFormMouse.pos = 1; break;
                    default: detailFormMouse.pos = 1; break;
                }
                detailFormMouse._offsetX = Location.X;
                detailFormMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(detailFormMouse.Position[detailFormMouse.pos].X + detailFormMouse._offsetX, detailFormMouse.Position[detailFormMouse.pos].Y + detailFormMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (detailFormMouse.pos)
                {
                    case 0: detailFormMouse.pos = 1; break;
                    case 1: detailFormMouse.pos = 2; break;
                    case 2:
                        if (refundC.Visible)
                        {
                            detailFormMouse.pos = 0;
                        }
                        else
                        {
                            detailFormMouse.pos = 1;
                        }
                        break;
                    default: detailFormMouse.pos = 1; break;
                }
                detailFormMouse._offsetX = Location.X;
                detailFormMouse._offsetY = Location.Y;
                Console.WriteLine(detailFormMouse.Position[detailFormMouse.pos].X + detailFormMouse._offsetX);
                NativeMethods.SetCursorPos(detailFormMouse.Position[detailFormMouse.pos].X + detailFormMouse._offsetX, detailFormMouse.Position[detailFormMouse.pos].Y + detailFormMouse._offsetY);
                return true;
                #endregion
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
    public class Refund_listItem
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

    }
    public class orderData
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
        /// 订单总金额
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
        /// 交易类型
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
        /// 会员优惠
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
        /// 云闪付优惠
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
        /// 支付账号（/会员账号）
        /// </summary>
        public string pay_account { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 实付金额
        /// </summary>
        public string receipt_fee { get; set; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 退款列表
        /// </summary>
        public List<Refund_listItem> refund_list { get; set; }
    }
    public class orderdetailSuccess
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
        /// 订单详情
        /// </summary>
        public orderData data { get; set; }
    }
}
