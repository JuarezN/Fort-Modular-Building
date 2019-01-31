using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance;
    public GameObject PartToBuild;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("There is two BuilderManager in the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            BuildCommand(Player.transform.position);
        }
    }

    void BuildCommand(Vector3 buildPosition) {
        GridCell gc = WorldGrid.GetWoldCell(buildPosition);
        if (gc.MidPart == null) {
            GameObject go = GameObject.Instantiate(PartToBuild, gc.transform.position , Quaternion.identity);
            go.transform.SetParent(gc.transform);
            gc.MidPart = go.GetComponent<CellPart>();
        }
    }
}
