using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] private StageData _stageData;
    [SerializeField] private int _stageNumber;

    [Space(10)]
    [SerializeField] private List<QuizTemplate.SerializedQuiz> _questions;
    public List<QuizTemplate.SerializedQuiz> GetQuestionList { get { return _questions; } }

    private bool _isGameLose = false;
    public void SetGameLose() => _isGameLose = true;

    [SerializeField] private Transform _hierarchyItem;
    public Transform GetHierarchyItem { get { return _hierarchyItem; } }

    [SerializeField] private StageScoreData _stageScoreData;

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
        if (_isGameLose)
        {
            LoseGame();
            _isGameLose = false;
        }
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

        AudioAssets.Instance.audioSFX.clip = AudioAssets.Instance.sfxChallengeEnemy;
        AudioAssets.Instance.audioSFX.Play();
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

        _stageScoreData.AddScoreData(Player.Instance.score, _questions.Count, DateTime.Now.ToString());

        switch (_stageNumber)
        {
            case 1:
                PlayerPrefs.SetInt("stage2", 1);
                _stageData.isStage2Unlocked = true;
                break;

            case 2:
                PlayerPrefs.SetInt("stage3", 1);
                _stageData.isStage3Unlocked = true;
                break;

            case 3:
                PlayerPrefs.SetInt("stage4", 1);
                _stageData.isStage4Unlocked = true;
                break;

            case 4:
                PlayerPrefs.SetInt("stage5", 1);
                _stageData.isStage5Unlocked = true;
                break;

            case 5:
                PlayerPrefs.SetInt("stage6", 1);
                _stageData.isStage6Unlocked = true;
                break;

            case 6:
                PlayerPrefs.SetInt("stage7", 1);
                _stageData.isStage7Unlocked = true;
                break;
        }

        AudioAssets.Instance.audioSFX.clip = AudioAssets.Instance.sfxWinGame;
        AudioAssets.Instance.audioSFX.Play();
    }

    public void LoseGame()
    {
        _uiManager.LoseGame();

        _stageScoreData.AddScoreData(Player.Instance.score, _questions.Count, DateTime.Now.ToString());

        AudioAssets.Instance.audioSFX.clip = AudioAssets.Instance.sfxLoseGame;
        AudioAssets.Instance.audioSFX.Play();
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