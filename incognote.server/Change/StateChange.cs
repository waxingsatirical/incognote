using Reinforced.Typings.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server.Change
{
    [TsInterface]
    public class StateChange
    {
        public StateChange(IEnumerable<string> path, string id, object payload)
        {
            Path = path;
            Id = id;
            Payload = payload;
        }

        public IEnumerable<string> Path { get; }
        public string Id { get; }
        public object Payload { get; }
    }
}
