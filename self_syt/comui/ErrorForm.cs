using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
namespace self_syt.comui
{
    public partial class ErrorForm : SkinMain
    {
        public string title = "提示";
        public string message = "网络出现错误，请检查网络是否正常";

        Rectangle rect_title;
        Rectangle rect_message;

        public bool isChoice = false;

        public comui.button.ButtonControl btn_know;

        public comui.button.ButtonControl btn_ok;
        public comui.button.ButtonControl btn_cancel;

        public ErrorForm(string _title, string _message, bool choice = false)
        {
            InitializeComponent();
            //防止在登录多次后未居中显示
            BaseValue.drawFormatTitle.Alignment = StringAlignment.Center;
            BaseValue.drawFormatTitle.LineAlignment = StringAlignment.Center;
            isChoice = choice;

            title = _title;
            message = _message;
            StartPosition = FormStartPosition.CenterScreen;
            this.panel_top.BackColor = BaseColor.color_White;
            this.panel_mid.BackColor = BaseColor.color_White;
            this.panel_bottom.BackColor = BaseColor.color_White;

            rect_title = new Rectangle(0, 0, this.panel_top.ClientRectangle.Width, this.panel_top.ClientRectangle.Height);
            rect_message = new Rectangle(0, 0, this.panel_mid.ClientRectangle.Width, this.panel_mid.ClientRectangle.Height);

            if (isChoice)
            {
                btn_ok = new button.ButtonControl();
                btn_ok.tips = "确定";
                btn_ok.MouseUp += btn_ok_MouseUp;
                btn_ok.Size = new Size(85, 30);
                btn_ok.Location = new Point(panel_bottom.ClientRectangle.Width / 2 + 6, (panel_bottom.ClientRectangle.Height - btn_ok.ClientRectangle.Height) / 2);
                this.panel_bottom.Controls.Add(btn_ok);

                btn_cancel = new button.ButtonControl();
                btn_cancel.tips = "取消";
                btn_cancel.isHasBorder = true;
                btn_cancel.Size = new Size(85, 30);
                btn_cancel.MouseUp += btn_cancel_MouseUp;
                btn_cancel.Location = new Point(panel_bottom.ClientRectangle.Width / 2 - btn_cancel.Width - 6, (panel_bottom.ClientRectangle.Height - btn_cancel.ClientRectangle.Height) / 2);
                this.panel_bottom.Controls.Add(btn_cancel);
            }
            else
            {
                btn_know = new button.ButtonControl();
                btn_know.isHasBorder = true;
                btn_know.tips = "我知道了";
                btn_know.Size = new Size(85, 30);
                btn_know.MouseUp += btn_know_MouseUp;
                btn_know.Location = new Point((panel_bottom.ClientRectangle.Width - btn_know.Width) / 2, (panel_bottom.ClientRectangle.Height - btn_know.ClientRectangle.Height) / 2);
                this.panel_bottom.Controls.Add(btn_know);
            }

        }

        void btn_cancel_MouseUp(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }

        void btn_ok_MouseUp(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        void btn_know_MouseUp(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void panel_top_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString(title, new Font(BaseValue.baseFont, 11, FontStyle.Bold), new SolidBrush(BaseColor.color_Black), rect_title, BaseValue.drawFormatTitle);
        }

        private void panel_mid_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //将接口返回的不清楚信息转化为正常的文字信息
            if (message.Contains("Money must be an integer"))
            {
                message = "订单金额不合法";
            }
            e.Graphics.DrawString(message, new Font(BaseValue.baseFont, 9), new SolidBrush(BaseColor.color_Black), rect_message, BaseValue.drawFormatTitle);
        }
    }
}
