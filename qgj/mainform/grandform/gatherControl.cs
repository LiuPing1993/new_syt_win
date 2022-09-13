using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;

namespace qgj
{
    public partial class gatherControl : UserControl
    {
        public monyinputControl moneyinputC = new monyinputControl();
        notdiscountControl notdiscountC = new notdiscountControl();

        numPadControl numC = new numPadControl();
        public moneyshowControl moneyshowC = new moneyshowControl();
        public paywayControl[] paywayC = new paywayControl[5];
        public mainform.intraform.barPayControl barPayC;
        public bool is_barpay = false;
        public bool is_comfirm = false;

        public dicountselectControl discountselectC = new dicountselectControl();

        public string sAllMoney = "";
        string sUndiscount = "";

        Panel panelCoupon = new Panel();
        Panel panelMemberInfo = new Panel();
        Panel panelOverCouponScroll = new Panel();
        mainCouponInfoControl[] mainCouponInfoC;
        public mainMemberInfoControl mainMemberInfoC = new mainMemberInfoControl("", "", "");
        public mainStoreInfoControl mainStoreInfoC = new mainStoreInfoControl("");
        List<mainCouponInfoControl> listCouponInfo = new List<mainCouponInfoControl>();
        public memberConfirmControl memberConfirmC = new memberConfirmControl();

        memberSuccess memberS;
        errorClass errorC;
        int iHttpResult = 0;
        string sHttpResult = "";

        bool isThreadRun = false;
        Thread thread;
        public delegate void addDelegate();
        public addDelegate d;
        public addDelegate d_dis;

        private DateTime last_dt = DateTime.Now;
        private Keys last_key = Keys.None;

        private DateTime last_dis_dt = DateTime.Now;
        private Keys last_dis_key = Keys.None;

        public gatherControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            moneyinputC.Location = new Point(360, 40);
            Controls.Add(moneyinputC);

            notdiscountC.Location = new Point(360, 100);
            Controls.Add(notdiscountC);

            numC.Location = new Point(360, 160);
            Controls.Add(numC);

            moneyshowC.Location = new Point(1, 451);
            Controls.Add(moneyshowC);

            discountselectC.Location = new Point(10, 10);
            Controls.Add(discountselectC);

            //添加支付方式
            for (int i = 0; i < 5; i++)
            {
                paywayC[i] = new paywayControl((PayWay)Enum.Parse(typeof(PayWay), i.ToString()));
                paywayC[i].Location = new Point(360 + i * 80, 420);
                Controls.Add(paywayC[i]);
            }

            barPayC = new  mainform.intraform.barPayControl(this);

            barPayC.Location = new Point(360, 420);

            Controls.Add(barPayC);

            reloadPayWay();

            panelMemberInfo.SetBounds(1, 50, 339, 90);
            panelMemberInfo.BackColor = Defcolor.MainBackColor;
            Controls.Add(panelMemberInfo);

            panelCoupon.SetBounds(1, 147, 359, 303);
            panelCoupon.BackColor = Defcolor.MainBackColor;
            panelCoupon.AutoScroll = true;
            panelCoupon.MouseEnter += new EventHandler(coupon_MouseEnter);
            Controls.Add(panelCoupon);

            panelOverCouponScroll.SetBounds(340, 0, 20, 450);
            panelOverCouponScroll.BackColor = Defcolor.MainBackColor;
            panelOverCouponScroll.Paint += new PaintEventHandler(overCoupon_Paint);
            Controls.Add(panelOverCouponScroll);
            panelOverCouponScroll.BringToFront();

            memberConfirmC.Location = new Point(626, 346);
            Controls.Add(memberConfirmC);
            memberConfirmC.BringToFront();
            memberConfirmC.Visible = false;
        }

        private void garterControl_Load(object sender, EventArgs e)
        {
            this.BackColor = Defcolor.MainBackColor;
            start();
        }

        public void start()
        {
            if (!isThreadRun)
            {
                //得到界面的金额
                thread = new Thread(getMoney);
                thread.IsBackground = true;

                d = new addDelegate(insertKey2Money);

                d_dis = new addDelegate(insertKey2discountMoney);

                thread.Start();

                isThreadRun = true;
            }
        }

