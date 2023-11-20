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
    public Button GetSubmitButton { get { return _btnSubmit; } }

    private bool _initializedSelectedItem = false;

    private ItemSlot _itemSlot;
    private UIManager _uiManager;

    void Awake()
    {
        _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _itemDisplayImage.sprite = null;
        _itemDisplayName.text = "";
        _itemDescription.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedItem(ItemSlot itemSlot)
    {
        if (_itemSlot && _itemSlot.gameObject != itemSlot.gameObject)
        {
            _itemSlot.DeselectItem();

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
        else
        {
            if (_initializedSelectedItem) return;

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
            _initializedSelectedItem = true;
        }
    }

    public void SubmitItem()
    {
        _uiManager.SubmitItem(_itemSlot);
    }
}
