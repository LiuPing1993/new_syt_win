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
    public partial class activateControl : baseRequestControl
    {
        logininputControl codeinputC = new logininputControl(InType.actcode);
        loginactivatebuttonControl activateC = new loginactivatebuttonControl("激        活");
        Label lblInfo = new Label();
        Label lbProxy = new Label();

        loginform.activate act;

        public activateControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            codeinputC.Location = new Point((ClientRectangle.Width - codeinputC.Width) / 2, 135);
            codeinputC.TabStop = false;
            Controls.Add(codeinputC);

            lblInfo.Text = "首次登录需要验证商户并激活";
            lblInfo.BackColor = Defcolor.MainBackColor;
            lblInfo.Font = new System.Drawing.Font(UserClass.fontName, 10);
            lblInfo.ForeColor = Defcolor.FontLiteGrayColor;
            lblInfo.SetBounds(100, 180, 300, 35);
            Controls.Add(lblInfo);

            lbProxy.Text = "设置代理上网";
            lbProxy.Font = new System.Drawing.Font(UserClass.fontName, 10);
            lbProxy.ForeColor = Color.FromArgb(100, 100, 100);
            lbProxy.BackColor = Defcolor.MainBackColor;
            lbProxy.Location = new Point(390, 310);
            Controls.Add(lbProxy);

            lbProxy.MouseUp += new MouseEventHandler(fnProxySet_Click);

            activateC.Location = new Point((ClientRectangle.Width - activateC.Width) / 2, 250);
            activateC.MouseUp += new MouseEventHandler(activate_MouseUp);
            Controls.Add(activateC);

        }
        public void fnProxySet_Click(object sender, MouseEventArgs e)
        {
            ((loginForm)Parent).fnShowControls(lgFormType.proxy);
        }
        private void activateControl_Load(object sender, EventArgs e)
        {
            ActiveControl = codeinputC;
            codeinputC.Focus();
        }
        private void activate_MouseUp(object sender, MouseEventArgs e)
        {
            if (codeinputC.fnGetValue() == "")
            {
                return;
            }
            start();
        }

        public void start()
        {
            if (!IsThreadRun)
            {
                act = new loginform.activate();
                act.activation_code = codeinputC.fnGetValue();
                d = new addDelegate(result);
                start(act);
                ((loginForm)Parent).waitC.sInfo = "激活中";
                ((loginForm)Parent).fnShowControls(lgFormType.wait);
            }
        }
        public void result()
        {
            string temp_error = "";
            errorinformationForm _infoF;
            try
            {
                if (iRThread == 0)
                {
                    if (act.iRHttp == 0)
                    {
                        //保存激活成功信息
                        loadconfigClass _lcc = new loadconfigClass("app_id");

                        _lcc.writetoConfig(act.resultS.data.app_id);

                        _lcc = new loadconfigClass("merchant_code");

                        _lcc.writetoConfig(act.resultS.data.merchant_code);

                        PublicMethods.writeIn(act.resultS.data.app_key);

                        BaseValue.app_id = act.resultS.data.app_id;

                        BaseValue.merchant_code = act.resultS.data.merchant_code;

                        ((loginForm)Parent).fnShowControls(lgFormType.login);

                        _infoF = new errorinformationForm("激活成功", sHttpResult);
                        _infoF.TopMost = true;
                        _infoF.StartPosition = FormStartPosition.CenterScreen;
                        _infoF.ShowDialog();
                        return;
                    }
                    else
                    {
                        temp_error = act.sRHttp;
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

            ((loginForm)Parent).fnShowControls(lgFormType.activate);
            _infoF = new errorinformationForm("激活失败", temp_error);
            _infoF.TopMost = true;
            _infoF.StartPosition = FormStartPosition.CenterScreen;
            _infoF.ShowDialog();
            ((loginForm)Parent).Refresh();
            ((loginForm)Parent).Activate();
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                if (!codeinputC.tbxValue.Focused)
                {
                    codeinputC.Focus();
                }
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                activate_MouseUp(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void activateControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid
            );

            e.Graphics.FillRectangle(new SolidBrush(Defcolor.MainRadColor), new Rectangle(0, 0, e.ClipRectangle.Width, 45));
            e.Graphics.DrawLine(new Pen(Defcolor.MainBackColor, 2), new Point(466, 10), new Point(486, 30));
            e.Graphics.DrawLine(new Pen(Defcolor.MainBackColor, 2), new Point(466, 30), new Point(486, 10));
            e.Graphics.DrawImage(Properties.Resources.qxtitle, 10, 8, 105, 27);
        }
    }

    public class errorClass
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string errCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMsg { get; set; }
    }

}
