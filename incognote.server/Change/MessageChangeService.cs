using incognote.dal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server.Change
{
    public interface IMessageChangeService
    {
        void MessageAdded(int id, IncomingMessage m);
    }

    public class MessageChangeService : IMessageChangeService
    {
        private readonly IMessageService messageService;
        private readonly IUserService userService;
        private readonly string groupName;

        public MessageChangeService(
            IMessageService messageService, 
            IUserService userService,
            string groupName)
        {
            this.messageService = messageService;
            this.userService = userService;
            this.groupName = groupName;
        }

        public async void MessageAdded(int id, IncomingMessage m)
        {
            var path = new[] { nameof(State.State.Messages).ToLower(), id.ToString("0000000000") };
            var message = new Message(m, userService.Name(m.ConnectionId));
            await messageService.StatePostAsync(groupName, path, message);
        }
    }
}
