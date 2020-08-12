using incognote.dal.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace incognote.server.Change
{
    public interface IStatusChangeService
    {
        void StatusAdded(int id, Status status);
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

        public async void StatusAdded(int id, Status status)
        {
            var path = new[] { nameof(State.State.Statuses).ToLower(), id.ToString("0000000000") };
            await messageService.StatePost(groupName, path, status);
            new Task(() => HideStatus(id, 1000)).Start();
        }

        private void HideStatus(int id, int afterMilliseconds)
        {
            var timer = new Timer(x =>
            {
                var path = new[] { nameof(State.State.Statuses).ToLower(), id.ToString("0000000000"), nameof(Status.Visible).ToLower() };
                messageService.StatePost(groupName, path, false);
            }, null, new TimeSpan(0,0,0,0, afterMilliseconds), Timeout.InfiniteTimeSpan);
        }
    }
}
