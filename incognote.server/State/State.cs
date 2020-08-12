using incognote.dal.Models;
using Reinforced.Typings.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.server.State
{
    [TsClass]
    public class State
    {
        public IEnumerable<Message> Messages { get; }
        public IEnumerable<Status> Statuses { get; }
    }
}
