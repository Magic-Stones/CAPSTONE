using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float searchRange = 3f;

    [SerializeField] private float setMoveSpeed = 2f;
    private float _moveSpeed;

    [SerializeField] private GameObject player;
    private Transform _targetTranform;

    private Rigidbody2D _rb2D;
    private GameMechanics _mechanics;

    void Awake() 
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _mechanics = FindObjectOfType<GameMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed = setMoveSpeed;
        _targetTranform = GameObject.FindGameObjectWithTag(player.tag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
    }

    private void FindTarget() 
    {
        Vector3 targetDirection = (_targetTranform.position - transform.position).normalized;

        if (Vector2.Distance(transform.position, _targetTranform.position) < searchRange)
        {
            _rb2D.MovePosition(transform.position + (targetDirection * _moveSpeed * Time.deltaTime));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag.Equals(player.tag))
        {
            _mechanics.TriggerMainBattle();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}
