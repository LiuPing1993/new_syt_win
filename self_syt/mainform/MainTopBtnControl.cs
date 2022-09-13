using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.mainform
{
    public partial class MainTopBtnControl : UserControl
    {
        public string name = "";
        Rectangle rect_name;
        public MainTopBtnControl()
        {
            InitializeComponent();
            rect_name = new Rectangle(new Point(0, 0), Size);
        }

        private void MainTopBtnControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString(name, new Font(BaseValue.baseFont, 9), new SolidBrush(BaseColor.color_White), rect_name, BaseValue.drawFormatTitle);
        }
    }
}
