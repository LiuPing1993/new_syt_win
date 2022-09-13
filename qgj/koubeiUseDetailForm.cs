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
    public partial class koubeiUseDetailForm : Form
    {

        string sTitle = "口碑券详情";

        string sCoupon = "";//口碑券码

        int iHttpResult = 0;
        string sHttpResult = "";

        koubeiUseDetailSuccess koubeiUseDetailS;

        Rectangle _rectClose = new Rectangle(700, 20, 10, 10);

        Panel koubeiUseDetailPanel = new Panel();

        Font myFont = new Font(UserClass.fontName, 12);
        string strLine;
        PointF strPoint;
        int y = 10;

        public koubeiUseDetailForm(string _insert)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sCoupon = _insert;

            InitializeComponent();

            koubeiUseDetailPanel.SetBounds(10, 50, 720, 430);
            koubeiUseDetailPanel.BackColor = Defcolor.MainBackColor;
            koubeiUseDetailPanel.AutoScroll = true;
            koubeiUseDetailPanel.Scroll += new ScrollEventHandler(scroll);
            koubeiUseDetailPanel.MouseWheel += new MouseEventHandler(m_scroll);
            this.Controls.Add(koubeiUseDetailPanel);
        }

        private void koubeiUseDetailForm_Load(object sender, EventArgs e)
        {
            koubeiDetailFirstLoadAction();

            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                Refresh();
            }
        }

        private void koubeiDetailFirstLoadAction()
        {
            try
            {
                if (koubeidetailhttp())
                {
                    drawDetail("门店名称", koubeiUseDetailS.data.store_name, ref y, true);
                    drawDetail("操作员", koubeiUseDetailS.data.employee_name, ref y);
                    drawDetail("核销时间", koubeiUseDetailS.data.time, ref y);
                    drawDetail("核销方式", koubeiUseDetailS.data.use_type, ref y);

                    drawDetail("核销流水号", koubeiUseDetailS.data.flow_no, ref y);
                    drawDetail("券类型", koubeiUseDetailS.data.coupon_type, ref y);
                    drawDetail("券名称", koubeiUseDetailS.data.name, ref y);

                    drawDetail("券编号", koubeiUseDetailS.data.code, ref y);
                    drawDetail("商品原价", koubeiUseDetailS.data.goods_original_money, ref y);
                    drawDetail("商品现价", koubeiUseDetailS.data.goods_current_money, ref y);

                    drawDetail("支付宝商家优惠", koubeiUseDetailS.data.merchant_money, ref y);
                    drawDetail("用户实付", koubeiUseDetailS.data.pay_money, ref y);
                    drawDetail("支付宝优惠", koubeiUseDetailS.data.alipay_money, ref y);

                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
        }

        private bool koubeidetailhttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.koubeidetail);
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
                koubeiUseDetailS = (koubeiUseDetailSuccess)JsonConvert.DeserializeObject(_RequestMsg, typeof(koubeiUseDetailSuccess));
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

        private void koubeiUseDetailForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_rectClose.Contains(e.Location))
            {
                Dispose();
            }
        }

        private void koubeiUseDetailForm_Paint(object sender, PaintEventArgs e)
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
                    koubeiUseDetailPanel.Controls.Add(detailItem);
                    y += 30;
                }
            }
            catch { }
        }

        private void scroll(object sender, ScrollEventArgs e)
        {
            koubeiUseDetailPanel.Refresh();
        }
        private void m_scroll(object sender, MouseEventArgs e)
        {
            koubeiUseDetailPanel.Refresh();
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

    public class koubeiDetail
    {
        /// <summary>
        /// 哈哈哈
        /// </summary>
        public string store_name { get; set; }
        /// <summary>
        /// 杨斌(0927)
        /// </summary>
        public string employee_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 验券核销
        /// </summary>
        public string use_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flow_no { get; set; }
        /// <summary>
        /// 口碑券
        /// </summary>
        public string coupon_type { get; set; }
        /// <summary>
        /// 口碑券
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_original_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_current_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string merchant_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alipay_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pay_money { get; set; }
    }

    public class koubeiUseDetailSuccess
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
        public koubeiDetail data { get; set; }
    }

}
