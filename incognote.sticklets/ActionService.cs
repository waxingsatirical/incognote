using incognote.server;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Action = incognote.server.Action;

namespace incognote.sticklets
{
    public interface IActionService
    {
        void SetInitialActionsForGroup(string groupName, bool gameInProgress);
        void SetInitialActions(string connectionId, bool gameInProgress);
    }

    public class ActionService : IActionService
    {
        private readonly IMessageService messageService;

        public ActionService(IMessageService messageService)
        {
            this.messageService = messageService;
        }
        public void SetInitialActionsForGroup(string groupName, bool gameInProgress)
        {
            var actions = new[] { ActionStartGame(gameInProgress) };
            messageService.SetActionsForGroup(groupName, actions);
        }
        public void SetInitialActions(string connectionId, bool gameInProgress)
        {
            var actions = new[] { ActionStartGame(!gameInProgress) };
            messageService.SetActions(connectionId, actions);
        }
        private Action ActionStartGame(bool enabled) => new Action("startGame", "Start Game", enabled);
    }


}
