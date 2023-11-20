using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    [SerializeField] private float _moveSpeed = 1f;

    [SerializeField] private GameObject _challenger;
    public GameObject GetChallenger { get { return _challenger; } }

    private CharacterController2D _controller2D;
    private Inventory _invetory;
    public Inventory GetInventory { get { return _invetory; } }

    void Awake()
    {
        Instance = this;
        _controller2D = GetComponent<CharacterController2D>();
        _invetory = GetComponent<Inventory>();
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
}
