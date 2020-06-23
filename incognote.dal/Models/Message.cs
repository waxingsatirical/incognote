using Reinforced.Typings.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.dal.Models
{
    [TsInterface]
    public class Message
    {
        public string ClientUniqueId { get; set; }
        public string Payload { get; set; }
    }
}
