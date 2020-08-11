using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace incognote.server
{
    public interface IMessageService
    {
        void SetActionsForGroup(string groupName, IEnumerable<Action> actions);
        void SetActions(string connectionId, IEnumerable<Action> actions);
        void SetActionForGroup(string groupName, Action action);
        void SetAction(string connectionId, Action action);
        Task StatePost(string groupName, string[] path, object payload);
    }

}
