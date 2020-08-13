using incognote.sticklets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace incognote.server.State
{
    public class ActionCollection
    {
        public static async Task<ActionCollection> CreateAsync(string connectionId, IEnumerable<Action> initialActions, IActionChangeService actionChangeService)
        {
            var ac = new ActionCollection(connectionId, actionChangeService);
            await actionChangeService.PostActionsForGroupAsync(initialActions);
            return ac;
        }

        private readonly string connectionId;
        private readonly IActionChangeService actionChangeService;

        private ActionCollection(string connectionId, IActionChangeService actionChangeService)
        {
            this.connectionId = connectionId;
            this.actionChangeService = actionChangeService;
        }

        public async Task EnableAsync(int id)
        {
            await actionChangeService.EnableActionAsync(connectionId, id);
        }

        public async Task DisableAsync(int id)
        {
            await actionChangeService.DisableActionAsync(connectionId, id);
        }
    }
}
