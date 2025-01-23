using Player;
using UnityEngine;

namespace Movements
{
    public class PlayerMovement : Movement
    {
        [SerializeField] private PlayerController _controller;

        protected override void FixedUpdate()
        {
            _moveDirection = _controller.MovementDirection;
            base.FixedUpdate(); 
        }
    }
}
