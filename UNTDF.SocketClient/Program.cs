using System.Net;
using System.Net.Sockets;
using System.Text;
using UNTDF.SocketClient.Clients;

var clients = ClientFactory.GetUDPMessageClients(5000);
int i = 0;
foreach(var client in clients)
{
    i++;
    await client.SendMessageAsync($"Client{i}: Lorem Ipsum ");
}
