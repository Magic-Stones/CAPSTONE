using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameMechanics;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainUI;
    public GameObject GetMainUI { get { return _mainUI; } }
    [SerializeField] private GameObject _laptopEnergy;
    [SerializeField] private GameObject _scoreDisplay;
    public GameObject GetScoreDisplay { get { return _scoreDisplay; } }

    [Space(10)]
    [SerializeField] private GameObject _inventoryUI;
    public GameObject GetInventoryUI { get { return _inventoryUI; } }
    private CanvasGroup _canvasInventoryUI;
    [SerializeField] private ItemSelection _itemSelection;

    [Space(10)]
    [SerializeField] private GameObject _quizUIObject;
    public GameObject GetQuizUI { get { return _quizUIObject; } }
    private QuizUI _quizUI;

    [Space(10)]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _gameWinPanel;
    public GameObject GetGameWinPanel { get { return _gameWinPanel; } }
    [SerializeField] private GameObject _gameLosePanel;
    public GameObject GetGameLosePanel { get { return _gameLosePanel; } }

    private GameMechanics _mechanics;

    void Awake()
    {
        _canvasInventoryUI = _inventoryUI.GetComponent<CanvasGroup>();
        _quizUI = _quizUIObject.GetComponent<QuizUI>();

        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuizChallengeEvent(object sender, OnQuizEventHandler quizEvent)
    {
        _quizUI.QuizUISetActive(true);
        _mainUI.SetActive(false);

        _quizUI.SetupQuiz(quizEvent.EnemyChallenger, quizEvent.TemplateData);
        _laptopEnergy.GetComponent<LaptopEnergy>().QuizEventRelocate();

        _mechanics.OnQuizCompletedEvent += QuizChallengeCompleted;
        _mechanics.OnQuizLeaveEvent += QuizChallengeLeave;
    }

    public void SubmitItem(ItemSlot itemSlot)
    {
        _quizUI.DisplayAnswer(itemSlot.GetQuizAnswer);
        InventoryBagHide();
    }

    public void QuizChallengeCompleted(object sender, OnQuizCompletedEventHandler args)
    {
        _mainUI.SetActive(true);
        _quizUI.QuizUISetActive(false);

        _laptopEnergy.GetComponent<LaptopEnergy>().ReturnToMainUI();

        _mechanics.OnQuizLeaveEvent -= QuizChallengeLeave;
        _mechanics.OnQuizCompletedEvent -= QuizChallengeCompleted;
    }

    public void QuizChallengeLeave(object sender, OnQuizLeaveEventHandler args)
    {
        _mainUI.SetActive(true);
        _quizUI.QuizUISetActive(false);

        _laptopEnergy.GetComponent<LaptopEnergy>().ReturnToMainUI();

        _mechanics.OnQuizCompletedEvent -= QuizChallengeCompleted;
        _mechanics.OnQuizLeaveEvent -= QuizChallengeLeave;
    }

    public void InventoryBagShow()
    {
        InventoryUISetActive(true);
        if (_mechanics.GetGameState == GameState.Default)
        {
            _mainUI.SetActive(false);
            _itemSelection.GetSubmitButton.interactable = false;
        }

        if (_mechanics.GetGameState == GameState.QuizEvent)
        {
            _itemSelection.GetSubmitButton.interactable = true;
            _quizUI.QuizUISetActive(false);
        }
    }

    public void InventoryBagHide()
    {
        if (_mechanics.GetGameState == GameState.Default) _mainUI.SetActive(true);
        if (_mechanics.GetGameState == GameState.QuizEvent) _quizUI.QuizUISetActive(true);
        InventoryUISetActive(false);
    }

    private void InventoryUISetActive(bool isActive)
    {
        if (isActive)
        {
            _canvasInventoryUI.alpha = 1f;
            _canvasInventoryUI.interactable = true;
            _canvasInventoryUI.blocksRaycasts = true;
        }
        else
        {
            _canvasInventoryUI.alpha = 0f;
            _canvasInventoryUI.interactable = false;
            _canvasInventoryUI.blocksRaycasts = false;
        }
    }

    public void WinGame()
    {
        _gameWinPanel.SetActive(true);
        _mainUI.SetActive(false);
        if (_settingsPanel.activeInHierarchy) _settingsPanel.SetActive(false);
    }

    public void LoseGame()
    {
        _gameLosePanel.SetActive(true);
        _quizUI.QuizUISetActive(false);
        _mainUI.SetActive(false);
    }

    public void BTN_Settings()
    {
        _settingsPanel.SetActive(true);
        _mainUI.SetActive(false);
    }

    public void BTN_Resume()
    {
        _mainUI.SetActive(true);
        _settingsPanel.SetActive(false);
    }

    public void BTN_RestartGame()
    {
        _mechanics.GetQuestionList.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BTN_NextStage(int stage)
    {
        SceneManager.LoadScene($"Stage-{stage}");
    }

    public void BTN_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
