using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class cashierForm : Form
    {
        PayWay paywayNow = PayWay.barcodePay;
        public scancodeControl scancodeC;
        public barcodeControl barcodeC;
        public cardrecordControl cardrecordC;
        public cashrecordControl cashrecordC;
        public storeuseingControl storeuseingC;

        public newpay.newBarPayControl new_barC;

        public bool IsStore = false;
        
        public payresultControl payresultC;

        public string code = "";
        public cashierForm(PayWay _insert,bool _isstore = false)
        {
            //NativeMethodsForm.m_aeroEnabled = false;
            paywayNow = _insert;
            IsStore = _isstore;
            InitializeComponent();
        }
        private void cashierForm_Load(object sender, EventArgs e)
        {

            UserClass.IsMain = false;

            payresultC = new payresultControl();
            payresultC.Location = new Point(0, 0);
            payresultC.Hide();
            Controls.Add(payresultC);
            switch (paywayNow)
            {
                    //todo 扫码支付
                //case PayWay.barcodePay: barcodeC = new barcodeControl(code); barcodeC.Location = new Point(0, 0); this.Controls.Add(barcodeC); break;
                case PayWay.barcodePay: new_barC = new newpay.newBarPayControl(this, code); new_barC.Location = new Point(0, 0); this.Controls.Add(new_barC); break;
                case PayWay.qrcodePay: scancodeC = new scancodeControl(); scancodeC.Location = new Point(0, 0); this.Controls.Add(scancodeC); break;
                case PayWay.cardPay: cardrecordC = new cardrecordControl(); cardrecordC.Location = new Point(0, 0); this.Controls.Add(cardrecordC); break;
                case PayWay.cashPay: cashrecordC = new cashrecordControl(); cashrecordC.Location = new Point(0, 0); this.Controls.Add(cashrecordC); break;
                case PayWay.storePay: storeuseingC = new storeuseingControl(); storeuseingC.Location = new Point(0, 0); this.Controls.Add(storeuseingC); break;
                default: break;
            }
        }
        public void fnShowResult(object _sender)
        {
            payresultC.Show();
            payresultC.start_countdown();
            ((UserControl)_sender).Hide();
        }
        public void fnPrint(bool _ismerchant)
        {
            if (IsStore)
            {
                return;
            }
            switch (paywayNow)
            {
                    //todo 扫码支付打印
                //case PayWay.barcodePay: barcodeC.fnPrint(_ismerchant); break;
                case PayWay.barcodePay: new_barC.fnPrint(_ismerchant); break;
                case PayWay.qrcodePay: scancodeC.print(_ismerchant); break;
                case PayWay.cardPay: cardrecordC.fnPrint(_ismerchant); break;
                case PayWay.cashPay: cashrecordC.print(_ismerchant); break;
                case PayWay.storePay: storeuseingC.fnPrint(_ismerchant); break;
                default: break;
            }
        }
        private void cashierForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //System.Threading.Thread.CurrentThread.Abort();
            UserClass.IsMain = true;
        }


        //private void scancodeC_MouseDown(object sender, MouseEventArgs e)
        //{
        //    NativeMethods.ReleaseCapture();
        //    NativeMethods.SendMessage((int)this.Handle, NativeMethods.WM_NCLBUTTONDOWN, NativeMethods.HTCAPTION, 0);
        //}

    }
}
