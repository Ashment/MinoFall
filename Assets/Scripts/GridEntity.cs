using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bState
{
    free,
    mblock,
    sblock,
    player,
};

public class GridEntity : MonoBehaviour {
    public Vector2Int curCoord;
    public Vector2Int targetCoord;
    public Vector2 relPos;
    public bState state;

    public GridEntity() {
        state = bState.free;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
