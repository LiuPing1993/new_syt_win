using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace self_syt.comui.button
{
    public partial class PicButtonControl : UserControl
    {
        public string tips = "刷新";
        public PictureBox Pic_logo;

        Rectangle rect_right;
        Rectangle rect;
        public PicButtonControl()
        {
            InitializeComponent();
            PicButtonControl_SizeChanged(null, null);
            rect_right = new Rectangle(ClientRectangle.Width / 3, 0, ClientRectangle.Width / 3 * 2, ClientRectangle.Height);
            rect = new Rectangle(0,0,ClientRectangle.Width,ClientRectangle.Height);
        }

        private void PicButtonControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            untils.UcomUntils.FrameRoundRectangle(rect,e.Graphics,4,BaseColor.color_Red);
            e.Graphics.DrawString(tips, new Font(BaseValue.baseFont, 10), new SolidBrush(BaseColor.color_Red), rect_right, BaseValue.drawFormatTitle);
        }

        private void PicButtonControl_Load(object sender, EventArgs e)
        {
            Pic_logo = new PictureBox();
            Pic_logo.Image = Properties.Resources.refresh;
            Pic_logo.SizeMode = PictureBoxSizeMode.StretchImage;
            Pic_logo.Size = new Size(12, 12);
            Pic_logo.Location = new Point(10, (ClientRectangle.Height - Pic_logo.Height) / 2);
            Controls.Add(Pic_logo);
        }

        private void PicButtonControl_SizeChanged(object sender, EventArgs e)
        {
            rect_right = new Rectangle(ClientRectangle.Width / 3, 0, ClientRectangle.Width / 3*2, ClientRectangle.Height);
            rect = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }
    }
}
