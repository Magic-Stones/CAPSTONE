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
    [SerializeField] private GameObject _inventoryUI;
    public GameObject GetInventoryUI { get { return _inventoryUI; } }
    [SerializeField] private GameObject _quizUI;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private ItemSelection _itemSelection;

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

        //_quizUI.GetComponent<QuizUI>().SetQuizTemplate = quizEvent.TemplateData;
        _quizUI.GetComponent<QuizUI>().SetupQuiz(quizEvent.EnemyChallenger, quizEvent.TemplateData);
        _mechanics.OnQuizCompletedEvent += QuizChallengeCompleted;
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

        _mechanics.OnQuizCompletedEvent -= QuizChallengeCompleted;
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

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
