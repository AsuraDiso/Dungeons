namespace Dungeons.Infrastructure
{
    public static class Locator<TSingleton>
    {
        public static TSingleton Instance { get; set; }
    }
}