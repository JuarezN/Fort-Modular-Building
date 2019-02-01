using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static float CellSize = 3;
    public static Hashtable MidCells = new Hashtable();
    public static Hashtable FrontWallCells = new Hashtable();
    public static Hashtable SideWallCells = new Hashtable();
    public static Hashtable GroundCells = new Hashtable();

    public static List<Hashtable> CellsHash = new List<Hashtable>();

    public static void Init() {
        CellsHash.Add(MidCells);
        CellsHash.Add(FrontWallCells);
        CellsHash.Add(SideWallCells);
        CellsHash.Add(GroundCells);
    }

    public static  GridCell GetWoldCell(Hashtable cellGroup, Vector3 position) {
        GridCell gc = FindExistingCells(cellGroup, ConvertToGridPosition(position));
        if (gc != null) {
            return gc;
        }
        GameObject go = new GameObject();
        go.transform.SetParent(BuilderManager.Instance.transform);
        go.AddComponent<GridCell>();
        gc = go.GetComponent<GridCell>();
        gc.transform.position = ConvertToGridPosition(position);
        FindCellConnection(gc, gc.transform.position);
        cellGroup.Add(gc.transform.position ,gc);
        return gc;
    }

    private static void FindCellConnection(GridCell newGrid, Vector3 position) {
        List<GridCell> gridCells = new List<GridCell>();
        foreach (Hashtable hashlist in CellsHash) {
            Debug.Log(new Vector3(position.x, position.y, position.z + CellSize) +"" + hashlist.Contains(new Vector3(position.x, position.y, position.z + CellSize)));
            if (hashlist.Contains(new Vector3(position.x, position.y, position.z + CellSize))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y, position.z + CellSize)]);
            }
            if (hashlist.Contains(new Vector3(position.x, position.y, position.z - CellSize))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y, position.z - CellSize)]);
            }
            if (hashlist.Contains(new Vector3(position.x, position.y + CellSize, position.z))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y + CellSize, position.z)]);
            }
            if (hashlist.Contains(new Vector3(position.x, position.y - CellSize, position.z))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y - CellSize, position.z)]);
            }
            if (hashlist.Contains(new Vector3(position.x + CellSize, position.y, position.z))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x +CellSize, position.y, position.z)]);
            }
            if (hashlist.Contains(new Vector3(position.x - CellSize, position.y, position.z))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x - CellSize, position.y, position.z)]);
            }
            if (hashlist.Contains(new Vector3(position.x, position.y + CellSize, position.z + CellSize))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y + CellSize, position.z + CellSize)]);
            }
            if (hashlist.Contains(new Vector3(position.x, position.y - CellSize, position.z + CellSize))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y - CellSize, position.z + CellSize)]);
            }
            if (hashlist.Contains(new Vector3(position.x, position.y + CellSize, position.z - CellSize))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y + CellSize, position.z - CellSize)]);
            }
            if (hashlist.Contains(new Vector3(position.x, position.y - CellSize, position.z - CellSize))) {
                gridCells.Add((GridCell)hashlist[new Vector3(position.x, position.y - CellSize, position.z - CellSize)]);
            }
        }
            Debug.Log(gridCells.Count);
        foreach (GridCell gc in gridCells) {
            gc.SupportedCells.Add(newGrid);
        }
        newGrid.SupportCells = gridCells;
    }

    private static Vector3 ConvertToGridPosition(Vector3 position) {
        int x = (int)position.x / (int)CellSize;
        int y = (int)position.y / (int)CellSize;
        int z = (int)position.z / (int)CellSize;
        Vector3 temp = new Vector3(x, y, z);
        temp *= CellSize;
        return temp;
    }

    private static GridCell FindExistingCells(Hashtable cellGroup ,Vector3 position) {
        if (cellGroup.Contains(position)) {
            return (GridCell)cellGroup[position];
        }
        return null;
    }
}
