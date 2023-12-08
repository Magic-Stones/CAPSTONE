using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemapGround;
    [SerializeField] private Tilemap _tilemapWalls;

    [SerializeField] private TileBase _tileFloor;
    [SerializeField] private TileBase _tileWallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, _tilemapGround, _tileFloor);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach (Vector2Int position in positions)
        {
            PaintSingleTile(tileMap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
    {
        Vector3Int tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);
    }

    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(_tilemapWalls, _tileWallTop, position);
    }

    public void ClearAllTiles()
    {
        _tilemapGround.ClearAllTiles();
        _tilemapWalls.ClearAllTiles();
    }
}
