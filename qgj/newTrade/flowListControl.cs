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
    public partial class flowListControl : UserControl
    {
        confirmcancelControl searchC = new confirmcancelControl("搜索");
        confirmcancelControl outputC = new confirmcancelControl("导出");
        paytypeSelectControl paytypeselectC = new paytypeSelectControl();
        moneytypeSelectControl moneytypeselectC = new moneytypeSelectControl();
        flowtypeSelectControl flowtypeSelectC = new flowtypeSelectControl();

        pagenumControl pagenumC = new pagenumControl(listType.flowlist);
        WaterTextBox searchTextBox = new WaterTextBox();

        flowListItemControl[] flowListItemC;
        flowlistSuccess flowlistS;
        flowsummarySuccess flowtotalS;
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

        string sOrderNum = ""; // 订单号/流水号
        string sType = ""; // 流水类型：门店交易、门店储值
        string sPayChannel = ""; // 支付渠道：支付宝、微信、银联……
        string sTradeType = ""; // 流水收支类型

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

        public flowListControl()
        {
            InitializeComponent();

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

            d = new addDelegate(flowListLoad);

        }
        private void flowListControl_Load(object sender, EventArgs e)
        {
            setdetailMouse();
            #region 控件

            numPanel.BackColor = Defcolor.MainBackColor;
            flowlistPanel.BackColor = Defcolor.MainBackColor;
            //numLabel.BackColor = Defcolor.MainBackColor;

            typeLabel.BackColor = colorTop;
            typePanel.BackColor = colorTop;
            paytypeselectC.Location = new Point(418, 78);
            paytypeselectC.Size = new Size(73, 211);
            paytypeselectC.label1.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label2.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label3.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label4.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label5.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label6.MouseUp += new MouseEventHandler(paytypeSelect);
            Controls.Add(paytypeselectC);
            paytypeselectC.Hide();

            paychannelLabel.BackColor = colorTop;
            paychannelPanel.BackColor = colorTop;
            moneytypeselectC.Location = new Point(566, 78);
            moneytypeselectC.label1.MouseUp += new MouseEventHandler(moneytypeSelect);
            moneytypeselectC.label2.MouseUp += new MouseEventHandler(moneytypeSelect);
            moneytypeselectC.label3.MouseUp += new MouseEventHandler(moneytypeSelect);
            Controls.Add(moneytypeselectC);
            moneytypeselectC.Hide();

            moneyLabel.BackColor = colorTop;
            moneyPanel.BackColor = colorTop;
            flowtypeSelectC.Location = new Point(178, 78);
            flowtypeSelectC.label1.MouseUp += new MouseEventHandler(flowtypeSelect);
            flowtypeSelectC.label2.MouseUp += new MouseEventHandler(flowtypeSelect);
            flowtypeSelectC.label3.MouseUp += new MouseEventHandler(flowtypeSelect);
            Controls.Add(flowtypeSelectC);
            flowtypeSelectC.Hide();

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
            Application.DoEvents();
            flowFirstLoadAction();
        }
        private void flowFirstLoadAction()
        {
            try
            {
                flowListHttp();
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

            flowListLoad();

            try
            {
                if (flowTotalHttp())
                {
                    lblOrderTotalMoney.Text = flowtotalS.data.trade_total;
                    lblOrderNum.Text = flowtotalS.data.trade_num;
                    lblRefundMoney.Text = flowtotalS.data.refund_total;
                    lblRefundNum.Text = flowtotalS.data.refund_num;
                    lblReceipt.Text = flowtotalS.data.receipt_total;
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
                pagenumC.fnSetAllPage(flowlistS.data.totalPage.ToString());

            }
            catch
            {
                pagenumC.fnSetAllPage("1");
            }

        }
        public void flowPageLoad()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(flowPageLoadAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }
        private void flowPageLoadAction()
        {
            try
            {
                flowListHttp();
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
        private void flowReload()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(flowReloadAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }
        private void flowReloadAction()
        {
            try
            {
                sOrderNum = searchTextBox.Text.ToString();
                sPageNum = "1";
                if (flowListHttp())
                {
                    flowTotalHttp();
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
        /// 流水明细记录列表
        /// </summary>
        private void flowListLoad()
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
                flowlistPanel.Controls.Clear();
                lblSummary.Hide();
                try
                {
                    //判断是否需要刷新统计展示数据
                    if (IsReTotal)
                    {
                        lblOrderTotalMoney.Text = flowtotalS.data.trade_total;
                        lblOrderNum.Text = flowtotalS.data.trade_num;
                        lblRefundMoney.Text = flowtotalS.data.refund_total;
                        lblRefundNum.Text = flowtotalS.data.refund_num;
                        lblReceipt.Text = flowtotalS.data.receipt_total;
                        IsReTotal = false;
                        if (flowlistS.data.totalPage == null)
                        {
                            pagenumC.fnSetAllPage("1");
                            return;
                        }
                        else
                        {
                            pagenumC.fnSetAllPage(flowlistS.data.totalPage.ToString());
                        }
                    }

                    int _listnum = Convert.ToInt32(flowlistS.data.flowList.Count);

                    flowlistMouse.listmaxpos = _listnum;
                    flowlistMouse.haslist = false;

                    flowListItemC = new flowListItemControl[_listnum];
                    for (int _i = 0; _i < _listnum; _i++)
                    {
                        flowlistMouse.haslist = true;

                        lblSummary.Show();

                        flowListItemC[_i] = new flowListItemControl();
                        if (_i % 2 != 0)
                        {
                            flowListItemC[_i].changeBackColor(Color.FromArgb(247, 247, 247));
                        }
                        else
                        {
                            flowListItemC[_i].changeBackColor(Color.FromArgb(255, 255, 255));
                        }

                        if (flowlistS.data.flowList[_i].trade_type == null)
                        {
                            flowlistS.data.flowList[_i].trade_type = "0";
                        }
                        if (flowlistS.data.flowList[_i].order_no == null)
                        {
                            flowlistS.data.flowList[_i].order_no = "";
                        }
                        
                        flowListItemC[_i].Location = new Point(0, _i * 30);
                        //orderlistdetailC[i].numLabel.Text = (i + 1).ToString();
                        int _temp = Convert.ToInt32(sPageNum);
                        flowListItemC[_i].numLabel.Text = ((_i + 1) + (_temp - 1) * 10).ToString();
                        flowListItemC[_i].typeLabel.Text = flowlistS.data.flowList[_i].type;
                        flowListItemC[_i].flownoLabel.Text = flowlistS.data.flowList[_i].flow_no;
                        flowListItemC[_i].paychannelLabel.Text = flowlistS.data.flowList[_i].pay_channel;
                        flowListItemC[_i].trademoneyLabel.Text = flowlistS.data.flowList[_i].trade_money;
                        flowListItemC[_i].moneyLabel.Text = flowlistS.data.flowList[_i].flow_money;
                        flowListItemC[_i].personLabel.Text = flowlistS.data.flowList[_i].employee;
                        flowListItemC[_i].tradetimeLabel.Text = flowlistS.data.flowList[_i].date;
                        flowListItemC[_i].detailLabel.Name = flowlistS.data.flowList[_i].flow_no;
                        flowListItemC[_i].refundLabel.Name = flowlistS.data.flowList[_i].order_no;
                        flowListItemC[_i].refundLabel.MouseUp += new MouseEventHandler(orderrefund_MouseUp);
                        flowListItemC[_i].detailLabel.MouseUp += new MouseEventHandler(flowdetail_MouseUp);
                        if (flowlistS.data.flowList[_i].trade_type != "1")
                        {
                            flowListItemC[_i].refundLabel.Text = " ";
                        }
                        flowlistPanel.Controls.Add(flowListItemC[_i]);
                    }
                }
                catch (Exception e)
                {
                    setdetailMouse(6);
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    Refresh();
                }
            }
        }
        private bool flowListHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.flowlist);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sOrderNum != "")
            {
                _urlC.addParameter("order_no", sOrderNum);
            }
            if (sPayChannel != "")
            {
                _urlC.addParameter("pay_channel", sPayChannel);
            }
            if (sTradeType != "")
            {
                _urlC.addParameter("trade_type", sTradeType);
            }
            if (sType != "")
            {
                _urlC.addParameter("type", sType);
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
                flowlistS = (flowlistSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(flowlistSuccess));
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
        private bool flowTotalHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.flowsummary);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sOrderNum != "")
            {
                _urlC.addParameter("order_no", sOrderNum);
            }
            if (sPayChannel != "")
            {
                _urlC.addParameter("pay_channel", sPayChannel);
            }
            if (sTradeType != "")
            {
                _urlC.addParameter("trade_type", sTradeType);
            }
            if (sType != "")
            {
                _urlC.addParameter("type", sType);
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
                flowtotalS = (flowsummarySuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(flowsummarySuccess));
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
                flowReload();
            }
        }
        private void output_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                excelForm _excelF = new excelForm(sStartTime, sEndTime, sOrderNum, sType, sPayChannel, sTradeType);
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
                flowCollectForm _flowCollect = new flowCollectForm(sOrderNum, sType, sPayChannel, sTradeType, sStartTime, sEndTime);
                _flowCollect.StartPosition = FormStartPosition.CenterParent;
                _flowCollect.TopMost = true;
                _flowCollect.ShowDialog();
                ((main)Parent.Parent).Refresh();
            }
        }
        private void flowdetail_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {

                flowDetailForm _flowdetailF = new flowDetailForm(((Label)sender).Name);
                _flowdetailF.TopMost = true;
                _flowdetailF.StartPosition = FormStartPosition.CenterParent;
                _flowdetailF.ShowDialog();
                if (_flowdetailF.DialogResult == DialogResult.OK)
                {
                    flowReload();
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
                string order_no = ((Label)sender).Name;
                if (order_no == "") return;
                refundForm _refundF = new refundForm(((Label)sender).Name);
                _refundF.TopMost = true;
                _refundF.StartPosition = FormStartPosition.CenterParent;
                _refundF.ShowDialog();
                if (_refundF.DialogResult == DialogResult.OK)
                {
                    flowReload();
                }
                else
                {
                    ((main)Parent.Parent).Refresh();
                }
            }
        }
        private void flowListControl_Paint(object sender, PaintEventArgs e)
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
        private void panel_Paint(object sender, PaintEventArgs e)
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
                paychannelLabel.Text = "支付宝";
                sPayChannel = "1";
            }
            else if (_lb.Text == "微信")
            {
                paychannelLabel.Text = "微信";
                sPayChannel = "2";
            }
            else if (_lb.Text == "银联")
            {
                paychannelLabel.Text = "银联";
                sPayChannel = "3";
            }
            else if (_lb.Text == "现金")
            {
                paychannelLabel.Text = "现金";
                sPayChannel = "4";
            }
            else if (_lb.Text == "储值")
            {
                paychannelLabel.Text = "储值";
                sPayChannel = "5";
            }
            else
            {
                paychannelLabel.Text = "类型";
                sPayChannel = "";
            }
            flowReload();
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
                moneyLabel.Text = "收款";
                sTradeType = "1";

            }
            else if (_lb.Text == "退款")
            {
                moneyLabel.Text = "退款";
                sTradeType = "2";
            }
            else
            {
                moneyLabel.Text = "流水";
                sTradeType = "";
            }
            flowReload();
            moneytypeselectC.Visible = false;
        }
        private void flowtypeSelectShow(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                flowtypeSelectC.Visible = flowtypeSelectC.Visible ? false : true;
                if (flowtypeSelectC.Visible)
                {
                    flowtypeSelectC.BringToFront();
                }
            }
        }
        private void flowtypeSelect(object sender, MouseEventArgs e)
        {
            Label _lb = (Label)sender;
            if (_lb.Text == "门店交易")
            {
                typeLabel.Text = "门店交易";
                sType = "1";

            }
            else if (_lb.Text == "门店储值")
            {
                typeLabel.Text = "门店储值";
                sType = "2";
            }
            else
            {
                typeLabel.Text = "类型";
                sType = "";
            }
            flowReload();
            flowtypeSelectC.Visible = false;
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
                if (iTemp != -1)
                {
                    Refresh();
                    iTemp = -1;
                }
            }
        }
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            flowlistMouse.isindateselecter = false;
            flowReload();
        }
        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            flowlistMouse.isindateselecter = true;
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
                        Console.WriteLine("flowlsit:" + flowlistMouse.pos);
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
        public void setdetailMouse(int startpos = 1)
        {
            flowlistMouse.Position.Clear();

            flowlistMouse.pos = startpos;
            flowlistMouse.listpos = 0;
            flowlistMouse.listmaxpos = 0;
            flowlistMouse._offsetX = 20;
            flowlistMouse._offsetY = 40;

            flowlistMouse.haslist = false;
            flowlistMouse.isindateselecter = false;

            flowlistMouse.Position.Add(new Point(185, 24));//收款0
            flowlistMouse.Position.Add(new Point(255, 24));//明细1
            flowlistMouse.Position.Add(new Point(325, 24));//储值2

            flowlistMouse.Position.Add(new Point(650, 24));//重新登录3
            flowlistMouse.Position.Add(new Point(715, 24));//最小化4
            flowlistMouse.Position.Add(new Point(755, 24));//关闭5

            flowlistMouse.Position.Add(new Point(60, 60));//财务流水6
            flowlistMouse.Position.Add(new Point(150, 60));//交易订单7
            flowlistMouse.Position.Add(new Point(260, 60));//储值明细8

            flowlistMouse.Position.Add(new Point(117, 100));//起始日期9
            flowlistMouse.Position.Add(new Point(247, 100));//结束日期10
            flowlistMouse.Position.Add(new Point(430, 96));//搜索框11
            flowlistMouse.Position.Add(new Point(535, 100));//搜索12

            #region ListItem
            flowlistMouse.Position.Add(new Point(735, 170));//列表第一条详情 13
            flowlistMouse.Position.Add(new Point(700, 170));//列表第一条退款 14
            flowlistMouse.Position.Add(new Point(735, 200));//列表第二条详情 15
            flowlistMouse.Position.Add(new Point(700, 200));//列表第二条退款 16
            flowlistMouse.Position.Add(new Point(735, 230));//列表第三条详情 17
            flowlistMouse.Position.Add(new Point(700, 230));//列表第三条退款 18  
            flowlistMouse.Position.Add(new Point(735, 260));//列表第四条详情 19
            flowlistMouse.Position.Add(new Point(700, 260));//列表第四条退款 20 
            flowlistMouse.Position.Add(new Point(735, 290));//列表第五条详情 21
            flowlistMouse.Position.Add(new Point(700, 290));//列表第五条退款 22 
            flowlistMouse.Position.Add(new Point(735, 320));//列表第六条详情 23
            flowlistMouse.Position.Add(new Point(700, 320));//列表第六条退款 24
            flowlistMouse.Position.Add(new Point(735, 350));//列表第七条详情 25
            flowlistMouse.Position.Add(new Point(700, 350));//列表第七条退款 26
            flowlistMouse.Position.Add(new Point(735, 380));//列表第八条详情 27
            flowlistMouse.Position.Add(new Point(700, 380));//列表第八条退款 28
            flowlistMouse.Position.Add(new Point(735, 410));//列表第九条详情 29
            flowlistMouse.Position.Add(new Point(700, 410));//列表第九条退款 30 
            flowlistMouse.Position.Add(new Point(735, 440));//列表第十条详情 31
            flowlistMouse.Position.Add(new Point(700, 440));//列表第十条退款 32
            #endregion
            flowlistMouse.Position.Add(new Point(275, 495));//更多汇总//33
            flowlistMouse.Position.Add(new Point(517, 495));//上一页34
            flowlistMouse.Position.Add(new Point(602, 495));//下一页35
            flowlistMouse.Position.Add(new Point(655, 495));//页码输入36
            flowlistMouse.Position.Add(new Point(720, 495));//跳页37

            flowlistMouse.Position.Add(new Point(395, 24));//会员38
            flowlistMouse.Position.Add(new Point(610, 100));//导出39
            flowlistMouse.Position.Add(new Point(465, 24));//验券40
            flowlistMouse.Position.Add(new Point(350, 60));//优惠券核销明细41
            flowlistMouse.Position.Add(new Point(460, 60));//口碑券核销明细42
        }
        public bool detailMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (flowlistMouse.isindateselecter)
            {
                return false;
            }
            else if (keyData == Keys.Down)
            {
                #region down
                switch (flowlistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: flowlistMouse.pos = 6; break;
                    case 6:
                    case 7:
                    case 8: flowlistMouse.pos = 9; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39:
                        if (flowlistMouse.haslist)
                        {
                            flowlistMouse.pos = 13;
                        }
                        else
                        {
                            flowlistMouse.pos = 0;
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
                            if (flowlistMouse.pos < 11 + 2 * flowlistMouse.listmaxpos)
                            {
                                flowlistMouse.pos += 2;
                            }
                            else
                            {
                                flowlistMouse.pos = 33;
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
                        flowlistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: flowlistMouse.pos = 0; break;
                    case 38: flowlistMouse.pos = 6; break;
                    case 40: flowlistMouse.pos = 6; break;
                    case 41: flowlistMouse.pos = 9; break;
                    case 42: flowlistMouse.pos = 9; break;
                    default: flowlistMouse.pos = 0; break;
                }
                flowlistMouse._offsetX = Parent.Parent.Location.X + 20;
                flowlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(flowlistMouse.Position[flowlistMouse.pos].X + flowlistMouse._offsetX, flowlistMouse.Position[flowlistMouse.pos].Y + flowlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                Console.WriteLine(flowlistMouse.pos);
                switch (flowlistMouse.pos)
                {
                    case 0: flowlistMouse.pos = 5; break;
                    case 1: flowlistMouse.pos = 0; break;
                    case 2: flowlistMouse.pos = 1; break;
                    case 38: flowlistMouse.pos = 2; break;
                    case 40: flowlistMouse.pos = 38; break;
                    case 3: flowlistMouse.pos = 40; break;
                    case 4: flowlistMouse.pos = 3; break;
                    case 5: flowlistMouse.pos = 4; break;

                    case 6: flowlistMouse.pos = 42; break;
                    case 7: flowlistMouse.pos = 6; break;
                    case 8: flowlistMouse.pos = 7; break;
                    case 41: flowlistMouse.pos = 8; break;
                    case 42: flowlistMouse.pos = 41; break;

                    case 9: flowlistMouse.pos = 39; break;
                    case 10: flowlistMouse.pos = 9; break;
                    case 11: flowlistMouse.pos = 10; break;
                    case 12: flowlistMouse.pos = 11; break;
                    case 39: flowlistMouse.pos = 12; break;
                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31:break;
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
                        flowlistMouse.pos -= 1; break;
                    case 33: flowlistMouse.pos = 37; break;
                    case 34: flowlistMouse.pos = 33; break;
                    case 35: flowlistMouse.pos = 34; break;
                    case 36: flowlistMouse.pos = 35; break;
                    case 37: flowlistMouse.pos = 36; break;
                    default: flowlistMouse.pos = 0; break;
                }

                flowlistMouse._offsetX = Parent.Parent.Location.X + 20;
                flowlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(flowlistMouse.Position[flowlistMouse.pos].X + flowlistMouse._offsetX, flowlistMouse.Position[flowlistMouse.pos].Y + flowlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (flowlistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: flowlistMouse.pos = 33; break;
                    case 6:
                    case 7:
                    case 8: flowlistMouse.pos = 0; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39: flowlistMouse.pos = 6; break;
                    case 13: flowlistMouse.pos = 9; break;
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: flowlistMouse.pos -= 2; break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32: flowlistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: flowlistMouse.pos = flowlistMouse.listmaxpos + (flowlistMouse.listmaxpos + 12) - 1; break;
                    case 38: flowlistMouse.pos = 33; break;
                    case 40: flowlistMouse.pos = 33; break;
                    case 41: flowlistMouse.pos = 0; break;
                    case 42: flowlistMouse.pos = 0; break;
                    default: flowlistMouse.pos = 0; break;
                }
                flowlistMouse._offsetX = Parent.Parent.Location.X + 20;
                flowlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(flowlistMouse.Position[flowlistMouse.pos].X + flowlistMouse._offsetX, flowlistMouse.Position[flowlistMouse.pos].Y + flowlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (flowlistMouse.pos)
                {
                    case 0: flowlistMouse.pos = 1; break;
                    case 1: flowlistMouse.pos = 2; break;
                    case 2: flowlistMouse.pos = 38; break;
                    case 38: flowlistMouse.pos = 40; break;
                    case 40: flowlistMouse.pos = 3; break;
                    case 3: flowlistMouse.pos = 4; break;
                    case 4: flowlistMouse.pos = 5; break;
                    case 5: flowlistMouse.pos = 0; break;

                    case 6: flowlistMouse.pos = 7; break;
                    case 7: flowlistMouse.pos = 8; break;
                    case 8: flowlistMouse.pos = 41; break;
                    case 41: flowlistMouse.pos = 42; break;
                    case 42: flowlistMouse.pos = 6; break;

                    case 9: flowlistMouse.pos = 10; break;
                    case 10: flowlistMouse.pos = 11; break;
                    case 11: flowlistMouse.pos = 12; break;
                    case 12: flowlistMouse.pos = 39; break;
                    case 39: flowlistMouse.pos = 9; break;
                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31:break;
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
                        flowlistMouse.pos -= 1; break;
                    case 33: flowlistMouse.pos = 34; break;
                    case 34: flowlistMouse.pos = 35; break;
                    case 35: flowlistMouse.pos = 36; break;
                    case 36: flowlistMouse.pos = 37; break;
                    case 37: flowlistMouse.pos = 33; break;
                    default: flowlistMouse.pos = 0; break;
                }
                flowlistMouse._offsetX = Parent.Parent.Location.X + 20;
                flowlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(flowlistMouse.Position[flowlistMouse.pos].X + flowlistMouse._offsetX, flowlistMouse.Position[flowlistMouse.pos].Y + flowlistMouse._offsetY);
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
    }
    public class flowListItem
    {
        /// <summary>
        /// 交易时间
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 流水类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string flow_no { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public string pay_channel { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public string trade_money { get; set; }
        /// <summary>
        /// 流水金额
        /// </summary>
        public string flow_money { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string employee { get; set; }

        public string order_no { get; set; }

        public string trade_type { get; set; }
    }
    public class flowlistData
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public string itemCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public string totalPage { get; set; }
        /// <summary>
        /// 流水记录
        /// </summary>
        public List<flowListItem> flowList { get; set; }
    }
    public class flowlistSuccess
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
        /// 数据内容
        /// </summary>
        public flowlistData data { get; set; }
    }
    public class flowSummary
    {
        /// <summary>
        /// 交易总金额
        /// </summary>
        public string trade_total { get; set; }
        /// <summary>
        /// 交易总笔数
        /// </summary>
        public string trade_num { get; set; }
        /// <summary>
        /// 退款总数
        /// </summary>
        public string refund_total { get; set; }
        /// <summary>
        /// 退款总笔数
        /// </summary>
        public string refund_num { get; set; }
        /// <summary>
        /// 实收总金额
        /// </summary>
        public string receipt_total { get; set; }
    }
    public class flowsummarySuccess
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
        /// 数据内容
        /// </summary>
        public flowSummary data { get; set; }
    }
}

