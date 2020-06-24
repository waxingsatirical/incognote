﻿using Microsoft.AspNetCore.SignalR;
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
    }
    public class RoomProvider : IRoomProvider
    {
        private readonly List<RoomBase> rooms;
        private readonly object roomsLocker = new object();
        private readonly IHubContext<Hub> hubContext;

        public RoomProvider(IHubContext<Hub> hubContext)
        {
            rooms = new List<RoomBase>();
            this.hubContext = hubContext;
        }
        public IRoom JoinRoom(string connectionId)
        {
            lock (roomsLocker)
            {
                var joinable = rooms.FirstOrDefault(r => r.CanJoin);
                var joinableName = joinable?.GroupName ?? Guid.NewGuid().ToString();
                if (joinable == null) //create new room
                {
                    joinable = new RoomBase(joinableName);                    
                    rooms.Add(joinable);
                }
                var addTask = hubContext.Groups.AddToGroupAsync(connectionId, joinableName);
                addTask.Wait();
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
            public RoomBase(string groupName)
            {
                GroupName = groupName;                
            }
            public bool CanJoin { get; set; } = true;
            public string GroupName { get; }
            public void AddConnection(string connectionId)
            {
                connectionIds.Add(connectionId);
            }
            public bool HasConnection(string connectionId)
            {
                return connectionIds.Contains(connectionId);
            }
        }
    }
}
