using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridEntity {

    public GManager manager;
    public GameObject[] curShotUI;
    public GameObject[] nextShotUI;

    public Transform shotSpawn;
    public GameObject shotFX;


    public int curShot, nextShot;

    public bool canMove = true;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindWithTag("MainCamera").GetComponent<GManager>();
        state = bState.player;
        //Initialize Player Pos Coord.
        curCoord = new Vector2Int
        {
            x = Mathf.RoundToInt(transform.position.x - manager.gridZero.x),
            y = Mathf.RoundToInt(transform.position.y - manager.gridZero.y)
        };

        targetCoord = curCoord;

        curShot = GetRandomShot();
        nextShot = GetRandomShot();

    }
	
	// Update is called once per frame
	void Update () {
        UpdateShotUI();

        //APPLY NUDGE SHOT
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            ApplyCurNudge();
            GameObject.Instantiate(shotFX, shotSpawn.position, Quaternion.identity);
        }

//        manager.gridE[curCoord.x, curCoord.y] = this;
        //PLAYER MOVEMENT
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetCoord.x -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetCoord.x += 1;
        }
        targetCoord.y = manager.curPeak+1;

        //MOVEMENT AND COORD POSITION UPDATE
        targetCoord.x = Mathf.Clamp(targetCoord.x, 0, manager.gridSize.x - 1);
        curCoord.x = Mathf.RoundToInt(transform.position.x - manager.gridZero.x);
        curCoord.y = Mathf.RoundToInt(transform.position.y - manager.gridZero.y);
        if (relPos != targetCoord)
        {
            Vector3 targetPosVec = new Vector3(targetCoord.x + manager.gridZero.x, targetCoord.y + manager.gridZero.y, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosVec, 0.1f);
        }

	}

    void ApplyCurNudge() {
        Mino target = null;
        bool targetFound = false;
        for(int i = curCoord.y; i < manager.gridSize.y; i++) {
            if(manager.gridE[curCoord.x, i].state == bState.mblock) {
                target = manager.gridE[curCoord.x, i].transform.parent.GetComponent<Mino>();
                targetFound = true;
                break;
            }
        }

        if (targetFound) {
            target.Nudge(curShot);
        }
        else {
            manager.invalidSFO.GetComponent<AudioSource>().Play();
        }

        curShot = nextShot;
        nextShot = GetRandomShot();

        return;
    }

    void UpdateShotUI() {
        if(curShot == -1) {
            curShotUI[0].SetActive(true);
            curShotUI[1].SetActive(false);
        }else{
            curShotUI[1].SetActive(true);
            curShotUI[0].SetActive(false);
        }

        if(nextShot == -1) {
            nextShotUI[0].SetActive(true);
            nextShotUI[1].SetActive(false);
        }else{
            nextShotUI[1].SetActive(true);
            nextShotUI[0].SetActive(false);
        }
    }

    int GetRandomShot() {
        int ret = Random.Range(-1, 2);
        while(ret == 0){
            ret = Random.Range(-1, 2);
        }
        return ret;
    }
}
