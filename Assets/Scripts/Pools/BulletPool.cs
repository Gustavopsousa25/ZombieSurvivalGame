using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private int _poolSize = 10;

    [SerializeField] private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i< _poolSize; ++i)
        {
            GameObject bullet = Instantiate( _bulletPrefab, transform.position, Quaternion.identity, gameObject.transform);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }
    public GameObject GetBullet()
    {
        GameObject bullet = pool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }
    public void ReturnToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = transform.position;
        pool.Enqueue(bullet);
    }
}
