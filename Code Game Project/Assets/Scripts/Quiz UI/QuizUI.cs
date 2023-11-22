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

    private GameObject _enemyChallenger;
    private TextMeshProUGUI _questionTMP;
    private Image _imgEnemy;
    private GameObject _answerBox;
    private Button _btnRunCode;

    private UIManager _uiManager;
    private GameMechanics _mechanics;

    void Awake()
    {
        _questionTMP = transform.Find("Quiz Box").GetComponentInChildren<TextMeshProUGUI>();
        _imgEnemy = transform.Find("Enemy").GetComponentInChildren<Image>();
        _answerBox = transform.Find("Answer Box").gameObject;
        _btnRunCode = transform.Find("Button - Run the code").GetComponent<Button>();

        _uiManager = GetComponentInParent<UIManager>();
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

    void OnEnable()
    {
        DisplayQuiz();
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
            _questionIndex++;
            DisplayQuiz();
        }
        else { Debug.Log("WRONG!"); }
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
}