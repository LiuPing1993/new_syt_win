using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class main : Form
    {
        public gatherControl gatherC;
        public detailTitleControl detailTitleC;
        public storeControl storeC;
        public memberControl memberC;
        public couponControl couponC;

        public toptitleControl toptitleC;
        public bottomtitleControl bottomtitleC;
        public SuspendedForm suspendedF;

        Point pointDown;

        ScanerHook listener = new ScanerHook();

        public bool new_customer_money = false;//是否获取到新的客显值(用于发送快捷键的情况下)
        //bool is_get_code = false;//从扫码枪获取到新的条码 

        public main()
        {
            //SetStyle(
            //    ControlStyles.UserPaint |
            //    ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.OptimizedDoubleBuffer |
            //    ControlStyles.ResizeRedraw |
            //    ControlStyles.DoubleBuffer, true);
            //UpdateStyles();
            //base.AutoScaleMode = AutoScaleMode.None;//如果系统设置150%缩放会产生问题

            #region 控件初始化
            this.Visible = false;
            //顶部控件
            toptitleC = new toptitleControl();
            toptitleC.AllowDrop = true;
            toptitleC.Location = new Point(0, 0);

            #region  确定窗体的位置可以拖动
            toptitleC.MouseDown += new MouseEventHandler(toptitleC_MouseDown);
            toptitleC.MouseUp += new MouseEventHandler(toptitleC_MouseUp);
            toptitleC.MouseMove += new MouseEventHandler(toptitleC_MouseMove);
            this.Controls.Add(toptitleC);
            #endregion

            //底部控件
            bottomtitleC = new bottomtitleControl();
            bottomtitleC.Location = new Point(0, 545);
            bottomtitleC.MouseUp += new MouseEventHandler(bottomtitleC_MouseUp);
            this.Controls.Add(bottomtitleC);

            
            gatherC = new gatherControl();
            gatherC.Location = new Point(0, 45);
            gatherC.Visible = true;
            this.Controls.Add(gatherC);

            //详细标题的控件
            detailTitleC = new detailTitleControl();
            detailTitleC.Location = new Point(0, 45);
            detailTitleC.Visible = false;
            this.Controls.Add(detailTitleC);

            //储值界面
            storeC = new storeControl();
            storeC.Location = new Point(0, 45);
            storeC.Visible = false;
            this.Controls.Add(storeC);

            //会员界面
            memberC = new memberControl();
            memberC.Location = new Point(0, 45);
            memberC.Visible = false;
            this.Controls.Add(memberC);

            //券码界面
            couponC = new couponControl();
            couponC.Location = new Point(0, 45);
            couponC.Visible = false;
            this.Controls.Add(couponC);
            #endregion

            InitializeComponent();

            //NativeMethods.SetClassLong(this.Handle, NativeMethods.GCL_STYLE, NativeMethods.GetClassLong(this.Handle, NativeMethods.GCL_STYLE) | NativeMethods.CS_DropSHADOW);
            try
            {
                loadconfigClass config = new loadconfigClass("font");
                string font = config.readfromConfig();
                if(font != "" && font == "songti")
                {
                    UserClass.fontName = "宋体";
                }

                Rectangle clientRectangle = ClientRectangle;
                Point clientPoint = PointToScreen(new Point(0, 0));
                clientRectangle.Offset(clientPoint.X - Left, clientPoint.Y - Top);
                //Region = new Region(clientRectangle);
                ArcRadius arcR = new ArcRadius(3, 3, 3, 3);
                Region = new Region(PublicMethods.CreateRoundPath(clientRectangle, arcR));
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
        }

        private void main_Load(object sender, EventArgs e)
        {

            //PublicMethods.FlushMemory();

            #region 派派小盒初始化
            string tempPaipai = "";
            if (UserClass.ppbC.initPaipai(ref tempPaipai))
            {
                UserClass.ppbC.setSence(1);
            }
            else
            {
                errorinformationForm errorF = new errorinformationForm("启动派派小盒失败", tempPaipai);
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterScreen;
                errorF.ShowDialog();
            }
            #endregion

            #region 部分配置初始化
            UserClass.mainHandle = this.Handle;
            UserClass.IsMain = true;
            //快捷键设置 - 调起
            loadconfigClass lcc = new loadconfigClass("hotkey");
            hotkeyClass hotkeyC = new hotkeyClass();
            if (lcc.readfromConfig() != "")
            {
                hotkeyC.fnSetHotKey(lcc.readfromConfig(), 100);
            }
            //快捷键设置 - 打印汇总
            lcc = new loadconfigClass("print_hotkey");
            hotkeyC.type = 2;
            if (lcc.readfromConfig() != "")
            {
                hotkeyC.fnSetHotKey(lcc.readfromConfig(), 101);
            }
            //是否使用键盘操作
            lcc = new loadconfigClass("usekeyboard");
            if (lcc.readfromConfig() == "true")
            {
                UserClass.isUseKeyBorad = true;
            }
            else
            {
                UserClass.isUseKeyBorad = false;
            }
            //读取快速支付设置
            lcc = new loadconfigClass("fastpayway");
            UserClass.fastPayWay = lcc.readfromConfig();
            fnSetGatherMouse();//初始化键盘操作鼠标位置

            //全局扫码枪钩子
            listener.ScanerEvent += Listener_ScanerEvent;
            lcc = new loadconfigClass("realtime");
            if (lcc.readfromConfig() == "true")
            {
                lcc = new loadconfigClass("allscan");
                if (lcc.readfromConfig() == "")
                {
                    lcc.writetoConfig("true");
                }
                listener.Start();
            }
            //是否开启悬浮窗
            //suspendedF = new SuspendedForm(this);
            //悬浮窗的开启
            suspendedF.Show();
            fnShowSuspendedForm();
            #endregion

            Application.DoEvents();

            Visible = true;
            TopMost = true;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                NativeMethods.AnimateWindow(this.Handle, 500, NativeMethods.AW_BLEND | NativeMethods.AW_ACTIVE);
            }

            gatherC.fnSetFocus();
        }
        private void Listener_ScanerEvent(ScanerHook.ScanerCodes codes)
        {
            try
            {
                //if (is_get_code) return;
                //is_get_code = true;
                loadconfigClass lcc = new loadconfigClass("allscan");
                if (lcc.readfromConfig() == "false")
                {
                    //关闭全局获取
                    return;
                }
                //Console.WriteLine("已获取键盘事件");

                if (codes.Result.Length <= 5)
                {
                    //PublicMethods.WriteLog(codes.Result);
                    return;
                }
                else
                {
                    //PublicMethods.WriteLog(codes.Result);
                }
                //得到悬浮窗的金额
                string temp_money = suspendedF.returnMoney2Pay();
                //当主窗体不可见的时候
                if (WindowState == FormWindowState.Minimized || !Visible)
                {
                    if (suspendedF.Visible && temp_money != "" && temp_money != "0.00")//悬浮窗有金额
                    {
                        //判断扫码后动作 1.是否需要发送快捷键
                        isBeforeScan();
                        new_customer_money = false;

                        if (gatherC.is_comfirm)//需要支付确认
                        {
                            gatherC.fnSetMoneyFormRecognition(temp_money);
                            WindowState = FormWindowState.Normal;
                            Show();

                            Application.DoEvents();

                            fnShowGatherC();
                            Activate();
                            TopMost = true;
                            gatherC.fnSetFocus(true);

                            //扫码进入,需要在当前一次支付显示仅条码界面
                            gatherC.is_barpay = true;
                            gatherC.barPayC.Show();
                            gatherC.barPayC.BringToFront();

                            gatherC.barPayC.code = codes.Result;
                            gatherC.barPayC.Refresh();
                        }
                        else
                        {
                            gatherC.fnSetMoneyFormRecognition(temp_money);
                            WindowState = FormWindowState.Normal;
                            Show();

                            Application.DoEvents();

                            fnShowGatherC();
                            //获取焦点重新的激活一次
                            Activate();
                            TopMost = true;
                            gatherC.fnSetFocus(true);
                            gatherC.paywayC[0].fnFastPay(codes.Result);
                        }
                    }
                }
                else
                {
                    //窗体不可见的情况下不做任何操作
                    //gatherC.paywayC[0].fnFastPay(codes.Result);
                }
            }
            catch { }
            //finally { is_get_code = false; }
        }

        private void isBeforeScan()
        {
            try
            {
                loadconfigClass lcc = new loadconfigClass("before_type");
                if (lcc.readfromConfig() == "key")
                {
                    loadconfigClass key_lcc = new loadconfigClass("before_key");
                    bool hotkey_ctrl = false;
                    string hotkey_value = "";
                    string hot_key_set = key_lcc.readfromConfig();
                    if (hot_key_set != "")
                    {
                        if (hot_key_set.IndexOf("+") != -1)
                        {
                            hotkey_ctrl = true;
                            hotkey_value = hot_key_set.Replace("Ctrl + ", "");
                        }
                        else
                        {
                            hotkey_ctrl = false;
                            hotkey_value = hot_key_set;
                        }
                        simulatePress(hotkey_ctrl, hotkey_value);
                    }
                }
                else if (lcc.readfromConfig() == "mouse")
                {
                    loadconfigClass key_lcc = new loadconfigClass("step_area_mouse");
                    string temp = key_lcc.readfromConfig();
                    if (temp != "")
                    {
                        string[] _settingtemp = PublicMethods.SplitByChar(temp, ',');
                        int _read_x = Convert.ToInt32(_settingtemp[0]);
                        int _read_y = Convert.ToInt32(_settingtemp[1]);
                        int _read_w = Convert.ToInt32(_settingtemp[2]);
                        int _read_h = Convert.ToInt32(_settingtemp[3]);
                        int _x = _read_x + _read_w / 2;
                        int _y = _read_y + _read_h / 2;
                        //设置当前的鼠标的位置
                        NativeMethods.SetCursorPos(_x, _y);
                        NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, _x, _y, 0, 0);
                    }
                }
                else
                {
                    return;
                }

                //延迟
                lcc = new loadconfigClass("before_time");
                string time = lcc.readfromConfig();
                int iTime = 2000;
                if (time != "" && PublicMethods.IsNumeric(time))
                {
                    iTime = Convert.ToInt32(time);
                    if (iTime < 1000) time = "1000";//最小延迟为1秒
                    if (iTime > 9000) time = "9000";//最大延迟为9秒
                }

                DateTime start = DateTime.Now;
                DateTime now = DateTime.Now;
                new_customer_money = false;
                while (true)
                {
                    if (new_customer_money)
                    {
                        pictureClass.Delay(Convert.ToUInt32(iTime));
                        break;
                    }
                    now = DateTime.Now;
                    TimeSpan t_span = now - start;
                    if (t_span.TotalMilliseconds > iTime + 1000)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString() + e.StackTrace.ToString());
            }
        }
        private void simulatePress(bool hotkey_ctrl, string hotkey_value)
        {
            try
            {
                if (hotkey_ctrl)
                {
                    Keys getType = (Keys)Enum.Parse(typeof(Keys), hotkey_value);
                    NativeMethods.keybd_event(17, 0, 0, 0);
                    NativeMethods.keybd_event((byte)getType, 0, 0, 0);
                    NativeMethods.keybd_event((byte)getType, 0, 2, 0);
                    NativeMethods.keybd_event(17, 0, 2, 0);
                }
                else
                {
                    Keys getType = (Keys)Enum.Parse(typeof(Keys), hotkey_value);
                    NativeMethods.keybd_event((byte)getType, 0, 0, 0);
                    NativeMethods.keybd_event((byte)getType, 0, 2, 0);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString() + e.StackTrace.ToString());
            }
        }
        public void fnShowSuspendedForm()
        {
            loadconfigClass lcc = new loadconfigClass("showsuspended");
            if (lcc.readfromConfig() == "true")
            {
                suspendedF.setFrom();
                suspendedF.Show();
                suspendedF.isShow = true;
            }
            else
            {
                suspendedF.Hide();
                suspendedF.isShow = false;
            }
            lcc = new loadconfigClass("realtime");
            if (lcc.readfromConfig() == "true")
            {
                listener.Start();
            }
            else
            {
                listener.Stop();
            }
        }

        /// <summary>
        /// 展示收款界面
        /// </summary>
        public void fnShowGatherC()
        {
            toptitleC.lblGather.ForeColor = Defcolor.FontTopSelect;
            toptitleC.lblDetail.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblStore.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblmember.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblCoupon.ForeColor = Defcolor.FontTopNotSselect;
            try
            {
                detailTitleC.Dispose();
                storeC.Dispose();
                memberC.Dispose();
                couponC.Dispose();

                gatherC.Visible = true;
                returnPosition();
                gatherC.fnSetFocus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        /// <summary>
        /// 展示明细页面
        /// </summary>
        public void fnShowDetailC()
        {
            toptitleC.lblGather.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblDetail.ForeColor = Defcolor.FontTopSelect;
            toptitleC.lblStore.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblmember.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblCoupon.ForeColor = Defcolor.FontTopNotSselect;
            try
            {
                gatherC.Visible = false;
                detailTitleC.Dispose();
                storeC.Dispose();
                memberC.Dispose();
                couponC.Dispose();

                detailTitleC = new detailTitleControl();
                detailTitleC.Location = new Point(0, 45);
                detailTitleC.Visible = true;
                Controls.Add(detailTitleC);
                detailTitleC.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }

        /// <summary>
        /// 展示储值页面
        /// </summary>
        public void fnShowStoreC()
        {
            toptitleC.lblGather.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblDetail.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblStore.ForeColor = Defcolor.FontTopSelect;
            toptitleC.lblmember.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblCoupon.ForeColor = Defcolor.FontTopNotSselect;
            try
            {
                gatherC.Visible = false;
                detailTitleC.Dispose();
                storeC.Dispose();
                memberC.Dispose();
                couponC.Dispose();
                    
                storeC = new storeControl();
                storeC.Location = new Point(0, 45);
                storeC.Visible = true;
                Controls.Add(storeC);
                storeC.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }

        /// <summary>
        /// 展示会员页面
        /// </summary>
        public void fnShowMemberC()
        {
            toptitleC.lblGather.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblDetail.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblStore.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblmember.ForeColor = Defcolor.FontTopSelect;
            toptitleC.lblCoupon.ForeColor = Defcolor.FontTopNotSselect;
            try
            {
                gatherC.Visible = false;
                detailTitleC.Dispose();
                storeC.Dispose();
                memberC.Dispose();
                couponC.Dispose();

                memberC = new memberControl();
                memberC.Location = new Point(0, 45);
                memberC.Visible = true;
                Controls.Add(memberC);
                memberC.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        /// <summary>
        /// 展示验券界面
        /// </summary>
        public void fnShowCouponC()
        {
            toptitleC.lblGather.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblDetail.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblStore.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblmember.ForeColor = Defcolor.FontTopNotSselect;
            toptitleC.lblCoupon.ForeColor = Defcolor.FontTopSelect;
            try
            {
                gatherC.Visible = false;
                detailTitleC.Dispose();
                storeC.Dispose();
                memberC.Dispose();
                couponC.Dispose();

                couponC = new couponControl();
                couponC.Location = new Point(0, 45);
                couponC.Visible = true;
                Controls.Add(couponC);
                couponC.Focus();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }

        protected override CreateParams CreateParams
        {
            //get
            //{
            //    CreateParams cp = base.CreateParams;
            //    cp.ExStyle |= 0x02000000;
            //    return cp;
            //}
            //get
            //{
            //    const int WS_MINIMIZEBOX = 0x00020000;
            //    CreateParams cp = base.CreateParams;
            //    cp.Style = cp.Style | WS_MINIMIZEBOX;
            //    return cp;
            //}
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;
                return cp;
            }
        }
        
        //鼠标的移动事件
        private void toptitleC_MouseDown(object sender, MouseEventArgs e)
        {
            pointDown = new Point(e.Location.X, e.Location.Y);

        }
        private void toptitleC_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - pointDown.X,
                    this.Location.Y + e.Y - pointDown.Y);
            }
        }
        private void toptitleC_MouseUp(object sender, MouseEventArgs e)
        {
            Refresh();
            gatherC.fnSetFocus();
        }
        private void bottomtitleC_MouseUp(object sender, MouseEventArgs e)
        {
            gatherC.fnSetFocus();
        }
        protected override void WndProc(ref Message message)
        {
            const int CanShu = 0x0312;
            if (UserClass.IsMain)
            {
                switch (message.Msg)
                {
                    case CanShu:
                        switch (Int32.Parse(message.WParam.ToString()))
                        {                                
                            case 100:
                                if (WindowState == FormWindowState.Minimized || !Visible)
                                {
                                    if(suspendedF.isShow && suspendedF.isSetRealTime)
                                    {
                                        gatherC.fnSetMoneyFormRecognition(suspendedF.returnMoney2Pay());
                                    }
                                    else
                                    {
                                        recognitionClass rc = new recognitionClass();
                                        gatherC.fnSetMoneyFormRecognition(rc.recognitionFormSelectArea());
                                    }
                                    WindowState = FormWindowState.Normal;
                                    
                                    Application.DoEvents();

                                    Show();
                                    
                                    fnShowGatherC();
                                    Activate();
                                    TopMost = true;
                                    gatherC.fnSetFocus(true);
                                    //gatherC.Refresh();
                                    //识别后自动选取支付方式
                                    gatherC.orcFastPay();
                                }
                                else
                                {
                                    gatherC.reloadPayWay(true);
                                    WindowState = FormWindowState.Minimized;
                                }
                                break;
                            case 101:
                                //快速汇总打印
                                Application.DoEvents();
                                summaryPrint sprint = new summaryPrint();
                                sprint.print();
                                break;
                            case 102:
                                //预留位置102
                                break;
                        }
                        break;
                    case 0x0014:
                        return;
                }
            }
            base.WndProc(ref message);
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener.Stop();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                return;
            }
            else
            {
                notifyIcon.Dispose();
            }
        }
        private void timerMemoryClean_Tick(object sender, EventArgs e)
        {
            return;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (WindowState == FormWindowState.Minimized || !Visible)
                {
                    recognitionClass rc = new recognitionClass();
                    gatherC.fnSetMoneyFormRecognition(rc.recognitionFormSelectArea());
                    WindowState = FormWindowState.Normal;
                    Show();
                    //Application.DoEvents();
                    fnShowGatherC();
                    Activate();
                    TopMost = true;
                    gatherC.fnSetFocus(true);
                }
                else
                {

                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip.Show();
            }
        }

        private void exitToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            notifyIcon.Dispose();
            PublicMethods.exitSystem();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (gatherC.Visible)
                {
                    if(!UserClass.isUseKeyBorad)
                    {
                        if (keyData == Keys.Up)
                        {
                            return true;
                        }
                        else if (keyData == Keys.Down)
                        {
                            return true;
                        }
                        else if (keyData == Keys.Left)
                        {
                            return true;
                        }
                        else if (keyData == Keys.Right)
                        {
                            return true;
                        }
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                    if (fnGatherMouseControl(keyData))
                    {
                        return true;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                else if (storeC.Visible && UserClass.isUseKeyBorad)
                {
                    if (fnStoreGatherMouseControl(keyData))
                    {
                        return true;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                else if (memberC.Visible && UserClass.isUseKeyBorad)
                {
                    if (fnStoreMemberMouseControl(keyData))
                    {
                        return true;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                else if (couponC.Visible && UserClass.isUseKeyBorad)
                {
                    if (fnCouponMouseControl(keyData))
                    {
                        return true;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        #region 普通收银界面
        public void returnPosition()
        {
            try
            {
                gatherMouse.pos = 16;
            }
            catch { }
        }
        public void fnSetGatherMouse()
        {
            gatherMouse.Position.Clear();

            gatherMouse.couponnum = 0;
            gatherMouse.pos = -1;
            gatherMouse.listpos = 0;
            //gatherMouse.listmaxpos = 0;
            gatherMouse.hasmember = false;

            gatherMouse.Position.Add(new Point(650, 24));//重新登录0
            gatherMouse.Position.Add(new Point(715, 24));//最小化1
            gatherMouse.Position.Add(new Point(755, 24));//关闭2

            gatherMouse.Position.Add(new Point(700, 108));//收款金额3
            gatherMouse.Position.Add(new Point(700, 162));//不打折金额4

            gatherMouse.Position.Add(new Point(297, 65));//会员输入5

            gatherMouse.Position.Add(new Point(75, 145));//会员折扣选择6

            gatherMouse.Position.Add(new Point(115, 240));//卡券1 7
            gatherMouse.Position.Add(new Point(115, 320));//卡券2 8
            gatherMouse.Position.Add(new Point(115, 400));//卡券3 9
            gatherMouse.Position.Add(new Point(115, 480));//卡券4 10

            gatherMouse.Position.Add(new Point(400, 502));//条码11
            gatherMouse.Position.Add(new Point(480, 502));//扫码12
            gatherMouse.Position.Add(new Point(560, 502));//银联记账13
            gatherMouse.Position.Add(new Point(640, 502));//现金记账14
            gatherMouse.Position.Add(new Point(720, 502));//储值15

            gatherMouse.Position.Add(new Point(185, 24));//收款16
            gatherMouse.Position.Add(new Point(255, 24));//明细17
            gatherMouse.Position.Add(new Point(325, 24));//储值18
            gatherMouse.Position.Add(new Point(395, 24));//会员19
            gatherMouse.Position.Add(new Point(465, 24));//验券20
        }
        public bool fnGatherMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                #region down
                switch (gatherMouse.pos)
                {
                    case 0: 
                    case 1:
                    case 2: gatherMouse.pos = 3; break;

                    case 3: gatherMouse.pos = 4; break;
                    case 4: gatherMouse.pos = 11; break;

                    case 5:
                        if (gatherMouse.hasmember)
                            gatherMouse.pos = 6;
                        else
                            return true;
                        break;
                    case 6:
                        if (gatherMouse.couponnum == 0)
                            return true;
                        else
                            gatherMouse.pos = 7;
                        break;
                    case 7:
                        if (gatherMouse.couponnum == 1)
                            return true;
                        else
                            gatherMouse.pos = 8;
                        break;
                    case 8:
                        if (gatherMouse.couponnum == 2)
                            return true;
                        else
                            gatherMouse.pos = 9;
                        break;
                    case 9:
                        if (gatherMouse.couponnum == 3)
                            return true;
                        else
                            gatherMouse.pos = 10;
                        break;
                    case 10:
                        if (gatherMouse.listpos == gatherMouse.couponnum)
                        {
                            return true;
                        }
                        else
                        {
                            gatherC.setscroll(false);
                            gatherMouse.listpos++;
                            break;
                        }
                    case 11: gatherMouse.pos = 16; break;
                    case 12: gatherMouse.pos = 16; break;
                    case 13: gatherMouse.pos = 16; break;
                    case 14: gatherMouse.pos = 16; break;
                    case 15: gatherMouse.pos = 16; break;
                    case 16:
                    case 17:
                    case 18: 
                    case 19: 
                    case 20: gatherMouse.pos = 3; break;
                    default: gatherMouse.pos = 16; break;
                }
                gatherMouse._offsetX = Location.X + 20;
                gatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(gatherMouse.Position[gatherMouse.pos].X + gatherMouse._offsetX, gatherMouse.Position[gatherMouse.pos].Y + gatherMouse._offsetY);
                if (gatherMouse.pos == 3 || gatherMouse.pos == 4)
                {
                    NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                switch (gatherMouse.pos)
                {
                    case 0: gatherMouse.pos = 20; break;
                    case 1: gatherMouse.pos = 0; break;
                    case 2: gatherMouse.pos = 1; break;

                    case 3: gatherMouse.pos = 5; break;
                    case 4: gatherMouse.pos = 5; break;

                    case 5: //gatherMouse.pos = 3; break;
                    case 6: //gatherMouse.pos = 11; break;
                    case 7: //gatherMouse.pos = 11; break;
                    case 8: //gatherMouse.pos = 11; break;
                    case 9: //gatherMouse.pos = 11; break;
                    case 10: //gatherMouse.pos = 11; break;
                        {
                            return true;
                        }
                    case 11:
                        if (gatherMouse.couponnum == 1)
                        {
                            gatherMouse.pos = 7;
                        }
                        else if (gatherMouse.couponnum == 2)
                        {
                            gatherMouse.pos = 8;
                        }
                        else if (gatherMouse.couponnum == 3)
                        {
                            gatherMouse.pos = 9;
                        }
                        else if (gatherMouse.couponnum == 0)
                        {
                            if (gatherMouse.hasmember)
                            {
                                gatherMouse.pos = 6;
                            }
                            else
                            {
                                gatherMouse.pos = 4;
                            }
                        }
                        else
                        {
                            gatherMouse.pos = 10;
                        }
                        break;
                    case 12: gatherMouse.pos = 11; break;
                    case 13: gatherMouse.pos = 12; break;
                    case 14: gatherMouse.pos = 13; break;
                    case 15: gatherMouse.pos = 14; break;
                    case 16: gatherMouse.pos = 2; break;
                    case 17: gatherMouse.pos = 16; break;
                    case 18: gatherMouse.pos = 17; break;
                    case 19: gatherMouse.pos = 18; break;
                    case 20: gatherMouse.pos = 19; break;
                    default: gatherMouse.pos = 16; break;
                }
                gatherMouse._offsetX = Location.X + 20;
                gatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(gatherMouse.Position[gatherMouse.pos].X + gatherMouse._offsetX, gatherMouse.Position[gatherMouse.pos].Y + gatherMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (gatherMouse.pos)
                {
                    case 0: 
                    case 1: 
                    case 2: gatherMouse.pos = 11; break;

                    case 3: gatherMouse.pos = 16; break;
                    case 4: gatherMouse.pos = 3; break;
                    case 5: gatherMouse.pos = 16; break;
                    case 6: gatherMouse.pos = 5; break;
                    case 7:
                        if (gatherMouse.listpos == 0)
                        {
                            gatherMouse.pos = 6;
                            break;
                        }
                        else
                        {
                            gatherC.setscroll(true);
                            gatherMouse.listpos--;
                            break;
                        }
                    case 8: gatherMouse.pos = 7; break;
                    case 9: gatherMouse.pos = 8; break;
                    case 10: gatherMouse.pos = 9; break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        gatherMouse.pos = 4;
                        break;
                    case 16: 
                    case 17:
                    case 18: 
                    case 19:
                    case 20: gatherMouse.pos = 11; break;
                    default: gatherMouse.pos = 16; break;
                }
                gatherMouse._offsetX = Location.X + 20;
                gatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(gatherMouse.Position[gatherMouse.pos].X + gatherMouse._offsetX, gatherMouse.Position[gatherMouse.pos].Y + gatherMouse._offsetY);
                if (gatherMouse.pos == 3 || gatherMouse.pos == 4)
                {
                    NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (gatherMouse.pos)
                {
                    case 0: gatherMouse.pos = 1; break;
                    case 1: gatherMouse.pos = 2; break;
                    case 2: gatherMouse.pos = 16; break;

                    case 3: //gatherMouse.pos = 5; break;
                    case 4: //gatherMouse.pos = 5; break;
                        return true;
                    case 5: //gatherMouse.pos = 3; break;
                    case 6: //gatherMouse.pos = 11; break;
                    case 7: //gatherMouse.pos = 11; break;
                    case 8: //gatherMouse.pos = 11; break;
                    case 9: //gatherMouse.pos = 11; break;
                    case 10: //gatherMouse.pos = 11; break;
                        {
                            if (UserClass.orderInfoC.getMoney() == "" || UserClass.orderInfoC.getMoney() == "0.00")
                            {
                                gatherMouse.pos = 3;
                            }
                            else
                            {
                                gatherMouse.pos = 11;
                            }
                            break;
                        }
                    case 11: gatherMouse.pos = 12; break;
                    case 12: gatherMouse.pos = 13; break;
                    case 13: gatherMouse.pos = 14; break;
                    case 14: gatherMouse.pos = 15; break;
                    case 15: gatherMouse.pos = 11; break;

                    case 16: gatherMouse.pos = 17; break;
                    case 17: gatherMouse.pos = 18; break;
                    case 18: gatherMouse.pos = 19; break;
                    case 19: gatherMouse.pos = 20; break;
                    case 20: gatherMouse.pos = 0; break;
                    default: gatherMouse.pos = 16; break;
                }
                gatherMouse._offsetX = Location.X + 20;
                gatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(gatherMouse.Position[gatherMouse.pos].X + gatherMouse._offsetX, gatherMouse.Position[gatherMouse.pos].Y + gatherMouse._offsetY);
                if (gatherMouse.pos == 3 || gatherMouse.pos == 4)
                {
                    NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }
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

        #region 储值收银界面
        public void fnSetStoreMouse(bool _notclearnum = false)
        {
            storeGatherMouse.Position.Clear();
            if (!_notclearnum)
            {
                storeGatherMouse.storenum = 0;
            }
            storeGatherMouse.pos = -1;
            storeGatherMouse.listpos = 0;
            //storeGatherMouse.listmaxpos = 0;
            storeGatherMouse.hasmember = false;

            storeGatherMouse.Position.Add(new Point(185, 24));//收款0
            storeGatherMouse.Position.Add(new Point(255, 24));//明细1
            storeGatherMouse.Position.Add(new Point(325, 24));//储值2

            storeGatherMouse.Position.Add(new Point(650, 24));//重新登录3
            storeGatherMouse.Position.Add(new Point(715, 24));//最小化4
            storeGatherMouse.Position.Add(new Point(755, 24));//关闭5

            storeGatherMouse.Position.Add(new Point(700, 108));//收款金额6

            storeGatherMouse.Position.Add(new Point(297, 65));//会员输入7

            storeGatherMouse.Position.Add(new Point(210, 230));//储值活动1 8
            storeGatherMouse.Position.Add(new Point(210, 310));//储值活动2 9
            storeGatherMouse.Position.Add(new Point(210, 390));//储值活动3 10
            storeGatherMouse.Position.Add(new Point(210, 470));//储值活动4 11

            storeGatherMouse.Position.Add(new Point(440, 502));//条码12
            storeGatherMouse.Position.Add(new Point(540, 502));//扫码13
            storeGatherMouse.Position.Add(new Point(640, 502));//银联记账14
            storeGatherMouse.Position.Add(new Point(740, 502));//现金记账15

            storeGatherMouse.Position.Add(new Point(395, 24));//会员16
            storeGatherMouse.Position.Add(new Point(465, 24));//验券17
        }
        public bool fnStoreGatherMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                #region down
                switch (storeGatherMouse.pos)
                {
                    case 0: 
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: storeGatherMouse.pos = 7; break;
                    case 6: storeGatherMouse.pos = 12; break;
                    case 7:
                        {
                            if (storeGatherMouse.hasmember == true)
                                storeGatherMouse.pos = 8;
                            else
                                //storeGatherMouse.pos = 12;
                                return true;
                            break;
                        }
                    case 8:
                        {
                            if (storeGatherMouse.storenum == 0)
                                //storeGatherMouse.pos = 12;
                                return true;
                            else
                                storeGatherMouse.pos = 9;
                            break;
                        }
                    case 9:
                        {
                            if (storeGatherMouse.storenum == 1)
                                //storeGatherMouse.pos = 12;
                                return true;
                            else
                                storeGatherMouse.pos = 10;
                            break;
                        }
                    case 10:
                        {
                            if (storeGatherMouse.storenum == 2)
                                //storeGatherMouse.pos = 12;
                                return true;
                            else
                                storeGatherMouse.pos = 11;
                            break;
                        }
                    case 11:
                        {
                            if (storeGatherMouse.listpos == storeGatherMouse.storenum)
                            {
                                //storeGatherMouse.pos = 12;
                                return true;
                            }
                            else
                            {
                                storeC.fnSetScroll(false);
                                storeGatherMouse.listpos++;
                                break;
                            }
                        }
                    case 12:
                    case 13:
                    case 14:
                    case 15: storeGatherMouse.pos = 0; break;
                    case 16: 
                    case 17: storeGatherMouse.pos = 7; break;
                    default: storeGatherMouse.pos = 7; break;
                }
                storeGatherMouse._offsetX = Location.X + 20;
                storeGatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(storeGatherMouse.Position[storeGatherMouse.pos].X + storeGatherMouse._offsetX, storeGatherMouse.Position[storeGatherMouse.pos].Y + storeGatherMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                switch (storeGatherMouse.pos)
                {
                    case 0: storeGatherMouse.pos = 5; break;
                    case 1: storeGatherMouse.pos = 0; break;
                    case 2: storeGatherMouse.pos = 1; break;
                    case 3: storeGatherMouse.pos = 17; break;
                    case 4: storeGatherMouse.pos = 3; break;
                    case 5: storeGatherMouse.pos = 4; break;

                    case 6: storeGatherMouse.pos = 7; break;
                    case 7: //storeGatherMouse.pos = 6; break;
                    case 8: //storeGatherMouse.pos = 6; break;
                    case 9: //storeGatherMouse.pos = 12; break;
                    case 10: //storeGatherMouse.pos = 12; break;
                    case 11: //storeGatherMouse.pos = 12; break;
                        return true;
                    case 12: storeGatherMouse.pos = 15; break;
                    case 13: storeGatherMouse.pos = 12; break;
                    case 14: storeGatherMouse.pos = 13; break;
                    case 15: storeGatherMouse.pos = 14; break;
                    case 16: storeGatherMouse.pos = 2; break;
                    case 17: storeGatherMouse.pos = 16; break;
                    default: storeGatherMouse.pos = 1; break;
                }
                storeGatherMouse._offsetX = Location.X + 20;
                storeGatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(storeGatherMouse.Position[storeGatherMouse.pos].X + storeGatherMouse._offsetX, storeGatherMouse.Position[storeGatherMouse.pos].Y + storeGatherMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (storeGatherMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: storeGatherMouse.pos = 12; break;
                    case 6:
                    case 7: storeGatherMouse.pos = 0; break;
                    case 8:
                        if (storeGatherMouse.listpos == 0)
                        {
                            storeGatherMouse.pos = 7;
                            break;
                        }
                        else
                        {
                            storeC.fnSetScroll(true);
                            storeGatherMouse.listpos--;
                            break;
                        }
                    case 9: storeGatherMouse.pos = 8; break;
                    case 10: storeGatherMouse.pos = 9; break;
                    case 11: storeGatherMouse.pos = 10; break;
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        if(!storeGatherMouse.hasmember)
                        {
                            storeGatherMouse.pos = 7;
                            break;
                        }
                        if (storeGatherMouse.storenum == 0)
                        {
                            storeGatherMouse.pos = 8;
                        }
                        else if (storeGatherMouse.storenum == 1)
                        {
                            storeGatherMouse.pos = 9;
                        }
                        else if (storeGatherMouse.storenum == 2)
                        {
                            storeGatherMouse.pos = 10;
                        }
                        else
                        {
                            storeGatherMouse.pos = 11;
                        }
                        break;
                    case 16: 
                    case 17: storeGatherMouse.pos = 12; break;
                    default: storeGatherMouse.pos = 12; break;
                }
                storeGatherMouse._offsetX = Location.X + 20;
                storeGatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(storeGatherMouse.Position[storeGatherMouse.pos].X + storeGatherMouse._offsetX, storeGatherMouse.Position[storeGatherMouse.pos].Y + storeGatherMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (storeGatherMouse.pos)
                {
                    case 0: storeGatherMouse.pos = 1; break;
                    case 1: storeGatherMouse.pos = 2; break;
                    case 2: storeGatherMouse.pos = 16; break;
                    case 3: storeGatherMouse.pos = 4; break;
                    case 4: storeGatherMouse.pos = 5; break;
                    case 5: storeGatherMouse.pos = 0; break;

                    case 6: return true;//storeGatherMouse.pos = 7; break;
                    case 7: storeGatherMouse.pos = 6; break;
                    case 8: storeGatherMouse.pos = 6; break;
                    case 9: storeGatherMouse.pos = 12; break;
                    case 10: storeGatherMouse.pos = 12; break;
                    case 11: storeGatherMouse.pos = 12; break;
                    case 12: storeGatherMouse.pos = 13; break;
                    case 13: storeGatherMouse.pos = 14; break;
                    case 14: storeGatherMouse.pos = 15; break;
                    case 15: storeGatherMouse.pos = 12; break;
                    case 16: storeGatherMouse.pos = 17; break;
                    case 17: storeGatherMouse.pos = 3; break;
                    default: storeGatherMouse.pos = 16; break;
                }
                storeGatherMouse._offsetX = Location.X + 20;
                storeGatherMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(storeGatherMouse.Position[storeGatherMouse.pos].X + storeGatherMouse._offsetX, storeGatherMouse.Position[storeGatherMouse.pos].Y + storeGatherMouse._offsetY);
                if (storeGatherMouse.pos == 6)
                {
                    NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }
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

        #region 会员界面
        public void fnSetMemberMouse(bool _notclearnum = false)
        {
            memberMouse.Position.Clear();
            memberMouse.pos = 3;

            memberMouse.Position.Add(new Point(185, 24));//收款0
            memberMouse.Position.Add(new Point(255, 24));//明细1
            memberMouse.Position.Add(new Point(325, 24));//储值2
            memberMouse.Position.Add(new Point(395, 24));//会员3

            memberMouse.Position.Add(new Point(650, 24));//重新登录4
            memberMouse.Position.Add(new Point(715, 24));//最小化5
            memberMouse.Position.Add(new Point(755, 24));//关闭6
            memberMouse.Position.Add(new Point(350, 205));//会员卡号/手机号输入7
            memberMouse.Position.Add(new Point(380, 270));//添加会员8

            memberMouse.Position.Add(new Point(465, 24));//验券9

        }
        public bool fnStoreMemberMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                #region down
                switch (memberMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6: 
                    case 9:memberMouse.pos = 7; break;
                    case 7: memberMouse.pos = 8; break;
                    case 8: memberMouse.pos = 3; break;
                    default: memberMouse.pos = 7; break;
                }
                memberMouse._offsetX = Location.X + 20;
                memberMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(memberMouse.Position[memberMouse.pos].X + memberMouse._offsetX, memberMouse.Position[memberMouse.pos].Y + memberMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                switch (memberMouse.pos)
                {
                    case 0: memberMouse.pos = 6; break;
                    case 1: memberMouse.pos = 0; break;
                    case 2: memberMouse.pos = 1; break;
                    case 3: memberMouse.pos = 2; break;
                    case 9: memberMouse.pos = 3; break;
                    case 4: memberMouse.pos = 9; break;
                    case 5: memberMouse.pos = 4; break;
                    case 6: memberMouse.pos = 5; break;

                    case 7: memberMouse.pos = 7; break;
                    case 8: memberMouse.pos = 8; break;
                    default: memberMouse.pos = 2; break;
                }
                memberMouse._offsetX = Location.X + 20;
                memberMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(memberMouse.Position[memberMouse.pos].X + memberMouse._offsetX, memberMouse.Position[memberMouse.pos].Y + memberMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (memberMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 9: memberMouse.pos = 8; break;
                    case 7: memberMouse.pos = 3; break;
                    case 8: memberMouse.pos = 7; break;
                    default: memberMouse.pos = 8; break;
                }
                memberMouse._offsetX = Location.X + 20;
                memberMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(memberMouse.Position[memberMouse.pos].X + memberMouse._offsetX, memberMouse.Position[memberMouse.pos].Y + memberMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (memberMouse.pos)
                {
                    case 0: memberMouse.pos = 1; break;
                    case 1: memberMouse.pos = 2; break;
                    case 2: memberMouse.pos = 3; break;
                    case 3: memberMouse.pos = 9; break;
                    case 9: memberMouse.pos = 4; break;
                    case 4: memberMouse.pos = 5; break;
                    case 5: memberMouse.pos = 6; break;
                    case 6: memberMouse.pos = 0; break;
                    case 7: memberMouse.pos = 7; break;
                    case 8: memberMouse.pos = 8; break;
                    default: memberMouse.pos = 4; break;
                }
                memberMouse._offsetX = Location.X + 20;
                memberMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(memberMouse.Position[memberMouse.pos].X + memberMouse._offsetX, memberMouse.Position[memberMouse.pos].Y + memberMouse._offsetY);
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

        #region 验券界面
        public void fnSetCouponMouse(bool _notclearnum = false)
        {
            couponMouse.Position.Clear();
            if(!_notclearnum)
            {
                couponMouse.pos = 4;
            }
            else
            {
                couponMouse.pos = 8;
            }
 
            couponMouse.Position.Add(new Point(185, 24));//收款0
            couponMouse.Position.Add(new Point(255, 24));//明细1
            couponMouse.Position.Add(new Point(325, 24));//储值2
            couponMouse.Position.Add(new Point(395, 24));//会员3
            couponMouse.Position.Add(new Point(465, 24));//验券4

            couponMouse.Position.Add(new Point(650, 24));//重新登录5
            couponMouse.Position.Add(new Point(715, 24));//最小化6
            couponMouse.Position.Add(new Point(755, 24));//关闭7

            couponMouse.Position.Add(new Point(350, 95));//优惠券码输入8
            couponMouse.Position.Add(new Point(350, 440));//金额9
            couponMouse.Position.Add(new Point(350, 500));//核销10

            couponMouse.Position.Add(new Point(320, 420));//核销11
        }
        public bool fnCouponMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Down)
            {
                #region down
                switch (couponMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7: couponMouse.pos = 8; break;
                    case 8: 
                        if (couponC.IsHasCoupon)
                        {
                            if(couponC.IsKoubeiCoupon)
                            {
                                couponMouse.pos = 11; break;
                            }
                            else
                            {
                                couponMouse.pos = 9; break;
                            }
                        }
                        break;
                    case 9: couponMouse.pos = 10; break;
                    case 10: couponMouse.pos = 0; break;
                    case 11: couponMouse.pos = 0; break;
                    default: couponMouse.pos = 4; break;
                }
                couponMouse._offsetX = Location.X + 20;
                couponMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(couponMouse.Position[couponMouse.pos].X + couponMouse._offsetX, couponMouse.Position[couponMouse.pos].Y + couponMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                switch (couponMouse.pos)
                {
                    case 0: couponMouse.pos = 7; break;
                    case 1: couponMouse.pos = 0; break;
                    case 2: couponMouse.pos = 1; break;
                    case 3: couponMouse.pos = 2; break;
                    case 4: couponMouse.pos = 3; break;
                    case 5: couponMouse.pos = 4; break;
                    case 6: couponMouse.pos = 5; break;
                    case 7: couponMouse.pos = 6; break;
                    case 8:
                    case 9:
                    case 10:
                    case 11: break;
                    default: couponMouse.pos = 4; break;
                }
                
                couponMouse._offsetX = Location.X + 20;
                couponMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(couponMouse.Position[couponMouse.pos].X + couponMouse._offsetX, couponMouse.Position[couponMouse.pos].Y + couponMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Up)
            {
                #region up
                switch (couponMouse.pos)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        if (couponC.IsKoubeiCoupon)
                        {
                            couponMouse.pos = 11; break;
                        }
                        else
                        {
                            couponMouse.pos = 10; break;
                        }
                    case 8: couponMouse.pos = 4; break;
                    case 9: couponMouse.pos = 8; break;
                    case 10: couponMouse.pos = 9; break;
                    case 11: couponMouse.pos = 8; break;
                    default: couponMouse.pos = 4; break;
                }
                couponMouse._offsetX = Location.X + 20;
                couponMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(couponMouse.Position[couponMouse.pos].X + couponMouse._offsetX, couponMouse.Position[couponMouse.pos].Y + couponMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (couponMouse.pos)
                {
                    case 0: couponMouse.pos = 1; break;
                    case 1: couponMouse.pos = 2; break;
                    case 2: couponMouse.pos = 3; break;
                    case 3: couponMouse.pos = 4; break;
                    case 4: couponMouse.pos = 5; break;
                    case 5: couponMouse.pos = 6; break;
                    case 6: couponMouse.pos = 7; break;
                    case 7: couponMouse.pos = 0; break;
                    case 8:
                    case 9:
                    case 10: 
                    case 11: break;
                    default: couponMouse.pos = 4; break;
                }
                Console.WriteLine(couponMouse.pos);
                couponMouse._offsetX = Location.X + 20;
                couponMouse._offsetY = Location.Y + 40;
                NativeMethods.SetCursorPos(couponMouse.Position[couponMouse.pos].X + couponMouse._offsetX, couponMouse.Position[couponMouse.pos].Y + couponMouse._offsetY);
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
