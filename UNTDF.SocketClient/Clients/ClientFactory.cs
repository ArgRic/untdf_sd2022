using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace UNTDF.SocketClient.Clients
{
    public static class ClientFactory
    {
        public static IEnumerable<ISocketClient> GetTCPMessageClients(int instances = 1)
        {
            var clients = new List<ISocketClient>();
            var instanceRange = Enumerable.Range(0, instances);
            foreach(var instance in instanceRange)
            {
                clients.Add(new MessageClientTCP(SocketType.Stream, ProtocolType.Tcp));
            }
            return clients;
        }

        public static IEnumerable<ISocketClient> GetUDPMessageClients(int instances = 1)
        {
            var clients = new List<ISocketClient>();
            var instanceRange = Enumerable.Range(0, instances);
            foreach (var instance in instanceRange)
            {
                clients.Add(new MessageClientUDP(SocketType.Dgram, ProtocolType.Udp));
            }
            return clients;
        }
    }
}
