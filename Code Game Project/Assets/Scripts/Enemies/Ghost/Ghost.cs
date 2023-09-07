using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float searchRange = 3f;
    [SerializeField] private float setMoveSpeed = 2f;
    private float _moveSpeed;

    private Vector3 targetDirection;

    private bool _enableMovement = true;

    [SerializeField] private GameObject player;
    private Transform _targetTransform;
    [SerializeField] private GameObject quizSheet;

    [SerializeField] private AnimationClip idleRight, idleLeft;

    private Animator _animator;
    private Rigidbody2D _rb2D;
    private GameMechanics _mechanics;

    void Awake() 
    {
        _animator = GetComponentInChildren<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        _mechanics = FindObjectOfType<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = setMoveSpeed;
        _targetTransform = GameObject.FindGameObjectWithTag(player.tag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        FaceDirection();
    }

    void FixedUpdate()
    {
        if (_enableMovement) FindTarget();
    }

    private void FindTarget() 
    {
        targetDirection = (_targetTransform.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, _targetTransform.position) < searchRange)
        {
            _rb2D.MovePosition(transform.position + (targetDirection * _moveSpeed * Time.deltaTime));
        }
    }

    private void FaceDirection()
    {
        if (targetDirection.x >= 0f) _animator.Play(idleRight.name);
        else _animator.Play(idleLeft.name);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag.Equals(player.tag))
        {
            _mechanics.TriggerMainBattle(quizSheet);
            _enableMovement = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}
