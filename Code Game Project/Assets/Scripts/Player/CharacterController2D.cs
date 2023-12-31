using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    public float ExternalMoveSpeed { get; set; } = 10f;
    [SerializeField] private Vector2 _moveDirection;
    private bool enableMovement = true;
    public bool SetEnableMovement { set { enableMovement = value; } }
    private Rigidbody2D _rigidBody2D;   // Set Interpolate to "Interpolate"

    [SerializeField] private AnimationClip _idleRight, _idleLeft;
    [SerializeField] private AnimationClip _walkRight, _walkLeft;
    private int _directionIndex = 1;

    [SerializeField] private Animator _animatorSprite;

    private GameControls _controls;
    private InputAction _movement;

    void OnEnable()
    {
        _movement = _controls.Player.Movement;
        _movement.Enable();
    }

    void OnDisable()
    {
        _movement.Disable();
    }

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        _controls = new GameControls();
    }

    // Start is called before the first frame updates
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        CharacterAnimation();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void ProcessInput()
    {
        if (enableMovement) _moveDirection = _movement.ReadValue<Vector2>();
        else _moveDirection = Vector2.zero;

        if (_moveDirection.x > 0f) 
        {
            _directionIndex = 1;
        }
        else if (_moveDirection.x < 0f) 
        {
            _directionIndex = 2;
        }
    }

    private void MoveCharacter()
    {
        _rigidBody2D.velocity = _moveDirection * ExternalMoveSpeed;
    }

    private void CharacterAnimation() 
    {
        switch (_directionIndex)
        {
            case 1:

                if (_moveDirection != Vector2.zero) 
                {
                    _animatorSprite.Play(_walkRight.name);
                }
                else 
                {
                    _animatorSprite.Play(_idleRight.name);
                }

                break;

            case 2:

                if (_moveDirection != Vector2.zero) 
                {
                    _animatorSprite.Play(_walkLeft.name);
                }
                else 
                {
                    _animatorSprite.Play(_idleLeft.name);
                }

                break;
        }
    }
}
