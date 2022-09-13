using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace qgj
{
    public partial class SetForm : Form
    {
        string sTitle = "设置";
        settingType stype = settingType.basesetting;
        BaseSetControl basesetC = new BaseSetControl();
        JointSetControl jointsetC = new JointSetControl();
        PassWordSetControl passwordsetC = new PassWordSetControl();
        OtherSetControl othersetC = new OtherSetControl();

        Rectangle rectClose = new Rectangle(705, 15, 14, 15);

        public main MainForm;
        public SetForm(main _m)
        {
            MainForm = _m;

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            basesetC.BackColor = Defcolor.MainBackColor;
            jointsetC.BackColor = Defcolor.MainBackColor;
            passwordsetC.BackColor = Defcolor.MainBackColor;

            basesetC.Location = new Point(131, 50);
            Controls.Add(basesetC);

            jointsetC.Location = new Point(131, 50);
            Controls.Add(jointsetC);
            jointsetC.Hide();

            passwordsetC.Location = new Point(131, 50);
            Controls.Add(passwordsetC);
            passwordsetC.Hide();

            othersetC.Location = new Point(131, 50);
            Controls.Add(othersetC);
            othersetC.Hide();
        }

        private void SetForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, e.ClipRectangle.Width - 2, 47));

            Font _myFont = new Font(UserClass.fontName, 10);
            SizeF _sizeF = e.Graphics.MeasureString(sTitle, _myFont);
            string _strLine = String.Format(sTitle);
            PointF _strPoint = new PointF(17, (50 - _sizeF.Height) / 2);

            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _strPoint);
            e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainGrayLineColor), 1), new Point(130, 50), new Point(130, 490));
            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(705, 15), new Point(719, 30));
            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(705, 30), new Point(719, 15));
        }

        private void lblSetTitle_Paint(object sender, PaintEventArgs e)
        {
            Label _lb = (Label)sender;
            if (_lb.Name == "lblBaseSet" && stype == settingType.basesetting)
            {
                lblBaseSet.ForeColor = Defcolor.MainRadColor;
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainRadColor), 2), new Point(129, 20), new Point(128, 30));
            }
            else if (_lb.Name == "lblJointSet" && stype == settingType.jointsetting)
            {
                lblJointSet.ForeColor = Defcolor.MainRadColor;
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainRadColor), 2), new Point(129, 20), new Point(128, 30));
            }
            else if (_lb.Name == "lblPassWordSet" && stype == settingType.passwordsetting)
            {
                lblPassWordSet.ForeColor = Defcolor.MainRadColor;
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainRadColor), 2), new Point(129, 20), new Point(128, 30));
            }
            else if (_lb.Name == "lblOtherSet" && stype == settingType.otherseting)
            {
                lblOtherSet.ForeColor = Defcolor.MainRadColor;
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.MainRadColor), 2), new Point(129, 20), new Point(128, 30));
            }
        }

        private void lblSetTitle_MouseUp(object sender, MouseEventArgs e)
        {
            Label _lb = (Label)sender;
            lblBaseSet.ForeColor = Color.Black;
            lblJointSet.ForeColor = Color.Black;
            lblPassWordSet.ForeColor = Color.Black;
            lblOtherSet.ForeColor = Color.Black;
            if (_lb.Name == "lblBaseSet")
            {
                basesetC.Show();
                basesetC.reloadSet();
                jointsetC.Hide();
                passwordsetC.Hide();
                othersetC.Hide();
                stype = settingType.basesetting;
            }
            else if (_lb.Name == "lblJointSet")
            {
                basesetC.Hide();
                jointsetC.Show();
                jointsetC.reloadSet();
                passwordsetC.Hide();
                othersetC.Hide();
                stype = settingType.jointsetting;
            }
            else if (_lb.Name == "lblPassWordSet")
            {
                basesetC.Hide();
                jointsetC.Hide();
                passwordsetC.Show();
                othersetC.Hide();
                stype = settingType.passwordsetting;
            }
            else if (_lb.Name == "lblOtherSet")
            {
                basesetC.Hide();
                jointsetC.Hide();
                passwordsetC.Hide();
                othersetC.Show();
                stype = settingType.otherseting;
            }
            lblBaseSet.Refresh();
            lblJointSet.Refresh();
            lblPassWordSet.Refresh();
            lblOtherSet.Refresh();
        }

        private void SetForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (rectClose.Contains(e.Location))
            {
                Dispose();
            }
        }

        private void SetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.gatherC.reloadPayWay(true);
            MainForm.gatherC.Refresh();
            MainForm.fnShowSuspendedForm();
            jointsetC.fnSaveHotKey();
            jointsetC.autoMouseSetC.save();
            UserClass.IsMain = true;
            PublicMethods.FlushMemory();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Dispose();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
