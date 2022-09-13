using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace qgj
{
    public partial class storeControl : UserControl
    {
        public storemoneyinputControl storemoneyinputC = new storemoneyinputControl();
        public storeshowControl storeshowC = new storeshowControl();

        public memberselectControl memberselectC = new memberselectControl();
        public memberConfirmControl memberConfirmC = new memberConfirmControl();
        storeEventControl[] storeEventC;
        storeMemberInfoControl storeMemberInfoC = new storeMemberInfoControl("", "", "", "");

        numPadControl numpadC = new numPadControl();
        paywayControl[] paywayC = new paywayControl[5];
        List<storeEventControl> storeinfoList = new List<storeEventControl>();

        public string sAllMoney = "";

        Panel panelStore = new Panel();
        public Panel panelMemberInfo = new Panel();
        Panel panelOverStoreScroll = new Panel();

        storelistSuccess storelistS;
        memberSuccess memberS;
        errorClass errorC;
        int iHttpResult = 0;
        string sHttpResult = "";
        public storeControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();

            memberselectC.Location = new Point(10, 10);
            this.Controls.Add(memberselectC);

            storemoneyinputC.Location = new Point(360, 40);
            this.Controls.Add(storemoneyinputC);

            storeshowC.Location = new Point(1, 451);
            this.Controls.Add(storeshowC);

            numpadC.Location = new Point(360, 160);
            this.Controls.Add(numpadC);

            for (int i = 0; i < 4; i++)
            {
                paywayC[i] = new paywayControl((PayWay)Enum.Parse(typeof(PayWay), i.ToString()), true);
                paywayC[i].SetBounds(360 + i * 100, 420, 100, 70);
                this.Controls.Add(paywayC[i]);
            }
            memberConfirmC.Location = new Point(626, 345);
            Controls.Add(memberConfirmC);
            memberConfirmC.BringToFront();
            memberConfirmC.Visible = false;

            panelMemberInfo.SetBounds(1, 50, 339, 90);
            panelMemberInfo.BackColor = Defcolor.MainBackColor;
            Controls.Add(panelMemberInfo);

            panelStore.SetBounds(1, 55, 359, 390);
            panelStore.BackColor = Defcolor.MainBackColor;
            panelStore.AutoScroll = true;
            panelStore.MouseEnter += new EventHandler(fnStore_MouseEnter);
            Controls.Add(panelStore);

            panelOverStoreScroll.SetBounds(340, 0, 20, 450);
            panelOverStoreScroll.BackColor = Defcolor.MainBackColor;
            panelOverStoreScroll.Paint += new PaintEventHandler(overStoreScroll_Paint);
            Controls.Add(panelOverStoreScroll);
            panelOverStoreScroll.BringToFront();

        }
        private void StoreControl_Load(object sender, EventArgs e)
        {
            UserClass.storeInfoC.reload();
            BackColor = Defcolor.MainBackColor;
            panelMemberInfo.Hide();
            ((main)Parent).fnSetStoreMouse();
            try
            {
                if (fnStoreListHttp())
                {
                    /*
                    storeGatherMouse.storenum = storelistS.data.Count;
                    int count = storelistS.data.Count + 1;
                    storeEventC = new storeEventControl[count];
                    storeEventC[0] = new storeEventControl("0", "", "普通储值", "充值任意金额", "");
                    storeEventC[0].Location = new Point(10, 5);
                    storeinfoList.Add(storeEventC[0]);
                    for (int i = 1; i < count; i++)
                    {
                        storeEventC[i] = new storeEventControl(
                            storelistS.data[i - 1].id,
                            storelistS.data[i - 1].recharge_money,
                            storelistS.data[i - 1].title,
                            storelistS.data[i - 1].sub_title,
                            storelistS.data[i - 1].start_time + " - " + storelistS.data[i - 1].end_time
                            );
                        storeEventC[i].Location = new Point(10, 5 + i * 85);
                        panelStore.Controls.Add(storeEventC[i]);
                        storeinfoList.Add(storeEventC[i]);
                    }
                    */
                    
                    int count = storelistS.data.Count;
                    storeGatherMouse.storenum = count - 1;
                    storeEventC = new storeEventControl[count];
                    for (int i = 0; i < count; i++)
                    {
                        storeEventC[i] = new storeEventControl( 
                            storelistS.data[i].id,
                            storelistS.data[i].recharge_money,
                            storelistS.data[i].title,
                            storelistS.data[i].sub_title,
                            storelistS.data[i].start_time + " - " + storelistS.data[i].end_time 
                            );
                        storeEventC[i].Location = new Point(10, 5 + i * 85);
                        panelStore.Controls.Add(storeEventC[i]);
                        storeinfoList.Add(storeEventC[i]);
                    }
                     
                }
            }
            catch (Exception ee) 
            { 
                Console.WriteLine(ee.ToString()); 
            }
            
        }
        private bool fnStoreListHttp()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.storelist);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);

                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                HttpClass _httpC = new HttpClass();
                string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + _sRequestMsg);

                if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                {
                    storelistS = (storelistSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(storelistSuccess));
                    return true;
                }
                else
                {
                    errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public bool fnMemberStoreInfo(string _sreachnum, ref string _name, ref string _num)
        {
            if (_sreachnum == "")
            {
                return false;
            }
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
                else
                {
                    errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                    iHttpResult = 1;
                    sHttpResult = errorC.errMsg;
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.Message.ToString();
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
            try
            {
                //ref数据
                _name = memberS.data.member_info.user_name;
                _num = memberS.data.member_info.account;

                if (memberS.data.member_info.member_code != "")
                {
                    storeMemberInfoC = new storeMemberInfoControl(
                        memberS.data.member_info.member_level,
                        memberS.data.member_info.member_code,
                        memberS.data.member_info.balance,
                        memberS.data.member_info.avatar
                        );
                    storeMemberInfoC.Location = new Point(10, 10);
                    panelMemberInfo.Controls.Add(storeMemberInfoC);
                    UserClass.storeInfoC.code = memberS.data.member_info.member_code;
                }
                else if (memberS.data.member_info.account != "")
                {
                    storeMemberInfoC = new storeMemberInfoControl(
                        memberS.data.member_info.member_level,
                        memberS.data.member_info.member_code,
                        memberS.data.member_info.balance,
                        memberS.data.member_info.avatar
                        );
                    storeMemberInfoC.Location = new Point(10, 10);
                    panelMemberInfo.Controls.Add(storeMemberInfoC);
                    UserClass.storeInfoC.code = memberS.data.member_info.account;
                }
                else
                {
                    errorinformationForm _errorF = new errorinformationForm("未能查询到", "未能获取到该会员");
                    _errorF.TopMost = true;
                    _errorF.StartPosition = FormStartPosition.CenterParent;
                    _errorF.ShowDialog();
                    ((main)Parent).Refresh();
                    return false;
                }
                storeGatherMouse.hasmember = true;
                panelStore.SetBounds(1, 140, 359, 305);
                panelMemberInfo.Show();
                return true;
            }
            catch (Exception e)
            {
                PublicMethods.WriteLog(e);
                errorinformationForm _errorF = new errorinformationForm("错误0079(日志已保存)", e.Message.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterScreen;
                _errorF.ShowDialog();
                //((main)Parent).Refresh();
                return false;
            }
        }
        private void StoreControl_Paint(object sender, PaintEventArgs e)
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
        private void overStoreScroll_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(0, 0), new Point(0, 450));
        }
        /// <summary>
        /// 清除所有会员及储值相关内容
        /// </summary>
        public void fnClearMemberStoreInfo()
        {
            sAllMoney = "";
            storemoneyinputC.fnInputNum("0.00");
            storemoneyinputC.IsAble = false;
            panelMemberInfo.Controls.Clear();
            panelMemberInfo.Hide();
            panelStore.SetBounds(1, 55, 359, 390);
            fnClearStoreSelect();
            storeshowC.fnSetShowStoreMoney(sAllMoney);
            UserClass.storeInfoC.code = "";
            UserClass.storeInfoC.storetype = "";
            UserClass.storeInfoC.setstoremoney("");
            ((main)Parent).fnSetStoreMouse(true);
            Refresh();
        }
        public void fnClearStoreSelect()
        {
            if (UserClass.storeInfoC.storetype != "")
            {
                foreach (storeEventControl value in storeinfoList)
                {
                    value.fnSetSelectStatus(false);
                }
                storeshowC.fnSetShowStoreMoney("");
                UserClass.storeInfoC.storetype = "";
                UserClass.storeInfoC.setstoremoney("");
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
                memberConfirmC.Visible = false;
            }
            else
            {
                storemoneyinputC.storemoneyinputControl_Leave(null, null);
                memberConfirmC.Visible = true;
                memberConfirmC.BringToFront();
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="_ismin"></param>
        public void fnReload(bool _ismin = true)
        {
            memberselectC.fnClearMemberSelect();
            if (_ismin)
            {
                loadconfigClass lcc = new loadconfigClass("successmini");
                if (lcc.readfromConfig() == "true")
                {
                    try
                    {
                        ((main)Parent).WindowState = FormWindowState.Minimized;
                    }
                    catch { }
                }
            }
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        public void fnSetFocus(bool _flag = false)
        {
            if (_flag)
            {
                storemoneyinputC.storemoneyinputControl_Click(this, null);
            }
            else
            {
                storemoneyinputC.storemoneyinputControl_Click(null, null);
            }
            storemoneyinputC.Focus();
        }
        private void fnStore_MouseEnter(object sender, EventArgs e)
        {
            if (panelStore.Controls.Count > 1)
            {
                panelStore.Focus();
            }
        }
        /// <summary>
        /// 输入金额
        /// </summary>
        /// <param name="_keyData"></param>
        /// <returns></returns>
        private bool fnSetMoneyFormKeyboard(System.Windows.Forms.Keys _keyData)
        {
            string _sKeyTemp = PublicMethods.KeyCodeToChar(_keyData).ToString();
            if (PublicMethods.moneyCheck(sAllMoney + _sKeyTemp))
            {
                if (sAllMoney == "" && (_sKeyTemp == "." || _sKeyTemp == "0"))
                {
                    _sKeyTemp = "0.";
                }
                sAllMoney += _sKeyTemp;
                storemoneyinputC.fnInputNum(sAllMoney);
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
                    storemoneyinputC.fnInputNum("0.00");
                    return true;
                }
                else
                {
                    if (sAllMoney.Length == 1)
                    {
                        sAllMoney = "";
                        storemoneyinputC.fnInputNum("0.00");
                        return true;
                    }
                    sAllMoney = sAllMoney.Remove(sAllMoney.Length - 1, 1);
                    storemoneyinputC.fnInputNum(sAllMoney);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                ((main)Parent).fnShowMemberC();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                ((main)Parent).toptitleC.close_MouseUp(null, null);
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                if (memberselectC.tbxInsert.Focused)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                if (UserClass.fastPayWay == "" || UserClass.fastPayWay == "bar")
                {
                    paywayC[0].fnFastPayStore();
                    return true;
                }
                if (UserClass.fastPayWay == "qr")
                {
                    paywayC[1].fnFastPayStore();
                    return true;
                }
            }
            else if (keyData == Keys.Space)
            {
                if (UserClass.fastPayWay == "")
                {
                    paywayC[1].fnFastPayStore();
                    return true;
                }
            }
            if (storemoneyinputC.fnGetStatus())
            {
                if (keyData == Keys.Enter)
                {
                    return true;
                }
                if (fnSetMoneyFormKeyboard(keyData))
                {
                    storeshowC.fnSetShowStoreMoney(sAllMoney);
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
        public void fnSetScroll(bool _isUp)
        {
            Point _p = new Point(panelStore.AutoScrollPosition.X, panelStore.AutoScrollPosition.Y);
            if (_isUp)
            {
                _p.Y = -(panelStore.AutoScrollPosition.Y + 85);
            }
            else
            {
                _p.Y = 85 - panelStore.AutoScrollPosition.Y;
            }
            panelStore.AutoScrollPosition = _p;
        }
    }
    public class storeListItem
    {
        /// <summary>
        /// 储值活动id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 储值活动标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 储值活动描述
        /// </summary>
        public string sub_title { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 储值金额
        /// </summary>
        public string recharge_money { get; set; }
    }

    public class storelistSuccess
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
        /// 任意储值开关
        /// </summary>
        public string if_arbitrary_recharge { get; set; }
        /// <summary>
        /// 储值活动列表
        /// </summary>
        public List<storeListItem> data { get; set; }
    }
}
