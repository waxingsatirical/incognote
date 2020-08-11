using incognote.dal.Models;
using incognote.server;
using incognote.server.Change;
using incognote.web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
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
        public void SetAction(string connectionId, Action action)
        {
            hubContext.Clients.Client(connectionId).SendAsync(Consts.SetActionString, action);
        }

        public void SetActionForGroup(string groupName, Action action)
        {
            hubContext.Clients.Group(groupName).SendAsync(Consts.SetActionString, action);
        }

        public void SetActionsForGroup(string groupName, IEnumerable<Action> actions)
        {
            hubContext.Clients.Group(groupName).SendAsync(Consts.SetActionsString, actions);
        }
        public void SetActions(string connectionId, IEnumerable<Action> actions)
        {
            hubContext.Clients.Client(connectionId).SendAsync(Consts.SetActionsString, actions);
        }

        public async Task StatePost(string groupName, string[] path, object payload)
        {
            var stateChange = new StateChange(path, payload);
            await hubContext.Clients.Group(groupName).SendAsync(Consts.StatePostString, stateChange);
        }

    }
}
