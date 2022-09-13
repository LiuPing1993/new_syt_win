using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class memberConfirmControl : UserControl
    {
        public memberConfirmControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainRadColor;
        }

        private void memberConfirmControl_Paint(object sender, PaintEventArgs e)
        {
            Font _Font = new Font(UserClass.fontName, 12);
            SizeF _sizeF = e.Graphics.MeasureString("确定", _Font);
            string _strLine = String.Format("确定");
            PointF _strPoint = new PointF((e.ClipRectangle.Width - _sizeF.Width) / 2, (e.ClipRectangle.Height - _sizeF.Height) / 2);
            e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.White), _strPoint);
        }

        private void memberConfirmControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (Parent.GetType() == typeof(gatherControl))
            {
                ((gatherControl)Parent).discountselectC.fnGetmemberInfo();
            }
            else if (Parent.GetType() == typeof(storeControl))
            {
                ((storeControl)Parent).memberselectC.fnGetmemberInfo();
            }
        }
    }
}
