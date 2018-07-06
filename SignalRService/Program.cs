using System;
using System.Collections.Generic;
using System.Linq;
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

            string url = "http://localhost:7054";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on{0}", url);
                Console.ReadLine();
            }
        }
    }
}
