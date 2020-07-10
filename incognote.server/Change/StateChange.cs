using Reinforced.Typings.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server.Change
{
    [TsInterface]
    public class StateChange
    {
        public StateChange(IEnumerable<string> path, object payload)
        {
            Path = path;
            Payload = payload;
        }

        public IEnumerable<string> Path { get; }
        public object Payload { get; }
    }
}
