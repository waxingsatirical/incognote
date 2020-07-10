using incognote.server.SignalR;
using incognote.web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incognote.web.SignalR
{
    public class ServerHubContext : IServerHubContext
    {
        private readonly IHubContext<MessageHub> hubContext;

        public ServerHubContext(IHubContext<MessageHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public IHubClients Clients => hubContext.Clients;

        public IGroupManager Groups => hubContext.Groups;
    }
}
