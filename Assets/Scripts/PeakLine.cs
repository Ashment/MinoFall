using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeakLine : MonoBehaviour {

    GManager manager;

    public Vector2Int curCoord;
    public Vector2Int targetCoord;
    public Vector2 relPos;

    // Use this for initialization
    void Start () {
        manager = GameObject.FindWithTag("MainCamera").GetComponent<GManager>();

        //INIT COORD POSITIONS
        curCoord = new Vector2Int
        {
            x = Mathf.RoundToInt(transform.position.x - manager.gridZero.x),
            y = Mathf.RoundToInt(transform.position.y - manager.gridZero.y)
        };
        targetCoord = curCoord;
    }
	
	// Update is called once per frame
	void Update () {
        targetCoord.x = Mathf.Clamp(targetCoord.x, 0, manager.gridSize.x - 1);
        targetCoord.y = manager.curPeak;

        if (relPos != targetCoord)
        {
            Vector3 targetPosVec = new Vector3(targetCoord.x + manager.gridZero.x, targetCoord.y + manager.gridZero.y, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosVec, 0.1f);
        }
    }
}
