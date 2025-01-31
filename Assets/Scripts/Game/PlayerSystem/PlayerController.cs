using UnityEngine;
using UnityEngine.InputSystem;

namespace Dungeons.Game.PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _input;
        public Vector3 MovementDirection => _input.Player.Move.ReadValue<Vector3>();
        public Vector3 AttackDirection => _input.Player.Attack.ReadValue<Vector3>();
        public bool TabPressed => _input.Player.Tab.ReadValue<float>() > 0;
        public bool EscTriggered => _input.Player.Esc.triggered;

        private void Awake()
        {
            _input = new PlayerInput();
            _input.Player.Enable();
        }
        
        private void OnDestroy()
        {
            _input.Player.Disable();
        }
    }
}