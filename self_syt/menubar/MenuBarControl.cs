using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace self_syt.menubar
{
    /*
     * 主界面的菜单按键
     * 
     */
    public partial class MenuBarControl : UserControl
    {
        public MenuBarControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            InitBtn();
        }
        public void InitBtn()
        {
            int index = 0;
            foreach (BaseEnum.MenuBarType item in Enum.GetValues(typeof(BaseEnum.MenuBarType)))
            {
                menubar.MenuBarBtnControl btn = new MenuBarBtnControl();
                btn.menuType = item;
                btn.MouseUp += btn_MouseUp;
                btn.tips = untils.UcomUntils.getDescription(item);
                if (btn.tips == "无")
                {
                    continue;
                }
                btn.Location = new Point(0, index * btn.Height);
                Controls.Add(btn);
                index++;
            }
        }

        void btn_MouseUp(object sender, MouseEventArgs e)
        {
            menubar.MenuBarBtnControl btn = (menubar.MenuBarBtnControl)sender;
            ShowMenuForm(btn.menuType);
        }

        public void ShowMenuForm(BaseEnum.MenuBarType type)
        {
            switch (type)
            {
                case BaseEnum.MenuBarType.pay:
                    break;
                case BaseEnum.MenuBarType.order:
                    order.OrderBaseForm form = new order.OrderBaseForm();
                    form.Show();
                    BaseModel.menuBarFormValue.Hide();
                    break;
                case BaseEnum.MenuBarType.setting:
                    setting.SetBaseForm Setform = new setting.SetBaseForm();
                    Setform.Show();
                    BaseModel.menuBarFormValue.Hide();
                    break;
                case BaseEnum.MenuBarType.exit:
                    //设置是从主界面退出
                    untils.UoperaConfig cf = new untils.UoperaConfig(BaseConfigValue.IsExitFromMain);
                    cf.WriteConfig("1");
                    BaseModel.menuBarFormValue.Hide();
                    comui.ErrorForm error = new comui.ErrorForm("提示", "确定将返回登录界面", true);
                    error.TopMost = true;
                    comui.MaskForm mask = new comui.MaskForm(error);
                    mask.ShowDialog();
                    if (error.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Application.Restart();
                        Application.ExitThread();
                        Thread thtmp = new Thread(new ParameterizedThreadStart(run));
                        object appName = Application.ExecutablePath;
                        Thread.Sleep(1);
                        thtmp.Start(appName);
                    }
                    break;
                default:
                    break;
            }
        }
        //重启当前的进程
        private void run(Object obj)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }
    }
}
