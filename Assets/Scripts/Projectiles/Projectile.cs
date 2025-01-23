using System;
using Combats;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 5f;
    private float _damage;
    private float _range = 5f;
    private float _lifetime;
    private Vector3 _direction;
    private GameObject _owner;
    public Vector3 Direction { set; get; }
    public float Damage { set; get; }
    public GameObject Owner { set; get; }

    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;

    private void Update()
    {
        transform.position += Direction * (_speed * Time.deltaTime);
        
        if (_lifetime >= _range - .25f)
        {
            transform.position -= new Vector3(0f, Time.deltaTime * 3, 0f);
        }

        _lifetime += Time.deltaTime;

        if (_lifetime >= _range)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var isCollided = IsOwnerPlayer() && IsCollisionWithEnemy(other.gameObject) ||
                         (IsOwnerEnemy() && IsCollisionWithPlayer(other.gameObject));
        if (isCollided)
        {
            Combat health = other.GetComponent<Combat>();
            if (health == null)
            {
                health = other.GetComponentInParent<Combat>() ?? other.GetComponentInChildren<Combat>();
            }

            if (health != null)
            {
                health.GetAttacked(-Damage);
            }

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