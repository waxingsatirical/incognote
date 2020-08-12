using incognote.dal.Models;
using incognote.server.Change;
using incognote.server.SignalR;
using incognote.server.State;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace incognote.server
{
    public interface IRoomProvider
    {
        IRoom ExistingRoom(string connectionId);
        IRoom JoinRoom(string connectionId);
    }
    public interface IRoom
    {
        public string GroupName { get; }
        public void ToGroup(IncomingMessage message);
    }
    public class RoomProvider : IRoomProvider
    {
        private readonly List<RoomBase> rooms;
        private readonly object roomsLocker = new object();
        private readonly IServerHubContext hubContext;
        private readonly IMessageService messageService;
        private readonly IUserService userService;

        public RoomProvider(
            IServerHubContext hubContext,
            IMessageService messageService,
            IUserService userService)
        {
            rooms = new List<RoomBase>();
            this.hubContext = hubContext;
            this.messageService = messageService;
            this.userService = userService;
        }
        public IRoom JoinRoom(string connectionId)
        {
            lock (roomsLocker)
            {
                var joinable = rooms.FirstOrDefault(r => r.CanJoin);
                var joinableName = joinable?.GroupName ?? Guid.NewGuid().ToString();
                if (joinable == null) //create new room
                {
                    joinable = new RoomBase(joinableName, messageService, userService);
                    rooms.Add(joinable);
                }
                var addTask = hubContext.Groups.AddToGroupAsync(connectionId, joinableName);
                addTask.Wait();
                joinable.AddConnection(connectionId);
                return joinable;
            }
        }
        public IRoom ExistingRoom(string connectionId)
        {
            lock (roomsLocker)
            {
                var room = rooms.FirstOrDefault(r => r.HasConnection(connectionId));
                return room;
            }
        }
        class RoomBase : IRoom
        {
            private readonly HashSet<string> connectionIds = new HashSet<string>();
            private readonly MessageCollection messages;
            private readonly StatusCollection statuses;
            private readonly IUserService userService;

            public RoomBase(
                string groupName, 
                IMessageService messageService, 
                IUserService userService)
            {
                GroupName = groupName;
                this.userService = userService;
                var messageChangeService = new MessageChangeService(messageService, userService, groupName);//TODO: factory for these please
                messages = new MessageCollection(messageChangeService);
                var statusChangeService = new StatusChangeService(messageService, groupName);
                statuses = new StatusCollection(statusChangeService);
            }
            public bool CanJoin { get; set; } = true;
            public string GroupName { get; }
            public void AddConnection(string connectionId)
            {
                connectionIds.Add(connectionId);
                var name = userService.Name(connectionId);
                statuses.Add(new Status($"{name} has joined the room"));
            }
            public bool HasConnection(string connectionId)
            {
                return connectionIds.Contains(connectionId);
            }

            public void ToGroup(IncomingMessage message)
            {
                messages.Add(message);
            }
        }
    }
}
