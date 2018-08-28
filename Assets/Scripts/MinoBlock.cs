using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoBlock : GridEntity {
    public GManager manager;

    // Use this for initialization
    void Start () {
        manager = GameObject.FindWithTag("MainCamera").GetComponent<GManager>();
        state = bState.mblock;
        //Initialize Player Pos Coord.
        curCoord = new Vector2Int
        {
            x = Mathf.RoundToInt(transform.position.x - manager.gridZero.x),
            y = Mathf.RoundToInt(transform.position.y - manager.gridZero.y)
        };
        targetCoord = curCoord;

    }
	
	// Update is called once per frame
	void Update () {

        Vector2 relPos = new Vector2(transform.position.x, transform.position.y) - manager.gridZero;
        curCoord.x = Mathf.RoundToInt(transform.position.x - manager.gridZero.x);
        curCoord.y = Mathf.RoundToInt(transform.position.y - manager.gridZero.y);
        targetCoord.x = Mathf.Clamp(targetCoord.x, 0, manager.gridSize.x - 1);
        targetCoord.y = Mathf.Clamp(targetCoord.y, 0, manager.gridSize.y - 1);
        manager.gridE[targetCoord.x, targetCoord.y] = this;

        if (relPos != targetCoord)
        {
            Vector3 targetPosVec = new Vector3(targetCoord.x + manager.gridZero.x, targetCoord.y + manager.gridZero.y, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosVec, 0.1f);
        }
    }

    public bool ValidDrop() {
        if (curCoord.y <= 0) {
            return false;
        }

        if (manager.gridE[curCoord.x, curCoord.y - 1].state == bState.mblock){
            if(manager.gridE[curCoord.x, curCoord.y - 1].transform.parent == transform.parent){
                return manager.gridE[curCoord.x, curCoord.y - 1].gameObject.GetComponent<MinoBlock>().ValidDrop();
            }
            else{
                return false;
            }
        }
        return true;
    }

    public bool SegValidDrop()
    {
        if (curCoord.y <= 0)
        {
            return false;
        }

        if (manager.gridE[curCoord.x, curCoord.y - 1].state == bState.mblock)
        {
            return false;
        }
        return true;
    }

    public bool ValidNudge(int dir) {
        if(dir > 0) {
            if(curCoord.x >= manager.gridSize.x - 1) {
                return false;
            }else if (manager.gridE[curCoord.x + 1, curCoord.y].state == bState.mblock)
            {
                if (manager.gridE[curCoord.x + 1, curCoord.y].transform.parent == transform.parent && manager.gridE[curCoord.x + 1, curCoord.y].GetComponent<MinoBlock>().ValidNudge(dir))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        if (dir < 0)
        {
            if (curCoord.x <= 0)
            {
                return false;
            }else if (manager.gridE[curCoord.x - 1, curCoord.y].state == bState.mblock)
            {
                if (manager.gridE[curCoord.x - 1, curCoord.y].transform.parent == transform.parent)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void ClearBlock() {
        manager.gridE[targetCoord.x, targetCoord.y] = manager.freeState;
        transform.parent.GetComponent<Mino>().segmented = true;
        Destroy(gameObject);
    }
}
