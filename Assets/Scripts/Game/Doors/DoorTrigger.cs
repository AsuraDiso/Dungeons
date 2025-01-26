using UnityEngine;

namespace Dungeons.Game.Doors
{
    public class DoorTrigger : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Door _door;

        private void OnTriggerEnter(Collider other)
        {
            _door?.TryToEnterRoom(other);
        }
    }
}