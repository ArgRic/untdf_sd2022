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

        public async Task HandleMessage(string message)
        {
            using StreamWriter file = File.AppendText("output.txt");
            await file.WriteLineAsync(message);
        }

    }
}
