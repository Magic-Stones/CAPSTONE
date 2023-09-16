using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Quiz", fileName = "Quiz Type")]
public class QuizUITemplate : ScriptableObject
{
    [Serializable]
    public class SerializedQuiz
    {
        [SerializeField] private string name;
        public string question;
        public string buttonAnswer_1;
        public string buttonAnswer_2;
        public string buttonAnswer_3;
        public string buttonAnswer_4;
        public int correctButtonNumber;
    }

    public List<SerializedQuiz> quizList;
}
