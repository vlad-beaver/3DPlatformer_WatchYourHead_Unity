using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Infrastructure.Abstractables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationAndMovementController : PuzzlePlayer
{
    // Declare reference variables
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Animator _animator;
    [SerializeField]
    private Animator _headAnimator;
    private int _isAngryHash;
    private int _isJoyHash;
    [SerializeField]
    private Transform _cameraTransform;

    // Variables to store optimized setter/getter parameter IDs
    private int _isWalkingHash;
    private int _isRunningHash;

    // Variables to store player input values
    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;
    private Vector3 _appliedMovement;
    private Vector3 _moveDir;
    private Vector3 _moveDirRun;
    private bool _isMovementPressed;
    private bool _isRunPressed;

    // Rotation variables
    private readonly float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    // Speed variables
    [SerializeField]
    private float _velocity = 6.0f;
    [SerializeField]
    private float _runMultiplayer = 2.0f;

    // Gravity variables
    [SerializeField]
    private float _gravity = -11.0f;
    private readonly float _groundedGravity = -.05f;

    // Jumping variables
    private bool _isJumpPressed = false;
    private float _initialJumpVelocity;
    [SerializeField]
    private float _maxJumpHeight = 1.0f;
    private readonly float _maxJumpTime = 0.75f;
    private bool _isJumping = false;
    private int _isJumpingHash;
    private bool _isJumpAnimating = false;

    // PickUp variables
    [Space]
    [SerializeField]
    private LayerMask _pickupLayer;
    [SerializeField]
    private Transform _pickupTarget;
    [SerializeField]
    private float _pickupRange;
    [SerializeField]
    private Transform _robotHead;
    [SerializeField]
    private Transform _robotHeadGround;
    private bool _isPickUpPressed = false;
    private bool _isDropDownPressed = false;
    private Rigidbody _currentObjectRigidbody;
    private Collider _currentObjectCollider;
    private int _isTakingHeadHash;
    private int _isDropDownHeadHash;
    private int _isHavingHeadHash;
    private bool _isLockDropDown;

    // For taking head before starting game
    private void Start()
    {
        _isPickUpPressed = true;
        OnTriggerStay(_headAnimator.GetComponent<Collider>());
        _isPickUpPressed = false;
    }

    // Awake is called earlier than Start in Unity's event life cycle
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        // Set the parameter hash references
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isTakingHeadHash = Animator.StringToHash("isTakingHead");
        _isDropDownHeadHash = Animator.StringToHash("isDropDownHead");
        _isHavingHeadHash = Animator.StringToHash("isHavingHead");
        _isAngryHash = Animator.StringToHash("isAngry");
        _isJoyHash = Animator.StringToHash("isJoy");

        // Set the player input callbacks
        _playerInput.CharacterControls.Move.started += OnMovementInput;
        _playerInput.CharacterControls.Move.canceled += OnMovementInput;
        // "performed" for gamepad controller
        _playerInput.CharacterControls.Move.performed += OnMovementInput;
        _playerInput.CharacterControls.Run.started += OnRun;
        _playerInput.CharacterControls.Run.canceled += OnRun;
        _playerInput.CharacterControls.Jump.started += OnJump;
        _playerInput.CharacterControls.Jump.canceled += OnJump;
        _playerInput.CharacterControls.TakeHead.started += OnPickUp;
        _playerInput.CharacterControls.TakeHead.canceled += OnPickUp;
        _playerInput.CharacterControls.DropHead.started += OnDropDown;
        _playerInput.CharacterControls.DropHead.canceled += OnDropDown;

        SetupJumpVariables();
    }

    private void SetupJumpVariables()
    {
        float timeToApex = _maxJumpTime / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _currentRunMovement.x = _currentMovementInput.x * _runMultiplayer;
        _currentRunMovement.z = _currentMovementInput.y * _runMultiplayer;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    private void OnPickUp(InputAction.CallbackContext context)
    {
        _isPickUpPressed = context.ReadValueAsButton();
    }

    private void OnDropDown(InputAction.CallbackContext context)
    {
        _isDropDownPressed = context.ReadValueAsButton();
    }

    private void HandleRotation()
    {
        // The change in position our character should point to
        var direction = new Vector3(_currentMovement.x, 0.0f, _currentMovement.z).normalized;
        _moveDir = _currentMovement;
        _moveDirRun = _currentRunMovement;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            _moveDir = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, _currentMovement.y, _velocity);
            _moveDirRun = Quaternion.Euler(0f, targetAngle, 0f) * new Vector3(0f, _currentRunMovement.y, (_velocity * _runMultiplayer));
        }
    }

    private void HandleAnimation()
    {
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);
        bool isTakingHead = _animator.GetBool(_isTakingHeadHash);
        bool isDropDownHead = _animator.GetBool(_isDropDownHeadHash);
        bool isHavingHead = _animator.GetBool(_isHavingHeadHash);

        // Start walking if movement pressed is true and not already walking
        if (_isMovementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }
        // Stop walking if _isMovementPressed is false and not already walking
        else if (!_isMovementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }
        // Start running if running and movement pressed are true and not currently running
        if ((_isMovementPressed && _isRunPressed) && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }
        // Stop running if _isRunPressed and isWalking are false and currently running
        else if ((!_isMovementPressed || !_isRunPressed) && isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }
        // Start taking head after robot takes head and only if he doesn't have head
        else if ((_isPickUpPressed && !isTakingHead) && !_currentObjectRigidbody)
        {
            _animator.SetBool(_isTakingHeadHash, true);
        }
        // Stop taking head if robot doesn't take head
        else if (!_isPickUpPressed && isTakingHead)
        {
            _animator.SetBool(_isTakingHeadHash, false);
        }
        // Start drop down head if _isDropDownPressed and he has head
        else if (_isDropDownPressed && !isDropDownHead && _currentObjectRigidbody && !_isLockDropDown)
        {
            _animator.SetBool(_isDropDownHeadHash, true);
        }
        // Stop drop down head if !_isDropDownPressed
        else if (!_isDropDownPressed && isDropDownHead)
        {
            _animator.SetBool(_isDropDownHeadHash, false);
        }
        // Enable animations for headHoldingInHands if robot takes head
        else if (_currentObjectRigidbody && !isHavingHead)
        {
            _animator.SetBool(_isHavingHeadHash, true);
        }
        // Disable animations for headHoldingInHands if robot takes head
        else if (!_currentObjectRigidbody && isHavingHead)
        {
            _animator.SetBool(_isHavingHeadHash, false);
        }
        // Enable head angry animation when robot doesn't have head
        else if (!isHavingHead)
        {
            _headAnimator.SetBool(_isJoyHash, false);
            _headAnimator.SetBool(_isAngryHash, true);
        }
        // Enable head joy animation when robot walking with head
        else if (_isMovementPressed && isWalking)
        {
            _headAnimator.SetBool(_isAngryHash, false);
            _headAnimator.SetBool(_isJoyHash, true);
        }
        // Enable head idle animation when robot having head
        else if (isHavingHead)
        {
            _headAnimator.SetBool(_isAngryHash, false);
            _headAnimator.SetBool(_isJoyHash, false);
        }
    }

    private void HandleGravity()
    {
        bool isFalling = _currentMovement.y <= 0.0f || !_isJumpPressed;
        float fallMultiplier = 2.0f;
        if (_characterController.isGrounded)
        {
            if (_isJumpAnimating)
            {
                // Set jump animation false
                _animator.SetBool(_isJumpingHash, false);
                _isJumpAnimating = false;
            }

            _currentMovement.y = _groundedGravity;
            _appliedMovement.y = _groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (_gravity * fallMultiplier * Time.deltaTime);
            _appliedMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * .5f, -20.0f);
        }
        else
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (_gravity * Time.deltaTime);
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * .5f;
        }
    }

    private void HandleJump()
    {
        if (!_isJumping && _characterController.isGrounded && _isJumpPressed)
        {
            // Set jump animation true
            _animator.SetBool(_isJumpingHash, true);
            _isJumpAnimating = true;

            _isJumping = true;
            _currentMovement.y = _initialJumpVelocity;
            _appliedMovement.y = _initialJumpVelocity;
        }
        else if (!_isJumpPressed && _isJumping && _characterController.isGrounded)
        {
            _isJumping = false;
        }
    }

    // Placing the head in front of robot
    private void HandleDropDown()
    {
        if (_isDropDownPressed)
        {
            _isLockDropDown = false;

            if (Physics.SphereCast(transform.position, 1f, transform.forward, out var hit, 2f)
                && hit.transform.CompareTag("Wall"))
            {
                _isLockDropDown = true;
                return;
            }

            HasHead.Value = false;
            if (_currentObjectRigidbody)
            {
                _currentObjectRigidbody.isKinematic = false;
                _currentObjectCollider.enabled = true;
                _currentObjectRigidbody.position = _robotHeadGround.position;
                _currentObjectRigidbody.rotation = _robotHeadGround.rotation;
                _currentObjectRigidbody = null;
                _currentObjectCollider = null;
            }
        }
    }

    // Robot takes the head using trigger
    private void OnTriggerStay(Collider other)
    {
        if (!_isPickUpPressed)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            HasHead.Value = true;
            if (_currentObjectRigidbody)
            {
                _currentObjectRigidbody = other.GetComponent<Rigidbody>();
                _currentObjectCollider = other.GetComponent<BoxCollider>();
                _currentObjectRigidbody.isKinematic = true;
                _currentObjectCollider.enabled = false;
                // Disable physics for picked head
                _currentObjectRigidbody.isKinematic = true;
                _currentObjectCollider.enabled = false;
            }
            else
            {
                _currentObjectRigidbody = other.GetComponent<Rigidbody>();
                _currentObjectCollider = other.GetComponent<BoxCollider>();
                // Disable physics for picked head
                _currentObjectRigidbody.isKinematic = true;
                _currentObjectCollider.enabled = false;
            }
        }
    }

    void Update()
    {
        HandleRotation();

        if (_isRunPressed)
        {
            _appliedMovement.x = _moveDirRun.x;
            _appliedMovement.z = _moveDirRun.z;
        }
        else
        {
            _appliedMovement.x = _moveDir.x;
            _appliedMovement.z = _moveDir.z;
        }

        _characterController.Move(_appliedMovement * Time.deltaTime);

        HandleGravity();
        HandleJump();
        HandleDropDown();
        HandleAnimation();

        if (_currentObjectRigidbody)
        {
            _currentObjectRigidbody.position = _robotHead.position;
            _currentObjectRigidbody.rotation = _robotHead.rotation;
        }
    }

    private void OnEnable()
    {
        // Enable the character controls action map
        _playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        // Disable the character controls action map
        _playerInput.CharacterControls.Disable();
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void Kill()
    {
        var scene = SceneManager.GetActiveScene().name;
        DOTween.KillAll();
        SceneManager.LoadScene(scene);
    }
}
