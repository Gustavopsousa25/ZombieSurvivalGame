using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDrops : MonoBehaviour, ICollectible
{
    [SerializeField] private int _minAmount, _maxAmount;
    [SerializeField] private GameObject _effect;

    private int _foodAmount;
    private Player _player;

    private void Start()
    {
        _player = Player.instance;
    }
    public void Collect()
    {
        _player.IncrementCurrency(_foodAmount = Random.Range(_minAmount, _maxAmount));
        Destroy(gameObject);
    }
}
