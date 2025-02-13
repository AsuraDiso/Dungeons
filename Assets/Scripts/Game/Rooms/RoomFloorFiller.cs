﻿using Dungeons.Game.MapGeneration;
using Dungeons.Infrastructure;
using Dungeons.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dungeons.Game.Rooms
{
    public class RoomFloorFiller : RoomFiller
    {
        public override void Fill(Room room, RoomData roomData)
        {
            var floor = new GameObject("Floor");
            floor.transform.SetPositionAndRotation(room.transform.position, Quaternion.identity);
            floor.transform.SetParent(room.transform);

            var tileScale = 2f;
            var floorDepth = 12;
            var floorLength = 20;
            for (var i = 0; i < floorLength * .5f; i++)
            for (var j = 0; j < floorDepth * .5f; j++)
            {
                var isDecor =_levelSystem.CurrentLevelPreset.DecoratedFloorPrefabs.Count > 0 && Random.value < _levelSystem.CurrentLevelPreset.DecorChance;
                var floorPrefabs = isDecor
                    ? _levelSystem.CurrentLevelPreset.DecoratedFloorPrefabs
                    : _levelSystem.CurrentLevelPreset.FloorPrefabs;
                var position = new Vector3(i * tileScale - floorLength * .5f + tileScale * .5f, 0,
                    j * tileScale - floorDepth * .5f + tileScale * .5f);
                var floorPrefab = Object.Instantiate(
                    floorPrefabs[Random.Range(0, floorPrefabs.Count)],
                    floor.transform);
                floorPrefab.transform.SetPositionAndRotation(floor.transform.position + position,
                    floor.transform.rotation);
            }
        }
    }
}