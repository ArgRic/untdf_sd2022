using System.Threading.Tasks;
using Grpc.Net.Client;
using UNTDF.gRPC.Client;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7018");
var client = new Divider.DividerClient(channel);
var reply = await client.GetDivisionAsync(
                  new DivRequest { 
                      Dividendo = 12, 
                      Divisor = 3 
                  });
Console.WriteLine("Division: " + reply.Resultado.ToString("0.00"));
Console.WriteLine("Press any key to exit...");
Console.ReadKey();