using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorridorFirstGen : RandomWalkDungeonGen
{
    [SerializeField]
    private int corridorLength = 14, count = 5;
    [SerializeField]
    [Range(.1f, 1)]
    private float roomPercent = .8f;


    protected override void RunProceduralGen()
    {
        CorridorFirstGeneration();
    }    

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();

        CreateCorridors(floorPos, potentialRoomPos);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPos);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPos);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPos.UnionWith(roomPositions);

        visualizer.PaintFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, visualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos)
    {
        var currPos = startPos;
        potentialRoomPos.Add(currPos);

        for (int i = 0; i < count; i++)
        {
            var corridor = ProceduralGen.RandomWalkCorridor(currPos, corridorLength);
            currPos = corridor[corridor.Count - 1];
            potentialRoomPos.Add(currPos);
            floorPos.UnionWith(corridor);
        }


    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(iterations, walkLength, startRandomlyEachIteration, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPos)
        {
            int neighborsCount = 0;
            foreach (var direction in Direction2D.directionList)
            {
                if (floorPos.Contains(position + direction))
                {
                    neighborsCount++;
                }
            }
            if(neighborsCount == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPos)
    {
        foreach (var position in deadEnds)
        {
            if(roomPos.Contains(position) == false)
            {
                var roomFloors = RunRandomWalk(iterations, walkLength, startRandomlyEachIteration, position);
                roomPos.UnionWith(roomFloors);
            }
        }
    } 
}
