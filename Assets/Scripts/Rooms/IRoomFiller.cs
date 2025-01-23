using MapGeneration;
using UnityEngine;

namespace Rooms
{
    public interface IRoomFiller
    {
        public void Fill(Room room, RoomData roomData, RoomConfigs roomConfigs);
        public bool IsValid();
    }
}