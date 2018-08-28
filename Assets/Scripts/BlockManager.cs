using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

    public GManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindWithTag("MainCamera").GetComponent<GManager>();
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void TickUpdate() {
//        Debug.Log("Block Manager Ticking...");
        gameObject.BroadcastMessage("TickDrop");

        /*
        for(int i=0; i < transform.childCount; i++) {
            Transform c = transform.GetChild(i);
            for(int j=0; j < c.childCount; j++) {
                MinoBlock cBlock = transform.GetChild(j).GetComponent<MinoBlock>();
                GridEntity g = new GridEntity();
                g.state = bState.free;
                manager.SetGridE(cBlock.curCoord.x, cBlock.curCoord.y, cBlock);
            }
        }
        */
    }
}