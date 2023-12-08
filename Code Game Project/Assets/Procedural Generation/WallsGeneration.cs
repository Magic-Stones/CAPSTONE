using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallsGeneration
{
    public static void GenerateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);

        foreach (Vector2Int position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (Vector2Int position in floorPositions)
        {
            foreach (Vector2Int direction in directionsList)
            {
                Vector2Int neighbourPosition = position + direction;

                if (!floorPositions.Contains(neighbourPosition)) wallPositions.Add(neighbourPosition);
            }
        }

        return wallPositions;
    }
}
