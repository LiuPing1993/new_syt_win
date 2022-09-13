using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
namespace self_syt.login
{
    public partial class SetWebProxyForm : SkinMain
    {
        SetWebProxyControl setWebC;
        public SetWebProxyForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            setWebC = new SetWebProxyControl();
            Controls.Add(setWebC);
        }
    }
}
