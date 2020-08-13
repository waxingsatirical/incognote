using incognote.dal.Models;
using incognote.server;
using incognote.server.Change;
using incognote.web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace incognote.web
{
    public class MessageService : IMessageService
    {
        private readonly IHubContext<MessageHub> hubContext;

        public MessageService(IHubContext<MessageHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        
        public async Task StatePostAsync(string connectionId, string[] path, object payload)
        {
            var stateChange = new StateChange(path, payload);
            await hubContext.Clients.Client(connectionId).SendAsync(Consts.StatePostString, stateChange);
        }

        public async Task StatePostAsyncForGroup(string groupName, string[] path, object payload)
        {
            var stateChange = new StateChange(path, payload);
            await hubContext.Clients.Group(groupName).SendAsync(Consts.StatePostString, stateChange);
        }
    }
}
