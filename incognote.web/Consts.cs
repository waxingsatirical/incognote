using Reinforced.Typings.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace incognote.web
{
    [TsClass]
    public static class Consts
    {
        [TsProperty(Constant = true)]
        public const string SignalRPath = "/MessageHub";
        [TsProperty(Constant = true)]
        public const string MessageReceivedString = nameof(MessageReceivedString);
        [TsProperty(Constant = true)]
        public const string SendMesssageString = nameof(SendMesssageString);
    }
}
