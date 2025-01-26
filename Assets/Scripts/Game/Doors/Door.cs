using Dungeons.Game.Rooms;
using UnityEngine;

namespace Dungeons.Game.Doors
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject _door;
        private Room _connectedRoom;
        private Vector2 _direction;
        public Room ConnectedRoom { get; set; }
        public Vector2 Direction { get; set; }

        public virtual void TryToEnterRoom(Collider other)
        {
            var entity = other.transform.parent.gameObject;

            if (ConnectedRoom != null) ConnectedRoom.TryEnterAnotherRoom(entity, Direction);
        }
    }
}