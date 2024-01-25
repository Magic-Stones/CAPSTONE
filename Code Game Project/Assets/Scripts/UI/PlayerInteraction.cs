using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _nearbyItem;
    [SerializeField] private ItemData _itemData;

    private GameObject _nearbyEnemy;
    private GameObject _nearbyChest;
    [SerializeField] private QuizTemplate _quizTemplate;

    private DungeonEntryways _nearbyEntryway;
    [SerializeField] private ItemData _entrywayKey;

    private string _defaultInteractText;

    private Button _button;
    private TextMeshProUGUI _textInteract;
    private GameMechanics _mechanics;

    void Awake()
    {
        _button = GetComponent<Button>();
        _textInteract = GetComponentInChildren<TextMeshProUGUI>();
        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _defaultInteractText = _textInteract.text;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ContainNearbyItem(GameObject nearbyItem)
    {
        _nearbyItem = nearbyItem;

        if (_nearbyItem)
        {
            _itemData = _nearbyItem.GetComponent<ItemInterface>().GetItemData;
            _textInteract.text = "Pick up";
            _button.interactable = true;
        }
        else
        {
            _itemData = null;
            _textInteract.text = _defaultInteractText;
            _button.interactable = false;
        }
    }

    public void ContainNearbyEnemy(GameObject nearbyEnemy)
    {
        _nearbyEnemy = nearbyEnemy;

        if (_nearbyEnemy)
        {
            _quizTemplate = nearbyEnemy.GetComponent<IEnemy>().GetQuizTemplate;
            _textInteract.text = "Challenge!";
            _button.interactable = true;
        }
        else
        {
            _textInteract.text = _defaultInteractText;
            _button.interactable = false;
        }
    }

    public void ContainNearbyChest(GameObject nearbyChest)
    {
        _nearbyChest = nearbyChest;

        if (_nearbyChest)
        {
            _quizTemplate = nearbyChest.GetComponent<DungeonChest>().GetQuizTemplate;
            _textInteract.text = "Unlock";
            _button.interactable = true;
        }
        else
        {
            _textInteract.text = _defaultInteractText;
            _button.interactable = false;
        }
    }

    public void ContainNearbyEntryway(GameObject nearbyEntryway)
    {
        if (nearbyEntryway)
        {
            _nearbyEntryway = nearbyEntryway.GetComponent<DungeonEntryways>();
            _entrywayKey = _nearbyEntryway.GetDungeonKey;

            _textInteract.text = "Open";

            if (Player.Instance.GetInventory.GetItemDictionary.ContainsKey(_entrywayKey))
                _button.interactable = true;
            else _button.interactable = false;
        }
        else
        {
            _nearbyEntryway = null;
            _entrywayKey = null;

            _textInteract.text = _defaultInteractText;
            _button.interactable = false;
        }
    }

    public void CollectItem()
    {
        if (!_nearbyItem) return;

        Player.Instance.GetInventory.AddItem(_itemData);
        AudioAssets.Instance.audioSFX.clip = AudioAssets.Instance.sfxPickUpItem;
        AudioAssets.Instance.audioSFX.Play();
        Destroy(_nearbyItem);
    }

    public void ChallengeEnemy()
    {
        if (!_nearbyEnemy) return;

        _mechanics.TriggerChallenge(_nearbyEnemy, _quizTemplate);
    }

    public void UnlockChest()
    {
        if (!_nearbyChest) return;

        _mechanics.TriggerChallenge(_nearbyChest, _quizTemplate);
    }

    public void OpenEntryway()
    {
        if (!_nearbyEntryway) return;

        _nearbyEntryway.UnlockEntryway();
        Player.Instance.GetInventory.RemoveItem(_entrywayKey);
    }
}
