using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _generalUI;
    [SerializeField] private GameObject _settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Settings()
    {
        _settingsPanel.SetActive(true);
        _generalUI.SetActive(false);
    }

    public void Resume()
    {
        _generalUI.SetActive(true);
        _settingsPanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
