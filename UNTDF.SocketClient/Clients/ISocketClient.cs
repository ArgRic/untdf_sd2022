using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNTDF.SocketClient.Clients
{
    public interface ISocketClient
    {
        Task SendMessageAsync(string message);
    }
}
