using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    public float ExternalMoveSpeed { get; set; } = 10f;
    [SerializeField] private Vector2 _moveDirection;
    private Rigidbody2D _rigidBody2D;   // Set Interpolate to "Interpolate"

    [SerializeField] private AnimationClip _idleRight, _idleLeft;
    [SerializeField] private AnimationClip _walkRight, _walkLeft;
    private int _directionIndex = 1;

    private PlayerInputActions _inputActions;
    private InputAction _inputMove;

    [SerializeField] private Animator _animatorSprite;

    void OnEnable()
    {
        _inputMove = _inputActions.Player.Move;
        _inputMove.Enable();
    }

    void OnDisable()
    {
        _inputMove.Disable();
    }

    void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        _inputActions = new PlayerInputActions();
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
        _moveDirection = _inputMove.ReadValue<Vector2>();

        if (_moveDirection.x > 0f) 
        {
            _directionIndex = 1;
        }
        else if (_moveDirection.x < 0f) 
        {
            _directionIndex = 2;
        }
        else 
        {
            _directionIndex = 1;
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
