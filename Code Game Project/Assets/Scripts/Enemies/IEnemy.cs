using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public QuizTemplate GetQuizTemplate { get; }
    public Sprite GetChallengePose { get; }
    public void OnQuizStart();
}
