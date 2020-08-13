using incognote.server;
using Microsoft.Extensions.ObjectPool;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incognote.sticklets
{
    public interface IActionChangeService
    {
        Task PostActionsForGroupAsync(IEnumerable<Action> actions);
        Task EnableActionAsync(string connectionId, int id);
        Task DisableActionAsync(string connectionId, int id);
    }

    public class ActionChangeService : IActionChangeService
    {
        private readonly string groupName;
        private readonly IMessageService messageService;
        private static readonly string[] actionsPath = new[] { nameof(server.State.State.Actions).ToLower() };

        public ActionChangeService(string groupName, IMessageService messageService)
        {
            this.groupName = groupName;
            this.messageService = messageService;
        }
        public async Task DisableActionAsync(string connectionId, int id)
            => await SetEnabled(connectionId, id, false);
        public async Task EnableActionAsync(string connectionId, int id)
            => await SetEnabled(connectionId, id, true);

        private async Task SetEnabled(string connectionId, int id, bool enable)
        {
            var path = actionsPath
                .Append(id.ToString("0000000000")) //TODO: extension method?
                .Append(nameof(Action.Enabled))
                .ToArray();
            await messageService.StatePostAsync(connectionId, path, enable);
        }

        public async Task PostActionsForGroupAsync(IEnumerable<Action> actions)
            => await messageService.StatePostAsyncForGroup(groupName, actionsPath, actions);
    }


}
