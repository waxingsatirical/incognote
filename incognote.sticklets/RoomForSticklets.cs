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
        private readonly IMessageService messageService;
        private readonly IActionService actionService;
        private Game game;
        private readonly List<Message> messages = new List<Message>();

        public RoomForSticklets(
            IIncomingService incomingService,
            IMessageService messageService,
            IActionService actionService
            )
        {
            GroupName = Guid.NewGuid().ToString();
            this.incomingService = incomingService;
            this.messageService = messageService;
            this.actionService = actionService;
        }

        public string GroupName { get; }

        public void Join(string connectionId)
        {
            connectionIds.Add(connectionId);
            actionService.SetInitialActions(connectionId, game != null);
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
