using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class paytypeSelectControl : UserControl
    {
        bool IsStore = false;
        public paytypeSelectControl(bool _isstore = false)
        {
            IsStore = _isstore;
            BackColor = Defcolor.MainBackColor;
            InitializeComponent();
            if (IsStore)
            {
                label5.Location = label6.Location;
                label6.Hide();
                this.Size = new Size(71, 176);
            }
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
        private void ordertypeSelectControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                    Defcolor.MainBackColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainBackColor, 0, ButtonBorderStyle.Solid,
                    Defcolor.MainBackColor, 1, ButtonBorderStyle.Solid,
                    Defcolor.MainBackColor, 1, ButtonBorderStyle.Solid
                );
        }
    }
}
