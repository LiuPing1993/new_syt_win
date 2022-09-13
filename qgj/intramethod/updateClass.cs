using System;
using System.Collections.Generic;
using System.Text;
using Iyond.Utility;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace qgj
{
    class updateClass
    {
        public static void autoupdate()
        {
            string _url = "";
            loadconfigClass _lcc = new loadconfigClass("proxy");
            _url = _lcc.readfromConfig();
            AutoUpdater _au = new AutoUpdater(_url);
            try
            {
                _au.Update();
            }
            catch (WebException exp)
            {
                MessageBox.Show(string.Format("无法找到指定资源\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (XmlException exp)
            {
                MessageBox.Show(string.Format("下载的升级文件列表错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (NotSupportedException exp)
            {
                MessageBox.Show(string.Format("升级地址配置错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException exp)
            {
                MessageBox.Show(string.Format("下载的升级文件错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(string.Format("升级过程中发送错误\n\n{0}", exp.Message), "自动升级", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
