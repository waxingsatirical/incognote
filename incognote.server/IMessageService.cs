using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace incognote.server
{
    public interface IMessageService
    {
        Task StatePostAsyncForGroup(string groupName, string[] path, object payload);
        Task StatePostAsync(string connectionId, string[] path, object payload);
    }

}
