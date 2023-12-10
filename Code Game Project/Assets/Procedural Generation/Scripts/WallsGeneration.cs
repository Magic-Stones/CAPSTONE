using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class WallsGeneration
{
    public static void GenerateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        HashSet<Vector2Int> cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, 
                                        HashSet<Vector2Int> floorPositions)
    {
        StringBuilder neighboursBinaryType = new StringBuilder();

        foreach (Vector2Int position in basicWallPositions)
        {
            neighboursBinaryType.Clear();
            foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
            {
                Vector2Int neighbourPosition = position + direction;

                if (floorPositions.Contains(neighbourPosition)) neighboursBinaryType.Append("1");
                else neighboursBinaryType.Append("0");
            }

            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType.ToString());
        }
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, 
                                          HashSet<Vector2Int> floorPositions)
    {
        StringBuilder neighboursBinaryType = new StringBuilder();

        foreach (Vector2Int position in cornerWallPositions)
        {
            neighboursBinaryType.Clear();
            foreach (Vector2Int direction in Direction2D.eightDirectionsList)
            {
                Vector2Int neighbourPosition = position + direction;

                if (floorPositions.Contains(neighbourPosition)) neighboursBinaryType.Append("1");
                else neighboursBinaryType.Append("0");
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType.ToString());
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