        #region 循环的读取界面的金额
        private void getMoney()
        {
            try
            {
                while (true)
                {
                    if (last_key != Keys.None)
                    {
                        TimeSpan ts = DateTime.Now.Subtract(last_dt);
                        if (ts.Milliseconds > 50)
                        {
                            Invoke(d);
                        }
                    }

                    if(last_dis_key != Keys.None)
                    {
                        TimeSpan ts = DateTime.Now.Subtract(last_dis_dt);
                        if (ts.Milliseconds > 50)
                        {
                            Invoke(d_dis);
                        }
                    }

                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString() + e.Message.ToString());
            }
            isThreadRun = false;
        }
        #endregion

        //收款金额键盘输入 阻止扫码枪
        private void insertKey2Money()
        {
            //如果得到手输入的金额
            if (fnSetMoneyFormKeyboard(last_key))
            {
                moneyshowC.fnSetShowGatherMoney(sAllMoney);//设置应收金额
                fnClearMemberCouponSelected();//清空会员选择
                if (!fnInsertDiscountCheck(sAllMoney, sUndiscount))//检查不打折金额
                {
                    sUndiscount = "";
                    notdiscountC.inputdiscountNum("0.00");
                    UserClass.orderInfoC.setNotDiscount(sUndiscount);
                }
            }
            last_key = Keys.None;
        }

        private void insertKey2discountMoney()
        {
            if (fnSetDiscountFormKeyboard(last_dis_key))
            {
                UserClass.orderInfoC.setNotDiscount(sUndiscount);
                fnClearMemberCouponSelected();
            }
            last_dis_key = Keys.None;
        }

