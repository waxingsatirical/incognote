using incognote.dal.Models;
using incognote.server;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace incognote.web.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IRoomProvider roomProvider;
        private readonly IUserService userService;

        public MessageHub(IRoomProvider roomProvider, IUserService userService)
        {
            this.roomProvider = roomProvider;
            this.userService = userService;
        }
        public override Task OnConnectedAsync()
        {
            var name = Context.GetHttpContext().Request.Query[Consts.NameParameterString].ToString();
            userService.Add(Context.ConnectionId, name);
            return base.OnConnectedAsync();
        }
        [HubMethodName(Consts.SendMesssageString)]
        public async Task Post(string payload)
        {
            var room = roomProvider.ExistingRoom(Context.ConnectionId);

            room.ToGroup(new IncomingMessage(Context.ConnectionId, payload));
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
            roomProvider.JoinRoom(Context.ConnectionId);

            //await Clients.Groups(room.GroupName).SendAsync(Consts.MessageReceivedString, msg);
        }
        [HubMethodName(Consts.ConnectionIdString)]
        public string ConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
