using Riptide.Utils;
using System.Net.Sockets;

namespace Riptide.Toolkit.Testing.Relay.Server
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, true);
            AdvancedServer server = new();
            server.Start((ushort)Ports.Server, 8);
            await server.Settle();

            while (true) await server.Settle();

            bool connected = false;

            UdpClient declarator = new((int)Ports.ServerDeclarator);
            declarator.Connect(System.Net.IPAddress.Loopback, (int)Ports.RelayDeclarator);
            await WaitForApproval().ConfigureAwait(false);
            await DeclareItself().ConfigureAwait(false);

            // Simplifications:
            async Task DeclareItself()
            {
                while (!connected)
                {
                    int result = await declarator.SendAsync(new byte[1] { (byte)DeclaratorCodes.Connect });
                    await Task.Delay(10);
                }
            }

            async Task WaitForApproval()
            {
                while (!connected)
                {
                    UdpReceiveResult result = await declarator.ReceiveAsync();
                    if (result.Buffer.Length == 0) continue;
                    connected = result.Buffer[0] == (byte)DeclaratorCodes.Approve;
                    RiptideLogger.Log(LogType.Info, $"[Server] Lobby declared.");
                }
            }
        }
    }
}