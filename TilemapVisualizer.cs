using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorMap, wallTileMap;
    [SerializeField]
    private TileBase floorTile, wallTop; // can make array and make it random

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap map, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(position, map, tile);
        }
    }

    private void PaintSingleTile(Vector2Int position, Tilemap map, TileBase tile)
    {
        var tilePos = map.WorldToCell((Vector3Int)position);
        map.SetTile(tilePos, tile);
    }

    public void Clear()
    {
        floorMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }

    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(position, wallTileMap, wallTop);
    }
}
