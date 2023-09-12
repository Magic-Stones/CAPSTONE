using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    private int _questionNum;
    private int _questionNumLimit;

    private int _pressedBtnNum;
    private int _correctBtnNum;

    private bool _setDisplayQuiz = true;

    [HideInInspector] public GameObject enemyChallenger;
    [SerializeField] private Image _imgEnemy;
    [SerializeField] private List<TextMeshProUGUI> _buttonAnswersTMP;
    private TextMeshProUGUI _questionTMP;

    void Awake()
    {
        _questionTMP = transform.Find("Quiz Box").GetComponentInChildren<TextMeshProUGUI>();

        _buttonAnswersTMP = new List<TextMeshProUGUI>();
        _buttonAnswersTMP.Add(transform.Find("Button 1").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 2").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 3").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 4").GetComponentInChildren<TextMeshProUGUI>());
    }

    // Start is called before the first frame update
    void Start()
    {
        _imgEnemy.sprite = enemyChallenger.GetComponent<IEnemy>().GetSpriteRenderer.sprite;

        _questionNum = 1;
        _questionNumLimit = 3;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayQuiz();
    }

    private void DisplayQuiz()
    {
        switch (_questionNum)
        {
            case 1:
                if (_setDisplayQuiz)
                {
                    _questionTMP.text = "public ___ score = 50;";
                    _buttonAnswersTMP[0].text = "float";
                    _buttonAnswersTMP[1].text = "int";
                    _buttonAnswersTMP[2].text = "string";
                    _buttonAnswersTMP[3].text = "bool";

                    _correctBtnNum = 2;
                }
                _setDisplayQuiz = false;
                break;

            case 2:
                if (_setDisplayQuiz)
                {
                    _questionTMP.text = "private ___ name = \"Ricardo\";";
                    _buttonAnswersTMP[0].text = "char";
                    _buttonAnswersTMP[1].text = "double";
                    _buttonAnswersTMP[2].text = "int";
                    _buttonAnswersTMP[3].text = "string";

                    _correctBtnNum = 4;
                }
                _setDisplayQuiz = false;
                break;

            case 3:
                if (_setDisplayQuiz)
                {
                    _questionTMP.text = "const string AMBATU = ___;";
                    _buttonAnswersTMP[0].text = "KHAN";
                    _buttonAnswersTMP[1].text = "BLOU";
                    _buttonAnswersTMP[2].text = "BASING";
                    _buttonAnswersTMP[3].text = "NAT";

                    _correctBtnNum = 1;
                }
                _setDisplayQuiz = false;
                break;
        }
    }

    private void CheckAnswer()
    {
        if (_pressedBtnNum == _correctBtnNum)
        {
            _setDisplayQuiz = true;

            if (_questionNum != _questionNumLimit) _questionNum++;
            else ChallengeComplete();

            Debug.Log("CORRECT");
        }
        else
        {
            Debug.Log("WRONG");
        }
    }

    private void ChallengeComplete()
    {
        Destroy(gameObject, 1f);

        IEnemy enemyAtb = enemyChallenger.GetComponent<IEnemy>();
        enemyAtb.Death();
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
