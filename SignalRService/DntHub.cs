using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRService
{
    public class DntHub : BaseHub
    {
        public override void Init()
        {
            if (GetCache("DntHub") == null)
            {
                SetCache(this, new TimeSpan(0, 0, 30, 0));
            }
        }


        public void ServiceSend(string name, string message)
        {
            //Clients.All.addMessage(name, message);
            Clients.Clients(List).addMessage(name, message);
        }

        public void ClientSend(string name, string message)
        {
            Console.WriteLine("{0}:   {1}", name, message);
            Console.WriteLine("-------------------------------------------------------------------------------");
        }


        /**//// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache.Get(cacheKey);
        }

        /**//// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(object objObject, TimeSpan timeout)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert("DntHub", objObject, null, DateTime.MaxValue, timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }
    }
}
