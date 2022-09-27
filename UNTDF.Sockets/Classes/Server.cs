using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UNTDF.SocketServer.Classes
{
    public class Server
    {
        private string EndpointAddress { get; }
        private string ackMessage { get; }
        private SocketType socketType { get; }
        private ProtocolType protocolType { get; }

        public Server(SocketType socketType, ProtocolType protocolType)
        {
            EndpointAddress = "localhost";
            ackMessage = "<|ACK|>";
            this.socketType = socketType;
            this.protocolType = protocolType;

        }

        public async Task StartAsync()
        {
            var messageHandler = new MessageHandler();
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(EndpointAddress);
            var ipEndPoint = new IPEndPoint(ipHostInfo.AddressList[0], 6400);

            using Socket socket = new(
                ipEndPoint.AddressFamily,
                socketType,
                protocolType);

            socket.Bind(ipEndPoint);
            socket.Listen(100);

            while (true)
            {
                var handler = await socket.AcceptAsync();

                // Receive message.
                var buffer = new byte[1_024];
                var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);

                if (response.IndexOf("<|EOM|>") > -1)
                {
                    var message = "Recibo:" + response.Replace("<|EOM|>", "");
                    Console.WriteLine(message);
                    messageHandler.HandleMessage(message);

                    var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
                    await handler.SendAsync(echoBytes, 0);
                    //break;
                }

            }
        }

    }
}
