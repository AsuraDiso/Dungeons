using Dungeons.Game.Enemies;
using Dungeons.Game.MapGeneration;
using Dungeons.Game.PlayerSystem;
using Dungeons.Infrastructure;
using Dungeons.Services;
using UnityEngine;

namespace Dungeons.Game.Rooms
{
    public class RoomMobsFiller : RoomFiller
    {
        public override void Fill(Room room, RoomData roomData)
        {
            float halfWidth = RoomConstants.RoomWidth * .5f, halfDepth = RoomConstants.RoomDepth * .5f;
            var player = Locator<Player>.Instance;
            if (room.Type == RoomType.Regular)
            {
                var mobs = _levelSystem.CurrentLevelPreset.Mobs;
                if (mobs.Count > 0)
                {
                    var mob = mobs[Random.Range(0, mobs.Count)];
                    SpawnMob(mob, room, halfWidth, halfDepth, player);
                }
            }
            else if (room.Type == RoomType.Boss)
            {
                var bosses = _levelSystem.CurrentLevelPreset.Bosses;
                if (bosses.Count > 0)
                {
                    var mob = bosses[Random.Range(0, bosses.Count)];
                    SpawnMob(mob, room, halfWidth, halfDepth, player);
                }
            }
        }

        private void SpawnMob(GameObject mob, Room room, float halfWidth, float halfDepth, Player player)
        {
            var mobPrefab = Object.Instantiate(mob, room.transform, true);
            var position = new Vector3(Random.Range(-halfWidth, halfWidth), 0f, Random.Range(-halfDepth, halfDepth));
            mobPrefab.transform.SetPositionAndRotation(room.transform.position + position, Quaternion.identity);
            var mobAI = mobPrefab.gameObject.GetComponent<MobAI>();
            if (mobAI != null) mobAI.Player = player;
        }
    }
}