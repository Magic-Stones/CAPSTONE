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
    }

    [SerializeField] private List<SerializedQuiz> _quizList;
    public List<SerializedQuiz> GetQuizList { get { return _quizList; } }
}
