using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRService
{
    public class DntHub : BaseHub
    {
        public void ServiceSend(string name, string message)
        {
            //Clients.All.addMessage(name, message);

            Clients.Clients(List).addMessage(name, message);
        }
    }
}
