using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class koubeiUseItemControl : UserControl
    {
        Color colorBack = Defcolor.MainBackColor;
        public koubeiUseItemControl()
        {
            InitializeComponent();
        }
        public void changeBackColor(Color _c)
        {
            colorBack = _c;
            //Refresh();
        }
        private void otherLabel_Paint(object sender, PaintEventArgs e)
        {
            Label _lb = (Label)sender;
            //lb.Font = new System.Drawing.Font(UserClass.fontName, 8);
            _lb.BackColor = colorBack;
            Rectangle myRectangle = new Rectangle(0, 0, _lb.Width, _lb.Height);
            ControlPaint.DrawBorder(e.Graphics, myRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }
        private void detailLabel_Paint(object sender, PaintEventArgs e)
        {
            Label _lb = (Label)sender;
            //lb.Font = new System.Drawing.Font(UserClass.fontName, 8);
            _lb.BackColor = colorBack;
            Rectangle myRectangle = new Rectangle(0, 0, _lb.Width, _lb.Height);
            ControlPaint.DrawBorder(e.Graphics, myRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }

        private void nameLabel_DoubleClick(object sender, MouseEventArgs e)
        {
            if (couponnameLabel.Text != "")
            {
                try
                {
                    Clipboard.SetDataObject(couponnameLabel.Text);
                    errorinformationForm ef = new errorinformationForm("提示", "已经复制到剪切板");
                    ef.TopMost = true;
                    ef.ShowDialog();
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
        }

        private void detailLabel_MouseEnter(object sender, EventArgs e)
        {
            Label _lb = (Label)sender;
            _lb.ForeColor = Defcolor.FontBlueColor;
        }

        private void detailLabel_MouseLeave(object sender, EventArgs e)
        {
            Label _lb = (Label)sender;
            _lb.ForeColor = SystemColors.MenuHighlight;
        }
    }
}
