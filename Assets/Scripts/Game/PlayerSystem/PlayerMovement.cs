using Movements;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerMovement : Movement
    {
        [SerializeField] private Player _player;

        protected override void FixedUpdate()
        {
            _moveDirection = _player.Controller.MovementDirection;
            base.FixedUpdate();
        }
    }
}