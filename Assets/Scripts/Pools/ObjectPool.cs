using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _objPrefab;

    [SerializeField] private int _poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    protected virtual void Awake()
    {
        for (int i = 0; i < _poolSize; ++i)
        {
            GameObject obj = Instantiate(_objPrefab, transform.position, Quaternion.identity, gameObject.transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public virtual  GameObject GetObject()
    {
        GameObject poolObject = pool.Dequeue();
        poolObject.SetActive(true);
        return poolObject;
    }
    public virtual void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = transform.position;
        pool.Enqueue(obj);
    }
}
