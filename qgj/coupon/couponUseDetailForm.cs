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
    public partial class couponUseDetailForm : Form
    {
        string sTitle = "优惠券详情";

        string sCoupon = "";//优惠券码

        int iHttpResult = 0;
        string sHttpResult = "";

        couponDetailSuccess couponDetailS;

        Rectangle _rectClose = new Rectangle(700, 20, 10, 10);

        Panel couponDetailPanel = new Panel();

        Font myFont = new Font(UserClass.fontName, 12);
        string strLine;
        PointF strPoint;
        int y = 10;

        public couponUseDetailForm(string _insert)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sCoupon = _insert;

            InitializeComponent();

            couponDetailPanel.SetBounds(10, 50, 720, 430);
            couponDetailPanel.BackColor = Defcolor.MainBackColor;
            couponDetailPanel.AutoScroll = true;
            couponDetailPanel.Scroll += new ScrollEventHandler(scroll);
            couponDetailPanel.MouseWheel += new MouseEventHandler(m_scroll);
            this.Controls.Add(couponDetailPanel);
        }

        private void couponUseDetailForm_Load(object sender, EventArgs e)
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
                if (coupondetailhttp())
                {
                    drawDetail("门店名称", couponDetailS.data.store_name + couponDetailS.data.store_branch_name, ref y, true);
                    drawDetail("操作员", couponDetailS.data.employee_name + couponDetailS.data.employee_code, ref y);
                    drawDetail("核销时间", couponDetailS.data.use_time, ref y);
                    switch (couponDetailS.data.use_channel)
                    {
                        case "1": drawDetail("核销方式", "买单核销", ref y); break;
                        case "2": drawDetail("核销方式", "验券核销", ref y); break;
                        case "3": drawDetail("核销方式", "商城核销", ref y); break;
                        default: break;
                    }
                    switch (couponDetailS.data.type)
                    {
                        case "CASH": drawDetail("券类型", "代金券", ref y); break;
                        case "DISCOUNT": drawDetail("券类型", "折扣券", ref y); break;
                        case "GIFT": drawDetail("券类型", "兑换券", ref y); break;
                        default: break;
                    }
                    
                    drawDetail("券名称", couponDetailS.data.title, ref y);
                    drawDetail("券编号", couponDetailS.data.code, ref y);
                    drawDetail("订单号", couponDetailS.data.order, ref y);
                    if(couponDetailS.data.use_channel == "2")
                    {
                        if(couponDetailS.data.type == "GIFT")
                        {
                            drawDetail("兑换商品金额", couponDetailS.data.total_money, ref y);
                        }
                        else
                        {
                            drawDetail("订单金额", couponDetailS.data.total_money, ref y);
                            drawDetail("优惠金额", couponDetailS.data.coupon_money, ref y);
                            drawDetail("实收金额", couponDetailS.data.receipt_money, ref y);
                        }
                    }
                    else
                    {
                        drawDetail("订单金额", couponDetailS.data.total_money, ref y);
                        drawDetail("优惠券金额", couponDetailS.data.coupon_money, ref y);
                        drawDetail("会员折扣优惠金额", couponDetailS.data.discount_money, ref y);
                        drawDetail("实收金额", couponDetailS.data.receipt_money, ref y);
                    }
                    
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
        }

        private bool coupondetailhttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.couponview);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("code", sCoupon);


            string _RequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _RequestUrl);

            HttpClass _httpC = new HttpClass();
            string _RequestMsg = _httpC.HttpGet(_RequestUrl);
            Console.WriteLine("result:" + _RequestMsg);
            if (_RequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                couponDetailS = (couponDetailSuccess)JsonConvert.DeserializeObject(_RequestMsg, typeof(couponDetailSuccess));
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

        private void couponUseDetailForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_rectClose.Contains(e.Location))
            {
                Dispose();
            }
        }

        private void couponUseDetailForm_Paint(object sender, PaintEventArgs e)
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

        private void drawDetail(string title, string contant, ref int y, bool first = false)
        {
            try
            {
                if (contant != "" && contant != "0.00")
                {
                    flowdetailitemControl detailItem = new flowdetailitemControl(title, contant, first);
                    detailItem.isFlowDetail = false;
                    detailItem.Location = new Point(10, y);
                    couponDetailPanel.Controls.Add(detailItem);
                    y += 30;
                }
            }
            catch { }
        }
        private void scroll(object sender, ScrollEventArgs e)
        {
            couponDetailPanel.Refresh();
        }
        private void m_scroll(object sender, MouseEventArgs e)
        {
            couponDetailPanel.Refresh();
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
    public class detailData
    {
        public string member_avatar { get; set; }
        public string member_account { get; set; }
        public string member_name { get; set; }
        public string member_level_name { get; set; }
        public string store_name { get; set; }
        public string store_branch_name { get; set; }
        public string employee_name { get; set; }
        public string employee_code { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string least_cost { get; set; }
        public string least_cost_title { get; set; }
        public string reduce_cost { get; set; }
        public string discount { get; set; }
        public string title { get; set; }
        public string sub_title { get; set; }
        public string tip { get; set; }
        public string code { get; set; }
        public string time_limit_week { get; set; }
        public string time_limit_begin_time { get; set; }
        public string time_limit_end_time { get; set; }
        public string description { get; set; }
        public string order { get; set; }
        public string use_channel { get; set; }
        public string use_time { get; set; }
        public string total_money { get; set; }
        public string undiscountable_money { get; set; }
        public string coupon_money { get; set; }
        public string discount_money { get; set; }
        public string receipt_money { get; set; }
        public string gift_goods_money { get; set; }
        public string color { get; set; }
        public string type { get; set; }
        public string use_time_info { get; set; }
    }

    public class couponDetailSuccess
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public detailData data { get; set; }
    }
}
