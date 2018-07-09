using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SignalRService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 官方 http://www.asp.net/signalr/overview/getting-started/tutorial-getting-started-with-signalr
            // 添加 Nuget
            // Microsoft.AspNet.SignalR.SelfHost
            // Microsoft.Owin.Cors
            // 参考代码 https://www.cnblogs.com/dunitian/p/5226515.html

            // 注册错误事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            string url = "http://localhost:7054";
            using (WebApp.Start(url))
            {
                BootSuccessed();
                EnterCommandMode();
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            ApplicationError(ex?.Message ?? "未知错误");
            if (ex != null)
            {
                //LogService.WriteLog(ex, "系统错误");
            }
        }

        /// <summary>
        /// 程序错误
        /// </summary>
        /// <param name="message">错误消息</param>
        internal static void ApplicationError(string message)
        {
            Console.WriteLine("程序出错" + Environment.NewLine + "错误信息:" + message);
        }

        /// <summary>
        /// 服务启动失败
        /// </summary>
        /// <param name="reason">失败信息</param>
        private static void BootFailed(string reason)
        {
            Console.WriteLine("启动失败" + Environment.NewLine + "失败原因:" + reason);
        }

        /// <summary>
        /// 服务启动成功
        /// </summary>
        private static void BootSuccessed()
        {
            Console.WriteLine("启动成功");
            Console.WriteLine("    启动时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Console.WriteLine("    版本号:   " + GetVersion());
            WriteLine();
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns>结果</returns>
        private static string GetVersion()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var versionAttribute = Attribute.GetCustomAttribute(currentAssembly, typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute;
            if (versionAttribute != null)
            {
                return versionAttribute.Version;
            }
            else
            {
                return "0.0.0.1";
            }
        }

        /// <summary>
        /// 输出行
        /// </summary>
        private static void WriteLine()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        /// <summary>
        /// 显示常用指令
        /// </summary>
        private static void ShowCommands()
        {
            Console.WriteLine("随便输入内容按enter向客户端发送");
        }

        /// <summary>
        /// 常用指令
        /// </summary>
        private static void EnterCommandMode()
        {
            WriteLine();
            ShowCommands();
            while (true)
            {
                var readLine = Console.ReadLine();
                var hub = DntHub.GetCache("DntHub");
                var dnt = hub as DntHub;
                dnt?.ServiceSend("服务器", readLine);
                WriteLine();
            }
        }
    }
}