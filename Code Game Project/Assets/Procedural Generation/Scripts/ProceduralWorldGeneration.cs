using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralWorldGeneration : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer _tilemapVisualizer;
    [SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;

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
