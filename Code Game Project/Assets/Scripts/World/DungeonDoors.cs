using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoors : MonoBehaviour
{
    [SerializeField] private GameObject _enemyTrigger;
    private IEnemy _enemyAtb;

    private bool _isDefeated;

    private Animator _animator;

    void Awake()
    {
        _enemyAtb = _enemyTrigger.GetComponent<IEnemy>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimator();
    }

    private void SetAnimator()
    {
        _isDefeated = _enemyAtb.GetIsDefeated;

        _animator.SetBool("IsOpen", _isDefeated);
    }
}
