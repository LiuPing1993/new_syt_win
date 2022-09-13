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
    public partial class flowDetailForm : Form
    {

        string sTitle = "流水详情";

        string sFlowNum = "";//流水号

        bool isReloadList = false;

        int iHttpResult = 0;
        string sHttpResult = "";

        flowDetailSuccess flowdetailS;

        Rectangle _rectClose = new Rectangle(700, 20, 10, 10);

        Panel flowDetailPanel = new Panel();

        Font myFont = new Font(UserClass.fontName, 12);
        string strLine;
        PointF strPoint;
        int y = 10;

        public flowDetailForm(string _insert)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sFlowNum = _insert;

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            flowDetailPanel.SetBounds(10, 50, 720, 430);
            flowDetailPanel.BackColor = Defcolor.MainBackColor;
            flowDetailPanel.AutoScroll = true;
            flowDetailPanel.Scroll += new ScrollEventHandler(scroll);
            flowDetailPanel.MouseWheel += new MouseEventHandler(m_scroll);
            this.Controls.Add(flowDetailPanel);

        }

        private void flowDetailForm_Load(object sender, EventArgs e)
        {
            flowDetailFirstLoadAction();

            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
        }

        private void flowDetailFirstLoadAction()
        {
            try
            {
                if (flowdetailhttp())
                {
                    drawDetail("订单号", flowdetailS.data.order_no, ref y, true);
                    drawDetail("下单时间", flowdetailS.data.create_time, ref y);
                    drawDetail("订单类型", flowdetailS.data.type, ref y);
                    y += 30;

                    drawDetail("系统流水号", flowdetailS.data.flow_no, ref y, true);
                    drawDetail("收款账号", flowdetailS.data.sk_account, ref y);
                    if (flowdetailS.data.pay_channel == "银联")
                    {
                        drawDetail("支付渠道", flowdetailS.data.pay_channel_type, ref y);
                    }
                    else
                    {
                        drawDetail("支付渠道", flowdetailS.data.pay_channel + flowdetailS.data.pay_channel_type, ref y);
                    }
                    drawDetail("支付账号", flowdetailS.data.pay_account, ref y);
                    drawDetail("支付时间", flowdetailS.data.pay_time, ref y);
                    drawDetail("银行流水号", flowdetailS.data.bank_trade_no, ref y);
                    drawDetail("支付流水号", flowdetailS.data.trade_no, ref y);
                    drawDetail("交易金额", flowdetailS.data.trade_money, ref y);
                    drawDetail("支付宝商家优惠", flowdetailS.data.alipay_merchant_discount, ref y);
                    drawDetail("用户实付", flowdetailS.data.pay_money, ref y);

                    if (flowdetailS.data.pay_channel_id == "1")
                    {
                        drawDetail("支付宝优惠", flowdetailS.data.alipay_or_wechat_discount, ref y);
                    }
                    else if (flowdetailS.data.pay_channel_id == "2")
                    {
                        drawDetail("微信优惠", flowdetailS.data.alipay_or_wechat_discount, ref y);
                    }
                    else if (flowdetailS.data.pay_channel_id == "7")
                    {
                        drawDetail("云闪付优惠", flowdetailS.data.alipay_or_wechat_discount, ref y);
                    }
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
        }

        private bool flowdetailhttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.flowdetail);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("flow_no", sFlowNum);


            string _RequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _RequestUrl);

            HttpClass _httpC = new HttpClass();
            string _RequestMsg = _httpC.HttpGet(_RequestUrl);
            Console.WriteLine("result:" + _RequestMsg);
            if (_RequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                flowdetailS = (flowDetailSuccess)JsonConvert.DeserializeObject(_RequestMsg, typeof(flowDetailSuccess));
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

        private void flowDetailForm_MouseUp(object sender, MouseEventArgs e)
        {
            
            if (_rectClose.Contains(e.Location))
            {
                if(isReloadList)
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

        private void flowDetailForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, e.ClipRectangle.Width, 47));

            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(700, 20), new Point(710, 30));
            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(700, 30), new Point(710, 20));

            myFont = new Font(UserClass.fontName, 12);
            strLine = String.Format(sTitle);
            strPoint = new PointF(18, 15);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
        }

        private void drawDetail(string title, string contant,ref int y,bool first = false)
        {
            try
            {
                if (contant != "" && contant != "0.00")
                {
                    flowdetailitemControl flowdetailItem = new flowdetailitemControl(title, contant, first);
                    flowdetailItem.Location = new Point(10, y);
                    if(title == "订单号")
                    {
                        flowdetailItem.orderdetail.MouseUp += new MouseEventHandler(orderDetail_MouseUp);
                    }
                    flowDetailPanel.Controls.Add(flowdetailItem);
                    y += 30;
                }
            }
            catch { }
        }

        private void scroll(object sender,ScrollEventArgs e)
        {
            flowDetailPanel.Refresh();
        }
        private void m_scroll(object sender, MouseEventArgs e)
        {
            flowDetailPanel.Refresh();
        }
        private void orderDetail_MouseUp(object sender, MouseEventArgs e)
        {
            if (flowdetailS.data.type == "门店储值")
            {
                storedetailForm _orderdetailF = new storedetailForm(flowdetailS.data.order_no);
                _orderdetailF.TopMost = true;
                _orderdetailF.StartPosition = FormStartPosition.CenterParent;
                _orderdetailF.ShowDialog();
                this.Refresh();
            }
            else
            {
                orderdetailForm _orderdetailF = new orderdetailForm(flowdetailS.data.order_no);
                _orderdetailF.TopMost = true;
                _orderdetailF.StartPosition = FormStartPosition.CenterParent;
                _orderdetailF.ShowDialog();
                if (_orderdetailF.DialogResult == DialogResult.OK)
                {
                    isReloadList = true;
                }
                else
                {
                    this.Refresh();
                }
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
                else if (keyData == Keys.Enter && UserClass.isUseKeyBorad)
                {
                    orderDetail_MouseUp(null, null);
                }
            }
            catch { }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

    public class flowDetailData
    {
        /// <summary>
        /// 收支类型
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 流水类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string flow_no { get; set; }
        /// <summary>
        /// 收款账号
        /// </summary>
        public string sk_account { get; set; }
        /// <summary>
        /// 交易渠道
        /// </summary>
        public string pay_channel_id { get; set; }
        /// <summary>
        /// 交易渠道名
        /// </summary>
        public string pay_channel { get; set; }
        /// <summary>
        /// 交易渠道类型名
        /// </summary>
        public string pay_channel_type { get; set; }
        /// <summary>
        /// 支付账号
        /// </summary>
        public string pay_account { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string pay_time { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public string bank_trade_no { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public string trade_money { get; set; }
        /// <summary>
        /// 支付宝商家优惠
        /// </summary>
        public string alipay_merchant_discount { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string pay_money { get; set; }
        /// <summary>
        /// 支付宝或微信优惠
        /// </summary>
        public string alipay_or_wechat_discount { get; set; }
    }

    public class flowDetailSuccess
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
        public flowDetailData data { get; set; }
    }

}
