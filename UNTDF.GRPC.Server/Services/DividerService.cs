using Grpc.Core;
using UNTDF.gRPC.Server;

namespace UNTDF.gRPC.Services
{
    public class DividerService : Divider.DividerBase
    {
        private readonly ILogger<DividerService> _logger;
        public DividerService(ILogger<DividerService> logger)
        {
            _logger = logger;
        }

        public override Task<DivResponse> GetDivision(DivRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DivResponse
            {
                Resultado = request.Dividendo / request.Divisor
            });
        }
    }
}