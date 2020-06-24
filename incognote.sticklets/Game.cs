using incognote.server;
using System;
using System.Collections;
using System.Collections.Generic;

namespace incognote.sticklets
{
    public class Game
    {
        private readonly string groupName;
        private readonly IActionService actionService;
        private readonly IMessageService messageService;
        private readonly IIncomingServiceListener incomingServiceListener;
        private readonly IEnumerable<string> connectionIds;

        public Game(
            string groupName, //TODO: GroupMessagingService so we don't need to keep passing this around
            IActionService actionService, 
            IMessageService messageService,
            IIncomingServiceListener incomingServiceListener,
            IEnumerable<string> connectionIds)
        {
            this.groupName = groupName;
            this.actionService = actionService;
            this.messageService = messageService;
            this.incomingServiceListener = incomingServiceListener;
            this.connectionIds = connectionIds;
        }

        internal void Start()
        {
            actionService.SetInitialActionsForGroup(groupName, true);
            messageService.ToGroup(groupName, "New game started.");
            incomingServiceListener.Subscribe()

        }
    }
}
