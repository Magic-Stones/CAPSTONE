using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private CharacterController2D _controller2D;

    void Awake()
    {
        _controller2D = GetComponent<CharacterController2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller2D.ExternalMoveSpeed = moveSpeed;
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
