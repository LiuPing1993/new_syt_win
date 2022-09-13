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
    public partial class memberControl : UserControl
    {
        int iHttpResult = 0;
        string sHttpResult = "";

        string sInfo = "请输入或扫描会员卡号或手机号";

        PictureBox searchPic = new PictureBox();
        public TextBox codeinsert = new TextBox();

        membersearchSuccess memberinfoS;

        Label lblAddMember = new Label();

        public memberControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            searchPic.SetBounds(540, 160, 20, 20);
            searchPic.BackColor = Color.White;
            searchPic.Image = Properties.Resources.search;
            searchPic.SizeMode = PictureBoxSizeMode.Zoom;
            searchPic.MouseUp += new MouseEventHandler(search_Click);
            Controls.Add(searchPic);

            //codeinsert.BackColor = Color.White;
            codeinsert.SetBounds(210, 160, 300, 30);
            codeinsert.Font = new Font(UserClass.fontName, 12);
            codeinsert.ForeColor = Defcolor.FontLiteGrayColor;
            codeinsert.ImeMode = System.Windows.Forms.ImeMode.Off;
            codeinsert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            codeinsert.Enter += new EventHandler(codeinsert_enter);
            Controls.Add(codeinsert);

            lblAddMember.Location = new Point(370, 230);
            lblAddMember.Text = "添加会员";
            lblAddMember.Font = new Font(UserClass.fontName, 9);
            lblAddMember.ForeColor = Defcolor.FontBlueColor;
            lblAddMember.MouseUp += new MouseEventHandler(memberadd_Click);
            Controls.Add(lblAddMember);
        }

        private void memberControl_Load(object sender, EventArgs e)
        {
            iHttpResult = 0;
            codeinsert.Focus();
            ((main)Parent).fnSetMemberMouse();
        }

        public bool fnMemberinfoHttp()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.membersearch);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("code", codeinsert.Text.Trim());
                _urlC.addParameter("token", UserClass.Token);
                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                HttpClass httpC = new HttpClass();
                string _sRequestMsg = httpC.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + _sRequestMsg);
                if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                {
                    memberinfoS = (membersearchSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(membersearchSuccess));
                    iHttpResult = 0;
                    sHttpResult = "成功";
                    return true;
                }
                else
                {
                    errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                    iHttpResult = 1;
                    sHttpResult = _errorC.errMsg;
                    return false;
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.Message.ToString();
                return false;
            }
            
        }
        public void search_Click(object sender, MouseEventArgs e)
        {
            if (codeinsert.Text == "")
            {
                return;
            }
            if (fnMemberinfoHttp())
            {
                memberDetailForm memberdetailF = new memberDetailForm(memberinfoS);
                memberdetailF.TopMost = true;
                memberdetailF.StartPosition = FormStartPosition.CenterParent;
                memberdetailF.ShowDialog();
                Refresh();
            }
            else
            {
                Refresh();
            }
        }
        public void memberadd_Click(object sender, MouseEventArgs e)
        {
            addMemberForm addmemberF = new addMemberForm(this);
            addmemberF.StartPosition = FormStartPosition.CenterParent;
            addmemberF.TopMost = true;
            addmemberF.ShowDialog();
            Refresh();
            if (addmemberF.DialogResult == DialogResult.OK)
            {
                search_Click(null, null);
            }
        }
        private void codeinsert_enter(object sender, EventArgs e)
        {
            keyboardClass.showKeyBoard();
        }
        private void memberControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid
            );

            PublicMethods.FillRoundRectangle(new Rectangle(200, 150, 580, 190), e.Graphics, 15, Color.White);

            User.drawFormatTitle.Alignment = StringAlignment.Center;
            User.drawFormatTitle.LineAlignment = StringAlignment.Center;
            
            string sLine = "";
            Color infoColor = Color.Gray;
            if (iHttpResult == 1)
            {
                sLine = String.Format(sHttpResult);
                infoColor = Defcolor.MainRadColor;
            }
            else
            {
                sLine = String.Format(sInfo);
            }
            e.Graphics.DrawString(sLine, new Font(UserClass.fontName, 9), new SolidBrush(infoColor), new Rectangle(200, 200, 380, 30), User.drawFormatTitle);
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (keyData == Keys.Tab)
                {
                    ((main)Parent).fnShowCouponC();
                    return true;
                }
                else if (keyData == Keys.Escape)
                {
                    ((main)Parent).toptitleC.close_MouseUp(null, null);
                    return true;
                }
                else if (keyData == Keys.Enter && Visible == true)
                {
                    search_Click(null, null);
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

    }

    public class memberinfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 李四
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 普通会员
        /// </summary>
        public string member_level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string member_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string member_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bonus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ID_number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string educational { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string occupation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string industry { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string income { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string interest { get; set; }
    }

    public class membersearchSuccess
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
        public memberinfo data { get; set; }
    }
}
