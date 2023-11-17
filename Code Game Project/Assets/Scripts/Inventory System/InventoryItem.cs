using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    [SerializeField] private ItemData _itemData;
    public ItemData GetItemData { get { return _itemData; } }
    [SerializeField] private int _stackSize;
    public int GetStackSize { get { return _stackSize; } }

    public InventoryItem(ItemData itemData)
    {
        _itemData = itemData;
        AddToStack();
    }

    public void AddToStack() { _stackSize++; }

    public void RemoveFromStack() { _stackSize--; }
}
