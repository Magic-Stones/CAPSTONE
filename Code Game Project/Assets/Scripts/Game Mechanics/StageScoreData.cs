using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Stage Score Data", fileName = "StageScoreData")]
public class StageScoreData : ScriptableObject
{
    [System.Serializable]
    public class StageScore
    {
        public int score;
        public int totalQuestions;
        public string dateTime;
    }

    public List<StageScore> stageScores;
    public bool isScoreDataUpdated = false;

    public void AddScoreData(int newScore, int newTotalQuestions, string newDateTime)
    {
        stageScores.Add(new StageScore 
        { 
            score = newScore, 
            totalQuestions = newTotalQuestions, 
            dateTime = newDateTime
        });
    }
}
