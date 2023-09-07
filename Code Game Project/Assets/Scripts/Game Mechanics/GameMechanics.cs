using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMechanics : MonoBehaviour
{
    public event EventHandler<OnMainBattleEvent> OnMainBattle;
    public class OnMainBattleEvent : EventArgs
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

    public void TriggerMainBattle(GameObject insertQuiz) 
    {
        OnMainBattle?.Invoke(this, new OnMainBattleEvent { quizSheet = insertQuiz });
    }
}
