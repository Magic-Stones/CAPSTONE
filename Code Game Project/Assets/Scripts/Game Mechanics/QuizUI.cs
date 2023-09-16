using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    private int _quizIndex;

    private int _pressedBtnNum;
    private int _correctBtnNum;

    private bool _setDisplayQuiz = true;

    [HideInInspector] public GameObject enemyChallenger;
    [SerializeField] private QuizUITemplate _quizTemplate;
    [SerializeField] private Image _imgEnemy;
    [SerializeField] private List<TextMeshProUGUI> _buttonAnswersTMP;
    private TextMeshProUGUI _questionTMP;
    private MainBattle _mainBattle;

    void Awake()
    {
        _questionTMP = transform.Find("Quiz Box").GetComponentInChildren<TextMeshProUGUI>();

        _buttonAnswersTMP = new List<TextMeshProUGUI>();
        _buttonAnswersTMP.Add(transform.Find("Button 1").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 2").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 3").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 4").GetComponentInChildren<TextMeshProUGUI>());

        _mainBattle = GetComponentInParent<MainBattle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _imgEnemy.sprite = enemyChallenger.GetComponent<IEnemy>().GetSpriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayQuiz();
    }

    private void DisplayQuiz()
    {
        if (_quizIndex < _quizTemplate.quizList.Count)
        {
            if (_setDisplayQuiz)
            {
                _questionTMP.text = _quizTemplate.quizList[_quizIndex].question;
                _buttonAnswersTMP[0].text = _quizTemplate.quizList[_quizIndex].buttonAnswer_1;
                _buttonAnswersTMP[1].text = _quizTemplate.quizList[_quizIndex].buttonAnswer_2;
                _buttonAnswersTMP[2].text = _quizTemplate.quizList[_quizIndex].buttonAnswer_3;
                _buttonAnswersTMP[3].text = _quizTemplate.quizList[_quizIndex].buttonAnswer_4;

                _correctBtnNum = _quizTemplate.quizList[_quizIndex].correctButtonNumber;
            }
            _setDisplayQuiz = false;
        }
    }

    private void CheckAnswer()
    {
        if (_pressedBtnNum == _correctBtnNum)
        {
            _setDisplayQuiz = true;

            _quizIndex++;
            if (_quizIndex == _quizTemplate.quizList.Count) ChallengeComplete();
        }
        else
        {
            Debug.Log("WRONG");
        }
    }

    private void ChallengeComplete()
    {
        IEnemy enemyAtb = enemyChallenger.GetComponent<IEnemy>();
        _mainBattle.ChallengeComplete(enemyAtb);
    }

    public void ButtonPressed_1()
    {
        _pressedBtnNum = 1;

        CheckAnswer();
    }

    public void ButtonPressed_2()
    {
        _pressedBtnNum = 2;

        CheckAnswer();
    }

    public void ButtonPressed_3()
    {
        _pressedBtnNum = 3;

        CheckAnswer();
    }

    public void ButtonPressed_4()
    {
        _pressedBtnNum = 4;

        CheckAnswer();
    }
}
