using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class RandomWalkDungeonGen : AbstractGen
{

    [SerializeField]
    protected int iterations = 10;
    [SerializeField]
    protected int walkLength = 10;
    [SerializeField]
    protected bool startRandomlyEachIteration = true;

    protected override void RunProceduralGen()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(iterations, walkLength, startRandomlyEachIteration, startPos);
        visualizer.Clear();
        visualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(int its, int length, bool randomEachIteration, Vector2Int position)
    {
        var currentPos = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for(int i = 0; i < its; i++)
        {
            var path = ProceduralGen.SimpleRandomWalk(currentPos, length);
            floorPositions.UnionWith(path);
            if (randomEachIteration)
                currentPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        return floorPositions;
    }

}
