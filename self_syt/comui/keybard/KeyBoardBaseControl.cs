using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.comui.keybard
{
    public partial class KeyBoardBaseControl : UserControl
    {

        public List<keybard.KeyBoardBtnControl> keyBtnList;
        public KeyBoardBaseControl(List<keybard.KeyBoardBtnControl> _keyBtnList)
        {
            InitializeComponent();
            this.keyBtnList = _keyBtnList;

        }

        int wid = 120;
        int hei = 60;
        private void InitBtn()
        {
            if (keyBtnList != null)
            {
                if (keyBtnList.Count > 0)
                {
                    foreach (var item in keyBtnList)
                    {
                        item.Size = new Size(wid, hei);
                        this.panel_list.Controls.Add(item);
                    }
                }
            }
        }

        private void KeyBoardBaseControl_Load(object sender, EventArgs e)
        {
            panel_list_SizeChanged(null, null);
            InitBtn();
        }

        private void panel_list_SizeChanged(object sender, EventArgs e)
        {
            wid = ClientRectangle.Width / 3;
            hei = ClientRectangle.Height / 4;
        }
    }
}
