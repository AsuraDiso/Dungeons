using Player;
using Combats;
using UnityEngine;

public class PlayerCombat : Combat
{
    [SerializeField] private PlayerController _controller;

    private Coroutine _attackCoroutine;

    public void FixedUpdate()
    {
        if (!_controller) return;
        var attackDirection = _controller.AttackDirection;
        var isattacking = attackDirection != Vector3.zero;
        if (isattacking && _attackCoroutine == null)
        {
            TryAttack(attackDirection);
        }
    }
    public override void Update()
    {
        if (!_controller) return;
        var attackDirection = _controller.AttackDirection;

        if (attackDirection != Vector3.zero)
        {
            _rigidbody.rotation = Quaternion.Lerp(
                _rigidbody.rotation, 
                Quaternion.LookRotation(attackDirection), 
                Time.fixedDeltaTime * 10f
            );
        }
    }
}
