using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.comui.button
{
    public partial class ButtonControl : UserControl
    {
        public string tips = "确定";
        Rectangle rect;
        public Color backColor = Color.Red;
        public Color fontColor = Color.White;

        public Font font = new Font(BaseValue.baseFont, 10);

        public bool isHasBorder = false;
        public ButtonControl()
        {
            InitializeComponent();
            ButtonControl_SizeChanged(null, null);
        }

        private void ButtonControl_Load(object sender, EventArgs e)
        {
            rect = new Rectangle(new Point(0, 0), Size);
        }

        private void ButtonControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (isHasBorder)
            {
                untils.UcomUntils.FrameRoundRectangle(rect, e.Graphics, 4, backColor);
                e.Graphics.DrawString(tips, font, new SolidBrush(backColor), rect, BaseValue.drawFormatTitle);
            }
            else
            {
                untils.UcomUntils.FillRoundRectangle(rect, e.Graphics, 4, backColor);
                e.Graphics.DrawString(tips, font, new SolidBrush(fontColor), rect, BaseValue.drawFormatTitle);
            }

        }

        private void ButtonControl_SizeChanged(object sender, EventArgs e)
        {
            rect = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }

        public void SetTips(string tp)
        {
            tips = tp;
            Refresh();
        }
    }
}
