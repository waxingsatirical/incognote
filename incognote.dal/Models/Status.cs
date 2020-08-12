using System;
using System.Collections.Generic;
using System.Text;

namespace incognote.dal.Models
{
    public class Status
    {
        public Status(string payload)
        {
            Payload = payload;
        }
        public string Payload { get; }
        public bool Visible { get; set; } = true;
    }
}
