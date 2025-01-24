using MapGeneration;
using PlayerSystem;
using Rooms;
using UnityEngine;

public class PlayerSpawner
{
    public Player Spawn(Player _playerprefab)
    {
        return GameObject.Instantiate(_playerprefab);
    }
}