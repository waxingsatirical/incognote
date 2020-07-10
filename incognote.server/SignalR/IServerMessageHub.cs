using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server.SignalR
{
    public interface IServerHubContext
    {
        IHubClients Clients { get; }        
        IGroupManager Groups { get; }
    }
}