        public bool fnMemberCouponInfo(string _sreachnum, ref string _name, ref string _num)
        {
            if (_sreachnum == "")
            {
                return false;
            }
            //请求会员/卡券等数据
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.getmember);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);
                _urlC.addParameter("code", _sreachnum);
                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                HttpClass _httpC = new HttpClass();
                string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + _sRequestMsg);
                if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                {
                    memberS = (memberSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(memberSuccess));
                    iHttpResult = 0; ;
                    sHttpResult = "请求成功";
                }
                else if (_sRequestMsg == "")
                {
                    errorinformationForm _errorF = new errorinformationForm("失败", "未能查询到用户信息");
                    _errorF.TopMost = true;
                    _errorF.StartPosition = FormStartPosition.CenterParent;
                    _errorF.ShowDialog();
                    ((main)Parent).Refresh();
                    return false;
                }
                else
                {
                    errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                    iHttpResult = 1;
                    sHttpResult = errorC.errMsg;
                }
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("未能查询到", e.Message.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                ((main)Parent).Refresh();
                return false;
            }

            if (iHttpResult == 1)
            {
                errorinformationForm _errorF = new errorinformationForm("未能查询到", sHttpResult);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                ((main)Parent).Refresh();
                return false;
            }
            //查询成功，构建页面
            try
            {
                //储值信息
                mainStoreInfoC = new mainStoreInfoControl(memberS.data.member_info.balance);
                mainStoreInfoC.Location = new Point(175, 10);
                panelMemberInfo.Controls.Add(mainStoreInfoC);

                //ref数据
                _name = memberS.data.member_info.user_name;
                _num = memberS.data.member_info.account;

                mainMemberInfoC = new mainMemberInfoControl(
                        memberS.data.member_info.member_level,
                        memberS.data.member_info.member_discount,
                        memberS.data.member_info.member_code
                        );
                mainMemberInfoC.Location = new Point(10, 10);
                panelMemberInfo.Controls.Add(mainMemberInfoC);

                if (memberS.data.member_info.member_code != "")
                {
                    UserClass.orderInfoC.member = memberS.data.member_info.member_code;
                }
                else if (memberS.data.member_info.account != "")
                {
                    UserClass.orderInfoC.member = memberS.data.member_info.account;
                }
                else if (memberS.data.member_info.user_name != "")
                {
                    _name = memberS.data.member_info.user_name;
                    _num = "";
                }
                else
                {
                    _name = "非会员";
                    _num = "";
                }
                if (memberS.data.coupon_list == null)
                {
                    return true;
                }
                //构建卡券页面
                int _iCouponNum = memberS.data.coupon_list.Count;
                gatherMouse.couponnum = _iCouponNum;
                int _iTemp = 0;
                mainCouponInfoC = new mainCouponInfoControl[_iCouponNum];
                listCouponInfo.Clear();
                foreach (Coupon_listItem _couponItem in memberS.data.coupon_list)
                {
                    if (_couponItem.type == "DISCOUNT")
                    {
                        mainCouponInfoC[_iTemp] = new mainCouponInfoControl(
                            _couponItem.discount,
                            _couponItem.least_cost_title,
                            _couponItem.title,
                            _couponItem.sub_title,
                            _couponItem.start_time + " - " + _couponItem.end_time,
                            _couponItem.color,
                            couponType.discountcoupon
                            );
                        mainCouponInfoC[_iTemp].Name = _couponItem.id;
                    }
                    else if (_couponItem.type == "CASH")
                    {
                        mainCouponInfoC[_iTemp] = new mainCouponInfoControl(
                            _couponItem.reduce_cost,
                            _couponItem.least_cost_title,
                            _couponItem.title,
                            _couponItem.sub_title,
                            _couponItem.start_time + " - " + _couponItem.end_time,
                            _couponItem.color,
                            couponType.cashcoupon
                            );
                        mainCouponInfoC[_iTemp].Name = _couponItem.id;
                    }
                    else if (_couponItem.type == "GIFT")
                    {
                        //string _leastCost = Math.Round((Convert.ToDouble(_couponItem.least_cost) / 100), 0).ToString();
                        mainCouponInfoC[_iTemp] = new mainCouponInfoControl(
                            "兑换券",
                            _couponItem.least_cost_title,
                            _couponItem.title,
                            _couponItem.gift,
                            _couponItem.start_time + " - " + _couponItem.end_time,
                            _couponItem.color,
                            couponType.giftcoupon
                            );
                        mainCouponInfoC[_iTemp].Name = _couponItem.id;
                    }
                    mainCouponInfoC[_iTemp].Location = new Point(10, 5 + _iTemp * 85);
                    panelCoupon.Controls.Add(mainCouponInfoC[_iTemp]);
                    listCouponInfo.Add(mainCouponInfoC[_iTemp]);
                    _iTemp++;
                }
                return true;
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("未查询到", e.Message.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                ((main)Parent).Refresh();
                return false;
            }
        }
        /// <summary>
        /// 清除所有会员及卡券相关内容
        /// </summary>
        public void fnClearMemberCouponInfo()
        {
            panelMemberInfo.Controls.Clear();
            panelCoupon.Controls.Clear();
            moneyshowC.fnSetShowGatherMoney(sAllMoney);
            moneyshowC.fnSetShowDiscountMoney("");
            UserClass.orderInfoC.member = "";
            UserClass.orderInfoC.UseMemberDiscount = false;
            UserClass.orderInfoC.coupon = "";
            ((main)Parent).fnSetGatherMouse();
            Refresh();
        }
        /// <summary>
        /// 清除所有会员及卡券的选择状态
        /// </summary>
        public void fnClearMemberCouponSelected()
        {
            if (UserClass.orderInfoC.member != "" || UserClass.orderInfoC.coupon != "")
            {
                mainMemberInfoC.setSelectStatus(false);
                mainStoreInfoC.setSelectStatus(false);
                foreach (mainCouponInfoControl _value in listCouponInfo)
                {
                    _value.fnSetSelectStatus(false);
                }
                moneyshowC.fnSetShowGatherMoney(sAllMoney);
                moneyshowC.fnSetShowDiscountMoney("");
                UserClass.orderInfoC.UseMemberDiscount = false;
                UserClass.orderInfoC.coupon = "";
                Refresh();
            }
        }
        private void coupon_MouseEnter(object sender, EventArgs e)
        {
            if (panelCoupon.Controls.Count > 0)
            {
                panelCoupon.Focus();
            }
        }
        private void garterControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid
            );
            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(340, 0), new Point(340, 500));
            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(0, 450), new Point(340, 450));
            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(760, 420), new Point(760, 489));
        }
        private void overCoupon_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(0, 0), new Point(0, 450));
        }

        /// <summary>
        /// 识别设置金额
        /// </summary>
        /// <param name="_insert"></param>
        public void fnSetMoneyFormRecognition(string _insert)
        {
            fnReload(false);
            if (_insert == "0")
            {
                moneyinputC.fnInputNum("0.00");
            }
            else if (_insert == "")
            {
                return;
            }
            else
            {
                if (PublicMethods.moneyCheck(_insert))
                {
                    sAllMoney = _insert;
                    moneyinputC.fnInputNum(_insert);
                    moneyshowC.fnSetShowGatherMoney(_insert);
                }
            }
        }

        /// <summary>
        /// 输入焦点改变
        /// </summary>
        /// <param name="_type"></param>
        public void fnFocusIsChange(string _type)
        {
            if (_type == "moneyinput")
            {
                notdiscountC.notdiscountControl_Leave(null, null);
                memberConfirmC.Visible = false;
            }
            else if (_type == "notdiscount")
            {
                moneyinputC.monyinputControl_Leave(null, null);
                memberConfirmC.Visible = false;
            }
            else
            {
                notdiscountC.notdiscountControl_Leave(null, null);
                moneyinputC.monyinputControl_Leave(null, null);
                memberConfirmC.Visible = true;
                memberConfirmC.BringToFront();
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void fnReload(bool _ismin = true)
        {
            sAllMoney = "";
            sUndiscount = "";
            moneyinputC.fnInputNum("0.00");
            notdiscountC.inputdiscountNum("0.00");
            moneyshowC.fnSetShowGatherMoney("0.00");
            UserClass.orderInfoC.reload();
            discountselectC.fnClearDiscountSelect();
            fnSetFocus();
            if (_ismin)
            {
                loadconfigClass _lcc = new loadconfigClass("successmini");
                if (_lcc.readfromConfig() == "true")
                {
                    try
                    {
                        ((main)Parent).WindowState = FormWindowState.Minimized;
                    }
                    catch { }
                }

                //自定义鼠标操作
                try
                {
                    _lcc = new loadconfigClass("autoevent");
                    string auto_event = _lcc.readfromConfig();
                    int step_num = 0;
                    if (auto_event == "" || auto_event == "0")
                    {
                        step_num = 0;
                    }
                    else
                    {
                        step_num = Convert.ToInt32(auto_event);
                        if (step_num > 5)
                        {
                            step_num = 5;
                        }
                    }

                    if (step_num > 0)
                    {
                        loadconfigClass area_c;
                        loadconfigClass time_c;
                        int time = 0;
                        int _read_x;
                        int _read_y;
                        int _read_w;
                        int _read_h;
                        int _x;
                        int _y;
                        for (int i = 1; i <= step_num; i++)
                        {
                            time_c = new loadconfigClass("step_time_" + i.ToString());
                            string temp = time_c.readfromConfig();
                            if (temp != "" && PublicMethods.IsNumeric(temp))
                            {
                                time = Convert.ToInt32(temp);
                                pictureClass.Delay((uint)time);
                            }
                            area_c = new loadconfigClass("step_area_" + i.ToString());
                            temp = area_c.readfromConfig();
                            if (temp != "")
                            {
                                string[] _settingtemp = PublicMethods.SplitByChar(temp, ',');
                                _read_x = Convert.ToInt32(_settingtemp[0]);
                                _read_y = Convert.ToInt32(_settingtemp[1]);
                                _read_w = Convert.ToInt32(_settingtemp[2]);
                                _read_h = Convert.ToInt32(_settingtemp[3]);
                                _x = _read_x + _read_w / 2;
                                _y = _read_y + _read_h / 2;
                                NativeMethods.SetCursorPos(_x, _y);
                                NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, _x, _y, 0, 0);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString() + e.StackTrace.ToString());
                }
            }
            try
            {
                //PublicMethods.FlushMemory();
            }
            catch { }
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public void fnSetFocus(bool _flag = false)
        {
            if (_flag)
            {
                moneyinputC.monyinputControl_Click(this, null);
            }
            else
            {
                moneyinputC.monyinputControl_Click(null, null);
            }
            moneyinputC.Focus();
        }

        /// <summary>
        /// 输入金额
        /// </summary>
        /// <param name="_keyData">键值</param>
        /// <returns>是否输入</returns>
        private bool fnSetMoneyFormKeyboard(System.Windows.Forms.Keys _keyData)
        {
            Console.WriteLine("输入金额按下..."+_keyData.ToString());
            string _sKeyTemp = PublicMethods.KeyCodeToChar(_keyData).ToString();
            if (PublicMethods.moneyCheck(sAllMoney + _sKeyTemp))
            {
                if (sAllMoney == "" && (_sKeyTemp == "." || _sKeyTemp == "0"))
                {
                    _sKeyTemp = "0.";
                }
                sAllMoney += _sKeyTemp;
                moneyinputC.fnInputNum(sAllMoney);
                return true;
            }
            else if (_keyData == Keys.Back)
            {
                if (sAllMoney == "")
                {
                    return false;
                }
                else if (sAllMoney == "0.")
                {
                    sAllMoney = "";
                    moneyinputC.fnInputNum("0.00");
                    return true;
                }
                else
                {
                    if (sAllMoney.Length == 1)
                    {
                        sAllMoney = "";
                        moneyinputC.fnInputNum("0.00");
                        return true;
                    }
                    sAllMoney = sAllMoney.Remove(sAllMoney.Length - 1, 1);
                    moneyinputC.fnInputNum(sAllMoney);
                    return true;
                }
            }
            else if (sAllMoney == "0.00" && PublicMethods.moneyCheck(_sKeyTemp))
            {
                sAllMoney = _sKeyTemp;
                moneyinputC.fnInputNum(sAllMoney);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 输入不可打折金额
        /// </summary>
        /// <param name="_keyData">键值</param>
        /// <returns>是否输入</returns>
        private bool fnSetDiscountFormKeyboard(System.Windows.Forms.Keys _keyData)
        {
            Console.WriteLine("当前输入的keyValue的值为："+_keyData.ToString());
            string _sKeyTemp = PublicMethods.KeyCodeToChar(_keyData).ToString();
            if (PublicMethods.moneyCheck(sUndiscount + _sKeyTemp))
            {
                if (sUndiscount == "" && (_sKeyTemp == "." || _sKeyTemp == "0"))
                {
                    _sKeyTemp = "0.";
                }
                if (fnInsertDiscountCheck(sAllMoney, sUndiscount + _sKeyTemp))
                {
                    sUndiscount += _sKeyTemp;
                    notdiscountC.inputdiscountNum(sUndiscount);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (_keyData == Keys.Back)
            {
                if (sUndiscount == "")
                {
                    return false;
                }
                else if (sUndiscount == "0.")
                {
                    sUndiscount = "";
                    notdiscountC.inputdiscountNum("0.00");
                    return true;
                }
                else
                {
                    if (sUndiscount.Length == 1)
                    {
                        sUndiscount = "";
                        notdiscountC.inputdiscountNum("0.00");
                        return true;
                    }
                    sUndiscount = sUndiscount.Remove(sUndiscount.Length - 1, 1);
                    notdiscountC.inputdiscountNum(sUndiscount);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断不打折金额
        /// </summary>
        /// <param name="_sAllMoney">总金额</param>
        /// <param name="_sUndiscount">不打折金额</param>
        /// <returns>是否合法</returns>
        public bool fnInsertDiscountCheck(string _sAllMoney, string _sUndiscount)
        {
            if (_sAllMoney == "")
            {
                return false;
            }
            if (_sUndiscount == "")
            {
                return true;
            }
            try
            {
                double _dAllMoney = Convert.ToDouble(_sAllMoney);
                double _dUndiscount = Convert.ToDouble(_sUndiscount);
                if (_dAllMoney < _dUndiscount)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

        //识别后快速支付
        public void orcFastPay()
        {
            try
            {
                loadconfigClass lcc = new loadconfigClass("orcfastpayway");
                string fast_pay_way = lcc.readfromConfig();
                if (is_barpay) 
                {
                    return;
                }

                if (fast_pay_way == "bar")
                {
                    paywayC[0].fnFastPay();
                }
                else if (fast_pay_way == "qr")
                {
                    paywayC[1].fnFastPay();
                }
                else if (fast_pay_way == "union")
                {
                    paywayC[2].fnFastPay();
                }
                else if (fast_pay_way == "cash")
                {
                    paywayC[3].fnFastPay();
                }
                else if (fast_pay_way == "balance")
                {
                    paywayC[4].fnFastPay();
                }
            }
            catch { }
        }

        public void reloadPayWay(bool is_set = false, bool not_only_onetime = false)
        {
            loadconfigClass sacnComfirmConfig = new loadconfigClass("scanComfirm");
            loadconfigClass onlyBarConfig = new loadconfigClass("onlyBar");
            
            if(onlyBarConfig.readfromConfig() == "true")
            {
                is_barpay = true;
                barPayC.Show();
                barPayC.BringToFront();
            }
            //else if(not_only_onetime)
            //{
            //    is_barpay = true;
            //    barPayC.Show();
            //    barPayC.BringToFront();
            //}
            else
            {
                is_barpay = false;
                barPayC.Hide();
            }

            if (sacnComfirmConfig.readfromConfig() == "true")
            {
                is_comfirm = true;
            }
            else
            {
                is_comfirm = false;
            }

            if (is_set && !is_barpay)
            {
                barPayC.code = "";
                barPayC.Refresh();
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                ((main)Parent).fnShowDetailC();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                ((main)Parent).toptitleC.close_MouseUp(null, null);
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                if (moneyinputC.fnGetStatus())//当焦点在金额输入框内时判断回车键距上一个按键的时间间隔
                {
                    TimeSpan ts = DateTime.Now.Subtract(last_dt);
                    if (ts.Milliseconds < 50)
                    {
                        last_key = Keys.None;
                        last_dt = DateTime.Now;
                    }
                }

                if (discountselectC.tbxInsert.Focused)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }

                //取消 判断当前是否仅条码支付的情况
                if (!is_barpay)
                {
                    if (UserClass.fastPayWay == "" || UserClass.fastPayWay == "bar")
                    {
                        paywayC[0].fnFastPay();
                        return true;
                    }
                    if (UserClass.fastPayWay == "qr")
                    {
                        paywayC[1].fnFastPay();
                        return true;
                    }
                }
                else
                {
                    //当仅条码支付时 , 回车键可以唤起支付
                    //barPayC.barPayControl_MouseUp(null, null);
                }
            }
            else if (keyData == Keys.Space)
            {
                if (UserClass.fastPayWay == "")
                {
                    paywayC[1].fnFastPay();
                    return true;
                }
            }
            else if (moneyinputC.fnGetStatus())
            {
                if (last_key != Keys.None)
                {
                    last_key = Keys.None;
                }
                else
                {
                    last_dt = DateTime.Now;
                    last_key = keyData;
                }
            }
            else if (notdiscountC.getStatus())
            {
                if (last_dis_key != Keys.None)
                {
                    last_dis_key = Keys.None;
                }
                else
                {
                    last_dis_dt = DateTime.Now;
                    last_dis_key = keyData;
                }
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void setscroll(bool _isUp)
        {
            Point _p = new Point(panelCoupon.AutoScrollPosition.X, panelCoupon.AutoScrollPosition.Y);
            if (_isUp)
            {
                _p.Y = -(panelCoupon.AutoScrollPosition.Y + 85);
            }
            else
            {
                _p.Y = 85 - panelCoupon.AutoScrollPosition.Y;
            }
            panelCoupon.AutoScrollPosition = _p;
        }
    }
}
