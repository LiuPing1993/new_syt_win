using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class moneytypeSelectControl : UserControl
    {
        public moneytypeSelectControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }

        private void moneytypeSelectControl_Paint(object sender, PaintEventArgs e)
        {

        }
        private void mouseEnter(object sender, EventArgs e)
        {
            Label _lb = (Label)sender;
            _lb.ForeColor = Color.Gray;
        }
        private void mouseLeave(object sender, EventArgs e)
        {
            Label _lb = (Label)sender;
            _lb.ForeColor = SystemColors.ControlText;
        }
    }
}
