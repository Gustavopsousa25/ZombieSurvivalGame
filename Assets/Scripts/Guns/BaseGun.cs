using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    [SerializeField] private int _damage, _bulletSize;
    [SerializeField] private float _fireRate, _bulletSpeed;
    [SerializeField] protected BaseBullet _bulletPrefab;
    [SerializeField] protected GameObject _muzzleEffect;
    [SerializeField] protected Transform _firePoint;

    protected float _timePassed;

    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public int BulletSize { get => _bulletSize; set => _bulletSize = value; }
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }

    private void Awake()
    {
        _timePassed = FireRate;
    }
    protected virtual void Update()
    {
        if (_timePassed < FireRate)
        {
            _timePassed += Time.deltaTime;
        }
    }
    public void Shoot()
    {
        if (_timePassed >= FireRate)
        {
            _timePassed = 0;
            FireBullet();
        }
    }
    protected virtual void FireBullet()
    {
        GameObject newMuzzleEffect = Instantiate(_muzzleEffect, _firePoint.position, Quaternion.identity, gameObject.transform);
        newMuzzleEffect.transform.right = _firePoint.forward;
        Destroy(newMuzzleEffect, 0.1f);
        BaseBullet newBullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        newBullet.BulletDamage = Damage;
        newBullet.transform.localScale = Vector3.one * BulletSize;
        newBullet.Speed = BulletSpeed;
        newBullet.transform.up = _firePoint.forward;
        newBullet.BulletMovement(transform.forward);
    }
}
