using Riptide.Toolkit.Examples;
using Riptide.Toolkit.Settings;
using Riptide.Utils;

namespace Riptide.Toolkit.Testing
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, true);
            try
            {
                ushort port = Basics.DefaultServerPort;
                if (!ConsoleArgs.TryGet("-ip", out string ip)) ip = Basics.DefaultServerIP;
                if (!ConsoleArgs.TryGet("-port", out string raw) || ushort.TryParse(raw, out port)) port = Basics.DefaultServerPort;
                Basics.Run(ip, port);
            }
            catch
            {
                try { Basics.Stop(); } catch { }
            }
            while (KeepWaiting()) await Task.Delay(10);
        }

        static bool KeepWaiting() => Basics.IsRunning;
    }
}
