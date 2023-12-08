using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProceduralWorldGeneration : MonoBehaviour
{
    [SerializeField] protected TilemapVisualizer _tilemapVisualizer = null;
    [SerializeField] protected Vector2Int _startPosition = Vector2Int.zero;

    //[ContextMenu(nameof(CreateWorld))]
    public void CreateWorld()
    {
        _tilemapVisualizer.ClearAllTiles();
        RunProceduralGeneration();
    }

    //[ContextMenu(nameof(DeleteWorld))]
    public void DeleteWorld()
    {
        _tilemapVisualizer.ClearAllTiles();
    }

    protected abstract void RunProceduralGeneration();
}
