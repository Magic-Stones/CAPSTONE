using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkGeneration : ProceduralWorldGeneration
{
    [SerializeField] protected SimpleRandomWalkTemplate _randomWalkTemplate;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = GenerateSimpleRandomWalk(_randomWalkTemplate, _startPosition);
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallsGeneration.GenerateWalls(floorPositions, _tilemapVisualizer);
    }

    protected HashSet<Vector2Int> GenerateSimpleRandomWalk(SimpleRandomWalkTemplate template, Vector2Int position)
    {
        Vector2Int currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < template.GetIterations; i++)
        {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, template.GetWalkLength);
            floorPositions.UnionWith(path);
            if (template.GetStartRandomIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }
}
