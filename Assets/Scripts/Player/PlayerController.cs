using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference MovementAction;

    [Header("Movement Settings")]
    [SerializeField]private float _movementSpeed;

    private bool _canMove;
    private Player _player; 
    private BaseGun _equippedGun;
    private Rigidbody _rigidBody;
    private Animator _animator;
    private GameManager _gameManager;
    private Vector2 WorldDirection = Vector2.zero;

    public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }
    public Animator PlayerAnimator { get => _animator; set => _animator = value; }
    public Rigidbody ControllerRigidBody { get => _rigidBody; set => _rigidBody = value; }
    public BaseGun EquippedGun { get => _equippedGun; set => _equippedGun = value; }

    private void OnEnable()
    {
        MovementAction.action.Enable();
    }
    private void OnDisable()
    {
        MovementAction.action.Disable();
    }
    private void Awake()
    {
        MovementAction.action.performed += OnPlayerMove;
        ControllerRigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _player = GetComponent<Player>();
        _gameManager = GameManager.instance;
        _canMove = true;   
        PlayerAnimator = GetComponentInChildren<Animator>();
        EquippedGun = GetComponentInChildren<BaseGun>();
    }
    private void Update()
    {
        if(_player.IsDead != true)
        {
            //PlayerInput();
            if (_gameManager.IsPaused != true)
            {
                FaceDirection();                
                /*if (_canMove != true)
                {
                    ControllerRigidBody.velocity = Vector3.zero;
                    PlayerAnimator.SetBool("isRuning", false);
                }**/
            }
        }
    }
    private void FixedUpdate()
    {
        MovementDirection(WorldDirection);
    }
    private void PlayerInput()
    {
        if(_gameManager.IsPaused != true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                FireGun();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player.InteractWithObject();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _gameManager.PowerUpMenuActive == false)
        {
            if(_gameManager.IsPaused == true)
            {
                _gameManager.ResumeGame();
            }
            else
            {
                _gameManager.PauseGame();
            }

        }
        
    }
    private void OnPlayerMove(InputAction.CallbackContext context) 
    {
        Vector2 input = context.ReadValue<Vector2>();
        WorldDirection.x = input.x;
        WorldDirection.y = input.y;
    }
    private void MovementDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            PlayerAnimator.SetBool("isRuning", false);
        }
        else
        {
        PlayerAnimator.SetBool("isRuning", true);

        }
        Vector3 currentVelocity = ControllerRigidBody.velocity;
        ControllerRigidBody.velocity = (direction.x * Vector3.right + direction.y * Vector3.forward) * MovementSpeed;
    }
    private void FaceDirection()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

    }

    private void FireGun()
    {
        if (EquippedGun != null)
        {
            StartCoroutine(MoveWaitTime());
            EquippedGun.Shoot();
        }  
    }
    IEnumerator MoveWaitTime() 
    {
        _canMove = false;
        yield return new WaitForSeconds(.2f);
        _canMove = true;
    }
}
