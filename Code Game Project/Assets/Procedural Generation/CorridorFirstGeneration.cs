using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstGeneration : SimpleRandomWalkGeneration
{
    [SerializeField] private int _corridorLength;
    [SerializeField] private int _corridorCount;
    
    [Range(0.1f, 1f)]
    [SerializeField] private float _roomPercent;

    protected override void RunProceduralGeneration()
    {
        GenerateCorridorFirst();
    }

    private void GenerateCorridorFirst()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        CreateRoomsAtDeadEnds(deadEnds, roomPositions);
        floorPositions.UnionWith(roomPositions);

        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallsGeneration.GenerateWalls(floorPositions, _tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (Vector2Int position in deadEnds)
        {
            if (!roomFloors.Contains(position))
            {
                HashSet<Vector2Int> createRoom = GenerateSimpleRandomWalk(_randomWalkTemplate, position);
                roomFloors.UnionWith(createRoom);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (Vector2Int position in floorPositions)
        {
            int neighboursCount = 0;

            foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
                if (floorPositions.Contains(position + direction)) neighboursCount++;
            if (neighboursCount == 1) deadEnds.Add(position);
        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomCreationCount = Mathf.RoundToInt(potentialRoomPositions.Count * _roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomCreationCount).ToList();
        foreach (Vector2Int roomPosition in roomsToCreate)
        {
            HashSet<Vector2Int> roomFloor = GenerateSimpleRandomWalk(_randomWalkTemplate, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        Vector2Int currentPosition = _startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < _corridorCount; i++)
        {
            List<Vector2Int> corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, _corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}
