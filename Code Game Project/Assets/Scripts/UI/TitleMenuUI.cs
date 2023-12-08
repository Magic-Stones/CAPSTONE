using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _titleMenu;
    [SerializeField] private GameObject _stageSelection;
    [SerializeField] private GameObject _aboutModule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageSelection(int stageNumber)
    {
        if (stageNumber == 0) SceneManager.LoadScene("StageTutorial");
        else SceneManager.LoadScene($"Stage-{stageNumber}");
    }

    public void StartPlay()
    {
        _stageSelection.SetActive(true);
        _titleMenu.SetActive(false);
        _aboutModule.SetActive(false);
    }

    public void TitleScreen()
    {
        _titleMenu.SetActive(true);
        _stageSelection.SetActive(false);
        _aboutModule.SetActive(false);
    }

    public void About()
    {
        _aboutModule.SetActive(true);
        _titleMenu.SetActive(false);
        _stageSelection.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application QUIT!");
    }
}
