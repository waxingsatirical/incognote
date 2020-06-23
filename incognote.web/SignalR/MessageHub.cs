using incognote.dal.Models;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace incognote.web.Hubs
{
    public class MessageHub : Hub
    {
        [HubMethodName(Consts.SendMesssageString)]
        public async Task Post(Message msg)
        {
            await Clients.All.SendAsync(Consts.MessageReceivedString, msg);
        }
    }
}
