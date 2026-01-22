using Riptide.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Riptide.Toolkit.Testing.Relay
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, true);
            UdpClient relay = new((int)Ports.Relay);
            AdvancedServer server = new();
            AdvancedClient client = new();
            server.Start((ushort)Ports.Server, 8);
            server.MessageReceived += (server, args) => Console.WriteLine($"[Server] Received: {args.Message}");
            await Task.Delay(1);

            Receive();
            //Translate();

            // Declares that there is a server waiting for connection.
            UdpClient relayDeclarator = new((int)Ports.RelayDeclarator);
            UdpClient serverDeclarator = new((int)Ports.ServerDeclarator);
            serverDeclarator.Connect(IPAddress.Loopback, (int)Ports.RelayDeclarator);
            var task = relayDeclarator.ReceiveAsync();
            serverDeclarator.Send(Encoding.UTF8.GetBytes(server.Port.ToString()));
            UdpReceiveResult result = await task;
            string port = Encoding.UTF8.GetString(result.Buffer);
            Console.WriteLine($"Received port: {port}");

            await Task.Delay(100);
            Console.WriteLine("Sending test data.");
            client.Connect(IPAddress.Loopback.ToString() + ":" + (ushort)Ports.Relay);
            await Update();
            await Task.Delay(1000);
            return 0;

            // Simplifications:
            async Task Update()
            {
                await Task.Delay(2);
                server.Update();
                await Task.Delay(2);
                client.Update();
                await Task.Delay(2);
            }

            async void Receive()
            {
                while (true)
                {
                    var result = await relay.ReceiveAsync();
                    string data = Encoding.UTF8.GetString(result.Buffer);
                    StringBuilder builder = new();
                    foreach (var v in result.Buffer)
                    {
                        //for (int i = 0; i < 8; i++)
                        //{
                        //    builder.Append(v >> (7 - i) & 0b1);
                        //}
                        builder.Append(v);
                        builder.Append(' ');
                    }
                    if (result.Buffer.Length > 0) builder.Remove(builder.Length - 1, 1);
                    Console.WriteLine($"Received on relay: {data} ({builder}) from ({result.RemoteEndPoint})");
                    relay.Send(result.Buffer, IPAddress.Loopback.ToString(), (int)Ports.Server);
                }
            }

            //async void Translate()
            //{

            //}
        }

        [AdvancedMessage(0u)] static void Handler() => Console.WriteLine("[][][] Fired handler [][][]");
    }
}
