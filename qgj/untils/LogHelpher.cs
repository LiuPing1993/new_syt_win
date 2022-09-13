using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace qgj.untils
{
    /*
     * 重新整理的日志信息，存在log文件夹下面
     */
    public class LogHelpher
    {
        /// <summary>
        /// 日志文件所在的路径
        /// </summary>
        public string filePath = System.Environment.CurrentDirectory + "\\log\\" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
        /// <summary>
        /// 日志文件队列
        /// </summary>
        public Queue<string> logQueue;
        /// <summary>
        /// 是否开启日志的上传
        /// </summary>
        public bool Isstart = false;

        public LogHelpher()
        {
            try
            {
                logQueue = new Queue<string>();
                MakeDirectory(System.Environment.CurrentDirectory + "\\log");
            }
            catch (Exception ex)
            {

                Console.WriteLine("日志类初始化异常：" + ex.Message.ToString());
            }

        }

        /// <summary>
        /// 开启日志写入的线程
        /// </summary>
        public void Start()
        {
            try
            {
                ComUntils.ConsoleStr("启动写入日志的线程");
                this.Isstart = true;
                Thread goup = new Thread(Up);
                goup.IsBackground = true;
                goup.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("开启日志写入出现异常，异常原因：" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 停止线程的上传
        /// </summary>
        public void Stop()
        {
            this.Isstart = false;
        }
        /// <summary>
        /// 循环写入
        /// </summary>
        private void Up()
        {
            try
            {
                while (Isstart)
                {
                    if (logQueue != null)
                    {
                        if (logQueue.Count > 0)
                        {
                            string msg = logQueue.Dequeue();
                            Write(msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ComUntils.ConsoleStr("循环写入log异常：" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 日志写入到磁盘文件
        /// </summary>
        /// <param name="value">日子的信息</param>
        private void Write(string value)
        {
            try
            {
                FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
                StreamWriter write = new StreamWriter(file, Encoding.Default);
                write.WriteLine(value);
                write.Close();
                file.Close();
            }
            catch (Exception ex)
            {

                ComUntils.ConsoleStr("写入文本异常：" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 确保日志文件夹的存在
        /// </summary>
        /// <param name="dirPath">文件夹的路径</param>
        private void MakeDirectory(string dirPath)
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }
            catch (Exception ex)
            {
                ComUntils.ConsoleStr("创建日志文件夹出错:" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 日志写入队列
        /// </summary>
        /// <param name="value">写入的信息</param>
        public void WriteLog(string value)
        {
            try
            {
                if (logQueue != null)
                {
                    logQueue.Enqueue(value);
                }
            }
            catch (Exception ex)
            {
                ComUntils.ConsoleStr("文本日志写入队列异常：" + ex.Message.ToString());
            }
        }
    }
}
