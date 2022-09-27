using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace UNTDF.SocketClient.Clients
{
    public static class ClientFactory
    {
        public static IEnumerable<MessageClient> GetTCPMessageClients(int instances = 1)
        {
            var clients = new List<MessageClient>();
            var instanceRange = Enumerable.Range(0, instances);
            foreach(var instance in instanceRange)
            {
                clients.Add(new MessageClient(SocketType.Stream, ProtocolType.Tcp));
            }
            return clients;
        }
        public static IEnumerable<MessageClient> GetUDPMessageClients(int instances = 1)
        {
            var clients = new List<MessageClient>();
            var instanceRange = Enumerable.Range(0, instances);
            foreach (var instance in instanceRange)
            {
                clients.Add(new MessageClient(SocketType.Dgram, ProtocolType.Udp));
            }
            return clients;
        }
    }
}
