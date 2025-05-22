using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    private float _speed;
    private int _BulletDamage;
    private Rigidbody _rb;
    private float lifetime = 1f;
    private float _timer;
    public int BulletDamage { get => _BulletDamage; set => _BulletDamage = value; }
    public float Speed { get => _speed; set => _speed = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if(_timer >= lifetime)
        {
            ObjectPool pool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool>();
            pool.ReturnToPool(gameObject);
        }
    }
    public void BulletMovement(Vector3 direction)
    {
        _rb.velocity = direction * Speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable target = collision.gameObject.GetComponent<IDamageable>();

        if (target != null)
        {
            target.TakeDamage(BulletDamage);
        }
        ObjectPool pool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool>();
        pool.ReturnToPool(gameObject);
    }
}
