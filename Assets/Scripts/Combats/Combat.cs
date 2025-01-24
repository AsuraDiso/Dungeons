using System.Collections;
using InventorySystem;
using Items;
using UnityEngine;

namespace Combats
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] protected Inventory _inventory;
        [SerializeField] protected Health _health;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected CapsuleCollider _collider;

        private Coroutine _cooldownCoroutine;
        private Transform _target;
        public Transform Target { get; set; }

        public virtual void Update()
        {
            if (Target && !_health.IsDead())
            {
                _rigidbody.rotation = Quaternion.Lerp(
                    _rigidbody.rotation, 
                    Quaternion.LookRotation(Target.position - transform.position), 
                    Time.fixedDeltaTime * 10f
                );
            }
        }
        public bool TryAttack(Vector3 direction)
        {
            if (CanAttack())
            {
                ResetAttackAnimation();
                PerformAttack(direction);
            }

            return CanAttack();
        }

        protected void PerformAttack(Vector3 direction)
        {
            var weapon = _inventory.GetItemInSlot(EquipSlot.Hand) as Weapon;
            if (weapon && weapon.Range > 2) {
                if (weapon.Projectile) {
                    var pos = transform.position + new Vector3(0f, 1f, 0f);
                    var projectile = Instantiate(weapon.Projectile, pos, Quaternion.identity);
                    projectile.Direction = direction;
                    projectile.Damage = weapon.Damage;
                    projectile.Owner = transform.gameObject;
                }
            }
            else
            {
                Vector3 forward = transform.forward;
                float attackRange = GetAttackRange();
                float attackAngle = 120f; 
                int layerMask = LayerMask.GetMask("Enemy");
                Collider[] hitTargets = Physics.OverlapSphere(transform.position, attackRange, layerMask);

                foreach (var target in hitTargets)
                {
                    var targetHealth = target.GetComponentInParent<Health>();
                    if (targetHealth && target.gameObject != gameObject)
                    {
                        Vector3 toTarget = target.transform.position - transform.position;
                        float angleToTarget = Vector3.Angle(forward, toTarget);

                        if (angleToTarget <= attackAngle / 2f && toTarget.magnitude <= attackRange)
                        {
                            targetHealth.DoDelta(-GetDamage());
                        }
                    }
                }
            }
            _cooldownCoroutine = StartCoroutine(AttackCoolDown());
        }

        protected async void ResetAttackAnimation()
        {
            _animator.SetBool("IsAttacking", true);
            await Awaitable.WaitForSecondsAsync(.5f);

            _animator.SetInteger("AnimationIndex", Random.Range(0, 5));
            _animator.SetBool("IsAttacking", false);
        }

        protected IEnumerator AttackCoolDown()
        {
            yield return new WaitForSeconds(GetCoolDown());
            _cooldownCoroutine = null;
        }

        public float GetAttackRange()
        {
            var weapon = _inventory.GetItemInSlot(EquipSlot.Hand) as Weapon;
            if (weapon && weapon.Range > _collider.radius + 1f)
            {
                return weapon.Range;
            }
            return _collider.radius + 1f;
        }
        public float GetCoolDown()
        {
            var weapon = _inventory.GetItemInSlot(EquipSlot.Hand) as Weapon;
            return weapon ? weapon.CoolDown : 1.5f;
        }
        public float GetDamage()
        {
            var weapon = _inventory.GetItemInSlot(EquipSlot.Hand) as Weapon;
            return weapon ? weapon.Damage : 10f;
        }
        public bool CanAttack() => _cooldownCoroutine == null;

        public void GetAttacked(float damage)
        {
            _health.DoDelta(damage);
            _animator.SetTrigger("Hit");
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            float attackRange = GetAttackRange();
            float attackAngle = 120f;

            Vector3 leftBoundary = Quaternion.Euler(0, -attackAngle / 2f, 0) * transform.forward * attackRange;
            Vector3 rightBoundary = Quaternion.Euler(0, attackAngle / 2f, 0) * transform.forward * attackRange;

            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
            Gizmos.DrawWireSphere(transform.position, GetAttackRange());
        }

    }
}
