using UnityEngine;
using System.Collections.Generic;

public class GridRenderer : MonoBehaviour
{
    public int gridSize = 10;
    public float cellSize = 1f;
    public Material lineMaterial;

    void Start()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        GameObject gridParent = new GameObject("GridLines");
        List<Vector3> linePositions = new List<Vector3>();

        // 세로선
        for (int x = 0; x <= gridSize; x++)
        {
            linePositions.Add(new Vector3(x * cellSize, 0, 0));
            linePositions.Add(new Vector3(x * cellSize, 0, gridSize * cellSize));
        }

        // 가로선
        for (int z = 0; z <= gridSize; z++)
        {
            linePositions.Add(new Vector3(0, 0, z * cellSize));
            linePositions.Add(new Vector3(gridSize * cellSize, 0, z * cellSize));
        }

        for (int i = 0; i < linePositions.Count; i += 2)
        {
            GameObject lineObj = new GameObject("GridLine");
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.material = lineMaterial;
            lr.positionCount = 2;
            lr.SetPosition(0, linePositions[i]);
            lr.SetPosition(1, linePositions[i + 1]);
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.useWorldSpace = true;
            lineObj.transform.parent = gridParent.transform;
        }
    }
}
