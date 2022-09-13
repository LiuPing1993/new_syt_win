using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace qgj
{
    public partial class testclassForm : Form
    {
        Thread t2;
        bool IsThreadRun = false;
        public delegate void addDelegate();
        public addDelegate d;

        public string txt = "";
        public string url = "";
        public int time = 5000;

        public testclassForm()
        {
            d = new addDelegate(fnUpdate);
            InitializeComponent();
        }

        private void testclassForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            url = this.textBox1.Text;
            try
            {
                if (this.textBox2.Text == "")
                {
                    time = 5000;
                }
                else
                {
                    time = Convert.ToInt32(this.textBox2.Text.ToString());
                }
            }
            catch{time=5000;}
            
            fnQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsThreadRun = false;
            t2.Abort();
        }

        private void fnQuery()
        {
            if (!IsThreadRun)
            {
                IsThreadRun = true;
                Thread thread = new Thread(fnQueryAction);
                thread.IsBackground = true;
                thread.Start();
                t2 = thread;
            }
        }
        private void fnQueryAction()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    HttpClass _httpC = new HttpClass();
                    txt = _httpC.HttpGet(url);
                    Invoke(d);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            IsThreadRun = false;
            t2.Abort();
        }

        private void fnUpdate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine + DateTime.Now.ToString() + "  :  " + txt);
            this.richTextBox1.Text += sb.ToString();
        }

    }
}
