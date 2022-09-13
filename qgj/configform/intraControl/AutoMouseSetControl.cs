using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class AutoMouseSetControl : UserControl
    {
        loadconfigClass lcc = new loadconfigClass("autoevent");
        loadconfigClass lcc_step;

        Label lblInfo = new Label();
        Label lblAdd = new Label();

        int step_num = 0;

        List<AutoStepControl> step_list = new List<AutoStepControl>();
        Rectangle add_rect;
        public AutoMouseSetControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            lblInfo.BackColor = Defcolor.MainBackColor;
            lblInfo.Text = "支付后操作";
            lblInfo.Font = new Font(UserClass.fontName, 9);
            lblInfo.ForeColor = Defcolor.FontGrayColor;
            lblInfo.SetBounds(5, 7, 75, 15);
            //Controls.Add(lblInfo);

            lblAdd.BackColor = Defcolor.MainBackColor;
            lblAdd.Text = "新增";
            lblAdd.Font = new Font(UserClass.fontName, 9);
            lblAdd.ForeColor = Defcolor.FontBlueColor;
            lblAdd.SetBounds(85, 7, 50, 15);
            lblAdd.MouseUp += new MouseEventHandler(add_MouseUp);
            //Controls.Add(lblAdd);

            load();
        }

        private void AutoMouseSetControl_Load(object sender, EventArgs e)
        {
            //load();
        }

        public void load()
        {
            //清空当前界面数据
            Controls.Clear();
            Controls.Add(lblInfo);
            Controls.Add(lblAdd);
            lblAdd.Location = new Point(85, 7);
            step_list = new List<AutoStepControl>();
            //获取设置的操作步数
            try
            {
                string auto_event = lcc.readfromConfig();
                if (auto_event == "" || auto_event == "0")
                {
                    step_num = 0;
                }
                else
                {
                    step_num = Convert.ToInt32(auto_event);
                    if (step_num > 5)
                    {
                        step_num = 5;
                    }
                }
            }
            catch 
            {
                step_num = 0;
            }

            int y = 2;
            for (int i = 1; i <= step_num; i++) 
            {
                AutoStepControl temp = new AutoStepControl();
                temp.no = i;
                temp.lbl_del.MouseUp += new MouseEventHandler(delete_MouseUp);

                //读取配置
                lcc_step = new loadconfigClass("step_time_" + i.ToString());
                string temp_config = lcc_step.readfromConfig();
                if (temp_config != "")
                {
                    temp.tbx_time.Text = temp_config;
                }
                else
                {
                    temp.tbx_time.Text = "0";
                }

                temp.Location = new Point(80, y);
                y = temp.Bottom;
                lblAdd.Location = new Point(85, y + 2);
                step_list.Add(temp);
            }
            if (step_list.Count == 5)
            {
                lblAdd.Hide();
            }
            else
            {
                lblAdd.Show();
            }
            foreach (var item in step_list)
            {
                Controls.Add(item);
            }
        }

        private void AutoMouseSetControl_Paint(object sender, PaintEventArgs e) { }

        private void AutoMouseSetControl_MouseUp(object sender, MouseEventArgs e) { }

        private void add_MouseUp(object sender, MouseEventArgs e)
        {
            if (step_num < 5) 
            {
                step_num++;
            }
            save();
            load();
        }

        private void delete_MouseUp(object sender, MouseEventArgs e)
        {
            Label temp = (Label)sender;
            AutoStepControl autoC = (AutoStepControl)(temp.Parent);
            step_list.Remove(autoC);

            if (step_num > 0)
            {
                step_num--;
            }
            save();
            load();
        }

        public void save()
        {
            lcc.writetoConfig(step_num.ToString());
            int index = 0;
            foreach (var item in step_list)
            {
                index++;
                lcc_step = new loadconfigClass("step_time_" + index.ToString());
                lcc_step.writetoConfig(item.time_value);
                //Console.WriteLine("save id" + index);
            }
            for (int i = index + 1; i < 6; i++)
            {
                //Console.WriteLine("delete id" + i);
                lcc_step = new loadconfigClass("step_time_" + i.ToString());
                lcc_step.writetoConfig("");
                lcc_step = new loadconfigClass("step_area_" + i.ToString());
                lcc_step.writetoConfig("");
            }
        }
    }
}
