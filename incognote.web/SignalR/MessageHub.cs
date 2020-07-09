using incognote.dal.Models;
using incognote.server;
using incognote.server.Change;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace incognote.web.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IRoomProvider roomProvider;

        public MessageHub(IRoomProvider roomProvider)
        {
            this.roomProvider = roomProvider;
        }
        [HubMethodName(Consts.SendMesssageString)]
        public async Task Post(Message msg)
        {
            var room = roomProvider.ExistingRoom(Context.ConnectionId);
            room ??= roomProvider.JoinRoom(Context.ConnectionId);

            room.ToGroup(msg.Payload);

            var stateChange = new StateChange(new[] { "messages", Guid.NewGuid().ToString() }, "someId", msg);
            await Clients.All.SendAsync(Consts.StatePostString, stateChange);
            //await Clients.All.SendAsync(Consts.MessageReceivedString, msg);
        }
        [HubMethodName(Consts.PerformActionString)]
        public async Task PerformAction(Message msg)
        {
            var room = roomProvider.ExistingRoom(Context.ConnectionId);

            await Clients.Groups(room.GroupName).SendAsync(Consts.MessageReceivedString, msg);
        }
        [HubMethodName(Consts.JoinString)]
        public async Task Join()
        {
            var room = roomProvider.ExistingRoom(Context.ConnectionId);

            //await Clients.Groups(room.GroupName).SendAsync(Consts.MessageReceivedString, msg);
        }

    }
}
