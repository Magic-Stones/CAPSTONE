using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
    [Header("Item Display")]
    [SerializeField] private Image _itemDisplayImage;
    private bool _itemImageFullAlpha = false;
    [SerializeField] private TextMeshProUGUI _itemDisplayName;

    [Header("Item Description")]
    [SerializeField] private TextMeshProUGUI _itemDescription;

    [Space(10)]
    [SerializeField] private Button _btnSubmit;

    private ItemSlot _itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        _itemDisplayImage.sprite = null;
        _itemDisplayName.text = "";
        _itemDescription.text = "";
        _btnSubmit.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedItem(ItemSlot itemSlot)
    {
        if (_itemSlot) _itemSlot.DeselectItem();

        _itemSlot = itemSlot;

        _itemDisplayImage.sprite = _itemSlot.GetItemImage.sprite;
        if (!_itemImageFullAlpha)
        {
            Color itemImageColor = _itemDisplayImage.color;
            itemImageColor.a = 1f;
            _itemDisplayImage.color = itemImageColor;
            _itemImageFullAlpha = true;
        }

        _itemDisplayName.text = _itemSlot.GetItemName;
        _itemDescription.text = _itemSlot.GetItemDescription;
    }
}
