using Rooms;
using UnityEngine;

namespace Doors
{
    public class Door : MonoBehaviour
    {   
        [SerializeField] private MeshFilter meshFilter;
        private Vector2 _direction;
        private Room _connectedRoom;
        public Room ConnectedRoom { get; set; }  
        public Vector2 Direction { get; set; }  

        public void TryToEnterRoom(Collider other)
        {
            var entity = other.transform.parent.gameObject;

            if (ConnectedRoom != null)
            {
                ConnectedRoom.TryEnterAnotherRoom(entity, Direction);   
            }
        }
    }
}
