using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizMultipleChoice : MonoBehaviour, IQuizBackend
{
    private int _quizIndex;

    private string _pressedBtnText;
    private string _correctAnswer;

    private bool _setDisplayQuiz = true;

    private int _quizTemplateIndex;
    public int SetQuizTemplateIndex { set { _quizTemplateIndex = value; } }

    [SerializeField] private MultipleChoiceTemplate _quizTemplate;
    private TextMeshProUGUI _questionTMP;
    [SerializeField] private List<TextMeshProUGUI> _buttonAnswersTMP;
    [SerializeField] private Image _imgEnemy;   // drag-drop Enemy.Image from editor
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
        _quizTemplate = _mainBattle.GetTemplateMultipleChoice[_quizTemplateIndex];
        _enemyChallenger = _player.GetChallenger;
        _imgEnemy.sprite = _enemyChallenger.GetComponent<IEnemy>().GetChallengePose;
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
                int lastIndex = _buttonAnswersTMP.Count - 1;
                while (lastIndex > 0)
                {
                    TextMeshProUGUI tempValue = _buttonAnswersTMP[lastIndex];
                    int randomIndex = UnityEngine.Random.Range(0, lastIndex);
                    _buttonAnswersTMP[lastIndex] = _buttonAnswersTMP[randomIndex];
                    _buttonAnswersTMP[randomIndex] = tempValue;
                    lastIndex--;
                }

                _questionTMP.text = _quizTemplate.quizList[_quizIndex].question;
                _buttonAnswersTMP[0].text = _quizTemplate.quizList[_quizIndex].answer1;
                _buttonAnswersTMP[1].text = _quizTemplate.quizList[_quizIndex].answer2;
                _buttonAnswersTMP[2].text = _quizTemplate.quizList[_quizIndex].answer3;
                _buttonAnswersTMP[3].text = _quizTemplate.quizList[_quizIndex].answer4;

                _correctAnswer = _quizTemplate.quizList[_quizIndex].CorrectAnswer;
            }
            _setDisplayQuiz = false;
        }
    }

    private void CheckAnswer()
    {
        if (_pressedBtnText.Equals(_correctAnswer))
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
        _pressedBtnText = transform.Find("Button 1").GetComponentInChildren<TextMeshProUGUI>().text;

        CheckAnswer();
    }

    public void ButtonPressed_2()
    {
        _pressedBtnText = transform.Find("Button 2").GetComponentInChildren<TextMeshProUGUI>().text;

        CheckAnswer();
    }

    public void ButtonPressed_3()
    {
        _pressedBtnText = transform.Find("Button 3").GetComponentInChildren<TextMeshProUGUI>().text;

        CheckAnswer();
    }

    public void ButtonPressed_4()
    {
        _pressedBtnText = transform.Find("Button 4").GetComponentInChildren<TextMeshProUGUI>().text;

        CheckAnswer();
    }
}
