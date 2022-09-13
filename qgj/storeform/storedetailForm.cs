using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace qgj
{
    public partial class storedetailForm : Form
    {
        string sTitle = "储值订单详情";
        string sStore = "";
        string sMerchant = "";
        string sAccount = "";
        string sOrderNum = "";
        string sTime = "";
        string sStoreTitle = "";
        string sStoreDetail = "";
        string sTotalMoney = "";
        string sReceipt = "";
        string sOrderStatus = "";

        confirmcancelControl printC = new confirmcancelControl("打印");
        confirmcancelControl refundC = new confirmcancelControl("退款");
        confirmcancelControl refrashC = new confirmcancelControl("刷新");


        int iHttpResult = 0;
        string sHttpResult = "";

        storedetailSuccess storedetailS = new storedetailSuccess();

        Panel panelRefundDetailArea = new Panel();

        public storedetailForm(string _insert)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            sOrderNum = _insert;

            BackColor = Defcolor.MainBackColor;

            //refundC.Location = new Point(490, 445);
            //refundC.MouseUp += new MouseEventHandler(refund_MouseUp);
            //Controls.Add(refundC);

            //printC.Location = new Point(570, 445);
            //printC.MouseUp += new MouseEventHandler(print_MouseUp);
            //Controls.Add(printC);

            refrashC.Location = new Point(650, 445);
            refrashC.MouseUp += new MouseEventHandler(refreshorder_MouseUp);
            Controls.Add(refrashC);

            panelRefundDetailArea.AutoScroll = true;
            panelRefundDetailArea.SetBounds(365, 135, 369, 280);
            panelRefundDetailArea.BackColor = Defcolor.MainBackColor;
            Controls.Add(panelRefundDetailArea);
        }

        private void storedetailForm_Load(object sender, EventArgs e)
        {
            fnStoreDetailFirstLoadAction();

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
                fnAddRefundList();
            }
        }

        private void fnStoreDetailFirstLoadAction()
        {
            try
            {
                if (fnStoreDetailHttp())
                {
                    sStore = storedetailS.data.store_name;
                    sMerchant = storedetailS.data.merchant_name;
                    sAccount = storedetailS.data.user_name + "(" + storedetailS.data.code + ")";
                    sTime = storedetailS.data.pay_time;
                    sTotalMoney = storedetailS.data.total_money;
                    sReceipt = storedetailS.data.receipt_fee;
                    sOrderStatus = storedetailS.data.order_status;
                    sStoreTitle = storedetailS.data.activity_name;
                    sStoreDetail = storedetailS.data.tips;
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
        }
        private bool fnStoreDetailHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.storedetail);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("order_no", sOrderNum);

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                storedetailS = (storedetailSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(storedetailSuccess));
                iHttpResult = 0; ;
                sHttpResult = "请求成功";
                return true;
            }
            else
            {
                errorClass errorp = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = errorp.errMsg;
                return false;
            }
        }
        private void storedetailForm_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle _rectClose = new Rectangle(700, 20, 10, 10);
            if (_rectClose.Contains(e.Location))
            {
                Dispose();
            }
        }
        private void fnAddRefundList()
        {
            try
            {
                int _iRefundItem = Convert.ToInt32(storedetailS.data.refund_list.Count);
                string _sTemp = "";

                if (storedetailS.data.alipay_discount_money != "")
                {
                    _sTemp = "支付宝优惠 " + storedetailS.data.alipay_discount_money;
                }
                else if (storedetailS.data.wechat_discount_money != "")
                {
                    _sTemp = "微信优惠 " + storedetailS.data.wechat_discount_money;
                }
                refunddetailControl _getsdetailC = new refunddetailControl(storedetailS.data.pay_time + " " + storedetailS.data.pay_type, storedetailS.data.pay_money, "操作员 " + storedetailS.data.employee_name, _sTemp, false);
                _getsdetailC.Location = new Point(0, 0);
                panelRefundDetailArea.Controls.Add(_getsdetailC);

                refunddetailControl[] _refunddetailC = new refunddetailControl[_iRefundItem];

                for (int _i = 0; _i < _iRefundItem; _i++)
                {
                    _refunddetailC[_i] = new refunddetailControl(storedetailS.data.refund_list[_i].refund_time + " " + storedetailS.data.pay_type, storedetailS.data.refund_list[_i].refund_money, "操作员 " + storedetailS.data.refund_list[_i].employee_name, "", true);
                    _refunddetailC[_i].Location = new Point(0, (_i + 1) * 70);
                    panelRefundDetailArea.Controls.Add(_refunddetailC[_i]);
                }
            }
            catch { }
        }
        private void storedetailForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

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
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, 100, 255, 25), drawFormat);

            strLine = string.Format("会员账号");
            strPoint = new PointF(20, 135);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sAccount);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, 135, 255, 25), drawFormat);

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
            strLine = string.Format(sTime);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);

            startY += 35;
            strLine = string.Format("储值活动");
            strPoint = new PointF(20, startY);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sStoreTitle);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);

            startY += 35;
            strLine = string.Format("活动内容");
            strPoint = new PointF(20, startY);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sStoreDetail);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);


            startY += 35;
            strLine = string.Format("应收金额");
            strPoint = new PointF(20, startY);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);
            strLine = string.Format(sTotalMoney);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(80, startY, 255, 25), drawFormat);

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
        private void refreshorder_MouseUp(object sender, MouseEventArgs e)
        {
            fnStoreDetailFirstLoadAction();
            fnAddRefundList();
            Refresh();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    Dispose();
                }
            }
            catch { }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
    public class storedetailData
    {
        /// <summary>
        /// 
        /// </summary>
        public string third_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string third_msg { get; set; }
        /// <summary>
        /// 奥斯卡股份
        /// </summary>
        public string merchant_name { get; set; }
        /// <summary>
        /// 亲快科技-演示门店
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 演示收银员(001)
        /// </summary>
        public string employee_name { get; set; }
        /// <summary>
        /// 银联记账
        /// </summary>
        public string pay_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 已付款
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
        public string if_user_show { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 顾磊
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 黄金会员
        /// </summary>
        public string member_level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string activity_name { get; set; }
        /// <summary>
        /// 充200.00元送0.00元
        /// </summary>
        public string tips { get; set; }
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
        public List<Refund_listItem> refund_list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receipt_fee { get; set; }
    }

    public class storedetailSuccess
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
        public storedetailData data { get; set; }
    }
}
