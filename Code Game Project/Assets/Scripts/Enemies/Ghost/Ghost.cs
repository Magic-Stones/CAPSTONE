using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Ghost : MonoBehaviour, IEnemy
{
    public int healthPoints = 3;

    [SerializeField] private float _searchRange = 3f;
    [SerializeField] private float _setMoveSpeed = 2f;
    private float _moveSpeed;

    private Vector3 _targetDirection;

    [SerializeField] private bool _enableMovement = true;

    private bool _isDefeated = false;
    public bool GetIsDefeated { get { return _isDefeated; } }

    private const string TARGET_TAG = "Player";
    private Transform _targetTransform;

    [SerializeField] private QuizTemplate _quizTemplate;
    public QuizTemplate GetQuizTemplate { get { return _quizTemplate; } }
    [SerializeField] private List<GameObject> _lootRewards;
    [SerializeField] private Transform _lootdropPoint;

    public GameObject GetEnemyObject { get { return gameObject; } }

    [SerializeField] private AnimationClip _idleRight, _idleLeft, _animExorcised;
    private Animator _animator;
    [SerializeField] private Sprite _quizChallengePose;
    public Sprite GetChallengePose { get { return _quizChallengePose; } }
    private BoxCollider2D _box2D;
    private Rigidbody2D _rb2D;

    private PlayerInteraction _playerInteraction;
    private GameMechanics _mechanics;

    void Awake() 
    {
        _animator = GetComponentInChildren<Animator>();
        _box2D = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();

        _playerInteraction = GameObject.Find("Button - Interact").GetComponent<PlayerInteraction>();
        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = _setMoveSpeed;
        _targetTransform = GameObject.FindGameObjectWithTag(TARGET_TAG).transform;
        _mechanics.OnQuizCompletedEvent += OnDefeated;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDefeated) FaceDirection();
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

    private void DropLootRewards()
    {
        StringBuilder builder;
        foreach (GameObject loot in _lootRewards)
        {
            GameObject lootReward = Instantiate(loot, _lootdropPoint.position, Quaternion.identity);
            builder = new StringBuilder(lootReward.name);
            builder.Replace(lootReward.name, loot.name);
            lootReward.name = builder.ToString();
            lootReward.transform.SetParent(_mechanics.GetHierarchyItem);
        }
    }

    public void OnDefeated(object sender, GameMechanics.OnQuizCompletedEventHandler completedEvent)
    {
        if (!completedEvent.EnemyChallenger.Equals(gameObject)) return;

        _isDefeated = true;
        _box2D.enabled = false;

        _animator.Play(_animExorcised.name);
        Invoke("Death", _animExorcised.length);
    }

    public void Death()
    {
        DropLootRewards();
        _mechanics.OnQuizCompletedEvent -= OnDefeated;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.CompareTag(TARGET_TAG))
        {
            _playerInteraction.ContainNearbyEnemy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(TARGET_TAG))
        {
            _playerInteraction.ContainNearbyEnemy(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRange);
    }
}
