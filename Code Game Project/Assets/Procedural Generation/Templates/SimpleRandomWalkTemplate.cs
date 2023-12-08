using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Procedural World Generation/Create Simple Random Walk Template", 
                 fileName = "SWR-Template")]
public class SimpleRandomWalkTemplate : ScriptableObject
{
    [SerializeField] private int _iterations;
    [SerializeField] private int _walkLength;
    [SerializeField] private bool _startRandomIteration;

    public int GetIterations { get { return _iterations; } }
    public int GetWalkLength { get { return _walkLength; } }
    public bool GetStartRandomIteration { get { return _startRandomIteration; } }
}
