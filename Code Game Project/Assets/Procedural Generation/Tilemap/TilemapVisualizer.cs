using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemapGround;
    [SerializeField] private Tilemap _tilemapWalls;

    [SerializeField] private TileBaseTemplate _tileBase;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, _tilemapGround, _tileBase.GetTileFloor);
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

    public void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        tile = BasicWallTypes(typeAsInt, tile);

        if (tile) PaintSingleTile(_tilemapWalls, tile, position);
    }

    private TileBase BasicWallTypes(int type, TileBase tile)
    {
        if (WallTypesClass.wallTop.Contains(type)) tile = _tileBase.GetTileWallTop;
        else if (WallTypesClass.wallBottom.Contains(type)) tile = _tileBase.GetTileWallBottom;
        else if (WallTypesClass.wallSideRight.Contains(type)) tile = _tileBase.GetTileWallSideRight;
        else if (WallTypesClass.wallSideLeft.Contains(type)) tile = _tileBase.GetTileWallSideLeft;
        else if (WallTypesClass.wallFull.Contains(type)) tile = _tileBase.GetTileWall;

        return tile;
    }

    public void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        tile = CornerWallTypes(typeAsInt, tile);

        if (tile) PaintSingleTile(_tilemapWalls, tile, position);
    }

    private TileBase CornerWallTypes(int type, TileBase tile)
    {
        if (WallTypesClass.wallInnerCornerDownLeft.Contains(type)) tile = _tileBase.GetTileWallInnerCornerDownLeft;
        else if (WallTypesClass.wallInnerCornerDownRight.Contains(type)) tile = _tileBase.GetTileWallInnerCornerDownRight;
        else if (WallTypesClass.wallDiagonalCornerDownLeft.Contains(type)) tile = _tileBase.GetTileWallDiagonalCornerDownLeft;
        else if (WallTypesClass.wallDiagonalCornerDownRight.Contains(type)) tile = _tileBase.GetTileWallDiagonalCornerDownRight;
        else if (WallTypesClass.wallDiagonalCornerUpLeft.Contains(type)) tile = _tileBase.GetTileWallDiagonalCornerUpLeft;
        else if (WallTypesClass.wallDiagonalCornerUpRight.Contains(type)) tile = _tileBase.GetTileWallDiagonalCornerUpRight;
        else if (WallTypesClass.wallFullEightDirections.Contains(type)) tile = _tileBase.GetTileWall;
        else if (WallTypesClass.wallBottomEightDirections.Contains(type)) tile = _tileBase.GetTileWallBottom;

        return tile;
    }

    public void ClearAllTiles()
    {
        _tilemapGround.ClearAllTiles();
        _tilemapWalls.ClearAllTiles();
    }
}
