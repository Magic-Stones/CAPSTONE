using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopEnergy : MonoBehaviour
{
    [SerializeField] private Transform _laptopEnergyIcon;
    [SerializeField] private Transform _laptopEnergyContainer;

    [SerializeField] private float _laptopEnergyIconCellSize = 10f;

    private UIManager _uiManager;
    private GameMechanics _mechanics;

    void Awake()
    {
        _laptopEnergyContainer = transform.Find("Laptop Energy Container");

        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshIcons()
    {
        int x = 0;
        int y = 0;

        foreach (Transform icon in _laptopEnergyContainer)
        {
            Destroy(icon.gameObject);
        }

        for (int i = 0; i < Player.Instance.GetLaptopEnergy; i++)
        {
            RectTransform laptopEnergyIcon = 
                Instantiate(_laptopEnergyIcon, _laptopEnergyContainer).GetComponent<RectTransform>();
            laptopEnergyIcon.anchoredPosition = new Vector2(x * _laptopEnergyIconCellSize, y * _laptopEnergyIconCellSize);
            x++;
            if (x == 5)
            {
                y--;
                x = 0;
            }
        }
    }

    public void QuizEventRelocate()
    {
        gameObject.transform.SetParent(_uiManager.GetQuizUI.transform);
        _uiManager.GetScoreDisplay.transform.SetParent(_uiManager.GetQuizUI.transform.Find("Text - Your Score"));
    }

    public void ReturnToMainUI()
    {
        gameObject.transform.SetParent(_uiManager.GetMainUI.transform);
        _uiManager.GetScoreDisplay.transform.SetParent(_uiManager.GetMainUI.transform.Find("Text - Your Score"));
    }
}
