using incognote.server;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Action = incognote.server.Action;

namespace incognote.sticklets
{
    public class ActionService
    {
        private readonly IMessageService messageService;

        public ActionService(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        public void SetInitialActions(string groupName)
        {
            var actions = new[] { ActionStartGame() };
            messageService.SetActionsForGroup(groupName, actions);
        }
        private Action ActionStartGame(bool enabled = true) => new Action("startGame", "Start Game", enabled);
    }
}
