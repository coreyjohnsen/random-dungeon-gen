using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGen
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPos);
        var prevPos = startPos;
        for(int i = 0; i < walkLength; i++)
        {
            var newPos = prevPos + Direction2D.getRandomDir();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.getRandomDir();
        var currentPos = startPos;
        corridor.Add(currentPos);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> directionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // up
        new Vector2Int(1, 0), // right
        new Vector2Int(0, -1), // down
        new Vector2Int(-1, 0) // left
    };

    public static Vector2Int getRandomDir()
    {
        return directionList[Random.Range(0, directionList.Count)];
    }
}
