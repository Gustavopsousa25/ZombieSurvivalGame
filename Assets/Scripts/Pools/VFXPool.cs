using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    [SerializeField] private GameObject _vfxPrefab;

    [SerializeField] private int _poolSize = 3;

    [SerializeField] private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _poolSize; ++i)
        {
            GameObject vfxObj = Instantiate(_vfxPrefab, transform.position, Quaternion.identity, gameObject.transform);
            vfxObj.SetActive(false);
            pool.Enqueue(vfxObj);
        }
    }
    public GameObject GetVFX()
    {
        GameObject vfxObj = pool.Dequeue();
        vfxObj.SetActive(true);
        return vfxObj;
    }
    public void ReturnToPool(GameObject vfxObj)
    {
        vfxObj.SetActive(false);
        pool.Enqueue(vfxObj);
    }

    public IEnumerator TimeToReturn(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
