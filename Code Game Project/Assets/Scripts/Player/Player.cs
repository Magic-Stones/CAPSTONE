using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    [SerializeField] private float _moveSpeed = 1f;

    private CharacterController2D _controller2D;
    private Inventory _inventory;
    public Inventory GetInventory { get { return _inventory; } }

    private PlayerInteraction _playerInteraction;

    void Awake()
    {
        Instance = this;
        _controller2D = GetComponent<CharacterController2D>();
        _inventory = GetComponent<Inventory>();

        _playerInteraction = GameObject.Find("Button - Interact").GetComponent<PlayerInteraction>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller2D.ExternalMoveSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 GetPosition() 
    {
        return transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Entryway"))
        {
            GameObject root = collision.transform.parent.parent.gameObject;
            _playerInteraction.ContainNearbyEntryway(root);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Entryway"))
        {
            _playerInteraction.ContainNearbyEntryway(null);
        }
    }
}
