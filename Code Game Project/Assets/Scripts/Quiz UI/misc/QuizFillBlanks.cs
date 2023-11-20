using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizFillBlanks : MonoBehaviour
{
    private int _quizIndex;
    [SerializeField] private GameObject _inputFieldPrefab;

    private string _playerInput;
    private string _correctAnswer;

    private bool _setDisplayQuiz = true;

    [SerializeField] private FillBlanksTemplate _quizTemplate;
    private TextMeshProUGUI _questionTMP;
    [SerializeField] private Image _imgEnemy;
    public GameObject _enemyChallenger;
    private Player _player;
    private MainBattle _mainBattle;

    void Awake()
    {
        _questionTMP = transform.Find("Quiz Box").GetComponentInChildren<TextMeshProUGUI>();
        _player = FindObjectOfType<Player>();
        _mainBattle = GetComponentInParent<MainBattle>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
                _questionTMP.text = _quizTemplate.quizList[_quizIndex].question;
                RectTransform inputField;
                int instantiateCount = _quizTemplate.quizList[_quizIndex].inputFieldPositions.Count;
                while (instantiateCount > 0)
                {
                    int positionIndex = instantiateCount - 1;
                    inputField = Instantiate(_quizTemplate.quizList[_quizIndex].inputField, transform);
                    inputField.anchoredPosition = _quizTemplate.quizList[_quizIndex].inputFieldPositions[positionIndex];
                    instantiateCount--;
                }
            }
            _setDisplayQuiz = false;
        }
    }

    private void CheckAnswers(List<TMP_InputField> inputFields)
    {
        int inputs = inputFields.Count;
        int inputIndex = 0;
        bool stopCheck = false;
        while (inputs > 0 && !stopCheck)
        {
            int correctAnswerIndex = inputs - 1;
            _playerInput = inputFields[inputIndex].text;
            _correctAnswer = _quizTemplate.quizList[_quizIndex].correctAnswers[correctAnswerIndex];

            if (_playerInput.Equals(_correctAnswer))
            {
                inputs--;
                inputIndex++;
            }
            else stopCheck = true;
        }

        if (!stopCheck)
        {
            _setDisplayQuiz = true;

            foreach (GameObject input in GameObject.FindGameObjectsWithTag(_inputFieldPrefab.tag))
            {
                Destroy(input);
            }

            _quizIndex++;
            if (_quizIndex == _quizTemplate.quizList.Count) ChallengeComplete();
        }
        //else Debug.Log("Wrong Answer(s)!");
    }

    private void ChallengeComplete()
    {
        IEnemy enemyAtb = _enemyChallenger.GetComponent<IEnemy>();
        _mainBattle.ChallengeComplete(enemyAtb);
    }

    public void PressTheAttack()
    {
        List<TMP_InputField> inputFields = new List<TMP_InputField>();
        foreach (GameObject inputObject in GameObject.FindGameObjectsWithTag(_inputFieldPrefab.tag))
        {
            inputFields.Add(inputObject.GetComponent<TMP_InputField>());
        }

        CheckAnswers(inputFields);
    }
}
