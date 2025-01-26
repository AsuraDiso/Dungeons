using Dungeons.Infrastructure;
using Dungeons.Services;
using UnityEngine;

namespace Dungeons.Game.Doors
{
    public class ExitDoor : Door
    {
        public override void TryToEnterRoom(Collider other)
        {
            Locator<LevelSystem>.Instance?.ChangeLevel();
        }
    }
}