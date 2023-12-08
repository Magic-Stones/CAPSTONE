using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Image _itemImage;
    public Image GetItemImage { get { return _itemImage; } }
    private string _itemName;
    public string GetItemName { get { return _itemName; } }
    private string _itemDescription;
    public string GetItemDescription { get { return _itemDescription; } }
    private string _quizAnswer;
    public string GetQuizAnswer { get { return _quizAnswer; } }

    private float _selectedItemEffectDistance = 7.5f;
    private Outline _outline;

    public InventoryItem inventoryItem;

    [SerializeField] private ItemSelection _itemSelection;

    void Awake()
    {
        _itemImage = transform.Find("Button - Item").GetComponent<Image>();
        _outline = GetComponent<Outline>();
        _itemSelection = GameObject.Find("Selected Item Panel").GetComponent<ItemSelection>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _itemImage.sprite = inventoryItem.GetItemData.itemIcon;
        _itemName = inventoryItem.GetItemData.itemName;
        _itemDescription = inventoryItem.GetItemData.itemDescription;
        _quizAnswer = inventoryItem.GetItemData.quizAnswer;

        _outline.effectDistance = Vector2.zero;
        gameObject.name = _itemName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem()
    {
        _outline.effectDistance = new Vector2(_selectedItemEffectDistance, -_selectedItemEffectDistance);
        _itemSelection.SetSelectedItem(this);
    }

    public void DeselectItem()
    {
        _outline.effectDistance = Vector2.zero;
    }

    public void ToBeDestroyed()
    {
        _itemSelection.SetSelectedItem(null);
        Destroy(gameObject);
    }
}
