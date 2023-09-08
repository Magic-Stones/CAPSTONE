using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [HideInInspector] public GameObject enemyChallenger;
    [SerializeField] private Image _imgEnemy;

    public Action _activeCallback;

    // Start is called before the first frame update
    void Start()
    {
        _imgEnemy.sprite = enemyChallenger.GetComponent<IEnemy>().GetSpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectAnswer()
    {
        IEnemy enemyAtb = enemyChallenger.GetComponent<IEnemy>();
        enemyAtb.Death();
    }
}
