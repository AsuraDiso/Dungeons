using Infrastructure;

namespace Services
{
    public class LevelSystem
    {
        private readonly ConfigService _configs;
        public LevelPreset CurrentLevelPreset { get; private set; }

        public LevelSystem(LevelPreset levelPreset)
        {
            _configs = Locator<ConfigService>.Instance;
            CurrentLevelPreset = levelPreset;
        }
    }
}