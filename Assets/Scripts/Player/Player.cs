using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, ICurable
{
    [SerializeField] private int _maxHp;

    private bool _isDead, _isInteracting, _canInteract;
    private int _hp, _currency;
    private PlayerController _controller;
    public static Player instance;
    private UIManager _uiManager;

    public bool IsDead { get => _isDead; set => _isDead = value; }
    public int Currency { get => _currency; set => _currency = value; }
    public PlayerController Controller { get => _controller; set => _controller = value; }
    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public bool CanInteract { get => _canInteract; set => _canInteract = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _hp = MaxHp;
        CanInteract = true;
        Currency = 0;
    }
    private void Start()
    {
        _uiManager = UIManager.instance;
        Controller = GetComponent<PlayerController>();
        _uiManager.UpdateCurrency(Currency);
    }

    public void Heal(int amount)
    {
        _hp += amount;
        _hp = Mathf.Clamp(_hp, 0, MaxHp);
        _uiManager.UpdateHealthBar(_hp, MaxHp);
    }
    public void IncrementCurrency(int Amount)
    {
        Currency += Amount;
        _uiManager.UpdateCurrency(Currency);
    }
    public void SpendCurrency(int price)
    {
        Currency -= price;
        _uiManager.UpdateCurrency(Currency);
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        _uiManager.UpdateHealthBar(_hp, MaxHp);
        if (_hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if (IsDead == false)
        {
            IsDead = true;
            Controller.PlayerAnimator.SetTrigger("isDead");
        }
        StartCoroutine(DeathScreen());
        Controller.ControllerRigidBody.isKinematic = true;
        Destroy(gameObject, 2);
    }
    IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(1.9f);
        _uiManager.ShowGameOverMenu();
    }
    private void OnTriggerEnter(Collider other)
    {
        ICollectible collectible = other.gameObject.GetComponent<ICollectible>();
        if (collectible != null)
        {
            collectible.Collect();
        }
    }
    public void InteractWithObject()
    {
        _isInteracting = true;
    }
    public void StopInteraction()
    {
        _isInteracting = false;
        CanInteract = true;
    }
    private void OnTriggerStay(Collider other)
    {
        IInteractable interactTarget = other.GetComponent<IInteractable>();
        if (interactTarget != null && _isInteracting == false)
        {
            _uiManager.ShowInteractText();
        }
        else if (interactTarget != null && _isInteracting)
        {
            if (CanInteract)
            {
                interactTarget.Interact();
            }
            CanInteract = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractable interactTarget = other.GetComponent<IInteractable>();
        if (interactTarget != null)
        {
            StopInteraction();
            _uiManager.HideInteractText();
        }
    }
}
