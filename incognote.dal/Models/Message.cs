using Reinforced.Typings.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.dal.Models
{
    public class IncomingMessage
    {
        public IncomingMessage(string connectionId, string payload)
        {
            ConnectionId = connectionId;
            Payload = payload;
        }

        public string ConnectionId { get; }
        public string Payload { get; }
    }
    [TsInterface]
    public interface IMessage
    {
        string Name { get; }
        string ConnectionId { get; }
        string Payload { get; }
    }
    public class Message : IMessage
    {
        private readonly IncomingMessage incoming;

        public Message(IncomingMessage incoming, string name)
        {
            this.incoming = incoming;
            Name = name;
        }
        
        public string Name { get; }
        public string ConnectionId => incoming.ConnectionId;
        public string Payload => incoming.Payload;
    }
}
