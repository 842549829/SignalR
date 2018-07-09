using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRService
{
    public class BaseHub : Hub
    {

        protected static IList<string> List = new List<string>();

        public override Task OnConnected()
        {
            //用户上线
            string connId = Context.ConnectionId;
            var model = List.SingleOrDefault(p => p == connId);
            if (model == null)
            {
                List.Add(connId);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //用户下线
            string connId = Context.ConnectionId;
            var model = List.SingleOrDefault(p => p == connId);
            if (model != null)
            {
                List.Remove(model);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}