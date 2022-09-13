using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace self_syt.comui.checkbox
{
    public partial class CheckBoxControl : UserControl
    {
        public PictureBox pic;
        public string tips = "记住账号";

        Rectangle rect_tips;
        Rectangle rect_select;

        public bool isSelect = false;

        public CheckBoxControl()
        {
            InitializeComponent();
            panel_left.BackColor = BaseColor.bottom_Color;
            rect_tips = new Rectangle(0, 0, this.panel_right.ClientRectangle.Width, this.panel_right.ClientRectangle.Height);
            pic = new PictureBox();
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new System.Drawing.Size(14, 14);
            pic.Location = new Point((panel_left.ClientRectangle.Width - pic.Width) / 2, (panel_left.ClientRectangle.Height - pic.Height) / 2);
            pic.Image = Properties.Resources.check;
            panel_left.Controls.Add(pic);

            rect_select = new Rectangle(this.pic.Location.X, this.pic.Location.Y, this.pic.Width, this.pic.Height);
        }

        private void panel_right_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString(tips, new Font(BaseValue.baseFont, 9), new SolidBrush(BaseColor.color_Black), rect_tips, BaseValue.drawFormatLeftMid);
        }

        private void panel_left_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isSelect)
            {
                untils.UcomUntils.FillRoundRectangle(rect_select, e.Graphics, 2, BaseColor.color_Red);
                pic.BackColor = BaseColor.color_Red;
                pic.Show();
            }
            else
            {
                untils.UcomUntils.FillRoundRectangle(rect_select, e.Graphics, 2, BaseColor.color_White);
                pic.Hide();
            }
        }

        public void SetSelected(bool re)
        {
            isSelect = re;
            panel_left.Refresh();
        }

        private void panel_right_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void panel_left_MouseUp(object sender, MouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }
}
