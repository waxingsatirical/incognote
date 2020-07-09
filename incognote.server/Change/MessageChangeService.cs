using incognote.dal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server.Change
{
    public interface IMessageChangeService
    {
        void MessageAdded(Message m);
    }

    public class MessageChangeService : IMessageChangeService
    {
        private readonly IMessageService messageService;
        private readonly string groupName;

        public MessageChangeService(IMessageService messageService, string groupName)
        {
            this.messageService = messageService;
            this.groupName = groupName;
        }

        public async void MessageAdded(Message m)
        {
            var path = new[] { nameof(State.State.Messages).ToLower() };
            var id = Guid.NewGuid().ToString();
            await messageService.StatePost(groupName, path, id, m.Payload);
        }
    }
}
