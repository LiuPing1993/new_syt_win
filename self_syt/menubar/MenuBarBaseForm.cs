using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace self_syt.menubar
{
    public partial class MenuBarBaseForm : Form
    {
        public menubar.MenuBarControl menC;
        public MenuBarBaseForm()
        {
            InitializeComponent();
            menC = new MenuBarControl();
            this.Controls.Add(menC);
        }
    }
}
