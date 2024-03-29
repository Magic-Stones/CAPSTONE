using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private QuizTemplate _quizTemplate;
    public QuizTemplate SetQuizTemplate { set { _quizTemplate = value; } }
    private int _questionIndex = 0;
    private string _correctAnswer;
    private string _submittedAnswer;

    private GameObject _popUps;
    private GameObject _popUpCorrect;
    private GameObject _popUpWrong;

    private GameObject _enemyChallenger;
    private TextMeshProUGUI _questionTMP;
    private Image _imgEnemy;
    private GameObject _answerBox;
    private Button _btnRunCode;

    public delegate void EventDelegate();
    private event EventDelegate _onWrongAnswer;

    private LaptopEnergy _laptopEnergy;
    private CanvasGroup _canvasGroup;
    private GameMechanics _mechanics;

    void Awake()
    {
        _popUps = transform.Find("Quiz Box").transform.GetChild(1).gameObject;
        _popUpCorrect = _popUps.transform.GetChild(0).gameObject;
        _popUpWrong = _popUps.transform.GetChild(1).gameObject;

        _questionTMP = transform.Find("Quiz Box").GetComponentInChildren<TextMeshProUGUI>();
        _imgEnemy = transform.Find("Enemy").GetComponentInChildren<Image>();
        _answerBox = transform.Find("Answer Box").gameObject;
        _btnRunCode = transform.Find("Button - Run the code").GetComponent<Button>();

        _laptopEnergy = GameObject.Find("Laptop Energy").GetComponent<LaptopEnergy>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _answerBox.SetActive(false);
        _btnRunCode.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupQuiz(GameObject enemy, QuizTemplate quizTemplate)
    {
        _enemyChallenger = enemy;
        _quizTemplate = quizTemplate;
        DisplayQuiz();
    }

    private void DisplayQuiz()
    {
        if (!_enemyChallenger) return;
        if (!_quizTemplate) return;

        if (_questionIndex < _quizTemplate.GetQuizList.Count)
        {
            _questionTMP.text = _quizTemplate.GetQuizList[_questionIndex].GetQuestion;
            _correctAnswer = _quizTemplate.GetQuizList[_questionIndex].GetCorrectAnswer;

            if (_questionIndex == 0) _imgEnemy.sprite = _enemyChallenger.GetComponent<IEnemy>().GetChallengePose;
        }
        else ChallengeComplete();
    }

    public void DisplayAnswer(string answer)
    {
        _submittedAnswer = answer;

        StringBuilder displayAnswer = new StringBuilder();
        displayAnswer.Append("Your answer: ");
        displayAnswer.Append(answer);

        _answerBox.SetActive(true);
        _answerBox.GetComponentInChildren<TextMeshProUGUI>().text = displayAnswer.ToString();
        _btnRunCode.interactable = true;
    }

    public void RunTheCode()
    {
        if (_submittedAnswer.Equals(_correctAnswer))
        {
            if (!_quizTemplate.GetQuizList[_questionIndex].GetExtraInfo.questionPassed)
            {
                _quizTemplate.GetQuizList[_questionIndex].GetExtraInfo.questionPassed = true;
                _mechanics.UpdateScore(1);
            }

            _popUpCorrect.SetActive(true);
            _popUpWrong.SetActive(false);
            _popUps.GetComponent<Animator>().SetTrigger("Pop up");

            _answerBox.SetActive(false);
            _btnRunCode.interactable = false;

            AudioAssets.Instance.audioSFX.clip = AudioAssets.Instance.sfxCorrectAnswer;
            AudioAssets.Instance.audioSFX.Play();

            _questionIndex++;
            DisplayQuiz();
        }
        else 
        {
            _popUpWrong.SetActive(true);
            _popUpCorrect.SetActive(false);
            _popUps.GetComponent<Animator>().SetTrigger("Pop up");

            _answerBox.SetActive(false);
            _btnRunCode.interactable = false;

            _onWrongAnswer?.Invoke();

            AudioAssets.Instance.audioSFX.clip = AudioAssets.Instance.sfxWrongAnswer;
            AudioAssets.Instance.audioSFX.Play();
        }
    }

    public void LeaveQuiz()
    {
        ResetQuiz();
        //_onWrongAnswer?.Invoke();
        _mechanics.LeaveChallenge(_enemyChallenger);
    }

    private void ChallengeComplete()
    {
        ResetQuiz();
        _mechanics.ChallengeCompleted(_enemyChallenger);
    }

    private void ResetQuiz()
    {
        _quizTemplate = null;
        _questionIndex = 0;
        _correctAnswer = string.Empty;
        _submittedAnswer = string.Empty;

        _questionTMP.text = "<question-box>";
        _imgEnemy.sprite = null;
        _answerBox.SetActive(false);
        _btnRunCode.interactable = false;
    }

    public void QuizUISetActive(bool isActive)
    {
        if (isActive)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            _onWrongAnswer += Player.Instance.TakeDamage;
            _onWrongAnswer += _laptopEnergy.RefreshIcons;

            DisplayQuiz();
        }
        else
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _onWrongAnswer -= Player.Instance.TakeDamage;
            _onWrongAnswer -= _laptopEnergy.RefreshIcons;
        }
    }
}
