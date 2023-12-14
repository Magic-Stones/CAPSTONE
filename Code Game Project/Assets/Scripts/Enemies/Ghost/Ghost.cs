using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Ghost : MonoBehaviour, IEnemy
{
    [SerializeField] private int _healthPoints;
    public int SetHealthPoints { set { _healthPoints = value; } }

    [SerializeField] private float _searchRange = 3f;
    [SerializeField] private float _setMoveSpeed = 2f;
    private float _moveSpeed;

    [SerializeField] private bool _enableMovement = true;
    private Vector3 _targetDirection;
    private bool _isDefeated = false;

    private const string TARGET_TAG = "Player";
    private Transform _targetTransform;

    [Space(10)]
    [SerializeField] private QuizTemplate _quizTemplate;
    public QuizTemplate GetQuizTemplate { get { return _quizTemplate; } }
    [SerializeField] private List<GameObject> _lootRewards;
    private Transform _lootdropPoint;

    [Space(10)]
    [SerializeField] private AnimationClip _idleRight;
    [SerializeField] private AnimationClip _idleLeft;
    [SerializeField] private AnimationClip _animExorcised;
    private Animator _animator;
    [SerializeField] private Sprite _quizChallengePose;
    public Sprite GetChallengePose { get { return _quizChallengePose; } }
    private BoxCollider2D _box2D;
    private Rigidbody2D _rb2D;

    private PlayerInteraction _playerInteraction;
    private GameMechanics _mechanics;

    void Awake() 
    {
        _lootdropPoint = transform.Find("Lootdrop Point");

        _animator = GetComponentInChildren<Animator>();
        _box2D = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();

        _playerInteraction = GameObject.Find("Button - Interact").GetComponent<PlayerInteraction>();
        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (QuizTemplate.SerializedQuiz quiz in _quizTemplate.GetQuizList)
        {
            quiz.GetExtraInfo.questionPassed = false;
            _mechanics.GetQuestionList.Add(quiz);
        }

        _moveSpeed = _setMoveSpeed;
        _targetTransform = GameObject.FindGameObjectWithTag(TARGET_TAG).transform;
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

            Rigidbody2D lootRb2D = lootReward.GetComponent<Rigidbody2D>();
            lootRb2D.AddForce(GameMechanics.PopOutLoot * 5f, ForceMode2D.Impulse);
        }
    }

    public void OnQuizStart()
    {
        _mechanics.OnQuizCompletedEvent += OnDefeated;
        _mechanics.OnQuizLeaveEvent += OnUndefeated;
    }

    private void OnDefeated(object sender, GameMechanics.OnQuizCompletedEventHandler completedEvent)
    {
        if (!completedEvent.EnemyChallenger.Equals(gameObject)) return;

        _isDefeated = true;
        _box2D.enabled = false;

        _mechanics.OnQuizLeaveEvent -= OnUndefeated;
        _animator.Play(_animExorcised.name);
        Invoke(nameof(Death), _animExorcised.length);
    }

    private void Death()
    {
        DropLootRewards();
        _mechanics.OnQuizCompletedEvent -= OnDefeated;
        Destroy(gameObject);
    }

    private void OnUndefeated(object sender, GameMechanics.OnQuizLeaveEventHandler leaveEvent)
    {
        if (!leaveEvent.EnemyChallenger.Equals(gameObject)) return;

        _mechanics.OnQuizCompletedEvent -= OnDefeated;
        _mechanics.OnQuizLeaveEvent -= OnUndefeated;
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
