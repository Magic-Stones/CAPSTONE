using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _titleMenu;
    [SerializeField] private GameObject _aboutModule;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TitleScreen()
    {
        _titleMenu.SetActive(true);
        _aboutModule.SetActive(false);
    }

    public void About()
    {
        _aboutModule.SetActive(true);
        _titleMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application QUIT!");
    }
}
