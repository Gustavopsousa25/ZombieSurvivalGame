using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGun : MonoBehaviour
{
    [SerializeField] private int _damage, _bulletSize;
    [SerializeField] private float _fireRate, _bulletSpeed;
    protected ObjectPool _bulletPool, _vfxPool;
    [SerializeField] protected Transform _firePoint;

    protected float _timePassed;

    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public int BulletSize { get => _bulletSize; set => _bulletSize = value; }
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }

    private void Awake()
    {
        _bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool>();
        _vfxPool = GameObject.FindGameObjectWithTag("VFXPool").GetComponent<ObjectPool>();
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
        SetUpBullet();
        SetUpVFX();
    }
    IEnumerator TimeToReturn( GameObject effect,float time)
    {

        yield return new WaitForSeconds(time);
        _vfxPool.ReturnToPool(effect);
    }

    private void SetUpBullet()
    {
        GameObject bulletPrefab = _bulletPool.GetObject();
        BaseBullet newBullet = bulletPrefab.GetComponent<BaseBullet>();
        newBullet.BulletDamage = Damage;
        newBullet.transform.localScale = Vector3.one * BulletSize;
        newBullet.Speed = BulletSpeed;
        newBullet.transform.up = transform.forward;
        newBullet.transform.position = _firePoint.position;
        newBullet.BulletMovement(transform.forward);
    }
    private void SetUpVFX()
    {
        GameObject newMuzzleEffect = _vfxPool.GetObject();
        newMuzzleEffect.transform.parent = _firePoint;
        newMuzzleEffect.transform.position = _firePoint.position;
        newMuzzleEffect.transform.right = _firePoint.forward;
        StartCoroutine(TimeToReturn(newMuzzleEffect, 0.2f));
    }

}
