using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public CellPart CellContent;
    public List<GridCell> SupportedCells = new List<GridCell>();
    public List<GridCell> SupportCells = new List<GridCell>();

    public void RemoveSupportCell(GridCell cellToRemove) {
        SupportCells.Remove(cellToRemove);
        if (SupportCells.Count == 0 && transform.position.y != 0) {
            OnDestroyCell();
        }
    }

    public void OnDestroyCell() {
        foreach (GridCell cell in SupportedCells) {
            cell.RemoveSupportCell(this);
            Debug.Log("Remover de"+ cell.name);
        }
        WorldGrid.MidCells.Remove(transform.position);
        foreach (DictionaryEntry h in WorldGrid.MidCells) {
            Debug.Log(h.Key);
            Debug.Log(WorldGrid.MidCells.Count);
        }
        gameObject.SetActive(false);
    }

}
