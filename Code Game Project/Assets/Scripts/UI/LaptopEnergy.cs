using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopEnergy : MonoBehaviour
{
    [SerializeField] private Transform _laptopEnergyIcon;
    [SerializeField] private Transform _laptopEnergyContainer;

    [SerializeField] private float _laptopEnergyIconCellSize = 10f;
    private int x = 0;
    private int y = 0;

    void Awake()
    {
        _laptopEnergyContainer = transform.Find("Laptop Energy Container");
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
}
