using Riptide.Utils;

namespace Riptide.Toolkit.Testing
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            RiptideLogger.Initialize(Console.WriteLine, Console.WriteLine, Console.WriteLine, Console.WriteLine, true);
            Examples.Basics.Run();
            while (KeepWaiting()) await Task.Delay(10);
        }

        static bool KeepWaiting() => Examples.Basics.IsRunning;
    }
}
