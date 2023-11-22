using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntryways : MonoBehaviour
{
    [SerializeField] private bool _isLocked = true;
    public bool GetIsLocked { get { return _isLocked; } }

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isLocked) _animator.SetBool("IsOpen", !_isLocked);
    }

    public void UnlockEntryway()
    {
        _isLocked = false;
    }
}
