using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance;
    public GameObject PartToBuild;
    public GameObject PartToBuild_Ground;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("There is two BuilderManager in the scene");
        }
        WorldGrid.Init();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            BuildCommand(Player.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            BuildCommand2(Player.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            DestroyCommand(Player.transform.position);
        }
    }

    void BuildCommand(Vector3 buildPosition) {
        GridCell gc = WorldGrid.GetWoldCell(WorldGrid.MidCells, buildPosition);
        if (gc.CellContent == null) {
            GameObject go = Instantiate(PartToBuild, gc.transform.position , Quaternion.identity);
            go.transform.SetParent(gc.transform);
            gc.CellContent = go.GetComponent<CellPart>();
            if (!CheckCellSupport(gc))
                gc.OnDestroyCell();
        }
    }
    void BuildCommand2(Vector3 buildPosition) {
        GridCell gc = WorldGrid.GetWoldCell(WorldGrid.GroundCells, buildPosition);
        if (gc.CellContent == null) {
            GameObject go = Instantiate(PartToBuild_Ground, gc.transform.position, Quaternion.identity);
            go.transform.SetParent(gc.transform);
            gc.CellContent = go.GetComponent<CellPart>();
            if (!CheckCellSupport(gc))
                gc.OnDestroyCell();
        }
    }
    void DestroyCommand(Vector3 buildPosition) {
        GridCell gc = WorldGrid.GetWoldCell(WorldGrid.MidCells, buildPosition);
        if (gc.CellContent != null) {
            Debug.Log("Destruir" + gc.name);
            gc.OnDestroyCell();
        }
    }

    private bool CheckCellSupport(GridCell cell) {
        if (cell.SupportCells.Count == 0) {
            if(cell.transform.position.y != 0)
            return false;
        }
        return true;
    }
}
