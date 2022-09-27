﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UNTDF.SocketClient.Clients
{
    public class MessageClient
    {
        private string EndpointAddress { get; }
        private SocketType socketType { get; }
        private ProtocolType protocolType { get; }

        public MessageClient(SocketType socketType, ProtocolType protocolType)
        {
            EndpointAddress = "localhost";
            this.socketType = socketType;
            this.protocolType = protocolType;

        }

        public async Task SendMessageAsync(string message)
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(EndpointAddress);
            var ipEndPoint = new IPEndPoint(ipHostInfo.AddressList[0], 6400);

            var client = new Socket(
                ipEndPoint.AddressFamily,
                socketType,
                protocolType);

            await client.ConnectAsync(ipEndPoint);
            while (true)
            {
                // Send message.
                var messageBytes = Encoding.UTF8.GetBytes(message + "<|EOM|>");
                _ = await client.SendAsync(messageBytes, SocketFlags.None);

                // Receive ack.
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                if (response == "<|ACK|>")
                {
                    break;
                }
            }

            client.Shutdown(SocketShutdown.Both);
        }
    }

}


