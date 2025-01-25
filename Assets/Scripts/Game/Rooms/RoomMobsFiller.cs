using Enemies;
using Infrastructure;
using MapGeneration;
using PlayerSystem;
using Services;
using UnityEngine;

namespace Rooms
{
    public class RoomMobsFiller : IRoomFiller
    {
        public bool IsValid()
        {
            return true;
        }

        private void SpawnMob(GameObject mob, Room room, float halfWidth, float halfDepth, Player player)
        {
            var mobPrefab = Object.Instantiate(mob, room.transform, true);
            var position = new Vector3(Random.Range(-halfWidth, halfWidth), 0f, Random.Range(-halfDepth, halfDepth));
            mobPrefab.transform.SetPositionAndRotation(room.transform.position + position, Quaternion.identity);
            var mobAI = mobPrefab.gameObject.GetComponent<MobAI>();
            if (mobAI != null) mobAI.Player = player;
        }

        public void Fill(Room room, RoomData roomData, RoomConfigs roomConfigs)
        {
            float halfWidth = RoomConstants.RoomWidth * .5f, halfDepth = RoomConstants.RoomDepth * .5f;
            var levelManager = Locator<LevelSystem>.Instance;
            var player = Locator<Player>.Instance;
            // var floorPreset = levelManager.GetFloorPreset(currentFloor);

            if (room.Type == RoomType.Regular)
            {
                // if (floorPreset.mobs.Count > 0)
                // {
                //     var mob = floorPreset.mobs[Random.Range(0, floorPreset.mobs.Count)];
                //     SpawnMob(mob, room, halfWidth, halfDepth, playerHealth);
                // }
            }
            else if (room.Type == RoomType.Boss)
            {
                // if (floorPreset.bosses.Count > 0)
                // {
                //     var mob = floorPreset.bosses[Random.Range(0, floorPreset.bosses.Count)];
                //     SpawnMob(mob, room, halfWidth, halfDepth, playerHealth);
                // }
            }
        }
    }
}