using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight2D : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _followTarget.position;
    }
}
