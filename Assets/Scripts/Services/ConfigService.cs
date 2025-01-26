using System;
using System.Collections.Generic;
using Dungeons.Game.PlayerSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dungeons.Services
{

    public class ConfigService : MonoBehaviour
    {
        [field: SerializeField] public string Seed { get; private set; }
        [field: SerializeField] public Player Player { get; private set; }
        [field: SerializeField] public List<LevelPreset> PresetLevels { get; private set; }

        public void Init()
        {
            InitSeed();
        }

        private void InitSeed()
        {
            var seed = Seed is null or "" ? (int)(DateTime.UtcNow.Ticks % int.MaxValue) : GetNumericSeed(Seed);
            Random.InitState(seed);
        }

        private static int GetNumericSeed(string seed)
        {
            var hash = 0;
            foreach (var c in seed)
            {
                hash = (hash << 5) - hash + c;
                hash %= int.MaxValue;
            }

            return hash;
        }

        public LevelPreset GetRandomLevelPreset(int level = 1)
        {
            return PresetLevels[Random.Range(0, PresetLevels.Count)];
        }
    }
}