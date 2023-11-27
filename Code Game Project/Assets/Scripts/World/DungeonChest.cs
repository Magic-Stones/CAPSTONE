using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DungeonChest : MonoBehaviour, IEnemy
{
    [SerializeField] private QuizTemplate _quizTemplate;
    public QuizTemplate GetQuizTemplate { get { return _quizTemplate; } }
    [SerializeField] private List<GameObject> _lootRewards;

    [SerializeField] private Sprite _quizChallengePose;
    public Sprite GetChallengePose { get { return _quizChallengePose; } }
    private BoxCollider2D _box2D;

    private PlayerInteraction _playerInteraction;
    private GameMechanics _mechanics;

    void Awake()
    {
        _quizChallengePose = GetComponentInChildren<SpriteRenderer>().sprite;
        _box2D = GetComponent<BoxCollider2D>();

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DropLootRewards()
    {
        StringBuilder builder;
        foreach (GameObject loot in _lootRewards)
        {
            GameObject lootReward = Instantiate(loot, transform.position, Quaternion.identity);
            builder = new StringBuilder(lootReward.name);
            builder.Replace(lootReward.name, loot.name);
            lootReward.name = builder.ToString();
            lootReward.transform.SetParent(_mechanics.GetHierarchyItem);
        }
    }

    public void OnQuizStart()
    {
        _mechanics.OnQuizCompletedEvent += OnUnlocked;
        _mechanics.OnQuizLeaveEvent += OnUndefeated;
    }

    private void OnUnlocked(object sender, GameMechanics.OnQuizCompletedEventHandler completedEvent)
    {
        if (!completedEvent.EnemyChallenger.Equals(gameObject)) return;

        _box2D.enabled = false;

        Invoke(nameof(UnlockChest), 1f);
    }

    private void UnlockChest()
    {
        DropLootRewards();
        _mechanics.OnQuizCompletedEvent -= OnUnlocked;
        Destroy(gameObject);
    }

    private void OnUndefeated(object sender, GameMechanics.OnQuizLeaveEventHandler leaveEvent)
    {
        if (!leaveEvent.EnemyChallenger.Equals(gameObject)) return;

        _mechanics.OnQuizCompletedEvent -= OnUnlocked;
        _mechanics.OnQuizLeaveEvent -= OnUndefeated;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerInteraction.ContainNearbyChest(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerInteraction.ContainNearbyChest(null);
        }
    }
}
