using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{

    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer visualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.directionList);
        foreach (var position in basicWallPositions)
        {
            visualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> positions, List<Vector2Int> directions)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in positions)
        {
            foreach (var direction in directions)
            {
                var neighborPosition = position + direction;
                if(positions.Contains(neighborPosition) == false)
                {
                    wallPositions.Add(neighborPosition);
                }
            }
        }
        return wallPositions;
    }

}
