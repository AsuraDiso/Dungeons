using Dungeons.Game.MapGeneration;
using Dungeons.Infrastructure;
using Dungeons.Services;

namespace Dungeons.Game.Rooms
{
    public abstract class RoomFiller
    {
        protected readonly LevelSystem _levelSystem;

        public RoomFiller()
        {
            _levelSystem = Locator<LevelSystem>.Instance;
        }
        
        public abstract void Fill(Room room, RoomData roomData, RoomConfigs roomConfigs);
        public virtual bool IsValid() => true;
    }
}