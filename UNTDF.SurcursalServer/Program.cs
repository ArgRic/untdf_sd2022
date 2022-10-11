using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using UNTDF.SucursalDTO;
using UNTDF.SurcursalServer.Config;

var config = MessageType.String;

List<Socket> clientSockets = new List<Socket>();
Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
int BUFFER_SIZE = 1_024;
Console.WriteLine("Server Setup...");
IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync("127.0.0.1");
var ipEndPoint = new IPEndPoint(ipHostInfo.AddressList[0], 8000);
var buffer = new byte[BUFFER_SIZE];

serverSocket.Bind(ipEndPoint);
serverSocket.Listen(10);
while (clientSockets.Count <= 4)
{
    Console.WriteLine($"Waiting for client #{clientSockets.Count}...");
    var client = await serverSocket.AcceptAsync(); 
    clientSockets.Add(client);
    Console.WriteLine($"Client #{clientSockets.Count} connected...");
}


while (true)
{
    Console.WriteLine($"Ingrese valor nuevo de multiplicador:");
    var rawInput = Console.ReadLine();

    if(double.TryParse(rawInput, out double doubleValue))
    {
        foreach(var client in clientSockets)
        {
            SendMessage(client, rawInput);
        }
    }

}

void SendMessage(Socket client, string rawInput) {

    Console.WriteLine($"Sending message...");
    while (true)
    {
        if (config == MessageType.Object) 
        {
            double.TryParse(rawInput, out double doubleValue);
            var dto = new SucursalMessageDTO()
            {
                Fecha = new DateTime(),
                Multiplicador = doubleValue
                
            };
            string jsonString = JsonSerializer.Serialize(dto);
            client.Send(Encoding.ASCII.GetBytes(jsonString));
        }
        else
        {
            client.Send(Encoding.ASCII.GetBytes(rawInput));
        }


        Console.WriteLine($"Awaiting response...");
        // Receive message from the server:
        var response = new byte[BUFFER_SIZE];
        var received = client.Receive(response);
        if (received == 0)
        {
            Console.WriteLine("Client closed connection!");
            return;
        }
        if (received == 1)
        {
            break;
        }
    }
}


