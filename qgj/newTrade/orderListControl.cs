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
    public partial class orderListControl : baseRequestControl
    {
        confirmcancelControl searchC = new confirmcancelControl("搜索");
        confirmcancelControl outputC = new confirmcancelControl("导出");
        paytypeSelectControl paytypeselectC = new paytypeSelectControl();
        orderstatusSelectControl orderstatusselectC = new orderstatusSelectControl();

        pagenumControl pagenumC = new pagenumControl(listType.neworderlist);
        WaterTextBox searchTextBox = new WaterTextBox();

        orderListItemControl[] orderlistitemC;
        Color colorTop = Color.FromArgb(223, 222, 222);

        public string sPageNum = "1";
        bool IsReTotal = false;

        string sOrderNum = "";
        string sPayChannel = "";
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
        int iTemp = 0;

        newTrade.order_list ol;
        public orderListControl()
        {
            InitializeComponent();

            waitinfoLabel.BackColor = Defcolor.MainBackColor;

            searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            searchTextBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            //searchTextBox.WatermarkText = "订单号";
            searchTextBox.WaterText = "订单号";
            searchTextBox.SetBounds(300, 18, 150, 25);
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

        }

        private void orderListControl_Load(object sender, EventArgs e)
        {
            setdetailMouse();
            #region 控件

            numPanel.BackColor = Defcolor.MainBackColor;
            orderlistPanel.BackColor = Defcolor.MainBackColor;
            //numLabel.BackColor = Defcolor.MainBackColor;

            paychannelLabel.BackColor = colorTop;
            paytypeselectC.Location = new Point(318, 78);
            paytypeselectC.label1.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label2.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label3.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label4.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label5.MouseUp += new MouseEventHandler(paytypeSelect);
            paytypeselectC.label6.MouseUp += new MouseEventHandler(paytypeSelect);
            Controls.Add(paytypeselectC);
            paytypeselectC.Hide();

            orderstatusLabel.BackColor = colorTop;
            orderstatusselectC.Location = new Point(538, 78);
            orderstatusselectC.label1.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label2.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label3.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label4.MouseUp += new MouseEventHandler(orderstatusSelect);
            orderstatusselectC.label5.MouseUp += new MouseEventHandler(orderstatusSelect);
            Controls.Add(orderstatusselectC);
            orderstatusselectC.Hide();

            Font myfont = new System.Drawing.Font(UserClass.fontName, 9);
            lblOrderTotalTitle.Font = myfont;
            lblOrderTotalTitle.Text = "订单金额 :";
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
            lblOrderNumTitle.Text = "订单笔数 :";
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
            lblRefundTitle.Text = "优惠金额 :";//"退款金额 :";
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
            //Controls.Add(lblRefundNumTitle);

            lblRefundNum.Font = myfont;
            lblRefundNum.AutoSize = true;
            lblRefundNum.ForeColor = Defcolor.MainRadColor;
            lblRefundNum.BackColor = Defcolor.MainBackColor;
            lblRefundNum.Location = new Point(210, 420);
            //Controls.Add(lblRefundNum);

            lblReceiptTitle.Font = myfont;
            lblReceiptTitle.Text = "实收金额 :";
            lblReceiptTitle.AutoSize = true;
            lblReceiptTitle.ForeColor = Defcolor.FontLiteGrayColor;
            lblReceiptTitle.BackColor = Defcolor.MainBackColor;
            lblReceiptTitle.Location = new Point(275, 400);
            //Controls.Add(lblReceiptTitle);

            lblReceipt.Font = myfont;
            lblReceipt.AutoSize = true;
            lblReceipt.ForeColor = Defcolor.FontBlueColor;
            lblReceipt.BackColor = Defcolor.MainBackColor;
            lblReceipt.Location = new Point(340, 400);
            //Controls.Add(lblReceipt);

            #endregion
            Application.DoEvents();
            timer.Start();

            list(true);

            Focus();
        }

        public void list(bool is_reload)
        {
            if (!IsThreadRun)
            {
                ol = new newTrade.order_list();
                ol.is_reload = is_reload;
                ol.start_time = sStartTime;
                ol.end_time = sEndTime;
                ol.order_status = sOrderStatus;
                ol.page = sPageNum;
                ol.limit = "10";
                d = new addDelegate(result_list);
                start(ol);
            }
        }

        public void result_list()
        {
            string temp_error = "";
            try
            {
                if (iRThread == 0)
                {
                    if (ol.iRHttp == 0)
                    {
                        orderListLoad();

                        if(ol.is_reload)
                        {
                            pagenumC.setTotalPage(ol.resultS.data.total_num, ol.resultS.data.limit);
                        }
                        return;
                    }
                    else
                    {
                        temp_error = ol.sRHttp;
                    }
                }
                else if (iRThread == 1)
                {
                    temp_error = sRThread;
                }
            }
            catch (Exception e)
            {
                PublicMethods.WriteLog(e);
                temp_error = e.Message.ToString();
            }
            errorinformationForm errorF = new errorinformationForm("失败", temp_error);
            errorF.TopMost = true;
            errorF.StartPosition = FormStartPosition.CenterParent;
            errorF.ShowDialog();
            ((main)Parent.Parent).Refresh();
        }

        public void orderlistPageLoad()
        {
            list(false);
        }

        private void orderlistReload()
        {
            sPageNum = "1";
            list(true);
        }


        /// <summary>
        /// 构建订单列表记录
        /// </summary>
        private void orderListLoad()
        {
           
            orderlistPanel.Controls.Clear();
            lblSummary.Hide();
            try
            {
                //统计展示数据
                //lblOrderTotalMoney.Text = orderlistsummaryS.data.trade_total;
                //lblOrderNum.Text = orderlistsummaryS.data.trade_num;
                //lblRefundMoney.Text = orderlistsummaryS.data.discount_total;

                int _listnum = Convert.ToInt32(ol.resultS.data.list.Count);

                orderlistMouse.listmaxpos = _listnum;
                orderlistMouse.haslist = false;

                orderlistitemC = new orderListItemControl[_listnum];
                for (int _i = 0; _i < _listnum; _i++)
                {
                    orderlistMouse.haslist = true;

                    orderlistitemC[_i] = new orderListItemControl();
                    if (_i % 2 != 0)
                    {
                        orderlistitemC[_i].changeBackColor(Color.FromArgb(247, 247, 247));
                    }
                    else
                    {
                        orderlistitemC[_i].changeBackColor(Color.FromArgb(255, 255, 255));
                    }
                    orderlistitemC[_i].Location = new Point(0, _i * 30);
                    //orderlistdetailC[i].numLabel.Text = (i + 1).ToString();
                    int _temp = Convert.ToInt32(sPageNum);
                    orderlistitemC[_i].numLabel.Text = ((_i + 1) + (_temp - 1) * 10).ToString();
                    orderlistitemC[_i].ordertimeLabel.Text = ol.resultS.data.list[_i].create_time;
                    orderlistitemC[_i].ordernumLabel.Text = ol.resultS.data.list[_i].order_no;
                    orderlistitemC[_i].paytypeLabel.Text = ol.resultS.data.list[_i].pay_channel;
                    //orderlistitemC[_i].ordermoneyLabel.Text = ol.resultS.data.list[_i].total_money;
                    //orderlistitemC[_i].orderreceiptLabel.Text = ol.resultS.data.list[_i].receipt_money;
                    orderlistitemC[_i].orderstatusLabel.Text = ol.resultS.data.list[_i].order_status;
                    orderlistitemC[_i].personLabel.Text = ol.resultS.data.list[_i].employee_name;
                    orderlistitemC[_i].detailLabel.Name = ol.resultS.data.list[_i].order_no.ToString();
                    orderlistitemC[_i].detailLabel.MouseUp += new MouseEventHandler(orderdetail_MouseUp);
                    //if (ol.resultS.data.list[_i].refund_status == "CLOSE")
                    //{
                    //    orderlistitemC[_i].refundLabel.Text = "";
                    //}
                    //else
                    {
                        orderlistitemC[_i].refundLabel.Name = ol.resultS.data.list[_i].order_no.ToString();
                        orderlistitemC[_i].refundLabel.MouseUp += new MouseEventHandler(orderrefund_MouseUp);
                    }
                    orderlistPanel.Controls.Add(orderlistitemC[_i]);
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

        private void search_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                orderlistReload();
            }
        }
        private void output_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsThreadRun)
            {
                excelForm _excelF = new excelForm(sStartTime, sEndTime, sOrderNum, sPayChannel, sOrderStatus, 1);
                _excelF.StartPosition = FormStartPosition.CenterParent;
                _excelF.TopMost = true;
                _excelF.ShowDialog();
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
                    orderlistReload();
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
                    orderlistReload();
                }
                else
                {
                    ((main)Parent.Parent).Refresh();
                }
            }
        }
        private void orderListControl_Paint(object sender, PaintEventArgs e)
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
            orderlistReload();
            paytypeselectC.Visible = false;
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
            orderlistReload();
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
                if (iTemp != -1)
                {
                    Refresh();
                    iTemp = -1;
                }
            }
        }
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            orderlistMouse.isindateselecter = false;
            orderlistReload();
        }
        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            orderlistMouse.isindateselecter = true;
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
                        Console.WriteLine("orderlsit:" + orderlistMouse.pos);
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
            orderlistMouse.Position.Clear();

            orderlistMouse.pos = 7;
            orderlistMouse.listpos = 0;
            orderlistMouse.listmaxpos = 0;
            orderlistMouse._offsetX = 20;
            orderlistMouse._offsetY = 40;

            orderlistMouse.haslist = false;
            orderlistMouse.isindateselecter = false;

            orderlistMouse.Position.Add(new Point(185, 24));//收款0
            orderlistMouse.Position.Add(new Point(255, 24));//明细1
            orderlistMouse.Position.Add(new Point(325, 24));//储值2

            orderlistMouse.Position.Add(new Point(650, 24));//重新登录3
            orderlistMouse.Position.Add(new Point(715, 24));//最小化4
            orderlistMouse.Position.Add(new Point(755, 24));//关闭5

            orderlistMouse.Position.Add(new Point(60, 60));//财务流水6
            orderlistMouse.Position.Add(new Point(150, 60));//交易订单7
            orderlistMouse.Position.Add(new Point(260, 60));//储值明细8

            orderlistMouse.Position.Add(new Point(117, 100));//起始日期9
            orderlistMouse.Position.Add(new Point(247, 100));//结束日期10
            orderlistMouse.Position.Add(new Point(430, 96));//搜索框11
            orderlistMouse.Position.Add(new Point(535, 100));//搜索12

            #region ListItem
            orderlistMouse.Position.Add(new Point(735, 170));//列表第一条详情 13
            orderlistMouse.Position.Add(new Point(700, 170));//列表第一条退款 14
            orderlistMouse.Position.Add(new Point(735, 200));//列表第二条详情 15
            orderlistMouse.Position.Add(new Point(700, 200));//列表第二条退款 16
            orderlistMouse.Position.Add(new Point(735, 230));//列表第三条详情 17
            orderlistMouse.Position.Add(new Point(700, 230));//列表第三条退款 18  
            orderlistMouse.Position.Add(new Point(735, 260));//列表第四条详情 19
            orderlistMouse.Position.Add(new Point(700, 260));//列表第四条退款 20 
            orderlistMouse.Position.Add(new Point(735, 290));//列表第五条详情 21
            orderlistMouse.Position.Add(new Point(700, 290));//列表第五条退款 22 
            orderlistMouse.Position.Add(new Point(735, 320));//列表第六条详情 23
            orderlistMouse.Position.Add(new Point(700, 320));//列表第六条退款 24
            orderlistMouse.Position.Add(new Point(735, 350));//列表第七条详情 25
            orderlistMouse.Position.Add(new Point(700, 350));//列表第七条退款 26
            orderlistMouse.Position.Add(new Point(735, 380));//列表第八条详情 27
            orderlistMouse.Position.Add(new Point(700, 380));//列表第八条退款 28
            orderlistMouse.Position.Add(new Point(735, 410));//列表第九条详情 29
            orderlistMouse.Position.Add(new Point(700, 410));//列表第九条退款 30 
            orderlistMouse.Position.Add(new Point(735, 440));//列表第十条详情 31
            orderlistMouse.Position.Add(new Point(700, 440));//列表第十条退款 32
            #endregion

            orderlistMouse.Position.Add(new Point(275, 495));//更多汇总//33
            orderlistMouse.Position.Add(new Point(517, 495));//上一页34
            orderlistMouse.Position.Add(new Point(602, 495));//下一页35
            orderlistMouse.Position.Add(new Point(655, 495));//页码输入36
            orderlistMouse.Position.Add(new Point(720, 495));//跳页37

            orderlistMouse.Position.Add(new Point(395, 24));//会员38
            orderlistMouse.Position.Add(new Point(610, 100));//导出39
            orderlistMouse.Position.Add(new Point(465, 24));//验券40
            orderlistMouse.Position.Add(new Point(350, 60));//优惠券核销明细41
            orderlistMouse.Position.Add(new Point(460, 60));//口碑券核销明细42
        }
        public bool detailMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (orderlistMouse.isindateselecter)
            {
                return false;
            }
            else if (keyData == Keys.Down)
            {
                #region down
                switch (orderlistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 38:
                    case 40:
                    case 3:
                    case 4:
                    case 5: orderlistMouse.pos = 6; break;
                    case 6:
                    case 7:
                    case 8:
                    case 41: 
                    case 42: orderlistMouse.pos = 9; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39:
                        if (orderlistMouse.haslist)
                        {
                            orderlistMouse.pos = 13;
                        }
                        else
                        {
                            orderlistMouse.pos = 0;
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
                            if (orderlistMouse.pos < 11 + 2 * orderlistMouse.listmaxpos)
                            {
                                orderlistMouse.pos += 2;
                            }
                            else
                            {
                                orderlistMouse.pos = 33;
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
                        orderlistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: orderlistMouse.pos = 0; break;
                    default: orderlistMouse.pos = 0; break;
                }
                orderlistMouse._offsetX = Parent.Parent.Location.X + 20;
                orderlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(orderlistMouse.Position[orderlistMouse.pos].X + orderlistMouse._offsetX, orderlistMouse.Position[orderlistMouse.pos].Y + orderlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                Console.WriteLine(orderlistMouse.pos);
                switch (orderlistMouse.pos)
                {
                    case 0: orderlistMouse.pos = 5; break;
                    case 1: orderlistMouse.pos = 0; break;
                    case 2: orderlistMouse.pos = 1; break;
                    case 38: orderlistMouse.pos = 2; break;
                    case 40: orderlistMouse.pos = 38; break;

                    case 3: orderlistMouse.pos = 40; break;
                    case 4: orderlistMouse.pos = 3; break;
                    case 5: orderlistMouse.pos = 4; break;

                    case 6: orderlistMouse.pos = 42; break;
                    case 7: orderlistMouse.pos = 6; break;
                    case 8: orderlistMouse.pos = 7; break;
                    case 41: orderlistMouse.pos = 8; break;
                    case 42: orderlistMouse.pos = 41; break;

                    case 9: orderlistMouse.pos = 39; break;
                    case 10: orderlistMouse.pos = 9; break;
                    case 11: orderlistMouse.pos = 10; break;
                    case 12: orderlistMouse.pos = 11; break;
                    case 39: orderlistMouse.pos = 12; break;
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
                        //if (orderlistS.data.orderList[(orderlistMouse.pos - 13) / 2].refund_status == "OPEN")
                        {
                            orderlistMouse.pos += 1;
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
                        orderlistMouse.pos -= 1; break;
                    case 33: orderlistMouse.pos = 37; break;
                    case 34: orderlistMouse.pos = 33; break;
                    case 35: orderlistMouse.pos = 34; break;
                    case 36: orderlistMouse.pos = 35; break;
                    case 37: orderlistMouse.pos = 36; break;
                    
                    default: orderlistMouse.pos = 0; break;
                }

                orderlistMouse._offsetX = Parent.Parent.Location.X + 20;
                orderlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(orderlistMouse.Position[orderlistMouse.pos].X + orderlistMouse._offsetX, orderlistMouse.Position[orderlistMouse.pos].Y + orderlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (orderlistMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 38:
                    case 40: orderlistMouse.pos = 33; break;
                    case 6:
                    case 7:
                    case 8: 
                    case 41: 
                    case 42: orderlistMouse.pos = 0; break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 39: orderlistMouse.pos = 6; break;
                    case 13: orderlistMouse.pos = 9; break;
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31: orderlistMouse.pos -= 2; break;
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                    case 24:
                    case 26:
                    case 28:
                    case 30:
                    case 32: orderlistMouse.pos -= 1; break;
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37: orderlistMouse.pos = orderlistMouse.listmaxpos + (orderlistMouse.listmaxpos + 12) - 1; break;
                    default: orderlistMouse.pos = 0; break;
                }
                orderlistMouse._offsetX = Parent.Parent.Location.X + 20;
                orderlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(orderlistMouse.Position[orderlistMouse.pos].X + orderlistMouse._offsetX, orderlistMouse.Position[orderlistMouse.pos].Y + orderlistMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (orderlistMouse.pos)
                {
                    case 0: orderlistMouse.pos = 1; break;
                    case 1: orderlistMouse.pos = 2; break;
                    case 2: orderlistMouse.pos = 38; break;
                    case 38: orderlistMouse.pos = 40; break;
                    case 40: orderlistMouse.pos = 3; break;
                    case 3: orderlistMouse.pos = 4; break;
                    case 4: orderlistMouse.pos = 5; break;
                    case 5: orderlistMouse.pos = 0; break;

                    case 6: orderlistMouse.pos = 7; break;
                    case 7: orderlistMouse.pos = 8; break;
                    case 8: orderlistMouse.pos = 41; break;
                    case 41: orderlistMouse.pos = 42; break;
                    case 42: orderlistMouse.pos = 6; break;
                    
                    case 9: orderlistMouse.pos = 10; break;
                    case 10: orderlistMouse.pos = 11; break;
                    case 11: orderlistMouse.pos = 12; break;
                    case 12: orderlistMouse.pos = 39; break;
                    case 39: orderlistMouse.pos = 9; break;
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
                        //if (orderlistS.data.orderList[(orderlistMouse.pos - 13) / 2].refund_status == "OPEN")
                        {
                            orderlistMouse.pos += 1;
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
                        orderlistMouse.pos -= 1; break;
                    case 33: orderlistMouse.pos = 34; break;
                    case 34: orderlistMouse.pos = 35; break;
                    case 35: orderlistMouse.pos = 36; break;
                    case 36: orderlistMouse.pos = 37; break;
                    case 37: orderlistMouse.pos = 33; break;
                    
                    default: orderlistMouse.pos = 0; break;
                }
                orderlistMouse._offsetX = Parent.Parent.Location.X + 20;
                orderlistMouse._offsetY = Parent.Parent.Location.Y + 40;
                NativeMethods.SetCursorPos(orderlistMouse.Position[orderlistMouse.pos].X + orderlistMouse._offsetX, orderlistMouse.Position[orderlistMouse.pos].Y + orderlistMouse._offsetY);
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

}
