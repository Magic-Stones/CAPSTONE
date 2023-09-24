using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Quiz/Fill in the Blanks", fileName = "FillBlanks")]
public class FillBlanksTemplate : ScriptableObject
{
    [Serializable]
    public struct SerializedQuiz
    {
        [SerializeField] private string name;

        [TextArea]
        public string question;

        public List<string> correctAnswers;
        public RectTransform inputField;
        public List<Vector2> inputFieldPositions;
    }

    public List<SerializedQuiz> quizList;
}
