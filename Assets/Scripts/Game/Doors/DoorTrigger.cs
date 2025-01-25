using UnityEngine;

namespace Dungeons.Game.Doors
{
    public class DoorTrigger : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private Door _door;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (_door != null) _door.TryToEnterRoom(other);
        }
    }
}