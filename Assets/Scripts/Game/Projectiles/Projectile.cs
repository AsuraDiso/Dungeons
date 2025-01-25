using Dungeons.Game.Combats;
using UnityEngine;

namespace Dungeons.Game.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _playerLayerMask;
        private float _damage;
        private Vector3 _direction;
        private float _lifetime;
        private GameObject _owner;
        private readonly float _range = 5f;
        private readonly float _speed = 5f;
        public Vector3 Direction { set; get; }
        public float Damage { set; get; }
        public GameObject Owner { set; get; }

        private void Update()
        {
            transform.position += Direction * (_speed * Time.deltaTime);

            if (_lifetime >= _range - .25f) transform.position -= new Vector3(0f, Time.deltaTime * 3, 0f);

            _lifetime += Time.deltaTime;

            if (_lifetime >= _range) Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            var isCollided = (IsOwnerPlayer() && IsCollisionWithEnemy(other.gameObject)) ||
                             (IsOwnerEnemy() && IsCollisionWithPlayer(other.gameObject));
            if (isCollided)
            {
                var health = other.GetComponent<Combat>();
                if (health == null) health = other.GetComponentInParent<Combat>() ?? other.GetComponentInChildren<Combat>();

                if (health != null) health.GetAttacked(-Damage);

                Destroy(gameObject);
            }
        }


        private bool IsOwnerPlayer()
        {
            return ((1 << Owner.layer) & _playerLayerMask) != 0;
        }

        private bool IsOwnerEnemy()
        {
            return ((1 << Owner.layer) & _enemyLayerMask) != 0;
        }

        private bool IsCollisionWithPlayer(GameObject otherObject)
        {
            return ((1 << otherObject.layer) & _playerLayerMask) != 0;
        }

        private bool IsCollisionWithEnemy(GameObject otherObject)
        {
            return ((1 << otherObject.layer) & _enemyLayerMask) != 0;
        }
    }
}