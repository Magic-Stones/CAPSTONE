using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainUI;
    [SerializeField] private GameObject _settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void BTN_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
