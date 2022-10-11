using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UNTDF.SocketClient.Clients
{
    public class MessageClientUDP : ISocketClient
    {
        private string EndpointAddress { get; }
        private SocketType socketType { get; }
        private ProtocolType protocolType { get; }

        public MessageClientUDP(SocketType socketType, ProtocolType protocolType)
        {
            EndpointAddress = "127.0.0.1";
            this.socketType = socketType;
            this.protocolType = protocolType;

        }

        public async Task SendMessageAsync(string message)
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(EndpointAddress);
            var ipEndPoint = new IPEndPoint(ipHostInfo.AddressList[0], 8000);

            var client = new Socket(
                ipEndPoint.AddressFamily,
                socketType,
                protocolType);

            await client.ConnectAsync(ipEndPoint);
         
            // Send message.
            var messageBytes = Encoding.UTF8.GetBytes(message);
            _ = await client.SendAsync(messageBytes, SocketFlags.None);

            client.Shutdown(SocketShutdown.Both);
        }
    }

}


