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
        OnQuizChallenge?.Invoke(this, new OnChallengeEvent { quizSheet = insertQuiz });
    }
}
