using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private Transform _itemSlot;

    [SerializeField] private float _itemSlotCellSize = 10f;
    private int x = 0;
    private int y = 0;

    [SerializeField] private List<InventoryItem> _inventoryList;

    private Inventory _inventory;

    void Awake()
    {
        _itemContainer = transform.Find("Item Container");

        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        _inventory.OnInventoryUpdateCallback = RefreshInventoryItems;

        _inventoryList = new List<InventoryItem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RefreshInventoryItems()
    {
        foreach (InventoryItem inventoryItem in _inventory.GetInventoryList)
        {
            if (!_inventoryList.Contains(inventoryItem))
            {
                RectTransform itemSlot = Instantiate(_itemSlot, _itemContainer).GetComponent<RectTransform>();
                itemSlot.anchoredPosition = new Vector2(x * _itemSlotCellSize, y * _itemSlotCellSize);
                itemSlot.GetComponent<ItemSlot>().inventoryItem = inventoryItem;
                x++;
                if (x == 5)
                {
                    y--;
                    x = 0;
                }

                _inventoryList.Add(inventoryItem);
            }
            else continue;
        }
    }
}
