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

    [SerializeField] private QuizUITemplate _quizTemplate;
    [SerializeField] private Image _imgEnemy;
    [SerializeField] private List<TextMeshProUGUI> _buttonAnswersTMP;
    private TextMeshProUGUI _questionTMP;
    private GameObject _enemyChallenger;
    private Player _player;
    private MainBattle _mainBattle;

    void Awake()
    {
        _questionTMP = transform.Find("Quiz Box").GetComponentInChildren<TextMeshProUGUI>();

        _buttonAnswersTMP = new List<TextMeshProUGUI>();
        _buttonAnswersTMP.Add(transform.Find("Button 1").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 2").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 3").GetComponentInChildren<TextMeshProUGUI>());
        _buttonAnswersTMP.Add(transform.Find("Button 4").GetComponentInChildren<TextMeshProUGUI>());

        _player = FindObjectOfType<Player>();
        _mainBattle = GetComponentInParent<MainBattle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _enemyChallenger = _player.GetChallenger;
        _imgEnemy.sprite = _enemyChallenger.GetComponent<IEnemy>().GetSpriteRenderer.sprite;
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
        IEnemy enemyAtb = _enemyChallenger.GetComponent<IEnemy>();
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
