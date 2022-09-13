using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace self_syt.menubar
{
    public partial class MenuBarBtnControl : UserControl
    {

        public string tips = "";
        Rectangle rect;
        public BaseEnum.MenuBarType menuType = BaseEnum.MenuBarType.none;

        public MenuBarBtnControl()
        {
            InitializeComponent();
            MenuBarBtnControl_SizeChanged(null, null);
        }

        private void MenuBarBtnControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString(tips, new Font(BaseValue.baseFont, 9), new SolidBrush(Color.Black), rect, BaseValue.drawFormatTitle);
        }

        private void MenuBarBtnControl_Load(object sender, EventArgs e)
        {
            rect = new Rectangle(new Point(0, 0), Size);
        }

        private void MenuBarBtnControl_SizeChanged(object sender, EventArgs e)
        {
            rect = new Rectangle(new Point(0, 0), Size);
        }
    }
}
