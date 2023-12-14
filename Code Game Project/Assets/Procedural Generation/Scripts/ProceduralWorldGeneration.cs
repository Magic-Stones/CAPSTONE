using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralWorldGeneration : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer _tilemapVisualizer;
    [SerializeField] protected Transform _startWorldPosition;
    protected Vector2Int _startPosition 
    { 
        get 
        { 
            return new Vector2Int(Mathf.RoundToInt(_startWorldPosition.position.x), 
                Mathf.RoundToInt(_startWorldPosition.position.y)); 
        } 
    }

    public void CreateWorld()
    {
        _tilemapVisualizer.ClearAllTiles();
        RunProceduralGeneration();
    }

    public void DeleteWorld()
    {
        _tilemapVisualizer.ClearAllTiles();
    }

    protected abstract void RunProceduralGeneration();
}
