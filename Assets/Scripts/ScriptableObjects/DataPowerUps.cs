using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PowerUps")]
public class DataPowerUps : ScriptableObject
{
    [SerializeField] private string _name, _desc;
    [SerializeField] private float _amount;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Type _powerUpType;

    private Player _player;

    public string Name { get => _name; set => _name = value; }
    public string Desc { get => _desc; set => _desc = value; }
    public int Price { get => _price; set => _price = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public Type PowerUpType { get => _powerUpType; set => _powerUpType = value; }

    public enum Type
    {
        FireRate,
        MaxLife,
        Damage,
        MoveSpeed,
        BulletSize,
        BulletSpeed,
    }

    public void PowerUp()
    {
        _player = Player.instance;
        switch (PowerUpType)
        {
            case Type.FireRate:
                _player.Controller.EquippedGun.FireRate *= _amount;
                break;
            case Type.Damage:
                _player.Controller.EquippedGun.Damage += Mathf.RoundToInt(_amount);
                break;
            case Type.MoveSpeed:
                _player.Controller.MovementSpeed += _amount;
                break;
            case Type.MaxLife:
                _player.MaxHp += Mathf.RoundToInt(_amount);
                break;
            case Type.BulletSize:
                _player.Controller.EquippedGun.BulletSize *= Mathf.RoundToInt(_amount);
                break;
            case Type.BulletSpeed:
                _player.Controller.EquippedGun.BulletSpeed *= Mathf.RoundToInt(_amount);
                break;
        }
    }
}
