using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Quiz Template", fileName = "QuizTemplate")]
public class QuizTemplate : ScriptableObject
{
    [Serializable]
    public struct SerializedQuiz
    {
        public string name;

        [TextArea]
        [SerializeField] private string _question;
        public string GetQuestion { get { return _question; } }

        [SerializeField] private string _correctAnswer;
        public string GetCorrectAnswer { get { return _correctAnswer; } }

        [SerializeField] private int _addPoints;
        public int GetAddPoints { get { return _addPoints; } }

        [Serializable]
        public class ExtraInformation
        {
            public bool questionPassed = false;
        }
        [SerializeField] private ExtraInformation _extraInformation;
        public ExtraInformation GetExtraInfo { get { return _extraInformation; } }
    }

    [SerializeField] private List<SerializedQuiz> _quizList;
    public List<SerializedQuiz> GetQuizList { get { return _quizList; } }
}
