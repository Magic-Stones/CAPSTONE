using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _generalUI;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _inventoryUI;
    private bool _enableInventory = false;
    private bool _inventorySwitch;

    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnInventoryCallback = () => { _enableInventory = !_enableInventory; };
        _inventorySwitch = _enableInventory;
        _inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inventorySwitch != _enableInventory)
        {
            _inventoryUI.SetActive(_enableInventory);
            _inventorySwitch = _enableInventory;
        }
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
