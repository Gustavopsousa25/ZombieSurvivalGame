using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerNpc : MonoBehaviour, IInteractable
{
    [SerializeField] private int _price;
    private Player _player;
    private UIManager _uiManager;
    private void Start()
    {
        _player = Player.instance;
        _uiManager = UIManager.instance;
    }
    public void Interact()
    {
       _uiManager.ShowNPCMenu();
    } 
    public void HealPlayer()
    {
        if (_player.Currency >= _price)
        {
            _player.SpendCurrency(_price);
            _player.Heal(_player.MaxHp);
        }
    }
}
