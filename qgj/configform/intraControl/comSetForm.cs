using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace qgj.configform.intraControl
{
    public partial class comSetForm : Form
    {
        loadconfigClass lcc;
        confirmcancelControl confirm = new confirmcancelControl("确定");
        List<string> com_list = new List<string>();

        public string baud_rate = "";
        public string parity = "";
        public string stop_bite = "";
        public string port_name = ""; 

        public comSetForm()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }

        private void comSetForm_Load(object sender, EventArgs e)
        {
            confirm.Location = new Point(150, 180);
            confirm.MouseUp += new MouseEventHandler(confirm_Click);
            Controls.Add(confirm);

            com_list = PublicMethods.GetComlist(false);
            if (com_list.Count > 0)
            {
                Console.WriteLine("系统获取");
                name_cbx.Items.Clear();
                foreach (var item in com_list)
                {
                    name_cbx.Items.Add(item);
                }
            }
            loadConfig();
        }

        private void loadConfig()
        {
            lcc = new loadconfigClass("com_name");
            port_name = lcc.readfromConfig();

            lcc = new loadconfigClass("com_baud_rate");
            baud_rate = lcc.readfromConfig();

            lcc = new loadconfigClass("com_parity");
            parity = lcc.readfromConfig();
            if(parity == "")
            {
                parity = "3";
            }

            lcc = new loadconfigClass("com_stop_bite");
            stop_bite = lcc.readfromConfig();
            if(stop_bite == "")
            {
                stop_bite = "1";
            }

            try
            {
                name_cbx.SelectedText = port_name;
                rate_cbx.SelectedText = baud_rate;
                parity_cbx.SelectedIndex = Convert.ToInt32(parity) - 1;
                stop_cbx.SelectedIndex = Convert.ToInt32(stop_bite) - 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString() + e.StackTrace.ToString());
            }
        }

        private void confirm_Click(object sender, MouseEventArgs e)
        {
            lcc = new loadconfigClass("com_name");
            lcc.writetoConfig(port_name);

            lcc = new loadconfigClass("com_baud_rate");
            lcc.writetoConfig(baud_rate);

            lcc = new loadconfigClass("com_parity");
            lcc.writetoConfig(parity);

            lcc = new loadconfigClass("com_stop_bite");
            lcc.writetoConfig(stop_bite);

            Close();
        }

        private void name_cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = name_cbx.SelectedItem.ToString();
                port_name = name;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message.ToString() + ee.StackTrace.ToString());
                port_name = "COM1";
            }
        }

        private void rate_cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string rate = rate_cbx.SelectedItem.ToString();
                baud_rate = rate;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message.ToString() + ee.StackTrace.ToString());
                baud_rate = "9600";
            }
        }

        private void parity_cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string _parity = (parity_cbx.SelectedIndex + 1).ToString();
                parity = _parity;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message.ToString() + ee.StackTrace.ToString());
                parity = "1";
            }
        }

        private void stop_cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string stop = (stop_cbx.SelectedIndex + 1).ToString();
                stop_bite = stop;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message.ToString() + ee.StackTrace.ToString());
                stop_bite = "1";
            }
        }

        private void comSetForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }

    }
}
