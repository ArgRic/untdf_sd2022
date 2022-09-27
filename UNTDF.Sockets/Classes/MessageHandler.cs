using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UNTDF.SocketServer.Classes
{
    public class MessageHandler
    {
        public MessageHandler()
        {
        }

        public async void HandleMessage(string message)
        {
            using StreamWriter file = new("output.txt");
            await file.WriteAsync(message);
        }

    }
}
