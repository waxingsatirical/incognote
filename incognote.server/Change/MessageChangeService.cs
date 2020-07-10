using incognote.dal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server.Change
{
    public interface IMessageChangeService
    {
        void MessageAdded(int id, Message m);
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

        public async void MessageAdded(int id, Message m)
        {
            var path = new[] { nameof(State.State.Messages).ToLower(), id.ToString("0000000000") };
            await messageService.StatePost(groupName, path, m);
        }
    }
}
