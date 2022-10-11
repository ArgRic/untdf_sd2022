using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UNTDF.SucursalClient.Config;
using UNTDF.SucursalClient.Model;
using UNTDF.SucursalDTO;

namespace UNTDF.SucursalClient
{
    public class SucursalClient
    {
        public string Name { get => this.Sucursal.Nombre; }
        private readonly MessageType messageType;
        private readonly Socket _socket;
        private readonly IPEndPoint _endpoint;
        private Sucursal Sucursal;
        private enum Result { Ok = 0, Error = 1}

        public SucursalClient(ProtocolType protocolType, Sucursal sucursal, MessageType messageType)
        {
            this.Sucursal = sucursal;
            this.messageType = messageType;
            IPHostEntry ipHostInfo = Dns.GetHostEntryAsync("127.0.0.1").Result;
            _endpoint = new IPEndPoint(ipHostInfo.AddressList[0], 8000);

            _socket = new Socket(
                _endpoint.AddressFamily,
                SocketType.Stream,
                protocolType);

        }
        public void Start()
        {
            StatusReport("Inicial");
            ConnectToServer();
            _ = Listen();
        }

        public void CloseConnection()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        private void StatusReport(string reportName)
        {
            string report = reportName += "\n";
            report += Sucursal.ReporteEstadoActual();
            Console.WriteLine(report);
        }

        private async Task<int> Listen()
        {
            int count = 1;
            while (true && _socket.Connected)
            {
                try
                {
                    var buffer = new byte[1_024];
                    var received = await _socket.ReceiveAsync(buffer, SocketFlags.None);
                    var response = Encoding.UTF8.GetString(buffer, 0, received);
                    
                    if (messageType == MessageType.String) 
                    { 
                        Sucursal.Multiplicador = Convert.ToDouble(response, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        var dto = JsonSerializer.Deserialize<SucursalMessageDTO>(response) ?? throw new JsonException("Deserialize Error");
                        Sucursal.Multiplicador = dto.Multiplicador;
                    }
                    SendResponse(Result.Ok);
                    StatusReport("Message #" + count++);
                    
                }
                catch (Exception)
                {
                    if (_socket.Connected)
                    {
                        SendResponse(Result.Error);
                    }
                    break;
                }

            }

            CloseConnection();
            return 0;
        }

        private void SendResponse(Result result)
        {
            if (result == Result.Ok)
            {
                _socket.Send(Encoding.UTF8.GetBytes("1"), SocketFlags.None);
                return;
            }

            _socket.Send(Encoding.UTF8.GetBytes("0"), SocketFlags.None);
            return;
        }

        private void ConnectToServer()
        {
            if (!_socket.Connected)
            {
                _socket.Connect(_endpoint);
            }
        }

    }
}
