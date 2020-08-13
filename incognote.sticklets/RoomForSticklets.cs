using incognote.dal.Models;
using incognote.server;
using System;
using System.Collections.Generic;

namespace incognote.sticklets
{
    public class RoomForSticklets : IIncomingServiceCaller
    {
        private readonly HashSet<string> connectionIds = new HashSet<string>();
        private readonly IIncomingService incomingService;
        private Game game;

        public RoomForSticklets(IIncomingService incomingService)
        {
            GroupName = Guid.NewGuid().ToString();
            this.incomingService = incomingService;
        }

        public string GroupName { get; }

        public void Join(string connectionId)
        {
            connectionIds.Add(connectionId);
        }

        public void StartGame()
        {
            //game = new Game(GroupName, actionService, messageService, connectionIds);
            //game.Start();
        }
        public bool PerformAction(string id, string connectionId, string payload)
        {
            return incomingService.PerformAction(id, connectionId, payload);
        }
    }
}
