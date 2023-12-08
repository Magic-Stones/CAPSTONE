using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
    [Header("Item Display")]
    [SerializeField] private Image _itemDisplayImage;
    private Color _itemImageColor;
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
        _itemImageColor = _itemDisplayImage.color;
        ResetDisplay();

        Vector2 vec = new Vector2(2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedItem(ItemSlot itemSlot)
    {
        if (!itemSlot)
        {
            ResetDisplay();
            return;
        }

        if (!_initializedSelectedItem)
        {
            _itemSlot = itemSlot;
            SetItemDisplay();
            _initializedSelectedItem = true;
        }
        else
        {
            _itemSlot.DeselectItem();
            _itemSlot = itemSlot;
            SetItemDisplay();
        }

        void SetItemDisplay()
        {
            _itemDisplayImage.sprite = _itemSlot.GetItemImage.sprite;
            _itemImageColor.a = 1f;
            _itemDisplayImage.color = _itemImageColor;

            _itemDisplayName.text = _itemSlot.GetItemName;
            _itemDescription.text = _itemSlot.GetItemDescription;
        }
    }

    public void SubmitItem()
    {
        _uiManager.SubmitItem(_itemSlot);
    }

    private void ResetDisplay()
    {
        _itemDisplayImage.sprite = null;
        _itemImageColor.a = 0f;
        _itemDisplayImage.color = _itemImageColor;

        _itemDisplayName.text = "";
        _itemDescription.text = "";

        _initializedSelectedItem = false;
    }
}
