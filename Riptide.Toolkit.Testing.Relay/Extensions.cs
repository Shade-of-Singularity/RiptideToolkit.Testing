namespace Riptide.Toolkit.Testing.Relay
{
    public static class Extensions
    {
        public static async Task Settle(this Peer peer)
        {
            await Task.Delay(2);
            peer.Update();
            await Task.Delay(2);
            peer.Update();
            await Task.Delay(2);
            peer.Update();
            await Task.Delay(2);
        }
    }
}
