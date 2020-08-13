using incognote.dal.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace incognote.server.Change
{
    public interface IStatusChangeService
    {
        Task StatusAddedAsync(int id, Status status);
    }

    public class StatusChangeService : IStatusChangeService
    {
        private readonly IMessageService messageService;
        private readonly string groupName;

        public StatusChangeService(
            IMessageService messageService,
            string groupName)
        {
            this.messageService = messageService;
            this.groupName = groupName;
        }

        public async Task StatusAddedAsync(int id, Status status)
        {
            var path = new[] { nameof(State.State.Statuses).ToLower(), id.ToString("0000000000") };
            await messageService.StatePostAsync(groupName, path, status);
            new Task(() => HideStatus(id, 1000)).Start();
        }

        private void HideStatus(int id, int afterMilliseconds)
        {
            var timer = new Timer(x =>
            {
                var path = new[] { nameof(State.State.Statuses).ToLower(), id.ToString("0000000000"), nameof(Status.Visible).ToLower() };
                messageService.StatePostAsync(groupName, path, false);
            }, null, new TimeSpan(0,0,0,0, afterMilliseconds), Timeout.InfiniteTimeSpan);
        }
    }
}
