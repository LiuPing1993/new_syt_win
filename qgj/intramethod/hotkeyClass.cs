using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    class hotkeyClass
    {
        public int type = 1;//快捷键类型 1调起/隐藏程序 2打印日汇总
        public hotkeyClass() { }
        public bool fnSetHotKeyValue(string _keyvalue)
        {
            string config_name = "hotkey";
            if(this.type == 2)
            {
                config_name = "print_hotkey";
            }
            loadconfigClass lcc = new loadconfigClass(config_name);
            if (_keyvalue == "单击设置快捷键")
            {
                return true;
            }
            return lcc.writetoConfig(_keyvalue);
        }
        public void fnSetHotKey(string _keyvalue,int _hotkeyid)
        {
            try
            {
                IntPtr _handle = UserClass.mainHandle;
                //卸载当前已注册的指定id的快捷键hock
                NativeMethods.UnregisterHotKey(_handle, _hotkeyid);

                if (_keyvalue.IndexOf("Ctrl") != -1)
                {
                    _keyvalue = _keyvalue.Replace("Ctrl + ", "");
                    Keys getType = (Keys)Enum.Parse(typeof(Keys), _keyvalue);
                    NativeMethods.RegisterHotKey(_handle, _hotkeyid, NativeMethods.常用按键.Ctrl, getType);
                }
                else
                {
                    Keys getType = (Keys)Enum.Parse(typeof(Keys), _keyvalue);
                    NativeMethods.RegisterHotKey(_handle, _hotkeyid, NativeMethods.常用按键.None, getType);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
    }
}
