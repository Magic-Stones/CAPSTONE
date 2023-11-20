using System;
using System.Collections;
using System.Collections.Generic;
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

    public enum GameState
    {
        Default, 
        QuizEvent
    }
    private GameState _gameState;
    public GameState GetGameState { get { return _gameState; } }

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
}