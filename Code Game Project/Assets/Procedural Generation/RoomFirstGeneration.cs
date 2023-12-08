using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstGeneration : SimpleRandomWalkGeneration
{
    [SerializeField] private int _minRoomWidth;
    [SerializeField] private int _minRoomHeight;

    [SerializeField] private int _dungeonWidth;
    [SerializeField] private int _dungeonHeight;

    [Range(0, 10)]
    [SerializeField] private int _offset;
    [SerializeField] private bool _randomWalkRooms;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        List<BoundsInt> roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning
            (new BoundsInt((Vector3Int)_startPosition, new Vector3Int(_dungeonWidth, _dungeonHeight, 0)), 
            _minRoomWidth, _minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);

        _tilemapVisualizer.PaintFloorTiles(floor);
        WallsGeneration.GenerateWalls(floor, _tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (BoundsInt room in roomsList)
        {
            for (int column = _offset; column < room.size.x - _offset; column++)
            {
                for (int row = _offset; row < room.size.y - _offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                    floor.Add(position);
                }
            }
        }

        return floor;
    }
}
