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
    public partial class detailControl : UserControl
    {
        confirmcancelControl searchC = new confirmcancelControl("搜索");
        confirmcancelControl outputC = new confirmcancelControl("导出");
        paytypeSelectControl paytypeselectC = new paytypeSelectControl();
        moneytypeSelectControl moneytypeselectC = new moneytypeSelectControl();
        orderstatusSelectControl orderstatusselectC = new orderstatusSelectControl();
        pagenumControl pagenumC = new pagenumControl();
        WaterTextBox searchTextBox = new WaterTextBox();

        orderlistdetailControl[] orderlistdetailC;
        tradelistSuccess tradelistS;
        tradetotalSuccess tradetotalS;
        errorClass errorC;
        Color colorTop = Color.FromArgb(223, 222, 222);

        public delegate void addDelegate();
        public addDelegate d;
        Thread t1;
        public bool IsThreadRun = false;
        int iTemp = 0;

        int iHttpResult = 0;
        string sHttpResult = "";

        public string sPageNum = "1";
        bool IsReTotal = false;

        string sOrderNum = "";
        string sPayType = "";
        string sTradeType = "";
        string sOrderStatus = "";
        string sStartTime = "";
        string sEndTime = "";

        Label lblOrderTotalTitle = new Label();
        Label lblOrderNumTitle = new Label();
        Label lblRefundTitle = new Label();
        Label lblRefundNumTitle = new Label();
        Label lblReceiptTitle = new Label();

        Label lblOrderTotalMoney = new Label();
        Label lblOrderNum = new Label();
        Label lblRefundMoney = new Label();
        Label lblRefundNum = new Label();
        Label lblReceipt = new Label();

        Label lblSummary = new Label();
        public detailControl()
        {
            InitializeComponent();
            personPicbox.Hide();

            waitinfoLabel.BackColor = Defcolor.MainBackColor;

            searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            searchTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            //searchTextBox.WatermarkText = "订单号";
            searchTextBox.WaterText = "订单号/流水号";
            searchTextBox.SetBounds(300, 18, 150, 25);
            Controls.Add(searchTextBox);

            waitinfoLabel.BackColor = Defcolor.MainBackColor;

            searchC.SetBounds(500, 12, 60, 25);
            searchC.MouseUp += new MouseEventHandler(search_MouseUp);
            Controls.Add(searchC);

            outputC.SetBounds(580, 12, 60, 25);
            outputC.MouseUp += new MouseEventHandler(output_MouseUp);
            Controls.Add(outputC);

            lblSummary.Text = "更多汇总";
            lblSummary.Font = new System.Drawing.Font(UserClass.fontName, 9);
            lblSummary.SetBounds(275, 420, 100, 30);
            lblSummary.ForeColor = Defcolor.FontBlueColor;
            lblSummary.MouseUp += new MouseEventHandler(summary_MouseUp);
            Controls.Add(lblSummary);
            lblSummary.Hide();

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.MinDate = DateTime.Now.AddMonths(-2);
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            sStartTime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            sStartTime += " 00:00:00";
            sEndTime = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            sEndTime += " 23:59:59";

            BackColor = Defcolor.MainBackColor;

            pagenumC.Location = new Point(500, 400);
            Controls.Add(pagenumC);

            d = new addDelegate(fnTradeListLoad);
        }

        private void detailControl_Load(object sender, EventArgs e)
        {
            setdetailMouse();
            #region 控件

            numPanel.BackColor = Defcolor.MainBackColor;
            orderlistPanel.BackColor = Defcolor.MainBackColor;
            //numLabel.BackColor = Defcolor.MainBackColor;

            paytyppeLabel.BackColor = colorTop;
            paytypeselectC.Location = new Point(188, 78);
            paytypeselectC.label1.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label2.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label3.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label4.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label5.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label6.MouseUp += new MouseEventHandler(paytypeSelect);
            Controls.Add(paytypeselectC);
            paytypeselectC.Hide();

            detailmoneyLabel.BackColor = colorTop;
            moneytypeselectC.Location = new Point(333, 78);
            moneytypeselectC.label1.MouseUp += new MouseEventHandler(moneytypeSelect);
            moneytypeselectC.label2.MouseUp += new MouseEventHandler(moneytypeSelect);
            moneytypeselectC.label3.MouseUp += new MouseEventHandler(moneytypeSelect);
            Controls.Add(moneytypeselectC);
            moneytypeselectC.Hide();

            orderstatusLabel.BackColor = colorTop;
            orderstatusselectC.Location = new Point(406, 78);
            orderstatusselectC.label1.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label2.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label3.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label4.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label5.MouseUp += new MouseEventHandler(orderstatusSelect);
            Controls.Add(orderstatusselectC);
            orderstatusselectC.Hide();

            Font myfont = new System.Drawing.Font(UserClass.fontName, 9);
            lblOrderTotalTitle.Font = myfont;
            lblOrderTotalTitle.Text = "交易金额 :";
            lblOrderTotalTitle.AutoSize = true;
            lblOrderTotalTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblOrderTotalTitle.BackColor = Defcolor.MainBackColor;
            lblOrderTotalTitle.Location = new Point(5, 400);
            Controls.Add(lblOrderTotalTitle);

            lblOrderTotalMoney.Font = myfont;
            lblOrderTotalMoney.AutoSize = true;
            lblOrderTotalMoney.ForeColor = Color.Black;
            lblOrderTotalMoney.BackColor = Defcolor.MainBackColor;
            lblOrderTotalMoney.Location = new Point(70, 400);
            Controls.Add(lblOrderTotalMoney);

            lblOrderNumTitle.Font = myfont;
            lblOrderNumTitle.Text = "交易笔数 :";
            lblOrderNumTitle.AutoSize = true;
            lblOrderNumTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblOrderNumTitle.BackColor = Defcolor.MainBackColor;
            lblOrderNumTitle.Location = new Point(5, 420);
            Controls.Add(lblOrderNumTitle);

            lblOrderNum.Font = myfont;
            lblOrderNum.AutoSize = true;
            lblOrderNum.ForeColor = Color.Black;
            lblOrderNum.BackColor = Defcolor.MainBackColor;
            lblOrderNum.Location = new Point(70, 420);
            Controls.Add(lblOrderNum);

            lblRefundTitle.Font = myfont;
            lblRefundTitle.Text = "退款金额 :";
            lblRefundTitle.AutoSize = true;
            lblRefundTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblRefundTitle.BackColor = Defcolor.MainBackColor;
            lblRefundTitle.Location = new Point(145, 400);
            Controls.Add(lblRefundTitle);

            lblRefundMoney.Font = myfont;
            lblRefundMoney.AutoSize = true;
            lblRefundMoney.ForeColor = Defcolor.MainRadColor;
            lblRefundMoney.BackColor = Defcolor.MainBackColor;
            lblRefundMoney.Location = new Point(210, 400);
            Controls.Add(lblRefundMoney);


            lblRefundNumTitle.Font = myfont;
            lblRefundNumTitle.Text = "退款笔数 :";
            lblRefundNumTitle.AutoSize = true;
            lblRefundNumTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblRefundNumTitle.BackColor = Defcolor.MainBackColor;
            lblRefundNumTitle.Location = new Point(145, 420);
            Controls.Add(lblRefundNumTitle);

            lblRefundNum.Font = myfont;
            lblRefundNum.AutoSize = true;
            lblRefundNum.ForeColor = Defcolor.MainRadColor;
            lblRefundNum.BackColor = Defcolor.MainBackColor;
            lblRefundNum.Location = new Point(210, 420);
            Controls.Add(lblRefundNum);

            lblReceiptTitle.Font = myfont;
            lblReceiptTitle.Text = "实收金额 :";
            lblReceiptTitle.AutoSize = true;
            lblReceiptTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblReceiptTitle.BackColor = Defcolor.MainBackColor;
            lblReceiptTitle.Location = new Point(275, 400);
            Controls.Add(lblReceiptTitle);

            lblReceipt.Font = myfont;
            lblReceipt.AutoSize = true;
            lblReceipt.ForeColor = Defcolor.FontBlueColor;
            lblReceipt.BackColor = Defcolor.MainBackColor;
            lblReceipt.Location = new Point(340, 400);
            Controls.Add(lblReceipt);

            #endregion
            timer.Start();
            fnTradeFirstLoadAction();
        }
        private void fnTradeFirstLoadAction()
        {
            try
            {
                fnTradeListHttp();
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                ((main)Parent.Parent).Refresh();
                return;
            }

            fnTradeListLoad();

            try
            {
                if (fnTradeTotalHttp())
                {
                    lblOrderTotalMoney.Text = tradetotalS.data.trade_total;
                    lblOrderNum.Text = tradetotalS.data.trade_num;
                    lblRefundMoney.Text = tradetotalS.data.refund_total;
                    lblRefundNum.Text = tradetotalS.data.refund_num;
                    lblReceipt.Text = tradetotalS.data.receipt_total;
                }
            }
            catch (Exception e)
            {
                iHttpResult = 2;
                sHttpResult = e.ToString();
            }
            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                return;
            }
            else if (iHttpResult == 2)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", "系统内部错误");
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                return;
            }

            try
            {
                pagenumC.fnSetAllPage(tradelistS.data.totalPage.ToString());

            }
            catch 
            {
                pagenumC.fnSetAllPage("1");
            }

        }
        public void fnTradePageLoad()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnTradePageLoadAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }
        private void fnTradePageLoadAction()
        {
            try
            {
                fnTradeListHttp();
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
            Invoke(d);
            IsThreadRun = false;
            t1.Abort();
        }
        private void fnTradeReload()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnTradeReloadAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }
        private void fnTradeReloadAction()
        {
            try
            {
                sOrderNum = searchTextBox.Text.ToString();
                sPageNum = "1";
                if (fnTradeListHttp())
                {
                    fnTradeTotalHttp();
                    IsReTotal = true;
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
            Invoke(d);
            IsThreadRun = false;
            t1.Abort();
        }

        /// <summary>
        /// 构建交易明细记录展示表格
        /// </summary>
        private void fnTradeListLoad()
        {
            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("失败", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                ((main)Parent.Parent).Refresh();
            }
            else
            {
                orderlistPanel.Controls.Clear();
                lblSummary.Hide();
                try
                {
                    //判断是否需要刷新统计展示数据
                    if (IsReTotal)
                    {
                        lblOrderTotalMoney.Text = tradetotalS.data.trade_total;
                        lblOrderNum.Text = tradetotalS.data.trade_num;
                        lblRefundMoney.Text = tradetotalS.data.refund_total;
                        lblRefundNum.Text = tradetotalS.data.refund_num;
                        lblReceipt.Text = tradetotalS.data.receipt_total;
                        IsReTotal = false;
                        if (tradelistS.data.totalPage == null)
                        {
                            pagenumC.fnSetAllPage("1");
                            return;
                        }
                        else
                        {
                            pagenumC.fnSetAllPage(tradelistS.data.totalPage.ToString());
                        }
                    }

                    int _listnum = Convert.ToInt32(tradelistS.data.orderList.Count);

                    detailMouse.listmaxpos = _listnum;
                    detailMouse.haslist = false;

                    orderlistdetailC = new orderlistdetailControl[_listnum];
                    for (int _i = 0; _i < _listnum; _i++)
                    {
                        detailMouse.haslist = true;

                        lblSummary.Show();

                        orderlistdetailC[_i] = new orderlistdetailControl();
                        if (_i % 2 != 0)
                        {
                            orderlistdetailC[_i].changeBackColor(Color.FromArgb(247, 247, 247));
                        }
                        else
                        {
                            orderlistdetailC[_i].changeBackColor(Color.FromArgb(255, 255, 255));
                        }
                        orderlistdetailC[_i].Location = new Point(0, _i * 30);
                        //orderlistdetailC[i].numLabel.Text = (i + 1).ToString();
                        int _temp = Convert.ToInt32(sPageNum);
                        orderlistdetailC[_i].numLabel.Text = ((_i + 1) + (_temp - 1) * 10).ToString();
                        orderlistdetailC[_i].ordernumLabel.Text = tradelistS.data.orderList[_i].order_no;
                        orderlistdetailC[_i].paytypeLabel.Text = tradelistS.data.orderList[_i].pay_channel;
                        orderlistdetailC[_i].ordermoneyLabel.Text = tradelistS.data.orderList[_i].order_money;
                        orderlistdetailC[_i].detailmoneyLabel.Text = tradelistS.data.orderList[_i].money;
                        orderlistdetailC[_i].orderstatusLabel.Text = tradelistS.data.orderList[_i].order_status;
                        orderlistdetailC[_i].personLabel.Text = tradelistS.data.orderList[_i].employee_name;
                        orderlistdetailC[_i].ordertimeLabel.Text = tradelistS.data.orderList[_i].time;
                        orderlistdetailC[_i].detailLabel.Name = tradelistS.data.orderList[_i].order_no.ToString();
                        orderlistdetailC[_i].detailLabel.MouseUp += new MouseEventHandler(orderdetail_MouseUp);
                        if (tradelistS.data.orderList[_i].refund_status == "CLOSE")
                        {
                            orderlistdetailC[_i].refundLabel.Text = "";
                        }
                        else
                        {
                            orderlistdetailC[_i].refundLabel.Name = tradelistS.data.orderList[_i].order_no.ToString();
                            orderlistdetailC[_i].refundLabel.MouseUp += new MouseEventHandler(orderrefund_MouseUp);
                        }
                        orderlistPanel.Controls.Add(orderlistdetailC[_i]);
                    }
                }
                catch (Exception e)
                {
                    setdetailMouse();
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    Refresh();
                }
            }
        }
        private bool fnTradeListHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.tradelist);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sOrderNum != "")
            {
                _urlC.addParameter("order_no", sOrderNum);
            }
            if (sPayType != "")
            {
                _urlC.addParameter("pay_channel", sPayType);
            }
            if (sTradeType != "")
            {
                _urlC.addParameter("trade_type", sTradeType);
            }
            if (sOrderStatus != "")
            {
                _urlC.addParameter("order_status", sOrderStatus);
            }
            if (sStartTime != "" && sEndTime != "")
            {
                _urlC.addParameter("start_time", sStartTime);
                _urlC.addParameter("end_time", sEndTime);
            }
            _urlC.addParameter("page_num", sPageNum);
            _urlC.addParameter("page_size", "10");

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                tradelistS = (tradelistSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(tradelistSuccess));
                iHttpResult = 0; ;
                sHttpResult = "请求成功";
                return true;
            }
            else
            {
                errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = errorC.errMsg;
                return false;
            }
        }
        private bool fnTradeTotalHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.tradetotal);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sOrderNum != "")
            {
                _urlC.addParameter("order_no", sOrderNum);
            }
            if (sPayType != "")
            {
                _urlC.addParameter("pay_channel", sPayType);
            }
            if (sTradeType != "")
            {
                _urlC.addParameter("trade_type", sTradeType);
            }
            if (sOrderStatus != "")
            {
                _urlC.addParameter("order_status", sOrderStatus);
            }
            if (sStartTime != "" && sEndTime != "")
            {
                _urlC.addParameter("start_time", sStartTime);
                _urlC.addParameter("end_time", sEndTime);
            }

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                tradetotalS = (tradetotalSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(tradetotalSuccess));
                iHttpResult = 0; ;
                sHttpResult = "请求成功";
                return true;
            }
            else
            {
                errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = errorC.errMsg;
                return false;
            }
        }
        private void search_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                fnTradeReload();
            }
        }
        private void output_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                excelForm _excelF = new excelForm(sStartTime, sEndTime, sPayType, sTradeType, sOrderStatus);
                _excelF.StartPosition = FormStartPosition.CenterParent;
                _excelF.TopMost = true;
                _excelF.ShowDialog();
                ((main)Parent.Parent).Refresh();
            }
        }
        private void summary_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                orderCollectForm _orderCollect = new orderCollectForm(sOrderNum, sPayType, sTradeType, sOrderStatus, sStartTime, sEndTime);
                _orderCollect.StartPosition = FormStartPosition.CenterParent;
                _orderCollect.TopMost = true;
                _orderCollect.ShowDialog();
                ((main)Parent.Parent).Refresh();
            }
        }
        private void orderdetail_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                orderdetailForm _orderdetailF = new orderdetailForm(((Label)sender).Name);
                _orderdetailF.TopMost = true;
                _orderdetailF.StartPosition = FormStartPosition.CenterParent;
                _orderdetailF.ShowDialog();
                if (_orderdetailF.DialogResult == DialogResult.OK)
                {
                    fnTradeReload();
                }
                else
                {
                    ((main)Parent.Parent).Refresh();
                }
            }
        }
        private void orderrefund_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                refundForm _refundF = new refundForm(((Label)sender).Name);
                _refundF.TopMost = true;
                _refundF.StartPosition = FormStartPosition.CenterParent;
                _refundF.ShowDialog();
                if (_refundF.DialogResult == DialogResult.OK)
                {
                    fnTradeReload();
                }
                else
                {
                    ((main)Parent.Parent).Refresh();
                }
            }
        }
        private void detailControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid
            );
            PublicMethods.FillRoundRectangle(new Rectangle(295, 11, 480, 37), e.Graphics, 7, Color.White);
            PublicMethods.FrameRoundRectangle(new Rectangle(295, 11, 480, 37), e.Graphics, 7, Defcolor.MainGrayLineColor);
        }
        private void numPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel _panel = (Panel)sender;
            _panel.BackColor = colorTop;
            if (_panel.Name == "operationPanel")
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
                );
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                    Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
                );
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            sStartTime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            sStartTime += " 00:00:00";
            Refresh();
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = dateTimePicker2.Value.AddMonths(-2);
            dateTimePicker1.MaxDate = dateTimePicker2.Value;
            sEndTime = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            sEndTime += " 23:59:59";
            Refresh();
        }
        private void paytypeSelectShow(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                paytypeselectC.Visible = paytypeselectC.Visible ? false : true;
                if (paytypeselectC.Visible)
                {
                    paytypeselectC.BringToFront();
                }
            }
        }
        private void paytypeSelect(object sender, MouseEventArgs e)
        {
            Label _lb = (Label)sender;
            if (_lb.Text == "支付宝")
            {
                paytyppeLabel.Text = "支付宝";
                sPayType = "1";

            }
            else if (_lb.Text == "微信")
            {
                paytyppeLabel.Text = "微信";
                sPayType = "2";
            }
            else if (_lb.Text == "银联")
            {
                paytyppeLabel.Text = "银联";
                sPayType = "3";
            }
            else if (_lb.Text == "现金")
            {
                paytyppeLabel.Text = "现金";
                sPayType = "4";
            }
            else if (_lb.Text == "储值")
            {
                paytyppeLabel.Text = "储值";
                sPayType = "5";
            }
            else
            {
                paytyppeLabel.Text = "类型";
                sPayType = "";
            }
            fnTradeReload();
            paytypeselectC.Visible = false;
        }
        private void moneytypeSelectShow(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                moneytypeselectC.Visible = moneytypeselectC.Visible ? false : true;
                if (moneytypeselectC.Visible)
                {
                    moneytypeselectC.BringToFront();
                }
            }
        }
        private void moneytypeSelect(object sender, MouseEventArgs e)
        {
            Label _lb = (Label)sender;
            if (_lb.Text == "收款")
            {
                detailmoneyLabel.Text = "收款";
                sTradeType = "1";

            }
            else if (_lb.Text == "退款")
            {
                detailmoneyLabel.Text = "退款";
                sTradeType = "2";
            }
            else
            {
                detailmoneyLabel.Text = "流水";
                sTradeType = "";
            }
            fnTradeReload();
            moneytypeselectC.Visible = false;
        }
        private void orderstatusSelectShow(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                orderstatusselectC.Visible = orderstatusselectC.Visible ? false : true;
                if (orderstatusselectC.Visible)
                {
                    orderstatusselectC.BringToFront();
                }
            }
        }
        private void orderstatusSelect(object sender, MouseEventArgs e)
        {
            Label _lb = (Label)sender;
            if (_lb.Text == "未付款")
            {
                orderstatusLabel.Text = "未付款";
                sOrderStatus = "1";

            }
            else if (_lb.Text == "已付款")
            {
                orderstatusLabel.Text = "已付款";
                sOrderStatus = "2";
            }
            else if (_lb.Text == "已退款")
            {
                orderstatusLabel.Text = "已退款";
                sOrderStatus = "3";
            }
            else if (_lb.Text == "已部分退款")
            {
                orderstatusLabel.Text = "已部分退款";
                sOrderStatus = "4";
            }
            else
            {
                orderstatusLabel.Text = "状态";
                sOrderStatus = "";
            }
            fnTradeReload();
            orderstatusselectC.Visible = false;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (IsThreadRun)
            {
                if (iTemp == 0)
                {
                    waitinfoLabel.Text = "查询中";
                    iTemp = 1;
                }
                else if (iTemp == 1)
                {
                    waitinfoLabel.Text = "查询中.";
                    iTemp = 2;
                }
                else if (iTemp == 2)
                {
                    waitinfoLabel.Text = "查询中..";
                    iTemp = 3;
                }
                else
                {
                    waitinfoLabel.Text = "查询中...";
                    iTemp = 0;
                }
            }
            else
            {
                waitinfoLabel.Text = "";
                if(iTemp != -1)
                {
                    Refresh();
                    iTemp = -1;
                }
            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    ((main)Parent.Parent).toptitleC.close_MouseUp(null, null);
                    return true;
                }
                else if (keyData == Keys.Tab)
                {
                    ((main)Parent.Parent).fnShowStoreC();
                    return true;
                }
                else if (Visible && (IsThreadRun == false) && UserClass.isUseKeyBorad)
                {

                    if (detailMouseControl(keyData))
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
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("detail:" + ee);
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        #region 键盘操作
        public void setdetailMouse()
        {
            detailMouse.Position.Clear();

            detailMouse.pos = 1;
            detailMouse.listpos = 0;
            detailMouse.listmaxpos = 0;
            detailMouse._offsetX = 20;
            detailMouse._offsetY = 40;

            detailMouse.haslist = false;
            detailMouse.isindateselecter = false;

            detailMouse.Position.Add(new Point(185, 24));//收款0
            detailMouse.Position.Add(new Point(255, 24));//明细1
            detailMouse.Position.Add(new Point(325, 24));//储值2

            detailMouse.Position.Add(new Point(650, 24));//重新登录3
            detailMouse.Position.Add(new Point(715, 24));//最小化4
            detailMouse.Position.Add(new Point(755, 24));//关闭5

            detailMouse.Position.Add(new Point(60, 60));//财务流水6
            detailMouse.Position.Add(new Point(150, 60));//交易订单7

            detailMouse.Position.Add(new Point(117, 100));//起始日期8
            detailMouse.Position.Add(new Point(247, 100));//结束日期9
            detailMouse.Position.Add(new Point(430, 96));//搜索框10
            detailMouse.Position.Add(new Point(535, 100));//搜索11
            detailMouse.Position.Add(new Point(610, 100));//导出12
            #region ListItem
            detailMouse.Position.Add(new Point(735, 170));//列表第一条详情 13
            detailMouse.Position.Add(new Point(700, 170));//列表第一条退款 14
            detailMouse.Position.Add(new Point(735, 200));//列表第二条详情 15
            detailMouse.Position.Add(new Point(700, 200));//列表第二条退款 16
            detailMouse.Position.Add(new Point(735, 230));//列表第三条详情 17
            detailMouse.Position.Add(new Point(700, 230));//列表第三条退款 18  
            detailMouse.Position.Add(new Point(735, 260));//列表第四条详情 19
            detailMouse.Position.Add(new Point(700, 260));//列表第四条退款 20 
            detailMouse.Position.Add(new Point(735, 290));//列表第五条详情 21
            detailMouse.Position.Add(new Point(700, 290));//列表第五条退款 22 
            detailMouse.Position.Add(new Point(735, 320));//列表第六条详情 23
            detailMouse.Position.Add(new Point(700, 320));//列表第六条退款 24
            detailMouse.Position.Add(new Point(735, 350));//列表第七条详情 25
            detailMouse.Position.Add(new Point(700, 350));//列表第七条退款 26
            detailMouse.Position.Add(new Point(735, 380));//列表第八条详情 27
            detailMouse.Position.Add(new Point(700, 380));//列表第八条退款 28
            detailMouse.Position.Add(new Point(735, 410));//列表第九条详情 29
            detailMouse.Position.Add(new Point(700, 410));//列表第九条退款 30 
            detailMouse.Position.Add(new Point(735, 440));//列表第十条详情 31
            detailMouse.Position.Add(new Point(700, 440));//列表第十条退款 32
            #endregion
            detailMouse.Position.Add(new Point(275, 495));//更多汇总//33
            detailMouse.Position.Add(new Point(517, 495));//上一页34
            detailMouse.Position.Add(new Point(602, 495));//下一页35
            detailMouse.Position.Add(new Point(655, 495));//页码输入36
            detailMouse.Position.Add(new Point(720, 495));//跳页37

            detailMouse.Position.Add(new Point(395, 24));//会员38
            detailMouse.Position.Add(new Point(260, 60));//储值明细39
            detailMouse.Position.Add(new Point(465, 24));//验券40
            detailMouse.Position.Add(new Point(350, 60));//优惠券核销明细41
            detailMouse.Position.Add(new Point(460, 60));//口碑券核销明细42
        }
        public bool detailMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (detailMouse.isindateselecter)
            {
                return false;
            }
            else if (keyData == Keys.Down)
            {
                #region down
                switch(detailMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 38:
                    case 40:

                    case 3:
                    case 4:
                    case 5: detailMouse.pos = 6; break;

                    case 6:
                    case 7: 
                    case 39:
                    case 41: 
                    case 42: detailMouse.pos = 8; break;

                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        if (detailMouse.haslist)
                        {
                            detailMouse.pos = 13;
                        }
                        else
                        {
                            detailMouse.pos = 0;
                        }
                        break;
                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31:
                        {
                            if (detailMouse.pos < 11 + 2 * detailMouse.listmaxpos)
                            {
                                detailMouse.pos += 2;
                            }
                            else
                            {
                                detailMouse.pos = 33;
                            }
                            break;
                        }
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32:
                        detailMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: detailMouse.pos = 0; break;
                    default: detailMouse.pos = 0; break;
                }
                detailMouse._offsetX = Parent.Parent.Location.X + 20;
                detailMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(detailMouse.Position[detailMouse.pos].X + detailMouse._offsetX, detailMouse.Position[detailMouse.pos].Y + detailMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                Console.WriteLine(detailMouse.pos);
                switch (detailMouse.pos)
                {
                    case 0: detailMouse.pos = 5; break;
                    case 1: detailMouse.pos = 0; break;
                    case 2: detailMouse.pos = 1; break;
                    case 38: detailMouse.pos = 2; break;
                    case 40: detailMouse.pos = 38; break;
                    case 3: detailMouse.pos = 40; break;
                    case 4: detailMouse.pos = 3; break;
                    case 5: detailMouse.pos = 4; break;

                    case 6: detailMouse.pos = 42; break;
                    case 7: detailMouse.pos = 6; break;
                    case 39: detailMouse.pos = 7; break;
                    case 41: detailMouse.pos = 39; break;
                    case 42: detailMouse.pos = 41; break;

                    case 8: detailMouse.pos = 12; break;
                    case 9: detailMouse.pos = 8; break;
                    case 10: detailMouse.pos = 9; break;
                    case 11: detailMouse.pos = 10; break;
                    case 12: detailMouse.pos = 11; break;
                    case 13: 
                    case 15: 
                    case 17: 
                    case 19: 
                    case 21: 
                    case 23:
                    case 25: 
                    case 27: 
                    case 29: 
                    case 31:
                        if (tradelistS.data.orderList[(detailMouse.pos - 13) / 2].refund_status == "OPEN")
                        {
                            detailMouse.pos += 1;
                        }
                        break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32:
                        detailMouse.pos -= 1; break;
                    case 33: detailMouse.pos = 37; break;
                    case 34: detailMouse.pos = 33; break;
                    case 35: detailMouse.pos = 34; break;
                    case 36: detailMouse.pos = 35; break;
                    case 37: detailMouse.pos = 36; break;
                    default: detailMouse.pos = 0; break;
                }
                
                detailMouse._offsetX = Parent.Parent.Location.X + 20;
                detailMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(detailMouse.Position[detailMouse.pos].X + detailMouse._offsetX, detailMouse.Position[detailMouse.pos].Y + detailMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (detailMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 38:
                    case 40:

                    case 3:
                    case 4:
                    case 5: detailMouse.pos = 33; break;
                    case 6:
                    case 7: 
                    case 39:
                    case 41: 
                    case 42: detailMouse.pos = 0; break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12: detailMouse.pos = 6; break;
                    case 13: detailMouse.pos = 8; break;
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: detailMouse.pos -= 2; break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32: detailMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: detailMouse.pos = detailMouse.listmaxpos + (detailMouse.listmaxpos + 12) - 1; break;
                    default: detailMouse.pos = 0; break;
                }
                detailMouse._offsetX = Parent.Parent.Location.X + 20;
                detailMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(detailMouse.Position[detailMouse.pos].X + detailMouse._offsetX, detailMouse.Position[detailMouse.pos].Y + detailMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (detailMouse.pos)
                {
                    case 0: detailMouse.pos = 1; break;
                    case 1: detailMouse.pos = 2; break;
                    case 2: detailMouse.pos = 38; break;
                    case 38: detailMouse.pos = 40; break;
                    case 40: detailMouse.pos = 3; break;
                    case 3: detailMouse.pos = 4; break;
                    case 4: detailMouse.pos = 5; break;
                    case 5: detailMouse.pos = 0; break;

                    case 6: detailMouse.pos = 7; break;
                    case 7: detailMouse.pos = 39; break;
                    case 39: detailMouse.pos = 41; break;
                    case 41: detailMouse.pos = 42; break;
                    case 42: detailMouse.pos = 6; break;

                    case 8: detailMouse.pos = 9; break;
                    case 9: detailMouse.pos = 10; break;
                    case 10: detailMouse.pos = 11; break;
                    case 11: detailMouse.pos = 12; break;
                    case 12: detailMouse.pos = 8; break;

                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31:
                        if (tradelistS.data.orderList[(detailMouse.pos - 13) / 2].refund_status == "OPEN")
                        {
                            detailMouse.pos += 1;
                        }
                        break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32:detailMouse.pos -= 1; break;

                    case 33: detailMouse.pos = 34; break;
                    case 34: detailMouse.pos = 35; break;
                    case 35: detailMouse.pos = 36; break;
                    case 36: detailMouse.pos = 37; break;
                    case 37: detailMouse.pos = 33; break;
                    default: detailMouse.pos = 0; break;
                }
                detailMouse._offsetX = Parent.Parent.Location.X + 20;
                detailMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(detailMouse.Position[detailMouse.pos].X + detailMouse._offsetX, detailMouse.Position[detailMouse.pos].Y + detailMouse._offsetY);
                Console.WriteLine(detailMouse.pos);
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
        #endregion
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            detailMouse.isindateselecter = false;
            fnTradeReload();
        }
        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            detailMouse.isindateselecter = true;
        }
    }
    public class OrderListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 今天
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string total_money { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string pay_channel { get; set; }
        /// <summary>
        /// 收款
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string money { get; set; }
        /// <summary>
        /// 已付款
        /// </summary>
        public string order_status { get; set; }
        public string employee_name { get; set; }
        public string refund_status { get; set; }
        public string order_money { get; set; }
    }
    public class tradelisData
    {
        /// <summary>
        /// 
        /// </summary>
        public string itemCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pageSign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totalPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderListItem> orderList { get; set; }
    }
    public class tradelistSuccess
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
        public tradelisData data { get; set; }
    }
    public class tradetotal
    {
        /// <summary>
        /// 
        /// </summary>
        public string trade_total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trade_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refund_total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refund_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receipt_total { get; set; }
    }
    public class tradetotalSuccess
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
        public tradetotal data { get; set; }
    }
}
