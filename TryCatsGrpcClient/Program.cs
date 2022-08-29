using System.Diagnostics;
using Grpc.Net.Client;
using TryCatsGrpcClient.Suport;
using TryCatsGrpcService;

namespace TryCatsGrpcClient
{
    internal class Program
    {
        private static GrpcChanelCreator _grpcChanelCreator;

        static async Task Main(string[] args)
        {
            // создаем канал для обмена сообщениями с сервером

            //Тип Grpc канала
            int defaultType = GetGrpcChanellType();

            // параметр - адрес сервера gRPC
            var defaultPort = GetGrpcDefaultPort();

            _grpcChanelCreator = GrpcChanelCreatorChoose(defaultType);

            using var channel = _grpcChanelCreator.CreateChanel(defaultPort);

            // создаем клиента
            var client = new Cats.CatsClient(channel);


            int cmdCode = 0;

            do
            {
                Console.Write("Inter cmd numbeer: ");
                string? cmd = Console.ReadLine();
                Int32.TryParse(cmd, out cmdCode);

                dynamic reply = "";

                try
                {
                    // обмениваемся сообщениями с сервером
                    switch (cmdCode)
                    {
                        case 1:
                            reply = await client.GetAllCatsAsync(new GetAllCatsRequest());
                            break;
                        case 2:
                            dynamic request = new GetCatByIdRequest() { Id = 1 };
                            reply = await client.GetCatByIdAsync(request);
                            break;

                        case 3:
                            request = new GetCatByNameRequest() { Name = "Vasya" };
                            reply = await client.GetCatByNameAsync(request);
                            break;

                        case 4:
                            request = new PostCatRequest() { Cat = new Cat() { Id = 3, Name = "Oleg", Summary = "IT" } };
                            reply = await client.PostCatAsync(request);
                            break;

                        case 5:
                            request = new PutCatRequest() { Cat = new Cat() { Id = 2, Name = "Oleg", Summary = "IT" } };
                            reply = await client.PutCatAsync(request);
                            break;

                        case 6:
                            request = new DeleteCatRequest() { Id = 1 };
                            reply = await client.DeleteCatAsync(request);
                            break;

                        default:
                            break;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
              

                if (cmdCode!= 0) Console.WriteLine($"Server reply: {reply}");


            } while (cmdCode !=0);

            Console.WriteLine("Press enter to end");
            Console.ReadLine();
        }

        private static GrpcChanelCreator GrpcChanelCreatorChoose(int defaultType)
        {
            return defaultType switch
            {
                0 => new BasicGrpcChanelCreator(),
                1 => new UntrustedGrpcChanelCreator(),
                2 => new UnsafeGrpcChanelCreator(),
                _ => new BasicGrpcChanelCreator(),
            };
        }

        private static int GetGrpcChanellType()
        {
            int defaultType = 0;

            Console.Write("Inter Type of GrpcChanelCreator: 0 - Basic, 1 - Untrusted, 2 -  Unsafe: ");

            if (Int32.TryParse(Console.ReadLine(), out int result))
            {
                defaultType = result;
            }

            return defaultType;
        }

        private static int GetGrpcDefaultPort()
        {
            int defaultPort = 7259;

            Console.Write("Inter port number: ");

            if (Int32.TryParse(Console.ReadLine(), out int result))
            {
                defaultPort = result;
            }

            return defaultPort;
        }
    }
}