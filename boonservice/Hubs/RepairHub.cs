using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace boonservice.Hubs
{
    public class RepairHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}