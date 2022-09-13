using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace self_syt.comui
{
    public partial class MaskForm : Form
    {
        Form p;
        public MaskForm(Form p)
        {
            this.p = p;
            TopMost = true;
            BackColor = Color.Black;
            WindowState = FormWindowState.Maximized;
            Opacity = 0.5;
            FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }

        private void MaskForm_Load(object sender, EventArgs e)
        {
            try
            {
                //2021-09-26 判断P是否为空
                if (p != null)
                {
                    p.ShowDialog();
                }
            }
            catch (Exception ee)
            {
                untils.UcomUntils.ConsoleMsg(ee.Message.ToString());
            }
            Hide();
            Close();
        }
    }
}
