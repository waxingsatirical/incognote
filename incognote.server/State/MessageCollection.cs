using incognote.dal.Models;
using incognote.server.Change;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace incognote.server.State
{
    public class MessageCollection : IEnumerable<IncomingMessage>
    {
        private readonly List<IncomingMessage> messages = new List<IncomingMessage>();
        private readonly IMessageChangeService messageChangeService;

        public MessageCollection(IMessageChangeService messageChangeService)
        {
            this.messageChangeService = messageChangeService;
        }
        public IEnumerator<IncomingMessage> GetEnumerator()
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

        public void Add(IncomingMessage message)
        {
            lock(messages)
            {
                messages.Add(message);
                messageChangeService.MessageAdded(messages.Count, message);
            }
        }
    }
}
