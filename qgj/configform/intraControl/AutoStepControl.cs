using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class AutoStepControl : UserControl
    {
        public string time_value;
        public int no = 1;
        Rectangle tbx_rect = new Rectangle(90, 5, 55, 20);
        public Rectangle select_area_rect = new Rectangle(210, 5, 60, 20);

        //public string tip = "选择区域";
        public AutoStepControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            lbl_title.BackColor = Defcolor.MainBackColor;
            lbl_click.BackColor = Defcolor.MainBackColor;
            lbl_del.BackColor = Defcolor.MainBackColor;
            lbl_del.ForeColor = Defcolor.FontBlueColor;
        }

        private void AutoStepControl_Load(object sender, EventArgs e)
        {
            switch (no)
            {
                case 1: lbl_title.Text = "第一步 : 延迟"; break;
                case 2: lbl_title.Text = "第二步 : 延迟"; break;
                case 3: lbl_title.Text = "第三步 : 延迟"; break;
                case 4: lbl_title.Text = "第四步 : 延迟"; break;
                case 5: lbl_title.Text = "第五步 : 延迟"; break;
                default:
                    lbl_title.Text = "第一步 : 延迟"; break;
            }
        }

        private void AutoStepControl_Paint(object sender, PaintEventArgs e)
        {
            StringFormat _drawFormat = new StringFormat();
            _drawFormat.Alignment = StringAlignment.Center;
            _drawFormat.LineAlignment = StringAlignment.Center;

            Font _myFont = new Font(UserClass.fontName, 9);
            if (is_set_area())
            {
                e.Graphics.DrawString("已设置", _myFont, new SolidBrush(Defcolor.FontLiteGrayColor), select_area_rect, _drawFormat);
            }
            else
            {
                e.Graphics.DrawString("选择区域", _myFont, new SolidBrush(Defcolor.FontLiteGrayColor), select_area_rect, _drawFormat);
            }
            PublicMethods.FrameRoundRectangle(new Rectangle(210, 5, 270, 25), e.Graphics, 5, Defcolor.MainGrayLineColor);

            PublicMethods.FrameRoundRectangle(new Rectangle(92, 5, 145, 25), e.Graphics, 5, Defcolor.MainGrayLineColor);
            PublicMethods.FillRoundRectangle(new Rectangle(92, 5, 145, 25), e.Graphics, 5, Color.White);
        }

        public bool is_set_area()
        {
            loadconfigClass lcc = new loadconfigClass("step_area_" + no);
            if(lcc.readfromConfig() == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void tbx_time_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if (((TextBox)sender).Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
            }

            //if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
            //    e.Handled = true;

            //if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            //{
            //    if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
            //    {
            //        if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
            //            e.Handled = true;
            //    }
            //    if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
            //    {
            //        if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
            //    }
            //}

            //if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            //{
            //    e.Handled = true;
            //}
        }

        private void tbx_time_TextChanged(object sender, EventArgs e)
        {
            time_value = tbx_time.Text.Trim();
        }

        private void AutoStepControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (select_area_rect.Contains(e.Location))
            {
                AutoMouseSetControl AutoMouseSetC = (AutoMouseSetControl)Parent;
                JointSetControl joinC = (JointSetControl)(AutoMouseSetC.Parent);
                joinC.fnAutoAreaSelect(no.ToString());
                Console.WriteLine("选择区域");
            }
        }
    }
}
