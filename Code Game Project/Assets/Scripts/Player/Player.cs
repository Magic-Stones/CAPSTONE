using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int _laptopEnergy = 5;
    public int GetLaptopEnergy { get { return _laptopEnergy; } }
    [SerializeField] private float _moveSpeed = 1f;
    public int score = 0;

    private Inventory _inventory;
    public Inventory GetInventory { get { return _inventory; } }

    public delegate void EventDelegate();
    public static event EventDelegate OnLaptopOutOfPower;

    private PlayerInteraction _playerInteraction;
    private CharacterController2D _controller2D;
    private GameMechanics _mechanics;

    void Awake()
    {
        Instance = this;
        _inventory = GetComponent<Inventory>();
        _controller2D = GetComponent<CharacterController2D>();

        _playerInteraction = GameObject.Find("Button - Interact").GetComponent<PlayerInteraction>();
        _mechanics = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameMechanics>();
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

        if (_laptopEnergy <= 0)
        {
            OnLaptopOutOfPower?.Invoke();
            return;
        }
    }

    public void LaptopOutOfPower()
    {
        _mechanics.SetGameLose();
        //_mechanics.LoseGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Entryway"))
        {
            DungeonEntryways entryway;
            Transform parentOfCollider = collision.collider.transform.parent;
            if (parentOfCollider.name.Equals("Close") || parentOfCollider.name.Equals("Open")) 
            { 
                entryway = parentOfCollider.parent.GetComponent<DungeonEntryways>();

                if (!entryway.GetIsLocked) return;
                _playerInteraction.ContainNearbyEntryway(entryway.gameObject);
            }
            else
            {
                entryway = parentOfCollider.GetComponent<DungeonEntryways>();

                if (!entryway.GetIsLocked) return;
                _playerInteraction.ContainNearbyEntryway(entryway.gameObject);
            }
        }

        if (collision.collider.name.Equals("Game Win Trigger"))
        {
            _mechanics.WinGame();
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
