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
    [SerializeField] private ItemSelection _itemSelection;

    [Space(10)]
    [SerializeField] private GameObject _quizUI;
    public GameObject GetQuizUI { get { return _quizUI; } }

    [Space(10)]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _gameWinPanel;
    public GameObject GetGameWinPanel { get { return _gameWinPanel; } }
    [SerializeField] private GameObject _gameLosePanel;
    public GameObject GetGameLosePanel { get { return _gameLosePanel; } }

    private GameMechanics _mechanics;

    void Awake()
    {
        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!_inventoryUI.activeInHierarchy) _inventoryUI.SetActive(true);
        _inventoryUI.SetActive(false);

        if (!_quizUI.activeInHierarchy) _quizUI.SetActive(true);
        _quizUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuizChallengeEvent(object sender, OnQuizEventHandler quizEvent)
    {
        _quizUI.SetActive(true);
        _mainUI.SetActive(false);

        _quizUI.GetComponent<QuizUI>().SetupQuiz(quizEvent.EnemyChallenger, quizEvent.TemplateData);
        _laptopEnergy.GetComponent<LaptopEnergy>().QuizEventRelocate();

        _mechanics.OnQuizCompletedEvent += QuizChallengeCompleted;
        _mechanics.OnQuizLeaveEvent += QuizChallengeLeave;
    }

    public void SubmitItem(ItemSlot itemSlot)
    {
        QuizUI quizUI = _quizUI.GetComponent<QuizUI>();
        quizUI.DisplayAnswer(itemSlot.GetQuizAnswer);

        InventoryBagHide();
    }

    public void QuizChallengeCompleted(object sender, OnQuizCompletedEventHandler args)
    {
        _mainUI.SetActive(true);
        _quizUI.SetActive(false);

        _laptopEnergy.GetComponent<LaptopEnergy>().ReturnToMainUI();

        _mechanics.OnQuizLeaveEvent -= QuizChallengeLeave;
        _mechanics.OnQuizCompletedEvent -= QuizChallengeCompleted;
    }

    public void QuizChallengeLeave(object sender, OnQuizLeaveEventHandler args)
    {
        _mainUI.SetActive(true);
        _quizUI.SetActive(false);

        _laptopEnergy.GetComponent<LaptopEnergy>().ReturnToMainUI();

        _mechanics.OnQuizCompletedEvent -= QuizChallengeCompleted;
        _mechanics.OnQuizLeaveEvent -= QuizChallengeLeave;
    }

    public void InventoryBagShow()
    {
        _inventoryUI.SetActive(true);
        if (_mechanics.GetGameState == GameState.Default)
        {
            _mainUI.SetActive(false);
            _itemSelection.GetSubmitButton.interactable = false;
        }

        if (_mechanics.GetGameState == GameState.QuizEvent)
        {
            _itemSelection.GetSubmitButton.interactable = true;
            _quizUI.SetActive(false);
        }
    }

    public void InventoryBagHide()
    {
        if (_mechanics.GetGameState == GameState.Default) _mainUI.SetActive(true);
        if (_mechanics.GetGameState == GameState.QuizEvent) _quizUI.SetActive(true);
        _inventoryUI.SetActive(false);
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
        _quizUI.SetActive(false);
        _mainUI.SetActive(false);
    }

    public void Settings()
    {
        _settingsPanel.SetActive(true);
        _mainUI.SetActive(false);
    }

    public void Resume()
    {
        _mainUI.SetActive(true);
        _settingsPanel.SetActive(false);
    }

    public void RestartGame()
    {
        _mechanics.GetQuestionList.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
