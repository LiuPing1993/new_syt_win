using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace qgj.untils
{
    /*一些常用的功能方法的集合*/
    public class ComUntils
    {
        /// <summary>
        /// 控制台输出异常
        /// </summary>
        /// <param name="result">异常信息</param>
        public static void ConsoleStr(string result)
        {
            Console.WriteLine(DateTime.Now.ToString() + ":" + result);
        }
        /// <summary>
        /// 得到枚举的具体的描述的信息
        /// </summary>
        /// <param name="enum_value">枚举的类型</param>
        /// <returns>返回枚举的描述</returns>
        public static string getDescription(object enum_value)
        {
            Type t = enum_value.GetType();
            FieldInfo info = t.GetField(Enum.GetName(t, enum_value));
            DescriptionAttribute description = (DescriptionAttribute)Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute));
            return description.Description.ToString();
        }
        /// <summary>
        /// 测量字体的宽度
        /// </summary>
        /// <param name="g">graphics对象</param>
        /// <param name="font">字体</param>
        /// <param name="text">字体的value</param>
        /// <returns>返回字体的宽度</returns>
        public static float CalTextWidth(Graphics g, Font font, string text)
        {
            StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            SizeF s = g.MeasureString(text, font, 0, sf);
            return s.Width;
        }
        /// <summary>
        /// 测试字体的高度
        /// </summary>
        /// <param name="g">graphics对象</param>
        /// <param name="font">字体</param>
        /// <param name="text">字体的value</param>
        /// <returns>返回字体的高度</returns>
        public static float CalTextHeight(Graphics g, Font font, string text)
        {
            StringFormat sf = StringFormat.GenericTypographic;
            sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            SizeF s = g.MeasureString(text, font, 0, sf);
            return s.Height;
        }
    }
}
