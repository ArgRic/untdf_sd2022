using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UNTDF.SocketServer.Classes;

string EndpointAddress = "127.0.0.1";
string EOM_value = "<|EOM|>";
string ACK_value = "<|ACK|>";
var socketType = SocketType.Stream;
var protocolType = ProtocolType.Tcp;

var messageHandler = new MessageHandler();
IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(EndpointAddress);
var ipEndPoint = new IPEndPoint(ipHostInfo.AddressList[0], 6400);

using Socket listener = new(
    ipEndPoint.AddressFamily,
    socketType,
    protocolType);

listener.Bind(ipEndPoint);
listener.Listen(100);

Console.WriteLine("Listening");
while (true)
{
    var handler = await listener.AcceptAsync();
    Console.WriteLine("Handling Response");
    //await Task.Delay(2000);
    // Receive message.
    var buffer = new byte[1_024];
    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
    var response = Encoding.UTF8.GetString(buffer, 0, received);

    if (response.IndexOf(EOM_value) > -1)
    {
        var message = "Recibo:" + response.Replace(EOM_value, "");
        Console.WriteLine(message);
        messageHandler.HandleMessage(message);

        var echoBytes = Encoding.UTF8.GetBytes(ACK_value);
        await handler.SendAsync(echoBytes, 0);
        
        //break;
    }

}