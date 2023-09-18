using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBattle : MonoBehaviour
{
    private Transform _worldUI;
    private GameObject _quizUI;
    private GameMechanics _mechanics;

    void Awake() 
    {
        _worldUI = transform.Find("World UI");
        _mechanics = FindObjectOfType<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuizChallengeEvent(object sender, GameMechanics.OnChallengeEvent e) 
    {
        if (!_worldUI) return;
        _worldUI.gameObject.SetActive(false);

        _quizUI = Instantiate(e.quizSheet, transform);
        _quizUI.transform.SetParent(transform);
    }

    public void ChallengeComplete(IEnemy enemyChallenger)
    {
        _mechanics.OnQuizChallenge -= QuizChallengeEvent;

        if (_quizUI) Destroy(_quizUI, 1f);
        _worldUI.gameObject.SetActive(true);
        enemyChallenger.Death();
    }
}
