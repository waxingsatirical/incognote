﻿using incognote.dal.Models;
using incognote.server;
using Microsoft.AspNetCore.SignalR;
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

            await Clients.Groups(room.GroupName).SendAsync(Consts.MessageReceivedString, msg);
        }
        [HubMethodName(Consts.PerformActionString)]
        public async Task PerformAction(Message msg)
        {
            var room = roomProvider.ExistingRoom(Context.ConnectionId);

            await Clients.Groups(room.GroupName).SendAsync(Consts.MessageReceivedString, msg);
        }

    }
}
