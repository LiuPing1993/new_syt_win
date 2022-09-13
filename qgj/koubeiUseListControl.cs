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
    public partial class koubeiUseListControl : UserControl
    {
        confirmcancelControl searchC = new confirmcancelControl("搜索");
        confirmcancelControl outputC = new confirmcancelControl("导出");

        pagenumControl pagenumC = new pagenumControl(listType.koubeilist);
        WaterTextBox searchTextBox = new WaterTextBox();

        koubeiUseItemControl[] koubeiUseItemC;
        koubeilistSuccess koubeiListS;
        koubeiSummarySuccess koubeiSummaryS;
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

        string sKeyWord = "";
        string sStartTime = "";
        string sEndTime = "";

        Label couponMoneyTitle = new Label();
        Label couponNumTitle = new Label();
        Label merchantDisTitle = new Label();
        Label receiptTitle = new Label();

        Label couponMoney = new Label();
        Label couponNum = new Label();
        Label merchantDis = new Label();
        Label receipt = new Label();

        public koubeiUseListControl()
        {
            InitializeComponent();

            waitinfoLabel.BackColor = Defcolor.MainBackColor;

            searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            searchTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            searchTextBox.WaterText = "口碑券名称";
            searchTextBox.SetBounds(300, 18, 150, 25);
            searchTextBox.TabIndex = 3;
            Controls.Add(searchTextBox);

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

            d = new addDelegate(ListLoad);
        }

        private void koubeiUseListControl_Load(object sender, EventArgs e)
        {
            setkoubeiListMouse();
            timePanel.BackColor = Defcolor.MainBackColor;
            listPanel.BackColor = Defcolor.MainBackColor;

            #region 汇总控件初始化
            Font myfont = new System.Drawing.Font(UserClass.fontName, 9);
            couponMoneyTitle.Font = myfont;
            couponMoneyTitle.Text = "券金额 :";
            couponMoneyTitle.AutoSize = true;
            couponMoneyTitle.ForeColor = Defcolor.FontLiteGrayColor;
            couponMoneyTitle.BackColor = Defcolor.MainBackColor;
            couponMoneyTitle.Location = new Point(5, 400);
            Controls.Add(couponMoneyTitle);

            couponMoney.Font = myfont;
            couponMoney.AutoSize = true;
            couponMoney.ForeColor = Color.Black;
            couponMoney.BackColor = Defcolor.MainBackColor;
            couponMoney.Location = new Point(70, 400);
            Controls.Add(couponMoney);

            couponNumTitle.Font = myfont;
            couponNumTitle.Text = "券笔数 :";
            couponNumTitle.AutoSize = true;
            couponNumTitle.ForeColor = Defcolor.FontLiteGrayColor;
            couponNumTitle.BackColor = Defcolor.MainBackColor;
            couponNumTitle.Location = new Point(5, 420);
            Controls.Add(couponNumTitle);

            couponNum.Font = myfont;
            couponNum.AutoSize = true;
            couponNum.ForeColor = Color.Black;
            couponNum.BackColor = Defcolor.MainBackColor;
            couponNum.Location = new Point(70, 420);
            Controls.Add(couponNum);

            merchantDisTitle.Font = myfont;
            merchantDisTitle.Text = "商家优惠 :";
            merchantDisTitle.AutoSize = true;
            merchantDisTitle.ForeColor = Defcolor.FontLiteGrayColor;
            merchantDisTitle.BackColor = Defcolor.MainBackColor;
            merchantDisTitle.Location = new Point(145, 400);
            Controls.Add(merchantDisTitle);

            merchantDis.Font = myfont;
            merchantDis.AutoSize = true;
            merchantDis.ForeColor = Defcolor.MainRadColor;
            merchantDis.BackColor = Defcolor.MainBackColor;
            merchantDis.Location = new Point(210, 400);
            Controls.Add(merchantDis);


            receiptTitle.Font = myfont;
            receiptTitle.Text = "实收金额 :";
            receiptTitle.AutoSize = true;
            receiptTitle.ForeColor = Defcolor.FontLiteGrayColor;
            receiptTitle.BackColor = Defcolor.MainBackColor;
            receiptTitle.Location = new Point(275, 400);
            Controls.Add(receiptTitle);

            receipt.Font = myfont;
            receipt.AutoSize = true;
            receipt.ForeColor = Defcolor.FontBlueColor;
            receipt.BackColor = Defcolor.MainBackColor;
            receipt.Location = new Point(340, 400);
            Controls.Add(receipt);
            #endregion

            Application.DoEvents();
            timer.Start();
            listFirstLoadAction();
            Focus();
        }

        private void koubeiUseListControl_Paint(object sender, PaintEventArgs e)
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

        private void listFirstLoadAction()
        {
            try
            {
                ListHttp();
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

            try
            {
                ListLoad();
            }
            catch (Exception e)
            {
                iHttpResult = 2;
                sHttpResult = e.ToString();
            }

            try
            {
                if (ListTotalHttp())
                {
                    couponMoney.Text = koubeiSummaryS.data.coupon_money;
                    couponNum.Text = koubeiSummaryS.data.count;
                    merchantDis.Text = koubeiSummaryS.data.alipay_merchant_discount_count;
                    receipt.Text = koubeiSummaryS.data.trade_money;
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
                pagenumC.fnSetAllPage(koubeiListS.data.totalPage.ToString());

            }
            catch
            {
                pagenumC.fnSetAllPage("1");
            }
        }

        public void listPageLoad()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(listPageLoadAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }

        private void listPageLoadAction()
        {
            try
            {
                ListHttp();
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

        private void listReload()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(listReloadAction);
                thread.IsBackground = true;
                thread.Start();
                t1 = thread;
            }
        }

        private void listReloadAction()
        {
            try
            {
                sKeyWord = searchTextBox.Text.ToString();
                sPageNum = "1";
                ListHttp();
                ListTotalHttp();
                IsReTotal = true;
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

        private void ListLoad()
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
                listPanel.Controls.Clear();
                try
                {

                    //判断是否需要刷新统计展示数据
                    if (IsReTotal)
                    {
                        couponMoney.Text = koubeiSummaryS.data.coupon_money;
                        couponNum.Text = koubeiSummaryS.data.count;
                        merchantDis.Text = koubeiSummaryS.data.alipay_merchant_discount_count;
                        receipt.Text = koubeiSummaryS.data.trade_money;
                        IsReTotal = false;
                        if (koubeiListS.data.totalPage == null)
                        {
                            pagenumC.fnSetAllPage("1");
                            return;
                        }
                        else
                        {
                            pagenumC.fnSetAllPage(koubeiListS.data.totalPage.ToString());
                        }
                    }

                    int _listnum = Convert.ToInt32(koubeiListS.data.flowList.Count);

                    koubeilistMouse.listmaxpos = _listnum;
                    koubeilistMouse.haslist = false;

                    koubeiUseItemC = new koubeiUseItemControl[_listnum];
                    for (int _i = 0; _i < _listnum; _i++)
                    {
                        koubeilistMouse.haslist = true;

                        koubeiUseItemC[_i] = new koubeiUseItemControl();
                        if (_i % 2 != 0)
                        {
                            koubeiUseItemC[_i].changeBackColor(Color.FromArgb(247, 247, 247));
                        }
                        else
                        {
                            koubeiUseItemC[_i].changeBackColor(Color.FromArgb(255, 255, 255));
                        }
                        koubeiUseItemC[_i].Location = new Point(0, _i * 30);
                        koubeiUseItemC[_i].timeLabel.Text = koubeiListS.data.flowList[_i].date;

                        koubeiUseItemC[_i].couponnameLabel.Text = koubeiListS.data.flowList[_i].name;
                        koubeiUseItemC[_i].couponmoneyLabel.Text = koubeiListS.data.flowList[_i].coupon_money;
                        koubeiUseItemC[_i].merchantdisLabel.Text = koubeiListS.data.flowList[_i].merchant_money;
                        koubeiUseItemC[_i].orderreceiptLabel.Text = koubeiListS.data.flowList[_i].trade_money;
                        koubeiUseItemC[_i].personLabel.Text = koubeiListS.data.flowList[_i].employee;
                        koubeiUseItemC[_i].detailLabel.Name = koubeiListS.data.flowList[_i].code;
                        koubeiUseItemC[_i].detailLabel.MouseUp += new MouseEventHandler(detail_MouseUp);
                        listPanel.Controls.Add(koubeiUseItemC[_i]);
                    }
                }
                catch (Exception e)
                {
                    setkoubeiListMouse();
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    Refresh();
                }
            }
        }

        private bool ListHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.koubeilist);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sKeyWord != "")
            {
                _urlC.addParameter("key_words", sKeyWord);
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
                koubeiListS = (koubeilistSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(koubeilistSuccess));
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

        private bool ListTotalHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.koubeisummary);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sKeyWord != "")
            {
                _urlC.addParameter("key_words", sKeyWord);
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
                koubeiSummaryS = (koubeiSummarySuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(koubeiSummarySuccess));
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

        private void detail_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                koubeiUseDetailForm _koubeidetailF = new koubeiUseDetailForm(((Label)sender).Name);
                _koubeidetailF.TopMost = true;
                _koubeidetailF.StartPosition = FormStartPosition.CenterParent;
                _koubeidetailF.ShowDialog();
                ((main)Parent.Parent).Refresh();
            }
        }

        private void search_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                listReload();
            }
        }

        private void output_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                excelForm _excelF = new excelForm(sStartTime, sEndTime, sKeyWord, true);
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
            //listMouse.isindateselecter = false;
            listReload();
        }
        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            //listMouse.isindateselecter = true;
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
                    ((main)Parent.Parent).fnShowGatherC();
                    return true;
                }
                else if (Visible && (IsThreadRun == false) && UserClass.isUseKeyBorad)
                {
                    
                    if (MouseControl(keyData))
                    {
                        Console.WriteLine("koubeilsit:" + koubeilistMouse.pos);
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
                Console.WriteLine("couponlist:" + ee);
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        #region 键盘操作
        public void setkoubeiListMouse()
        {
            koubeilistMouse.Position.Clear();

            koubeilistMouse.pos = 42;
            koubeilistMouse.listpos = 0;
            koubeilistMouse.listmaxpos = 0;
            koubeilistMouse._offsetX = 20;
            koubeilistMouse._offsetY = 40;

            koubeilistMouse.haslist = false;
            koubeilistMouse.isindateselecter = false;

            koubeilistMouse.Position.Add(new Point(185, 24));//收款0
            koubeilistMouse.Position.Add(new Point(255, 24));//明细1
            koubeilistMouse.Position.Add(new Point(325, 24));//储值2

            koubeilistMouse.Position.Add(new Point(650, 24));//重新登录3
            koubeilistMouse.Position.Add(new Point(715, 24));//最小化4
            koubeilistMouse.Position.Add(new Point(755, 24));//关闭5

            koubeilistMouse.Position.Add(new Point(60, 60));//财务流水6
            koubeilistMouse.Position.Add(new Point(150, 60));//交易订单7
            koubeilistMouse.Position.Add(new Point(260, 60));//储值明细8

            koubeilistMouse.Position.Add(new Point(117, 100));//起始日期9
            koubeilistMouse.Position.Add(new Point(247, 100));//结束日期10
            koubeilistMouse.Position.Add(new Point(430, 96));//搜索框11
            koubeilistMouse.Position.Add(new Point(535, 100));//搜索12

            #region ListItem
            koubeilistMouse.Position.Add(new Point(735, 170));//列表第一条详情 13
            koubeilistMouse.Position.Add(new Point(700, 170));//列表第一条退款 14
            koubeilistMouse.Position.Add(new Point(735, 200));//列表第二条详情 15
            koubeilistMouse.Position.Add(new Point(700, 200));//列表第二条退款 16
            koubeilistMouse.Position.Add(new Point(735, 230));//列表第三条详情 17
            koubeilistMouse.Position.Add(new Point(700, 230));//列表第三条退款 18  
            koubeilistMouse.Position.Add(new Point(735, 260));//列表第四条详情 19
            koubeilistMouse.Position.Add(new Point(700, 260));//列表第四条退款 20 
            koubeilistMouse.Position.Add(new Point(735, 290));//列表第五条详情 21
            koubeilistMouse.Position.Add(new Point(700, 290));//列表第五条退款 22 
            koubeilistMouse.Position.Add(new Point(735, 320));//列表第六条详情 23
            koubeilistMouse.Position.Add(new Point(700, 320));//列表第六条退款 24
            koubeilistMouse.Position.Add(new Point(735, 350));//列表第七条详情 25
            koubeilistMouse.Position.Add(new Point(700, 350));//列表第七条退款 26
            koubeilistMouse.Position.Add(new Point(735, 380));//列表第八条详情 27
            koubeilistMouse.Position.Add(new Point(700, 380));//列表第八条退款 28
            koubeilistMouse.Position.Add(new Point(735, 410));//列表第九条详情 29
            koubeilistMouse.Position.Add(new Point(700, 410));//列表第九条退款 30 
            koubeilistMouse.Position.Add(new Point(735, 440));//列表第十条详情 31
            koubeilistMouse.Position.Add(new Point(700, 440));//列表第十条退款 32
            #endregion
            koubeilistMouse.Position.Add(new Point(275, 495));//更多汇总//33
            koubeilistMouse.Position.Add(new Point(517, 495));//上一页34
            koubeilistMouse.Position.Add(new Point(602, 495));//下一页35
            koubeilistMouse.Position.Add(new Point(655, 495));//页码输入36
            koubeilistMouse.Position.Add(new Point(720, 495));//跳页37

            koubeilistMouse.Position.Add(new Point(395, 24));//会员38
            koubeilistMouse.Position.Add(new Point(610, 100));//导出39
            koubeilistMouse.Position.Add(new Point(465, 24));//验券40
            koubeilistMouse.Position.Add(new Point(350, 60));//优惠券核销明细41
            koubeilistMouse.Position.Add(new Point(460, 60));//口碑券核销明细42
        }
        public bool MouseControl(System.Windows.Forms.Keys keyData)
        {
            if (koubeilistMouse.isindateselecter)
            {
                return false;
            }
            else if (keyData == Keys.Down)
            {
                #region down
                switch (koubeilistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: koubeilistMouse.pos = 6; break;
                    case 6:
                    case 7:
                    case 8: koubeilistMouse.pos = 9; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39:
                        if (koubeilistMouse.haslist)
                        {
                            koubeilistMouse.pos = 13;
                        }
                        else
                        {
                            koubeilistMouse.pos = 0;
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
                            if (koubeilistMouse.pos < 11 + 2 * koubeilistMouse.listmaxpos)
                            {
                                koubeilistMouse.pos += 2;
                            }
                            else
                            {
                                koubeilistMouse.pos = 33;
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
                        koubeilistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: koubeilistMouse.pos = 0; break;
                    case 38: koubeilistMouse.pos = 6; break;
                    case 40: koubeilistMouse.pos = 6; break;
                    case 41: koubeilistMouse.pos = 9; break;
                    case 42: koubeilistMouse.pos = 9; break;
                    default: koubeilistMouse.pos = 0; break;
                }
                koubeilistMouse._offsetX = Parent.Parent.Location.X + 20;
                koubeilistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(koubeilistMouse.Position[koubeilistMouse.pos].X + koubeilistMouse._offsetX, koubeilistMouse.Position[koubeilistMouse.pos].Y + koubeilistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                Console.WriteLine(koubeilistMouse.pos);
                switch (koubeilistMouse.pos)
                {
                    case 0: koubeilistMouse.pos = 5; break;
                    case 1: koubeilistMouse.pos = 0; break;
                    case 2: koubeilistMouse.pos = 1; break;
                    case 38: koubeilistMouse.pos = 2; break;
                    case 40: koubeilistMouse.pos = 38; break;
                    case 3: koubeilistMouse.pos = 40; break;
                    case 4: koubeilistMouse.pos = 3; break;
                    case 5: koubeilistMouse.pos = 4; break;

                    case 6: koubeilistMouse.pos = 42; break;
                    case 7: koubeilistMouse.pos = 6; break;
                    case 8: koubeilistMouse.pos = 7; break;
                    case 41: koubeilistMouse.pos = 8; break;
                    case 42: koubeilistMouse.pos = 41; break;

                    case 9: koubeilistMouse.pos = 39; break;
                    case 10: koubeilistMouse.pos = 9; break;
                    case 11: koubeilistMouse.pos = 10; break;
                    case 12: koubeilistMouse.pos = 11; break;
                    case 39: koubeilistMouse.pos = 12; break;
                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: break;
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
                        koubeilistMouse.pos -= 1; break;
                    case 33: koubeilistMouse.pos = 37; break;
                    case 34: koubeilistMouse.pos = 33; break;
                    case 35: koubeilistMouse.pos = 34; break;
                    case 36: koubeilistMouse.pos = 35; break;
                    case 37: koubeilistMouse.pos = 36; break;
                    default: koubeilistMouse.pos = 0; break;
                }

                koubeilistMouse._offsetX = Parent.Parent.Location.X + 20;
                koubeilistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(koubeilistMouse.Position[koubeilistMouse.pos].X + koubeilistMouse._offsetX, koubeilistMouse.Position[koubeilistMouse.pos].Y + koubeilistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (koubeilistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: koubeilistMouse.pos = 33; break;
                    case 6:
                    case 7:
                    case 8: koubeilistMouse.pos = 0; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39: koubeilistMouse.pos = 6; break;
                    case 13: koubeilistMouse.pos = 9; break;
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: koubeilistMouse.pos -= 2; break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32: koubeilistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: koubeilistMouse.pos = koubeilistMouse.listmaxpos + (koubeilistMouse.listmaxpos + 12) - 1; break;
                    case 38: koubeilistMouse.pos = 33; break;
                    case 40: koubeilistMouse.pos = 33; break;
                    case 41: koubeilistMouse.pos = 0; break;
                    case 42: koubeilistMouse.pos = 0; break;
                    default: koubeilistMouse.pos = 0; break;
                }
                koubeilistMouse._offsetX = Parent.Parent.Location.X + 20;
                koubeilistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(koubeilistMouse.Position[koubeilistMouse.pos].X + koubeilistMouse._offsetX, koubeilistMouse.Position[koubeilistMouse.pos].Y + koubeilistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (koubeilistMouse.pos)
                {
                    case 0: koubeilistMouse.pos = 1; break;
                    case 1: koubeilistMouse.pos = 2; break;
                    case 2: koubeilistMouse.pos = 38; break;
                    case 38: koubeilistMouse.pos = 40; break;
                    case 40: koubeilistMouse.pos = 3; break;
                    case 3: koubeilistMouse.pos = 4; break;
                    case 4: koubeilistMouse.pos = 5; break;
                    case 5: koubeilistMouse.pos = 0; break;

                    case 6: koubeilistMouse.pos = 7; break;
                    case 7: koubeilistMouse.pos = 8; break;
                    case 8: koubeilistMouse.pos = 41; break;
                    case 41: koubeilistMouse.pos = 42; break;
                    case 42: koubeilistMouse.pos = 6; break;

                    case 9: koubeilistMouse.pos = 10; break;
                    case 10: koubeilistMouse.pos = 11; break;
                    case 11: koubeilistMouse.pos = 12; break;
                    case 12: koubeilistMouse.pos = 39; break;
                    case 39: koubeilistMouse.pos = 9; break;
                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: break;
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
                        koubeilistMouse.pos -= 1; break;
                    case 33: koubeilistMouse.pos = 34; break;
                    case 34: koubeilistMouse.pos = 35; break;
                    case 35: koubeilistMouse.pos = 36; break;
                    case 36: koubeilistMouse.pos = 37; break;
                    case 37: koubeilistMouse.pos = 33; break;
                    default: koubeilistMouse.pos = 0; break;
                }
                koubeilistMouse._offsetX = Parent.Parent.Location.X + 20;
                koubeilistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(koubeilistMouse.Position[koubeilistMouse.pos].X + koubeilistMouse._offsetX, koubeilistMouse.Position[koubeilistMouse.pos].Y + koubeilistMouse._offsetY);
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

    public class FlowListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 胖龙虾一份
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coupon_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string merchant_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trade_money { get; set; }
        /// <summary>
        /// 杨斌(0927)
        /// </summary>
        public string employee { get; set; }
    }

    public class koubeiList
    {
        /// <summary>
        /// 
        /// </summary>
        public string itemCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string page_sign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totalPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FlowListItem> flowList { get; set; }
    }

    public class koubeilistSuccess
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
        public koubeiList data { get; set; }
    }

    public class koubeiSummary
    {
        /// <summary>
        /// 
        /// </summary>
        public string count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coupon_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alipay_merchant_discount_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alipay_discount_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trade_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_original_money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_current_money { get; set; }
    }

    public class koubeiSummarySuccess
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
        public koubeiSummary data { get; set; }
    }

}
