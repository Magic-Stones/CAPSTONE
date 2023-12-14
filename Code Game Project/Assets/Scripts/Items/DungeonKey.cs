using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonKey : MonoBehaviour, ItemInterface
{
    [SerializeField] private ItemData _dungeonKeyData;
    public ItemData GetItemData { get { return _dungeonKeyData; } }

    private float _gravityTimeLimit = 1f;

    private Rigidbody2D _rb2D;
    private PlayerInteraction _playerInteraction;

    void Awake()
    {
        TryGetComponent(out _rb2D);

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

    void FixedUpdate()
    {
        if (_rb2D)
        {
            if (_gravityTimeLimit > 0f) _gravityTimeLimit -= Time.deltaTime;
            else _rb2D.gravityScale = 0f;
        }
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
