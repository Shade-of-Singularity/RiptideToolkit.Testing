using Riptide.Utils;
using System.Net;

namespace Riptide.Toolkit.Testing.Relay.Client
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, true);
            AdvancedClient client = new();
            client.Connect(IPAddress.Loopback.ToString() + ":" + (ushort)Ports.Server);
            while (true) await client.Settle();
        }
    }
}
