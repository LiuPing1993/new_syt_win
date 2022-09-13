using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace qgj
{
    class loadconfigClass
    {
        string sGetType = string.Empty;
        //IniFiles ini;
        public loadconfigClass(string _gettype)
        {
            sGetType = _gettype;
            //ini = new IniFiles(Application.StartupPath + "//" + "qgj.ini");
        }
        public string readfromConfig()
        {
            
            /*
            string _returnValue = "";
            try
            {
                if (string.IsNullOrEmpty(ini.IniReadValue("qgj",sGetType)))
                {
                    return _returnValue;
                }
                else
                {
                    _returnValue = ini.IniReadValue("qgj", sGetType);
                    return _returnValue;
                }
            }
            catch (Exception e)
            {
                return "";
            }*/
            string _returnValue = "";
            try 
            {
                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[sGetType]))
                {
                    return _returnValue;
                }
                else
                {
                    _returnValue = System.Configuration.ConfigurationManager.AppSettings[sGetType];
                    return _returnValue;
                }
            }
            catch (Exception e)
            {
                return "";
            }
            
        }
        public bool writetoConfig(string _newvalue)
        {
            /*
            try
            {
                ini.IniWriteValue("qgj", sGetType, _newvalue);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            */
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove(sGetType);
                config.AppSettings.Settings.Add(sGetType, _newvalue);
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
