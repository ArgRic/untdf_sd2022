using System.Net.Sockets;
using UNTDF.SucursalClient;
using UNTDF.SucursalClient.Model;
using UNTDF.SucursalClient.Config;

var sucursales = ModelFactory.GetSucursalesSampleModel();

var clients = new List<SucursalClient>();
foreach (var sucursal in sucursales)
{
    clients.Add(new SucursalClient(ProtocolType.Tcp, sucursal, MessageType.Object));
}

foreach (var client in clients)
{
    client.Start();
}

var instruccion = "Presione cualquier tecla para limpiar pantalla.\nPresione ESC para salir.";
Console.WriteLine(instruccion);
var readKey = Console.ReadKey();
while(readKey.Key != ConsoleKey.Escape)
{
    Console.Clear();
    Console.WriteLine(instruccion);
    readKey = Console.ReadKey();
}


foreach (var client in clients)
{
    try 
    { 
        client.CloseConnection();
    }
    catch
    {
        continue;
    }
}