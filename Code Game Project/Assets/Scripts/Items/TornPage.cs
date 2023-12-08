using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornPage : MonoBehaviour, ItemInterface
{
    [SerializeField] private ItemData _tornPageData;
    public ItemData GetItemData { get { return _tornPageData; } }
    private PlayerInteraction _playerInteraction;

    void Awake()
    {
        _playerInteraction = GameObject.Find("Button - Interact").GetComponent<PlayerInteraction>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInteraction.ContainNearbyItem(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInteraction.ContainNearbyItem(null);
        }
    }
}
