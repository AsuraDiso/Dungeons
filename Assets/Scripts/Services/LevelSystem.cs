using Dungeons.Infrastructure;

namespace Dungeons.Services
{
    public class LevelSystem
    {
        private readonly ConfigService _configs;

        public LevelSystem(LevelPreset levelPreset)
        {
            _configs = Locator<ConfigService>.Instance;
            CurrentLevelPreset = levelPreset;
        }

        public LevelPreset CurrentLevelPreset { get; private set; }
    }
}