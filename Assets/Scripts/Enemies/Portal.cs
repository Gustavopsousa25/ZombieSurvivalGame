using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : BaseEnemy
{
    [SerializeField] private ObjectPool _enemyPool;
    [SerializeField] private float _spawnRate;

    protected override void Awake()
    {
        base.Awake();
        _enemyPool = GetComponent<ObjectPool>();
    }
    protected override void Start()
    {
        GameManager.instance.PortalsLeft ++;
        InvokeRepeating("SpawnRandomEnemy", 1, _spawnRate);
    }
    private void SpawnRandomEnemy()
    {
       /* GameObject enemiePrefab = _enemyPool[Random.Range(0, _enemyPool.Length)];
        GameObject clone = Instantiate(enemiePrefab, transform.position, Quaternion.identity);

        Enemy_Walker enemy = clone.GetComponent<Enemy_Walker>();
       */
       SetUpEnemies();
    }
    public override void Die()
    {
        _enemyUi.SetActive(false);
        _collider.isTrigger = true;
        _canMove = false;
        _isDead = true;
        GameManager.instance.PortalDestoyed();
        Destroy(gameObject);
    }
    public void SetUpEnemies()
    {
        GameObject enemy = _enemyPool.GetObject();
        Enemy_Walker enemyScript = enemy.GetComponent<Enemy_Walker>();
        enemyScript.SetDefaultValues();
        enemy.transform.position = transform.position;
        enemyScript.OnEnemieDeath += () => _enemyPool.ReturnToPool(enemy);
         
    }
}
