using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> _inventory;
    public List<InventoryItem> GetInventoryList { get { return _inventory; } }
    private Dictionary<ItemData, InventoryItem> _itemDictionary;

    private event Action _onInventoryUpdate;
    public Action OnInventoryUpdateCallback { set { _onInventoryUpdate = value; } }

    void Awake()
    {
        _inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<ItemData, InventoryItem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemData itemData)
    {
        if (_itemDictionary.TryGetValue(itemData, out InventoryItem inventoryItem))
        {
            inventoryItem.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            _inventory.Add(newItem);
            _itemDictionary.Add(itemData, newItem);
        }

        _onInventoryUpdate?.Invoke();
    }

    public void RemoveItem(ItemData itemData)
    {
        if (_itemDictionary.TryGetValue(itemData, out InventoryItem inventoryItem))
        {
            inventoryItem.RemoveFromStack();

            if (inventoryItem.GetStackSize == 0)
            {
                _inventory.Remove(inventoryItem);
                _itemDictionary.Remove(itemData);
            }
        }
    }
}
