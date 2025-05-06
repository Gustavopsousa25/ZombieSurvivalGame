using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : BaseEnemy
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private float _spawnRate;

    protected override void Start()
    {
        GameManager.instance.PortalsLeft ++;
        InvokeRepeating("SpawnRandomEnemy", 1, _spawnRate);
    }
    private void SpawnRandomEnemy()
    {
            GameObject enemiePrefab = _enemies[Random.Range(0, _enemies.Length)];
            Instantiate(enemiePrefab, transform.position, Quaternion.identity);

    }
    public override void Die()
    {
        _enemyUi.SetActive(false);
        _collider.isTrigger = true;
        _canMove = false;
        _isDead = true;
        Destroy(gameObject);
        GameManager.instance.PortalDestoyed();
    }
}
