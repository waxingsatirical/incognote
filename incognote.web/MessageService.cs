using incognote.dal.Models;
using incognote.server;
using incognote.server.Change;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace incognote.web
{
    public class MessageService : IMessageService
    {
        private readonly IHubContext<Hub> hubContext;

        public MessageService(IHubContext<Hub> hubContext)
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

        public async Task ToGroup(string groupName, string message)
        {
            var msg = new Message() { Payload = message };
            await hubContext.Clients.Group(groupName).SendAsync(Consts.MessageReceivedString, msg);
        }

        public async Task StatePost(string groupName, string[] path, string id, string payload)
        {
            var stateChange = new StateChange(path, id, payload);
            await hubContext.Clients.All.SendAsync(Consts.StatePostString, stateChange);
        }

    }
}
