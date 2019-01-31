using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static float CellSize = 3;
    public static List<GridCell> Cells = new List<GridCell>();

    public static  GridCell GetWoldCell(Vector3 position) {
        GridCell gc = FindExistingCells(ConvertToGridPosition(position));
        if (gc != null) {
            return gc;
        }
        GameObject go = new GameObject();
        go.transform.SetParent(BuilderManager.Instance.transform);
        go.AddComponent<GridCell>();
        gc = go.GetComponent<GridCell>();
        gc.transform.position = ConvertToGridPosition(position);
        Cells.Add(gc);
        return gc;
    }

    private static Vector3 ConvertToGridPosition(Vector3 position) {
        int x = (int)position.x / (int)CellSize;
        int y = (int)position.y / (int)CellSize;
        int z = (int)position.z / (int)CellSize;
        Vector3 temp = new Vector3(x, y, z);
        temp *= CellSize;
        return temp;
    }

    private static GridCell FindExistingCells(Vector3 position) {
        foreach (GridCell gc in Cells) {
            if (Vector3.Distance( gc.transform.position, position) < CellSize) {
                return gc;
            }
        }
        return null;
    }
}
