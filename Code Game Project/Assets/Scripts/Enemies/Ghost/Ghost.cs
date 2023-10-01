using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IEnemy
{
    public int healthPoints = 3;

    [SerializeField] private float _searchRange = 3f;
    [SerializeField] private float _setMoveSpeed = 2f;
    private float _moveSpeed;

    private Vector3 _targetDirection;

    private bool _enableMovement = true;

    private bool _isDefeated = false;
    public bool GetIsDefeated { get { return _isDefeated; } }

    private const string TARGET_TAG = "Player";
    private Transform _targetTransform;
    [SerializeField] private GameObject _quizSheet;

    [SerializeField] private AnimationClip _idleRight, _idleLeft;
    private Animator _animator;
    [SerializeField] private Sprite _quizChallengePose;
    public Sprite GetChallengePose { get { return _quizChallengePose; } }
    private BoxCollider2D _box2D;
    private Rigidbody2D _rb2D;
    private GameMechanics _mechanics;

    void Awake() 
    {
        _animator = GetComponentInChildren<Animator>();
        _box2D = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
        _mechanics = FindObjectOfType<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = _setMoveSpeed;
        _targetTransform = GameObject.FindGameObjectWithTag(TARGET_TAG).transform;
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
        _targetDirection = (_targetTransform.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, _targetTransform.position) < _searchRange)
        {
            _rb2D.MovePosition(transform.position + (_targetDirection * _moveSpeed * Time.deltaTime));
        }
    }

    private void FaceDirection()
    {
        if (_targetDirection.x >= 0f) _animator.Play(_idleRight.name);
        else _animator.Play(_idleLeft.name);
    }

    public void Death()
    {
        _isDefeated = true;
        _box2D.enabled = false;
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag.Equals(TARGET_TAG))
        {
            _mechanics.TriggerChallenge(_quizSheet);
            _enableMovement = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRange);
    }
}
