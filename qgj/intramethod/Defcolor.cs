using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace qgj
{
    public class Defcolor
    {
        private static Color mainradcolor = Color.FromArgb(212, 60, 51);
        private static Color maingraylinecolor = Color.FromArgb(199, 199, 199);
        private static Color mainbackcolor = Color.FromArgb(242, 242, 242);
        private static Color backcolor = Color.FromArgb(250, 250, 251);
        private static Color bottombackcolor = Color.FromArgb(229, 229, 229);

        //支付方式按钮背景色
        private static Color paywaybackcolor = Color.FromArgb(250, 250, 251);
        private static Color paywaybackcolorMouseIn = Color.FromArgb(220, 220, 220);
        private static Color paywaybackcolorMouseDown = Color.FromArgb(150, 150, 150);
        //数字键盘背景色
        private static Color numboardbackcolor = Color.FromArgb(250, 250, 251);
        private static Color numboardbackcolorMouseIn = Color.FromArgb(240, 240, 240);
        private static Color numboardbackcolorMouseDown = Color.FromArgb(230, 230, 230);
        //顶部功能键背景色
        private static Color topbuttonradcolor = Color.FromArgb(200, 39, 40);
        private static Color topbuttonradcolorMouseIn = Color.FromArgb(240, 39, 40);
        private static Color topbuttonradcolorMouseDown = Color.FromArgb(180, 39, 40);

        public static Color orderlisttopbackcolor = Color.FromArgb(231, 227, 218);

        private static Color fontbluecolor = Color.FromArgb(71, 76, 253);
        private static Color fontradcolor = Color.FromArgb(200, 39, 40);
        private static Color fontgraycolor = Color.FromArgb(60, 60, 60);
        private static Color fontlitegraycolor = Color.FromArgb(83, 83, 83);
        private static Color fonttopselect = Color.White;
        private static Color fonttopenter = Color.FromArgb(240, 140, 140);
        private static Color fonttopnotselect = Color.FromArgb(253, 162, 164);

        private static Color discountcolor = Color.FromArgb(209, 208, 208);
        private static Color buttonbackcolor = Color.FromArgb(194, 194, 194);
        private static Color buttonlinecolor = Color.FromArgb(172, 171, 171);
        private static Color buttonlineradcolor = Color.FromArgb(208, 82, 45);
        /// <summary>
        /// 主界面红色（200,39,40）
        /// </summary>
        public static Color MainRadColor
        {
            get
            {
                return mainradcolor;
            }
        }
        /// <summary>
        /// 主界面灰色边线（199,199,199）
        /// </summary>
        public static Color MainGrayLineColor
        {
            get
            {
                return maingraylinecolor;
            }
        }
        /// <summary>
        /// 主界面背景颜色(242,242,242)
        /// </summary>
        public static Color MainBackColor
        {
            get
            {
                return mainbackcolor;
            }
        }
        /// <summary>
        /// 数字键、支付方式键背景颜色（250,250,251）
        /// </summary>
        public static Color BackColor
        {
            get
            {
                return backcolor;
            }
        }
        /// <summary>
        /// 主界面下底栏背景色（229,229,229）
        /// </summary>
        public static Color BottomBackColor
        {
            get
            {
                return bottombackcolor;
            }
        }
        /// <summary>
        /// 支付方式按钮通常背景色（250,250,251）
        /// </summary>
        public static Color PayWayBackColor
        {
            get
            {
                return paywaybackcolor;
            }
        }
        /// <summary>
        /// 支付方式按钮鼠标进入背景色（220,220,220）
        /// </summary>
        public static Color PayWayBackColorMouseIn
        {
            get
            {
                return paywaybackcolorMouseIn;
            }
        }
        /// <summary>
        /// 支付方式按钮鼠标按下背景色（150,150,150）
        /// </summary>
        public static Color PayWayBackColorMouseDown
        {
            get
            {
                return paywaybackcolorMouseDown;
            }
        }
        /// <summary>
        /// 数字键背景色（250,250,251）
        /// </summary>
        public static Color NumboardBackColor
        {
            get
            {
                return numboardbackcolor;
            }
        }
        /// <summary>
        /// 数字键鼠标进入背景色（240,240,240）
        /// </summary>
        public static Color NumboardBackColorMouseIn
        {
            get
            {
                return numboardbackcolorMouseIn;
            }
        }
        /// <summary>
        /// 数字键鼠标按下背景色（230,230,230）
        /// </summary>
        public static Color NumboardBackColorMouseDown
        {
            get
            {
                return numboardbackcolorMouseDown;
            }
        }

        public static Color TopButtonRadColor
        {
            get
            {
                return topbuttonradcolor;
            }
        }
        public static Color TopButtonRadColorMouseIn
        {
            get
            {
                return topbuttonradcolorMouseIn;
            }
        }
        public static Color TopButtonRadColorMouseDown
        {
            get
            {
                return topbuttonradcolorMouseDown;
            }
        }

        /// <summary>
        /// 蓝色字体1（71,76,253）
        /// </summary>
        public static Color FontBlueColor
        {
            get
            {
                return fontbluecolor;
            }
        }
        /// <summary>
        /// 红色字体1（200,39,40）
        /// </summary>
        public static Color FontRadColor
        {
            get
            {
                return fontradcolor;
            }
        }
        /// <summary>
        /// 灰色字体1（60,60，60）
        /// </summary>
        public static Color FontGrayColor
        {
            get
            {
                return fontgraycolor;
            }
        }
        /// <summary>
        /// 灰色字体2（83,83,83）
        /// </summary>
        public static Color FontLiteGrayColor
        {
            get
            {
                return fontlitegraycolor;
            }
        }
        /// <summary>
        /// 主界面标签选择后颜色（白色）
        /// </summary>
        public static Color FontTopSelect
        {
            get
            {
                return fonttopselect;
            }
        }
        public static Color FontTopEnter
        {
            get
            {
                return fonttopenter;
            }
        }
        /// <summary>
        /// 主界面标签未选择颜色(253, 162, 164)
        /// </summary>
        public static Color FontTopNotSselect
        {
            get
            {
                return fonttopnotselect;
            }
        }
        /// <summary>
        /// 主界面选择会员背景色（209,208,208）
        /// </summary>
        public static Color DiscountColor
        {
            get
            {
                return discountcolor;
            }
        }
        /// <summary>
        /// 确认取消按钮背景色（194,194,194）
        /// </summary>
        public static Color ButtonBackColor
        {
            get
            {
                return buttonbackcolor;
            }
        }
        /// <summary>
        /// 确认取消按钮边线色（172,171,171）
        /// </summary>
        public static Color ButtonLineColor
        {
            get
            {
                return buttonlinecolor;
            }
        }
        /// <summary>
        /// 打印按钮边线色（208,82,45）
        /// </summary>
        public static Color ButtonLineRadColor
        {
            get
            {
                return buttonlineradcolor;
            }
        }
    }
}
