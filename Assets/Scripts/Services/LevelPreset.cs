using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(menuName = "Config/" + nameof(LevelPreset))]
    public class LevelPreset : ScriptableObject
    {
        [field: SerializeField] public float DecorChance { get; private set; }
        [field: SerializeField] public List<GameObject> WallPrefabs { get; private set; }
        [field: SerializeField] public List<GameObject> DecoratedWallPrefabs { get; private set; }
        [field: SerializeField] public List<GameObject> FloorPrefabs { get; private set; }
        [field: SerializeField] public List<GameObject> DecoratedFloorPrefabs { get; private set; }
    }
}