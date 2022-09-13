using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.mainform
{
    public partial class MainBaseControl : UserControl
    {
        //串口打印的线程
        public print.com.PrintSpComThread comprintThread = new print.com.PrintSpComThread();

        public mainform.MainTopControl mainTopC = new MainTopControl();

        Button printbtn;

     

        public MainBaseControl()
        {
            //双缓冲解决控件的闪烁问题
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            Dock = DockStyle.Fill;
            this.panel_top.Controls.Add(mainTopC);
            panel_mid.Height = ClientRectangle.Height - this.panel_top.ClientRectangle.Height - this.panel_bottom.ClientRectangle.Height;
            comui.keybard.NumPadKeyControl numPad = new comui.keybard.NumPadKeyControl();
            numPad.Location = new Point(50, 50);
            this.panel_mid.Controls.Add(numPad);
            printbtn = new Button();
            printbtn.MouseUp += new MouseEventHandler(Print_MouseUp);
            printbtn.Text = "打印测试";
            this.panel_bottom.Controls.Add(printbtn);

            //开启串口打印的线程
            comprintThread.Start();

            untils.UoperaConfig operaC = new untils.UoperaConfig("pc_sn");
            string value = operaC.ReadConfig();

        }

        void scanHook_ScanerEvent(untils.UscanHook.ScanerCodes codes)
        {
            untils.UcomUntils.ConsoleMsg("得到扫描的二维码的值为：" + codes.Result.ToString());
        }

        public void Print_MouseUp(object sender, MouseEventArgs e)
        {
            print.PrintTest printtest = new print.PrintTest("XP-80");
            model.print.PrintTitleModel printTitleModel = new model.print.PrintTitleModel();
            printTitleModel.title = "商家联";
            printTitleModel.no = "2";
            printTitleModel.type = "美团外卖";
            printTitleModel.storeName = "这是亲享的测试门店-亲享奶茶";
            printtest.printTitleModel = printTitleModel;
            printtest.printNow();
        }
        public void KeyBtn_MouseUp(object sender, MouseEventArgs e)
        {
            comui.keybard.KeyBoardBtnControl btn = (comui.keybard.KeyBoardBtnControl)sender;
            MessageBox.Show(btn.name);
        }
        private void panel_mid_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            Console.WriteLine("得到的键盘按钮为：" + keyData.ToString());
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            suspend.SuspendForm form = new suspend.SuspendForm();
            form.Show();
        }
    }
}
