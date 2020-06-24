using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server
{
    public interface IMessageService
    {
        void ToGroup(string groupName, string message);
        void SetActionsForGroup(string groupName, IEnumerable<Action> actions);
        void SetActions(string connectionId, IEnumerable<Action> actions);
        void SetActionForGroup(string groupName, Action action);
        void SetAction(string connectionId, Action action);
    }

}
