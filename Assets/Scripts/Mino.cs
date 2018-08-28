using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour {

    public GManager manager;

    public MinoBlock[] cBlocks;
    public int maxOffsetMod;
    public int minOffsetMod;

    public bool segmented = false;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindWithTag("MainCamera").GetComponent<GManager>();
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (transform.childCount == 0)
        {
            Destroy(this);
        }
        */
        /*FOR TESTING NUDGE
		if(Input.GetKeyDown(KeyCode.A)){
            Nudge(-1);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            Nudge(1);
        }
        */
    }

    public void Nudge(int dir) {
        bool allValid = true;
        foreach(MinoBlock i in cBlocks) {
            if (!i.ValidNudge(dir)) {
                allValid = false;
            }
        }
        if (allValid) {
            foreach(MinoBlock i in cBlocks) {
                manager.gridE[i.targetCoord.x, i.targetCoord.y] = manager.freeState;
                i.targetCoord.x += dir;
            }
            manager.shotSFO.GetComponent<AudioSource>().Play();
        }
        else {
            manager.invalidSFO.GetComponent<AudioSource>().Play();
        }
    }

    public bool AllValid() {
        bool allValid = true;
        foreach (MinoBlock i in cBlocks)
        {
            if (i != null && !i.ValidDrop())
            {
                allValid = false;
                break;
            }
        }
        return allValid;
    }

    void TickDrop() {
//       Debug.Log("Tick Dropping...");
        if (segmented) {
            foreach (MinoBlock i in cBlocks) {
                if(i != null && i.ValidDrop()){
                    manager.gridE[i.targetCoord.x, i.targetCoord.y] = manager.freeState;
                    i.targetCoord.y -= 1;
                }
            }
        }else if (AllValid()) {
            foreach (MinoBlock i in cBlocks) {
                if(i != null){
                    manager.gridE[i.targetCoord.x, i.targetCoord.y] = manager.freeState;
                    i.targetCoord.y -= 1;
                }
            }
        }
    }

    public int GetPeak() {
        int peak = 0;
        foreach (MinoBlock i in cBlocks){
            if(i.curCoord.y > peak) {
                peak = i.curCoord.y;
            }
        }
        return peak;
    }


}
