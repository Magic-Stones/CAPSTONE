using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMechanics : MonoBehaviour
{
    public event EventHandler<OnQuizEventHandler> OnQuizEvent;
    public class OnQuizEventHandler : EventArgs
    {
        public GameObject EnemyChallenger { get; set; }
        public QuizTemplate TemplateData { get; set; }
    }

    public event EventHandler<OnQuizCompletedEventHandler> OnQuizCompletedEvent;
    public class OnQuizCompletedEventHandler : EventArgs
    {
        public GameObject EnemyChallenger { get; set; }
    }

    public event EventHandler<OnQuizLeaveEventHandler> OnQuizLeaveEvent;
    public class OnQuizLeaveEventHandler : EventArgs
    {
        public GameObject EnemyChallenger { get; set; }
    }

    public enum GameState
    {
        Default, 
        QuizEvent
    }
    private GameState _gameState;
    public GameState GetGameState { get { return _gameState; } }

    [SerializeField] private List<QuizTemplate.SerializedQuiz> _questions;
    public List<QuizTemplate.SerializedQuiz> GetQuestionList { get { return _questions; } }

    [SerializeField] private Transform _hierarchyItem;
    public Transform GetHierarchyItem { get { return _hierarchyItem; } }

    private CharacterController2D _playerController2D;
    private UIManager _uiManager;

    void Awake()
    {
        _playerController2D = FindObjectOfType<CharacterController2D>();
        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameState = GameState.Default;

        _uiManager.GetScoreDisplay.GetComponent<TextMeshProUGUI>().text =
            $"{Player.Instance.score}/{_questions.Count}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerChallenge(GameObject enemyChallenger, QuizTemplate quizTemplate) 
    {
        _gameState = GameState.QuizEvent;
        _playerController2D.SetEnableMovement = false;

        OnQuizEvent += _uiManager.QuizChallengeEvent;
        if (enemyChallenger.CompareTag("Enemy") || enemyChallenger.CompareTag("DungeonChest")) 
            enemyChallenger.GetComponent<IEnemy>().OnQuizStart();

        OnQuizEvent?.Invoke(this, new OnQuizEventHandler 
        { 
            EnemyChallenger = enemyChallenger, 
            TemplateData = quizTemplate
        });
    }

    public void ChallengeCompleted(GameObject enemyChallenger)
    {
        _gameState = GameState.Default;
        _playerController2D.SetEnableMovement = true;

        OnQuizEvent -= _uiManager.QuizChallengeEvent;

        OnQuizCompletedEvent?.Invoke(this, new OnQuizCompletedEventHandler { EnemyChallenger = enemyChallenger });
    }

    public void LeaveChallenge(GameObject enemyChallenger)
    {
        _gameState = GameState.Default;
        _playerController2D.SetEnableMovement = true;

        OnQuizEvent -= _uiManager.QuizChallengeEvent;

        OnQuizLeaveEvent?.Invoke(this, new OnQuizLeaveEventHandler { EnemyChallenger = enemyChallenger });
    }

    public void UpdateScore(int score)
    {
        Player.Instance.score += score;
        _uiManager.GetScoreDisplay.GetComponent<TextMeshProUGUI>().text =
            $"{Player.Instance.score}/{_questions.Count}";
    }

    public void WinGame()
    {
        _uiManager.WinGame();
    }

    public void LoseGame()
    {
        _uiManager.LoseGame();
    }

    public static Vector2 GetRandomDirection
    {
        get
        {
            float x = UnityEngine.Random.Range(-1f, 1f);
            float y = UnityEngine.Random.Range(-1f, 1f);
            Vector2 randomDirection = new Vector2(x, y);

            return randomDirection;
        }
    }

    public static Vector2 PopOutLootMotion
    {
        get
        {
            float x = UnityEngine.Random.Range(-1f, 1f);
            Vector2 randomDirectionX = new Vector2(x, 1f);

            return randomDirectionX;
        }
    }
}