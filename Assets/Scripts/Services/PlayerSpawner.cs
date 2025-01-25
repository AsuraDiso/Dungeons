using PlayerSystem;
using UnityEngine;

namespace Services
{
    public class PlayerSpawner
    {
        public Player Spawn(Player playerPrefab)
        {
            return Object.Instantiate(playerPrefab);
        }
    }
}