using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Quiz/Multiple Choice", fileName = "MultipleChoice")]
public class MultipleChoiceTemplate : ScriptableObject
{
    [Serializable]
    public struct SerializedQuiz
    {
        [SerializeField] private string name;
        public string question;
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public string CorrectAnswer { get { return answer1; } }
    }

    public List<SerializedQuiz> quizList;
}
