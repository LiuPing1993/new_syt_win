using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace self_syt.login
{
    public partial class SetWebProxyControl : UserControl
    {

        public comui.button.ButtonControl btn_ok;
        public comui.button.ButtonControl btn_cancel;

        untils.UoperaConfig config = new untils.UoperaConfig(BaseConfigValue.proxyPara);
        public SetWebProxyControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            btn_ok = new comui.button.ButtonControl();
            btn_ok.Size = new Size(80, 32);
            btn_ok.Location = new Point(ClientRectangle.Width / 2 + 6, ClientRectangle.Height - btn_ok.Height - 20);
            btn_ok.MouseUp += btn_ok_MouseUp;
            Controls.Add(btn_ok);

            btn_cancel = new comui.button.ButtonControl();
            btn_cancel.MouseUp += btn_cancel_MouseUp;
            btn_cancel.tips = "取消";
            btn_cancel.Size = new Size(80, 32);
            btn_cancel.isHasBorder = true;
            btn_cancel.Location = new Point(ClientRectangle.Width / 2 - btn_cancel.Width - 6, ClientRectangle.Height - btn_ok.Height - 20);
            Controls.Add(btn_cancel);

            BackColor = BaseColor.color_White;

            textBox1.Font = new Font(BaseValue.baseFont, 11);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.TextAlign = HorizontalAlignment.Center;
            //读取当前已经设置的代理的地址
            this.textBox1.Text = config.ReadConfig();
        }

        void btn_cancel_MouseUp(object sender, MouseEventArgs e)
        {
            ((SetWebProxyForm)this.Parent).Close();
        }

        void btn_ok_MouseUp(object sender, MouseEventArgs e)
        {
            config.WriteConfig(this.textBox1.Text.ToString().Trim());
            ((SetWebProxyForm)this.Parent).Close();
        }

        private void SetWebProxyControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            untils.UcomUntils.FrameRoundRectangle(new Rectangle(this.textBox1.Location.X - 5, this.textBox1.Location.Y - 5, this.textBox1.Width + 10, this.textBox1.Height + 10), e.Graphics, 7, BaseColor.line_gray);
        }
    }
}
