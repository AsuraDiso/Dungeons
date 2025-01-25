using Dungeons.Game.PlayerSystem;
using UnityEngine;

namespace Dungeons.Services
{
    public class PlayerSpawner
    {
        public Player Spawn(Player playerPrefab)
        {
            return Object.Instantiate(playerPrefab);
        }
    }
}