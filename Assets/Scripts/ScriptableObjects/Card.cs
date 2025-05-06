using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private DataPowerUps _assignData;
    [SerializeField] private Text _name, _description, _price;
    [SerializeField] private Image _displayImage;

    private Player _player;
    private GameManager _gameManager;
    private PowerUpManager _powerUpManager;
    private void Awake()
    {
        _name.text = _assignData.Name;
        _description.text = _assignData.Desc;
        _price.text = _assignData.Price.ToString();
        _displayImage.sprite = _assignData.Sprite;
    }
    private void Start()
    {
        _powerUpManager = PowerUpManager.instance;
    }
    public void BuyPowerUp()
    {
        _player = Player.instance;
        _gameManager = GameManager.instance;
        if (_player.Currency >= _assignData.Price)
        {
            _player.SpendCurrency(_assignData.Price);
            _assignData.PowerUp();
            _powerUpManager.ActiveCards.Remove(gameObject);
            if (_powerUpManager.ActiveCards.Count <= 0)
            {
                _gameManager.DisablePowerUpMenu();
            }
            Destroy(gameObject);
        }      
    }
}
