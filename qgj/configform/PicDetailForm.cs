using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class PicDetailForm : Form
    {
        public PicDetailSetControl picsetC = new PicDetailSetControl();
        public PicDetailForm()
        {
            InitializeComponent();
            picsetC.Location = new Point(0, 0);
            Controls.Add(picsetC);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Dispose();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PicDetailForm_Load(object sender, EventArgs e)
        {

        }
    }
}
