using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace self_syt.comui
{
    public partial class BaseNetControl : UserControl
    {
        public delegate void addDelegate();

        public addDelegate d;

        public delegate void typeDelegate(int type);

        public typeDelegate d_type;

        public Thread t;

        public bool IsThreadRun = false;

        BaseRequest model;

        public int iRThread = -1;

        public string sRThread = "";
        public BaseNetControl()
        {
            InitializeComponent();
        }

        public void start(BaseRequest model)
        {
            if (IsThreadRun)
            {
                return;
            }
            this.model = model;
            IsThreadRun = true;
            Thread thread = new Thread(run);
            thread.IsBackground = true;
            thread.Start();
            t = thread;
        }

        private void run()
        {
            try
            {
                model.get();
                iRThread = 0;
                sRThread = "";
            }
            catch (Exception e)
            {
                iRThread = 1;
                sRThread = e.Message.ToString();
                untils.UcomUntils.ConsoleMsg("接口调取失败：" + e.Message.ToString());
            }

            try
            {
                IsThreadRun = false;
                BeginInvoke(d);
            }
            catch (Exception ex)
            {
                untils.UcomUntils.ConsoleMsg("接口调取异常：" + ex.Message.ToString());
            }
        }

        public void ShowMessage(string title, string message)
        {
            ErrorForm form = new ErrorForm(title, message);
            form.TopMost = true;
            MaskForm mask = new MaskForm(form);
            mask.ShowDialog();
        }
    }
}
