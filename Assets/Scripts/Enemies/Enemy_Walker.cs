using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy_Walker : BaseEnemy
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed, _attackRange;
    [SerializeField] private GameObject[] _drops;
    [SerializeField] private Transform punchLocator;
    public Action OnEnemieDeath;
    private float _timePassed;
    private Portal _portal;
    protected Transform _target;
    private NavMeshAgent _agent;
    private Rigidbody _rb;

    protected override void Awake()
    {
        base.Awake();
        _timePassed = _attackSpeed;
    }
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
        _target = Player.instance.transform;
        _rb = GetComponent<Rigidbody>();
        _portal = GetComponentInParent<Portal>();    
        _canMove = true;
    }
    protected virtual void Update()
    {
        if (_timePassed < _attackSpeed)
        {
            _timePassed += Time.deltaTime;
        }
        if (_canMove)
        {
            MoveTowardsPlayer();
        }
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _attackRange))
        {
            if (hit.collider.gameObject.GetComponent<Player>())
            {
                if (_timePassed >= _attackSpeed)
                {
                    _timePassed = 0;
                    StartCoroutine(MoveWaitTime());
                    _animator.SetTrigger("isAttacking");
                }
            }
        }
    }
    public void Attack()
    {
        if (_isDead == false) 
        {
            Collider[] hitObjects = Physics.OverlapSphere(punchLocator.transform.position, 1.0f);
            for (int i = 0; i < hitObjects.Length; ++i) 
            {
                Player  player = hitObjects[i].gameObject.GetComponent<Player>();
                if(player != null) 
                {
                    player.TakeDamage(_damage);
                    return;
                }
            }              
        }
    }
    IEnumerator MoveWaitTime()
    {
        _canMove = false;
        _agent.enabled = false;
        _rb.velocity = Vector3.zero;
        _animator.SetBool("isRuning", false);
        yield return new WaitForSeconds(.5f);
        _canMove = true;
        _agent.enabled = true;
    }
    public void DropItem()
    {
        GameObject newDrop = Instantiate(_drops[0], transform.position, Quaternion.identity);
    }
    protected void MoveTowardsPlayer()
    {
        if (_target != null && _isDead == false)
        {
            Vector3 position = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            transform.LookAt(position);
            _animator.SetBool("isRuning", true);
            _agent.SetDestination(position);
        }
    }
    public override void Die()
    {
        base.Die();
        _rb.velocity = Vector3.zero;
        OnEnemieDeath?.Invoke();
    }
 
}
