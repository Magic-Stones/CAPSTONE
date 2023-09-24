using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMechanics : MonoBehaviour
{
    public event EventHandler<OnChallengeEvent> OnQuizChallenge;
    public class OnChallengeEvent : EventArgs
    {
        public GameObject quizSheet;
    }

    private CharacterController2D _playerController2D;
    private MainBattle _mainBattle;

    void Awake()
    {
        _playerController2D = FindObjectOfType<CharacterController2D>();
        _mainBattle = FindObjectOfType<MainBattle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerChallenge(GameObject insertQuiz) 
    {
        _playerController2D.SetEnableMovement = false;
        OnQuizChallenge += _mainBattle.QuizChallengeEvent;
        OnQuizChallenge?.Invoke(this, new OnChallengeEvent { quizSheet = insertQuiz });
    }
}
