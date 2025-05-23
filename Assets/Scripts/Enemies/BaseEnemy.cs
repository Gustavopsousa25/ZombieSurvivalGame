using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHP;
    [SerializeField] private Image _hpBar;
    [SerializeField] protected GameObject _enemyUi;
    public Action OnEnemieDeath;
    protected bool _canMove, _isDead;
    protected int _hp;
    protected Player _player;
    protected Collider _collider;
    protected Animator _animator;

    protected virtual void Awake()
    {
        _hp = _maxHP;
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();
    }
    protected virtual void Start()
    {
        UpdateHealthBar(_hp, _maxHP);
        _player = Player.instance;
        _enemyUi.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        _enemyUi.SetActive(true);
        _hp -= damage;
        UpdateHealthBar(_hp, _maxHP);
        if (_hp <= 0)
        {
            Die();
        }
    }
    protected virtual void UpdateHealthBar(float hp, float maxhp)
    {
        _hpBar.fillAmount = hp / maxhp;
    }
    public virtual void Die()
    {
        _canMove = false;
        if (_isDead == false)
        {
            _isDead = true;
            _enemyUi?.SetActive(false);
            _animator.SetTrigger("isDead");
            OnEnemieDeath?.Invoke();
        }
    }
    public void SetDefaultValues()
    {
        UpdateHealthBar(_hp, _maxHP);
        _hp = _maxHP;
        _enemyUi.SetActive(false);
        _isDead = false;
        _canMove = true;
    }
}
