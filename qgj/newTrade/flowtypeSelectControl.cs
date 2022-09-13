using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class flowtypeSelectControl : UserControl
    {
        public flowtypeSelectControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }

        private void flowtypeSelectControl_Load(object sender, EventArgs e)
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
