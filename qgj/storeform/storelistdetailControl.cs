using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class storelistdetailControl : UserControl
    {
        Color colorBack = Defcolor.MainBackColor;
        public storelistdetailControl()
        {
            InitializeComponent();
        }
        public void changeBackColor(Color _c)
        {
            colorBack = _c;
            Refresh();
        }
        private void numLabel_Paint(object sender, PaintEventArgs e)
        {
            Label _lb = (Label)sender;
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
            _lb.BackColor = colorBack;
            Rectangle myRectangle = new Rectangle(0, 0, _lb.Width, _lb.Height);
            ControlPaint.DrawBorder(e.Graphics, myRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }
        private void operation_MouseEnter(object sender, EventArgs e)
        {
            Label _lb = (Label)sender;
            _lb.ForeColor = Defcolor.FontBlueColor;
        }
        private void operation_MouseLeave(object sender, EventArgs e)
        {
            Label _lb = (Label)sender;
            _lb.ForeColor = SystemColors.MenuHighlight;
        }

        private void membernumLabel_DoubleClick(object sender, EventArgs e)
        {
            if (membernumLabel.Text != "")
            {
                try
                {
                    Clipboard.SetDataObject(membernumLabel.Text);
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
    }
}
