using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGen : MonoBehaviour
{

    [SerializeField]
    protected TilemapVisualizer visualizer = null;
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenDungeon()
    {
        visualizer.Clear();
        RunProceduralGen();
    }

    protected abstract void RunProceduralGen();

}
