using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.comui.keybard
{
    public partial class KeyBoardBtnControl : UserControl
    {
        public string name = "";
        Rectangle rect_name;
        Rectangle rect_border;

        public BaseEnum.RadiusLocation radLoc = BaseEnum.RadiusLocation.none;

        public KeyBoardBtnControl()
        {
            InitializeComponent();
            BackColor = BaseColor.color_White;
            //页面加载时候重新设置控件的大小
            KeyBoardBtnControl_SizeChanged(null, null);
        }

        private void KeyBoardBtnControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (radLoc == BaseEnum.RadiusLocation.left_top)
            {
                //画白色的背景
                BackColor = Color.FromArgb(224, 224, 224);
                untils.UcomUntils.FillRoundRectangle_four(rect_border, e.Graphics, 10, 0, 0, 0, Color.FromArgb(255, 255, 255));
            }
            else if (radLoc == BaseEnum.RadiusLocation.right_top)
            {
                BackColor = Color.FromArgb(224, 224, 224);
                untils.UcomUntils.FillRoundRectangle_four(rect_border, e.Graphics, 0, 10, 0, 0, Color.FromArgb(255, 255, 255));
            }
            else if (radLoc == BaseEnum.RadiusLocation.left_bottom)
            {
                BackColor = Color.FromArgb(224, 224, 224);
                untils.UcomUntils.FillRoundRectangle_four(rect_border, e.Graphics, 0, 0, 10, 0, Color.FromArgb(255, 255, 255));
            }
            else if (radLoc == BaseEnum.RadiusLocation.right_bottom)
            {
                BackColor = Color.FromArgb(224, 224, 224);
                untils.UcomUntils.FillRoundRectangle_four(rect_border, e.Graphics, 0, 0, 0, 10, Color.FromArgb(255, 255, 255));
            }
            //边框
            ControlPaint.DrawBorder(e.Graphics, rect_border,
                Color.FromArgb(214, 214, 214), 0, ButtonBorderStyle.Solid,
                Color.FromArgb(214, 214, 214), 0, ButtonBorderStyle.Solid,
                Color.FromArgb(214, 214, 214), 1, ButtonBorderStyle.Solid,
                Color.FromArgb(214, 214, 214), 1, ButtonBorderStyle.Solid);
            e.Graphics.DrawString(name, new Font(BaseValue.baseFont, 12), new SolidBrush(Color.Red), rect_name, BaseValue.drawFormatTitle);
        }

        private void KeyBoardBtnControl_SizeChanged(object sender, EventArgs e)
        {
            rect_name = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            rect_border = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }
    }
}
