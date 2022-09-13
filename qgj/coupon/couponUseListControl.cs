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
    public partial class couponUseListControl : UserControl
    {
        confirmcancelControl searchC = new confirmcancelControl("搜索");
        confirmcancelControl outputC = new confirmcancelControl("导出");
        paytypeSelectControl paytypeselectC = new paytypeSelectControl();
        orderstatusSelectControl orderstatusselectC = new orderstatusSelectControl();

        pagenumControl pagenumC = new pagenumControl(listType.couponlist);
        WaterTextBox searchTextBox = new WaterTextBox();

        couponUseItemControl[] couponUseItemC;
        couponUseListSuccess couponUseListS;
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

        string sCouponType = "";
        string sKeyWord = "";
        string sStartTime = "";
        string sEndTime = "";

        public couponUseListControl()
        {
            InitializeComponent();

            waitinfoLabel.BackColor = Defcolor.MainBackColor;

            searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            searchTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            searchTextBox.WaterText = "优惠券名称";
            searchTextBox.SetBounds(360, 18, 120, 25);
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
            ccbCouponType.SelectedIndex = 0;
        }

        private void couponUseListControl_Load(object sender, EventArgs e)
        {
            
            setcouponListMouse();
            numPanel.BackColor = Defcolor.MainBackColor;
            listPanel.BackColor = Defcolor.MainBackColor;

            Application.DoEvents();
            timer.Start();
            listFirstLoadAction();
            Focus();
            //dateTimePicker1.Focus();
        }

        private void couponUseListControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
               Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
               Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
               Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid
           );
            PublicMethods.FillRoundRectangle(new Rectangle(350, 11, 490, 37), e.Graphics, 7, Color.White);
            PublicMethods.FrameRoundRectangle(new Rectangle(350, 11, 490, 37), e.Graphics, 7, Defcolor.MainGrayLineColor);
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
                pagenumC.fnSetAllPage(couponUseListS.data.totalPage.ToString());

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

                    int _listnum = Convert.ToInt32(couponUseListS.data.consumeList.Count);

                    couponlistMouse.listmaxpos = _listnum;
                    couponlistMouse.haslist = false;

                    couponUseItemC = new couponUseItemControl[_listnum];
                    for (int _i = 0; _i < _listnum; _i++)
                    {
                        couponlistMouse.haslist = true;

                        couponUseItemC[_i] = new couponUseItemControl();
                        if (_i % 2 != 0)
                        {
                            couponUseItemC[_i].changeBackColor(Color.FromArgb(247, 247, 247));
                        }
                        else
                        {
                            couponUseItemC[_i].changeBackColor(Color.FromArgb(255, 255, 255));
                        }
                        couponUseItemC[_i].Location = new Point(0, _i * 30);
                        couponUseItemC[_i].timeLabel.Text = couponUseListS.data.consumeList[_i].date;
                        string coupon_type = "";
                        switch(couponUseListS.data.consumeList[_i].coupon_type)
                        {
                            case "CASH":coupon_type = "代金券";break;
                            case "DISCOUNT": coupon_type = "折扣券"; break;
                            case "GIFT": coupon_type = "兑换券"; break;
                            default: coupon_type = ""; break;
                        }
                        couponUseItemC[_i].coupontypeLabel.Text = coupon_type;
                        couponUseItemC[_i].couponnameLabel.Text = couponUseListS.data.consumeList[_i].title;
                        couponUseItemC[_i].usetypeLabel.Text = couponUseListS.data.consumeList[_i].use_channel;
                        couponUseItemC[_i].ordermoneyLabel.Text = couponUseListS.data.consumeList[_i].money;
                        couponUseItemC[_i].orderreceiptLabel.Text = couponUseListS.data.consumeList[_i].receipt_money;
                        couponUseItemC[_i].nicknameLabel.Text = couponUseListS.data.consumeList[_i].nickname;
                        couponUseItemC[_i].personLabel.Text = couponUseListS.data.consumeList[_i].employee_name;
                        couponUseItemC[_i].detailLabel.Name = couponUseListS.data.consumeList[_i].code.ToString();
                        couponUseItemC[_i].detailLabel.MouseUp += new MouseEventHandler(detail_MouseUp);
                        listPanel.Controls.Add(couponUseItemC[_i]);
                    }
                }
                catch (Exception e)
                {
                    setcouponListMouse();
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
            UrlClass _urlC = new UrlClass(Url.couponlist);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            if (sKeyWord != "")
            {
                _urlC.addParameter("coupon_name", sKeyWord);
            }
            if (sCouponType != "")
            {
                _urlC.addParameter("type", sCouponType);
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
                couponUseListS = (couponUseListSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(couponUseListSuccess));
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

        private void getCouponType()
        {
            switch (ccbCouponType.SelectedIndex)
            {
                case 0: sCouponType = ""; break;
                case 1: sCouponType = "CASH"; break;
                case 2: sCouponType = "DISCOUNT"; break;
                case 3: sCouponType = "GIFT"; break;
                default: sCouponType = ""; break;
            }
        }

        private void detail_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                couponUseDetailForm _coupondetailF = new couponUseDetailForm(((Label)sender).Name);
                _coupondetailF.TopMost = true;
                _coupondetailF.StartPosition = FormStartPosition.CenterParent;
                _coupondetailF.ShowDialog();
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
                excelForm _excelF = new excelForm(sStartTime, sEndTime, sKeyWord,sCouponType, true);
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
                        Console.WriteLine("couponlsit:" + couponlistMouse.pos);
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

        private void ccbCouponType_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCouponType();
        }

        #region 键盘操作
        public void setcouponListMouse()
        {
            couponlistMouse.Position.Clear();

            couponlistMouse.pos = 41;
            couponlistMouse.listpos = 0;
            couponlistMouse.listmaxpos = 0;
            couponlistMouse._offsetX = 20;
            couponlistMouse._offsetY = 40;

            couponlistMouse.haslist = false;
            couponlistMouse.isindateselecter = false;

            couponlistMouse.Position.Add(new Point(185, 24));//收款0
            couponlistMouse.Position.Add(new Point(255, 24));//明细1
            couponlistMouse.Position.Add(new Point(325, 24));//储值2

            couponlistMouse.Position.Add(new Point(650, 24));//重新登录3
            couponlistMouse.Position.Add(new Point(715, 24));//最小化4
            couponlistMouse.Position.Add(new Point(755, 24));//关闭5

            couponlistMouse.Position.Add(new Point(60, 60));//财务流水6
            couponlistMouse.Position.Add(new Point(150, 60));//交易订单7
            couponlistMouse.Position.Add(new Point(260, 60));//储值明细8

            couponlistMouse.Position.Add(new Point(117, 100));//起始日期9
            couponlistMouse.Position.Add(new Point(247, 100));//结束日期10
            couponlistMouse.Position.Add(new Point(430, 96));//搜索框11
            couponlistMouse.Position.Add(new Point(535, 100));//搜索12

            #region ListItem
            couponlistMouse.Position.Add(new Point(735, 170));//列表第一条详情 13
            couponlistMouse.Position.Add(new Point(700, 170));//列表第一条退款 14
            couponlistMouse.Position.Add(new Point(735, 200));//列表第二条详情 15
            couponlistMouse.Position.Add(new Point(700, 200));//列表第二条退款 16
            couponlistMouse.Position.Add(new Point(735, 230));//列表第三条详情 17
            couponlistMouse.Position.Add(new Point(700, 230));//列表第三条退款 18  
            couponlistMouse.Position.Add(new Point(735, 260));//列表第四条详情 19
            couponlistMouse.Position.Add(new Point(700, 260));//列表第四条退款 20 
            couponlistMouse.Position.Add(new Point(735, 290));//列表第五条详情 21
            couponlistMouse.Position.Add(new Point(700, 290));//列表第五条退款 22 
            couponlistMouse.Position.Add(new Point(735, 320));//列表第六条详情 23
            couponlistMouse.Position.Add(new Point(700, 320));//列表第六条退款 24
            couponlistMouse.Position.Add(new Point(735, 350));//列表第七条详情 25
            couponlistMouse.Position.Add(new Point(700, 350));//列表第七条退款 26
            couponlistMouse.Position.Add(new Point(735, 380));//列表第八条详情 27
            couponlistMouse.Position.Add(new Point(700, 380));//列表第八条退款 28
            couponlistMouse.Position.Add(new Point(735, 410));//列表第九条详情 29
            couponlistMouse.Position.Add(new Point(700, 410));//列表第九条退款 30 
            couponlistMouse.Position.Add(new Point(735, 440));//列表第十条详情 31
            couponlistMouse.Position.Add(new Point(700, 440));//列表第十条退款 32
            #endregion
            couponlistMouse.Position.Add(new Point(275, 495));//更多汇总//33
            couponlistMouse.Position.Add(new Point(517, 495));//上一页34
            couponlistMouse.Position.Add(new Point(602, 495));//下一页35
            couponlistMouse.Position.Add(new Point(655, 495));//页码输入36
            couponlistMouse.Position.Add(new Point(720, 495));//跳页37

            couponlistMouse.Position.Add(new Point(395, 24));//会员38
            couponlistMouse.Position.Add(new Point(610, 100));//导出39
            couponlistMouse.Position.Add(new Point(465, 24));//验券40
            couponlistMouse.Position.Add(new Point(350, 60));//优惠券核销明细41
            couponlistMouse.Position.Add(new Point(460, 60));//口碑券核销明细42
        }
        public bool MouseControl(System.Windows.Forms.Keys keyData)
        {
            if (couponlistMouse.isindateselecter)
            {
                return false;
            }
            else if (keyData == Keys.Down)
            {
                #region down
                switch (couponlistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: couponlistMouse.pos = 6; break;
                    case 6:
                    case 7:
                    case 8: couponlistMouse.pos = 9; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39:
                        if (couponlistMouse.haslist)
                        {
                            couponlistMouse.pos = 13;
                        }
                        else
                        {
                            couponlistMouse.pos = 0;
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
                            if (couponlistMouse.pos < 11 + 2 * couponlistMouse.listmaxpos)
                            {
                                couponlistMouse.pos += 2;
                            }
                            else
                            {
                                couponlistMouse.pos = 33;
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
                        couponlistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: couponlistMouse.pos = 0; break;
                    case 38: couponlistMouse.pos = 6; break;
                    case 40: couponlistMouse.pos = 6; break;
                    case 41: couponlistMouse.pos = 9; break;
                    case 42: couponlistMouse.pos = 9; break;
                    default: couponlistMouse.pos = 0; break;
                }
                couponlistMouse._offsetX = Parent.Parent.Location.X + 20;
                couponlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(couponlistMouse.Position[couponlistMouse.pos].X + couponlistMouse._offsetX, couponlistMouse.Position[couponlistMouse.pos].Y + couponlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                Console.WriteLine(couponlistMouse.pos);
                switch (couponlistMouse.pos)
                {
                    case 0: couponlistMouse.pos = 5; break;
                    case 1: couponlistMouse.pos = 0; break;
                    case 2: couponlistMouse.pos = 1; break;
                    case 38: couponlistMouse.pos = 2; break;
                    case 40: couponlistMouse.pos = 38; break;
                    case 3: couponlistMouse.pos = 40; break;
                    case 4: couponlistMouse.pos = 3; break;
                    case 5: couponlistMouse.pos = 4; break;

                    case 6: couponlistMouse.pos = 42; break;
                    case 7: couponlistMouse.pos = 6; break;
                    case 8: couponlistMouse.pos = 7; break;
                    case 41: couponlistMouse.pos = 8; break;
                    case 42: couponlistMouse.pos = 41; break;

                    case 9: couponlistMouse.pos = 39; break;
                    case 10: couponlistMouse.pos = 9; break;
                    case 11: couponlistMouse.pos = 10; break;
                    case 12: couponlistMouse.pos = 11; break;
                    case 39: couponlistMouse.pos = 12; break;
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
                        couponlistMouse.pos -= 1; break;
                    case 33: couponlistMouse.pos = 37; break;
                    case 34: couponlistMouse.pos = 33; break;
                    case 35: couponlistMouse.pos = 34; break;
                    case 36: couponlistMouse.pos = 35; break;
                    case 37: couponlistMouse.pos = 36; break;
                    default: couponlistMouse.pos = 0; break;
                }

                couponlistMouse._offsetX = Parent.Parent.Location.X + 20;
                couponlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(couponlistMouse.Position[couponlistMouse.pos].X + couponlistMouse._offsetX, couponlistMouse.Position[couponlistMouse.pos].Y + couponlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (couponlistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: couponlistMouse.pos = 33; break;
                    case 6:
                    case 7:
                    case 8: couponlistMouse.pos = 0; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39: couponlistMouse.pos = 6; break;
                    case 13: couponlistMouse.pos = 9; break;
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: couponlistMouse.pos -= 2; break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32: couponlistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: couponlistMouse.pos = couponlistMouse.listmaxpos + (couponlistMouse.listmaxpos + 12) - 1; break;
                    case 38: couponlistMouse.pos = 33; break;
                    case 40: couponlistMouse.pos = 33; break;
                    case 41: couponlistMouse.pos = 0; break;
                    case 42: couponlistMouse.pos = 0; break;
                    default: couponlistMouse.pos = 0; break;
                }
                couponlistMouse._offsetX = Parent.Parent.Location.X + 20;
                couponlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(couponlistMouse.Position[couponlistMouse.pos].X + couponlistMouse._offsetX, couponlistMouse.Position[couponlistMouse.pos].Y + couponlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (couponlistMouse.pos)
                {
                    case 0: couponlistMouse.pos = 1; break;
                    case 1: couponlistMouse.pos = 2; break;
                    case 2: couponlistMouse.pos = 38; break;
                    case 38: couponlistMouse.pos = 40; break;
                    case 40: couponlistMouse.pos = 3; break;
                    case 3: couponlistMouse.pos = 4; break;
                    case 4: couponlistMouse.pos = 5; break;
                    case 5: couponlistMouse.pos = 0; break;

                    case 6: couponlistMouse.pos = 7; break;
                    case 7: couponlistMouse.pos = 8; break;
                    case 8: couponlistMouse.pos = 41; break;
                    case 41: couponlistMouse.pos = 42; break;
                    case 42: couponlistMouse.pos = 6; break;

                    case 9: couponlistMouse.pos = 10; break;
                    case 10: couponlistMouse.pos = 11; break;
                    case 11: couponlistMouse.pos = 12; break;
                    case 12: couponlistMouse.pos = 39; break;
                    case 39: couponlistMouse.pos = 9; break;
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
                        couponlistMouse.pos -= 1; break;
                    case 33: couponlistMouse.pos = 34; break;
                    case 34: couponlistMouse.pos = 35; break;
                    case 35: couponlistMouse.pos = 36; break;
                    case 36: couponlistMouse.pos = 37; break;
                    case 37: couponlistMouse.pos = 33; break;
                    default: couponlistMouse.pos = 0; break;
                }
                couponlistMouse._offsetX = Parent.Parent.Location.X + 20;
                couponlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(couponlistMouse.Position[couponlistMouse.pos].X + couponlistMouse._offsetX, couponlistMouse.Position[couponlistMouse.pos].Y + couponlistMouse._offsetY);
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
    public class ConsumeListItem
    {
        public int id { get; set; }
        public string date { get; set; }
        public string member_avatar { get; set; }
        public string title { get; set; }
        public string sub_title { get; set; }
        public string use_channel { get; set; }
        public string code { get; set; }
        public string use_time { get; set; }
        public string money { get; set; }
        public string coupon_type { get; set; }
        public string receipt_money { get; set; }
        public string nickname { get; set; }
        public string employee_name { get; set; }
    }
    public class couponUseList
    {
        public string itemCount { get; set; }
        public string totalPage { get; set; }
        public string pageSign { get; set; }
        public List<ConsumeListItem> consumeList { get; set; }
    }
    public class couponUseListSuccess
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public couponUseList data { get; set; }
    }
}
