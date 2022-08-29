using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using TryCatsGrpcService;

namespace TryCatsGrpcClient.Suport
{
    abstract class GrpcChanelCreator
    {
        GrpcChannel channel;

        public abstract GrpcChannel CreateChanel(int port);
    }


    class BasicGrpcChanelCreator : GrpcChanelCreator
    {
        public override GrpcChannel CreateChanel(int port)
        {
            return GrpcChannel.ForAddress($"https://localhost:{port}");
        }
    }

    class UntrustedGrpcChanelCreator : GrpcChanelCreator
    {
        public override GrpcChannel CreateChanel(int port)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            return GrpcChannel.ForAddress($"https://localhost:{port}", new GrpcChannelOptions { HttpHandler = httpHandler });
        }
    }

    class UnsafeGrpcChanelCreator : GrpcChanelCreator
    {
        public override GrpcChannel CreateChanel(int port)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var httpHandler = new HttpClientHandler();

            return GrpcChannel.ForAddress($"http://localhost:{port}");
        }
    }


}
