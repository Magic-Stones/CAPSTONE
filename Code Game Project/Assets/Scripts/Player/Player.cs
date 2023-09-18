using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private GameObject _challenger;
    public GameObject GetChallenger { get { return _challenger; } }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Enemy"))
        {
            _challenger = collision.gameObject;
        }
    }
}
