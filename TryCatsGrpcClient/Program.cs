using Grpc.Net.Client;
using TryCatsGrpcService;

namespace TryCatsGrpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7259");

            // создаем клиента
            var client = new Cats.CatsClient(channel);

            int cmdCode = 0;

            do
            {
                Console.Write("Inter cmd numbeer: ");
                string? cmd = Console.ReadLine();
                Int32.TryParse(cmd, out cmdCode);

                dynamic reply = "";

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
                        request = new PostCatRequest() { Cat = new Cat() {Id = 3, Name = "Oleg", Summary = "IT"} };
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

                if (cmdCode!= 0) Console.WriteLine($"Server reply: {reply}");


            } while (cmdCode !=0);

            Console.WriteLine("Press enter to end");
            Console.ReadLine();
        }
    }
}