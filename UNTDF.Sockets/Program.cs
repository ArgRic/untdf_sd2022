using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UNTDF.SocketServer.Classes;

string EndpointAddress = "127.0.0.1";
string EOM_value = "<|EOM|>";
string ACK_value = "<|ACK|>";
var socketType = SocketType.Dgram;
var protocolType = ProtocolType.Udp;
string initMessage =
    "Socket Server\n" +
    $"Endpoint: {EndpointAddress}\n" +
    $"Socket Type: {socketType}\n" +
    $"Protocol Type: {protocolType}\n";

Console.WriteLine(initMessage);
var messageHandler = new MessageHandler();
IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(EndpointAddress);
var ipEndPoint = new IPEndPoint(ipHostInfo.AddressList[0], 8000);

using Socket listener = new(
    ipEndPoint.AddressFamily,
    socketType,
    protocolType);

listener.Bind(ipEndPoint);
if (protocolType == ProtocolType.Tcp) 
{ 
    listener.Listen(100);
    int i = 1;
    while (true)
    {
        var handler = await listener.AcceptAsync();
        Console.WriteLine("Handling Response #" + i++);

        // Receive message.
        var buffer = new byte[1_024];
        var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
        var response = Encoding.UTF8.GetString(buffer, 0, received);

        if (response.IndexOf(EOM_value) > -1)
        {
            var message = response.Replace(EOM_value, "");
            Console.WriteLine("Payload: " + message);
            messageHandler.HandleMessage(message);
        }

    }
}
else
{
    int i = 1;
    while (true)
    {
        var buffer = new byte[1_024];
        var endpoint = (EndPoint)ipEndPoint;
        Console.WriteLine("Handling Response #" + i++);

        // Receive message.
        var received = listener.ReceiveFrom(buffer, ref endpoint);
        var response = Encoding.UTF8.GetString(buffer, 0, received);

      
        Console.WriteLine(response);
        await messageHandler.HandleMessage(response);
    }

}