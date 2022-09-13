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
    public partial class WaitControl : UserControl
    {
        public string tips = "请稍后";
        Rectangle rect;
        public WaitControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            //页面加载的时候主动改变下页面的高度和宽度
            WaitControl_SizeChanged(null,null);
        }

        private void WaitControl_Load(object sender, EventArgs e)
        {
            rect = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }

        private void WaitControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString(tips, new Font(BaseValue.baseFont, 10), new SolidBrush(Color.Black), rect, BaseValue.drawFormatTitle);
        }

        private void WaitControl_SizeChanged(object sender, EventArgs e)
        {
            rect = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }
    }
}
