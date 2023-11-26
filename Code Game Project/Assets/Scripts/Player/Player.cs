using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    [SerializeField] private int _laptopEnergy = 5;
    public int GetLaptopEnergy { get { return _laptopEnergy; } }
    [SerializeField] private float _moveSpeed = 1f;

    private Inventory _inventory;
    public Inventory GetInventory { get { return _inventory; } }

    public delegate void EventDelegate();
    public static event EventDelegate OnLaptopOutOfPower;

    private PlayerInteraction _playerInteraction;
    private CharacterController2D _controller2D;

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
        OnLaptopOutOfPower += LaptopOutOfPower;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage() 
    {
        _laptopEnergy--;

        if (_laptopEnergy < 0)
        {
            OnLaptopOutOfPower?.Invoke();
            return;
        }
    }

    public void LaptopOutOfPower()
    {
        Debug.Log("OUT OF POWER!");
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
