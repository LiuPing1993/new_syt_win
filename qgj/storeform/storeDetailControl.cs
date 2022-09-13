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
    public partial class storeDetailControl : UserControl
    {
        confirmcancelControl searchC = new confirmcancelControl("搜索");
        confirmcancelControl outputC = new confirmcancelControl("导出");
        paytypeSelectControl paytypeselectC = new paytypeSelectControl(true);
        pagenumControl pagenumC = new pagenumControl(listType.storelist);
        //watermarkTextBox searchTextBox = new watermarkTextBox();
        WaterTextBox tbxSearch = new WaterTextBox();
        Color colorTop = Color.FromArgb(223, 222, 222);

        storelistdetailControl[] storelistdetailC;
        storetradelistSuccess storetradelistS;
        storetradetotalSuccess storetradetotalS;
        errorClass errorC;

        string sKeyWords = "";
        string sPayType = "";
        string sStartTime = "";
        string sEndTime = "";

        int iHttpResult = 0;
        string sHttpResult = "";

        public string sPageNum = "1";
        bool IsReTotal = false;

        public delegate void addDelegate();
        public addDelegate d;
        Thread t;
        public bool IsThreadRun = false;
        int iTemp = 0;

        Label lblStoreTotalTitle = new Label();
        Label lblStoreNumTitle = new Label();
        Label lblStoreReceiptTitle = new Label();

        Label lblStoreTotalMoney = new Label();
        Label lblStoreNum = new Label();
        Label lblStoreReceipt = new Label();

        Label lblSummary = new Label();
        public storeDetailControl()
        {
            InitializeComponent();

            tbxSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            //searchTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            //searchTextBox.WatermarkText = "储值名、会员卡号、会员手机";
            tbxSearch.WaterText = "储值名、会员卡号、会员手机";
            tbxSearch.SetBounds(300, 18, 170, 25);
            Controls.Add(tbxSearch);

            waitinfoLabel.BackColor = Defcolor.MainBackColor;

            searchC.SetBounds(500, 12, 60, 25);
            searchC.MouseUp += new MouseEventHandler(search_MouseUp);
            Controls.Add(searchC);

            outputC.SetBounds(580, 12, 60, 25);
            outputC.MouseUp += new MouseEventHandler(output_MouseUp);
            Controls.Add(outputC);

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

            d = new addDelegate(fnStoreListLoad);

            Font myfont = new System.Drawing.Font(UserClass.fontName, 9);
            lblStoreTotalTitle.Font = myfont;
            lblStoreTotalTitle.Text = "储值金额 :";
            lblStoreTotalTitle.AutoSize = true;
            lblStoreTotalTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblStoreTotalTitle.Location = new Point(5, 400);
            Controls.Add(lblStoreTotalTitle);

            lblStoreTotalMoney.Font = myfont;
            lblStoreTotalMoney.AutoSize = true;
            lblStoreTotalMoney.ForeColor = Color.Black;
            lblStoreTotalMoney.Location = new Point(70, 400);
            Controls.Add(lblStoreTotalMoney);

            lblStoreNumTitle.Font = myfont;
            lblStoreNumTitle.Text = "储值笔数 :";
            lblStoreNumTitle.AutoSize = true;
            lblStoreNumTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblStoreNumTitle.Location = new Point(5, 420);
            Controls.Add(lblStoreNumTitle);

            lblStoreNum.Font = myfont;
            lblStoreNum.AutoSize = true;
            lblStoreNum.ForeColor = Color.Black;
            lblStoreNum.Location = new Point(70, 420);
            Controls.Add(lblStoreNum);

            lblStoreReceiptTitle.Font = myfont;
            lblStoreReceiptTitle.Text = "实收金额 :";
            lblStoreReceiptTitle.AutoSize = true;
            lblStoreReceiptTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblStoreReceiptTitle.Location = new Point(145, 400);
            Controls.Add(lblStoreReceiptTitle);

            lblStoreReceipt.Font = myfont;
            lblStoreReceipt.AutoSize = true;
            lblStoreReceipt.ForeColor = Defcolor.FontBlueColor;
            lblStoreReceipt.Location = new Point(210, 400);
            Controls.Add(lblStoreReceipt);

            lblSummary.Text = "更多汇总";
            lblSummary.Font = new System.Drawing.Font(UserClass.fontName, 9);
            lblSummary.SetBounds(145, 420, 100, 30);
            lblSummary.ForeColor = Defcolor.FontBlueColor;
            lblSummary.MouseUp += new MouseEventHandler(summary_MouseUp);
            Controls.Add(lblSummary);
            lblSummary.Hide();
        }

        private void storeDetailControl_Load(object sender, EventArgs e)
        {
            fnSetStoreMouse();
            paytypeLabel.BackColor = colorTop;
            paytypeselectC.Location = new Point(444, 78);
            paytypeselectC.label1.MouseUp += new MouseEventHandler(fnPayTypeSelect);
            paytypeselectC.label2.MouseUp += new MouseEventHandler(fnPayTypeSelect);
            paytypeselectC.label3.MouseUp += new MouseEventHandler(fnPayTypeSelect);
            paytypeselectC.label4.MouseUp += new MouseEventHandler(fnPayTypeSelect);
            paytypeselectC.label5.MouseUp += new MouseEventHandler(fnPayTypeSelect);
            Controls.Add(paytypeselectC);
            paytypeselectC.Hide();
            Application.DoEvents();
            timer.Start();
            fnStoreFirstLoadAction();
            Focus();
        }

        private void fnStoreFirstLoadAction()
        {
            try
            {
                fnStoreListHttp();
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

            fnStoreListLoad();

            try
            {
                if (fnStoreTotalHttp())
                {
                    lblStoreTotalMoney.Text = storetradetotalS.data.total_recharge;
                    lblStoreNum.Text = storetradetotalS.data.num;
                    lblStoreReceipt.Text = storetradetotalS.data.total_pay;
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
                pagenumC.fnSetAllPage(storetradelistS.data.totalPage.ToString());
            }
            catch
            {
                pagenumC.fnSetAllPage("1");
            }
        }
        public void fnStorePageLoad()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnStorePageLoadAction);
                thread.IsBackground = true;
                thread.Start();
                t = thread;
            }
        }
        private void fnStorePageLoadAction()
        {
            try
            {
                fnStoreListHttp();
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.ToString();
            }
            Invoke(d);
            IsThreadRun = false;
            t.Abort();
        }
        private void fnStoreReload()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnStoreReloadAction);
                thread.IsBackground = true;
                thread.Start();
                t = thread;
            }
        }
        private void fnStoreReloadAction()
        {
            try
            {
                sKeyWords = tbxSearch.Text.ToString();
                sPageNum = "1";
                if (fnStoreListHttp())
                {
                    fnStoreTotalHttp();
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
            t.Abort();
        }
        private void fnStoreListLoad()
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
                storelistPanel.Controls.Clear();
                lblSummary.Hide();
                try
                {

                    if (IsReTotal)
                    {
                        lblStoreTotalMoney.Text = storetradetotalS.data.total_recharge;
                        lblStoreNum.Text = storetradetotalS.data.num;
                        lblStoreReceipt.Text = storetradetotalS.data.total_pay;
                        IsReTotal = false;
                        if (storetradelistS.data.totalPage == null)
                        {
                            pagenumC.fnSetAllPage("1");
                            return;
                        }
                        else
                        {
                            pagenumC.fnSetAllPage(storetradelistS.data.totalPage.ToString());
                        }
                    }
                    int _iListNum = Convert.ToInt32(storetradelistS.data.orderList.Count);

                    storeMouse.listmaxpos = _iListNum;
                    storeMouse.haslist = false;

                    storelistdetailC = new storelistdetailControl[_iListNum];
                    for (int i = 0; i < _iListNum; i++)
                    {
                        storeMouse.haslist = true;
                        lblSummary.Show();
                        storelistdetailC[i] = new storelistdetailControl();
                        if (i % 2 != 0)
                        {
                            storelistdetailC[i].changeBackColor(Color.FromArgb(247, 247, 247));
                        }
                        else
                        {
                            storelistdetailC[i].changeBackColor(Color.FromArgb(255, 255, 255));
                        }
                        storelistdetailC[i].Location = new Point(0, i * 30);
                        int temp = Convert.ToInt32(sPageNum);
                        storelistdetailC[i].numLabel.Text = ((i + 1) + (temp - 1) * 10).ToString();
                        storelistdetailC[i].membernumLabel.Text = storetradelistS.data.orderList[i].member_account;
                        storelistdetailC[i].membernameLabel.Text = storetradelistS.data.orderList[i].member_name;
                        if (storetradelistS.data.orderList[i].activity_title == "" || storetradelistS.data.orderList[i].activity_title == null)
                        {
                            storelistdetailC[i].storetypeLabel.Text = "普通储值活动";
                        }
                        else
                        {
                            storelistdetailC[i].storetypeLabel.Text = storetradelistS.data.orderList[i].activity_title;
                        }
                        storelistdetailC[i].storemoneyLabel.Text = storetradelistS.data.orderList[i].order_money;
                        storelistdetailC[i].paytypeLabel.Text = storetradelistS.data.orderList[i].pay_channel;
                        storelistdetailC[i].personLabel.Text = storetradelistS.data.orderList[i].employee_name;
                        storelistdetailC[i].storetimeLabel.Text =storetradelistS.data.orderList[i].time;
                        storelistdetailC[i].operationLabel.Text = "详情";
                        storelistdetailC[i].operationLabel.Name = storetradelistS.data.orderList[i].order_no.ToString();
                        storelistdetailC[i].operationLabel.MouseUp += new MouseEventHandler(orderdetail_MouseUp);
                        storelistPanel.Controls.Add(storelistdetailC[i]);
                    }
                }
                catch(Exception e)
                {
                    fnSetStoreMouse();
                    Console.WriteLine(e);
                }
                finally
                {
                    Refresh();
                }
            }
        }
        private bool fnStoreListHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.storetradelist);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sPayType != "")
            {
                _urlC.addParameter("pay_channel", sPayType);
            }
            if (sKeyWords != "")
            {
                try
                {
                    if (PublicMethods.IsNumeric(sKeyWords))
                    {
                        _urlC.addParameter("code",sKeyWords);
                    }
                    else
                    {
                        _urlC.addParameter("activity_title", sKeyWords);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
                storetradelistS = (storetradelistSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(storetradelistSuccess));
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
        private bool fnStoreTotalHttp()
        {

            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.storetradetotal);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sPayType != "")
            {
                _urlC.addParameter("pay_channel", sPayType);
            }
            if (sKeyWords != "")
            {
                try
                {
                    if (PublicMethods.IsNumeric(sKeyWords))
                    {
                        _urlC.addParameter("code", sKeyWords);
                    }
                    else
                    {
                        _urlC.addParameter("activity_title", sKeyWords);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
                storetradetotalS = (storetradetotalSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(storetradetotalSuccess));
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
        private void orderdetail_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                storedetailForm _orderdetailF = new storedetailForm(((Label)sender).Name);
                _orderdetailF.TopMost = true;
                _orderdetailF.StartPosition = FormStartPosition.CenterParent;
                _orderdetailF.ShowDialog();
                ((main)Parent.Parent).Refresh();
            }
        }
        private void search_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("按下");
            if (!IsThreadRun)
            {
                fnStoreReload();
            }
        }
        private void output_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                excelForm _excelF = new excelForm(sStartTime, sEndTime, sPayType, sKeyWords);
                _excelF.StartPosition = FormStartPosition.CenterParent;
                _excelF.TopMost = true;
                _excelF.ShowDialog();
                ((main)Parent.Parent).Refresh();
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
        private void summary_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                storeCollectForm _orderCollect = new storeCollectForm(sKeyWords, sPayType, sStartTime, sEndTime);
                _orderCollect.StartPosition = FormStartPosition.CenterParent;
                _orderCollect.TopMost = true;
                _orderCollect.ShowDialog();
                ((main)Parent.Parent).Refresh();
            }
        }
        private void storeDetailControl_Paint(object sender, PaintEventArgs e)
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
            Panel panel = (Panel)sender;
            panel.BackColor = colorTop;
            if (panel.Name == "operationPanel")
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
        private void fnPayTypeSelectShow(object sender, MouseEventArgs e)
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
        private void fnPayTypeSelect(object sender, MouseEventArgs e)
        {
            Label lb = (Label)sender;
            if (lb.Text == "支付宝")
            {
                paytypeLabel.Text = "支付宝";
                sPayType = "1";

            }
            else if (lb.Text == "微信")
            {
                paytypeLabel.Text = "微信";
                sPayType = "2";
            }
            else if (lb.Text == "银联")
            {
                paytypeLabel.Text = "银联";
                sPayType = "3";
            }
            else if (lb.Text == "现金")
            {
                paytypeLabel.Text = "现金";
                sPayType = "4";
            }
            else
            {
                paytypeLabel.Text = "类型";
                sPayType = "";
            }
            fnStoreReload();
            paytypeselectC.Visible = false;
            //Console.WriteLine("paytype:" + STRpaytype);
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
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (keyData ==Keys.Escape)
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
                    if (fnStoreMouseControl(keyData))
                    {
                        Console.WriteLine("storelist:" + storeMouse.pos);
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
            catch(Exception ee)
            {
                Console.WriteLine("storedetail" + ee);
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        #region 键盘操作
        public void fnSetStoreMouse()
        {
            storeMouse.Position.Clear();

            storeMouse.pos = 7;
            storeMouse.listpos = 39;
            storeMouse.listmaxpos = 0;
            storeMouse._offsetX = 20;
            storeMouse._offsetY = 40;

            storeMouse.haslist = false;
            storeMouse.isindateselecter = false;

            storeMouse.Position.Add(new Point(185, 24));//收款0
            storeMouse.Position.Add(new Point(255, 24));//明细1
            storeMouse.Position.Add(new Point(325, 24));//储值2

            storeMouse.Position.Add(new Point(650, 24));//重新登录3
            storeMouse.Position.Add(new Point(715, 24));//最小化4
            storeMouse.Position.Add(new Point(755, 24));//关闭5

            storeMouse.Position.Add(new Point(60, 60));//财务流水6
            storeMouse.Position.Add(new Point(150, 60));//交易订单7

            storeMouse.Position.Add(new Point(117, 100));//起始日期8
            storeMouse.Position.Add(new Point(247, 100));//结束日期9
            storeMouse.Position.Add(new Point(430, 96));//搜索框10
            storeMouse.Position.Add(new Point(535, 100));//搜索11
            storeMouse.Position.Add(new Point(610, 100));//导出12
            #region ListItem
            storeMouse.Position.Add(new Point(735, 170));//列表第一条详情 13
            storeMouse.Position.Add(new Point(700, 170));//列表第一条退款 14
            storeMouse.Position.Add(new Point(735, 200));//列表第二条详情 15
            storeMouse.Position.Add(new Point(700, 200));//列表第二条退款 16
            storeMouse.Position.Add(new Point(735, 230));//列表第三条详情 17
            storeMouse.Position.Add(new Point(700, 230));//列表第三条退款 18  
            storeMouse.Position.Add(new Point(735, 260));//列表第四条详情 19
            storeMouse.Position.Add(new Point(700, 260));//列表第四条退款 20 
            storeMouse.Position.Add(new Point(735, 290));//列表第五条详情 21
            storeMouse.Position.Add(new Point(700, 290));//列表第五条退款 22 
            storeMouse.Position.Add(new Point(735, 320));//列表第六条详情 23
            storeMouse.Position.Add(new Point(700, 320));//列表第六条退款 24
            storeMouse.Position.Add(new Point(735, 350));//列表第七条详情 25
            storeMouse.Position.Add(new Point(700, 350));//列表第七条退款 26
            storeMouse.Position.Add(new Point(735, 380));//列表第八条详情 27
            storeMouse.Position.Add(new Point(700, 380));//列表第八条退款 28
            storeMouse.Position.Add(new Point(735, 410));//列表第九条详情 29
            storeMouse.Position.Add(new Point(700, 410));//列表第九条退款 30 
            storeMouse.Position.Add(new Point(735, 440));//列表第十条详情 31
            storeMouse.Position.Add(new Point(700, 440));//列表第十条退款 32
            #endregion
            storeMouse.Position.Add(new Point(517, 495));//上一页33
            storeMouse.Position.Add(new Point(602, 495));//下一页34
            storeMouse.Position.Add(new Point(655, 495));//页码输入35
            storeMouse.Position.Add(new Point(720, 495));//跳页36

            storeMouse.Position.Add(new Point(395, 24));//会员37

            storeMouse.Position.Add(new Point(150, 495));//汇总38
            storeMouse.Position.Add(new Point(260, 60));//储值明细39
            storeMouse.Position.Add(new Point(465, 24));//验券40
            storeMouse.Position.Add(new Point(350, 60));//优惠券核销明细41
            storeMouse.Position.Add(new Point(460, 60));//口碑券核销明细42
        }
        public bool fnStoreMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (storeMouse.isindateselecter)
            {
                return false;
            }
            else if (keyData == Keys.Down)
            {
                #region down
                switch (storeMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 40:

                    case 3:
                    case 4:
                    case 5: storeMouse.pos = 6; break;

                    case 6:
                    case 7:
                    case 39: 
                    case 41: 
                    case 42: storeMouse.pos = 8; break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        if (storeMouse.haslist)
                        {
                            storeMouse.pos = 13;
                        }
                        else
                        {
                            storeMouse.pos = 0;
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
                            if (storeMouse.pos < 11 + 2 * storeMouse.listmaxpos)
                            {
                                storeMouse.pos += 2;
                            }
                            else
                            {
                                storeMouse.pos = 33;
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
                        storeMouse.pos -= 1; break;
                    case 38:
                    case 33:
                    case 34:
                    case 35:
                    case 36: storeMouse.pos = 0; break;
                    case 37: storeMouse.pos = 6; break;
                    default: storeMouse.pos = 0; break;
                }
                storeMouse._offsetX = Parent.Parent.Location.X + 20;
                storeMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(storeMouse.Position[storeMouse.pos].X + storeMouse._offsetX, storeMouse.Position[storeMouse.pos].Y + storeMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                Console.WriteLine(storeMouse.pos);
                switch (storeMouse.pos)
                {
                    case 0: storeMouse.pos = 5; break;
                    case 1: storeMouse.pos = 0; break;
                    case 2: storeMouse.pos = 1; break;
                    case 3: storeMouse.pos = 40; break;
                    case 4: storeMouse.pos = 3; break;
                    case 5: storeMouse.pos = 4; break;
                    case 6: storeMouse.pos = 42; break;
                    case 7: storeMouse.pos = 6; break;
                    case 39: storeMouse.pos = 7; break;
                    case 8: storeMouse.pos = 12; break;
                    case 9: storeMouse.pos = 8; break;
                    case 10: storeMouse.pos = 9; break;
                    case 11: storeMouse.pos = 10; break;
                    case 12: storeMouse.pos = 11; break;
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
                        if (storetradelistS.data.orderList[(storeMouse.pos - 13) / 2].refund_status == "OPEN")
                        {
                            storeMouse.pos += 1;
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
                        storeMouse.pos -= 1; break;
                    case 33: storeMouse.pos = 38; break;
                    case 34: storeMouse.pos = 33; break;
                    case 35: storeMouse.pos = 34; break;
                    case 36: storeMouse.pos = 35; break;
                    case 37: storeMouse.pos = 2; break;
                    case 38: storeMouse.pos = 36; break;
                    case 40: storeMouse.pos = 37; break;
                    case 41: storeMouse.pos = 39; break;
                    case 42: storeMouse.pos = 41; break;
                    default: storeMouse.pos = 0; break;
                }

                storeMouse._offsetX = Parent.Parent.Location.X + 20;
                storeMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(storeMouse.Position[storeMouse.pos].X + storeMouse._offsetX, storeMouse.Position[storeMouse.pos].Y + storeMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (storeMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: storeMouse.pos = 33; break;
                    case 6:
                    case 7:
                    case 39: storeMouse.pos = 0; break;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12: storeMouse.pos = 6; break;
                    case 13: storeMouse.pos = 8; break;
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: storeMouse.pos -= 2; break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32: storeMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 38:
                    case 36: storeMouse.pos = storeMouse.listmaxpos + (storeMouse.listmaxpos + 12) - 1; break;
                    case 37: storeMouse.pos = 33; break;
                    case 40: storeMouse.pos = 33; break;
                    case 41: storeMouse.pos = 0; break;
                    case 42: storeMouse.pos = 0; break;
                    default: storeMouse.pos = 0; break;
                }
                storeMouse._offsetX = Parent.Parent.Location.X + 20;
                storeMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(storeMouse.Position[storeMouse.pos].X + storeMouse._offsetX, storeMouse.Position[storeMouse.pos].Y + storeMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (storeMouse.pos)
                {
                    case 0: storeMouse.pos = 1; break;
                    case 1: storeMouse.pos = 2; break;
                    case 2: storeMouse.pos = 37; break;
                    case 3: storeMouse.pos = 4; break;
                    case 4: storeMouse.pos = 5; break;
                    case 5: storeMouse.pos = 0; break;

                    case 6: storeMouse.pos = 7; break;
                    case 7: storeMouse.pos = 39; break;
                    case 39: storeMouse.pos = 41; break;

                    case 8: storeMouse.pos = 9; break;
                    case 9: storeMouse.pos = 10; break;
                    case 10: storeMouse.pos = 11; break;
                    case 11: storeMouse.pos = 12; break;
                    case 12: storeMouse.pos = 8; break;
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
                        if (storetradelistS.data.orderList[(storeMouse.pos - 13) / 2].refund_status == "OPEN")
                        {
                            storeMouse.pos += 1;
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
                        storeMouse.pos -= 1; break;
                    case 38: storeMouse.pos = 33; break;
                    case 33: storeMouse.pos = 34; break;
                    case 34: storeMouse.pos = 35; break;
                    case 35: storeMouse.pos = 36; break;
                    case 36: storeMouse.pos = 38; break;
                    case 37: storeMouse.pos = 40; break;
                    case 40: storeMouse.pos = 3; break;
                    case 41: storeMouse.pos = 42; break;
                    case 42: storeMouse.pos = 6; break;
                    default: storeMouse.pos = 0; break;
                }
                storeMouse._offsetX = Parent.Parent.Location.X + 20;
                storeMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(storeMouse.Position[storeMouse.pos].X + storeMouse._offsetX, storeMouse.Position[storeMouse.pos].Y + storeMouse._offsetY);
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
            storeMouse.isindateselecter = false;
            fnStoreReload();
        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            storeMouse.isindateselecter = true;
        }

        private void storeDetailControl_VisibleChanged(object sender, EventArgs e)
        {
            Focus();
        }
    }
}
public class StoreListItem
{
    /// <summary>
    /// 
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string date { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string total_money { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string img { get; set; }
    /// <summary>
    /// 高规格(002)
    /// </summary>
    public string employee_name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string order_no { get; set; }
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
    /// 
    /// </summary>
    public string order_money { get; set; }
    /// <summary>
    /// 未付款
    /// </summary>
    public string order_status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string refund_status { get; set; }

    public string member_account { get; set; }

    public string member_name { get; set; }

    public string activity_title { get; set; }
}

public class storelistData
{
    /// <summary>
    /// 
    /// </summary>
    public string itemCount { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string totalPage { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pageSign { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<StoreListItem> orderList { get; set; }
}

public class storetradelistSuccess
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
    public storelistData data { get; set; }
}

public class storetradetotalData
{
    /// <summary>
    /// 
    /// </summary>
    public string total_recharge { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string total_pay { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string num { get; set; }
}

public class storetradetotalSuccess
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
    public storetradetotalData data { get; set; }
}
