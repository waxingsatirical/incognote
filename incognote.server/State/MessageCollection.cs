using incognote.dal.Models;
using incognote.server.Change;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace incognote.server.State
{
    public class MessageCollection : IEnumerable<Message>
    {
        private readonly List<Message> messages = new List<Message>();
        private readonly IMessageChangeService messageChangeService;

        public MessageCollection(IMessageChangeService messageChangeService)
        {
            this.messageChangeService = messageChangeService;
        }
        public IEnumerator<Message> GetEnumerator()
        {
            lock(messages)
            {
                return messages.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (messages)
            {
                return messages.ToArray().GetEnumerator();
            }
        }

        public void Add(Message message)
        {
            lock(messages)
            {
                messages.Add(message);
                messageChangeService.MessageAdded(messages.Count, message);
            }
        }
    }
}
