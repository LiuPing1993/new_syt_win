using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace self_syt
{
    public class BaseColor
    {
        /// <summary>
        /// 程序的主背景色
        /// </summary>
        public static Color mainColor = Color.FromArgb(212, 60, 51);
        /// <summary>
        /// 纯白色
        /// </summary>
        public static Color color_White = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// 纯黑色
        /// </summary>
        public static Color color_Black = Color.FromArgb(51,51,51);
        /// <summary>
        /// 红色
        /// </summary>
        public static Color color_Red = Color.FromArgb(214,56,56);
        /// <summary,>
        /// 线条的颜色
        /// </summary>
        public static Color line = Color.FromArgb(216, 216, 216);
        /// <summary>
        /// 灰色的线条
        /// </summary>
        public static Color line_gray = Color.FromArgb(137,137,137);
        /// <summary>
        /// 深灰
        /// </summary>
        public static Color deep_Gray = Color.FromArgb(102,102,102);
        /// <summary>
        /// 浅灰
        /// </summary>
        public static Color little_Gray = Color.FromArgb(241, 241, 241);
        /// <summary>
        /// 底部的颜色
        /// </summary>
        public static Color bottom_Color = Color.FromArgb(230,230,230);
    }
}
