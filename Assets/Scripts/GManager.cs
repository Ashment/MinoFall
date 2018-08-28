using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GManager : MonoBehaviour {
    public bool gameOver = false;
    public float killTime;

    public int curPeak;
    public int maxPeak = 10;

    public int GetPeakTimer = 0;

    public GameObject clearSFO, invalidSFO, failSFO, shotSFO;

    public Transform GOTop, GOBot;
    public TextMeshPro FinalScoreDisp;
    public Vector3 GOTopTarget;
    public Vector3 GOBotTarget;

    public Vector2Int gridSize = new Vector2Int(9, 10);
    public Vector2 gridZero = new Vector2(-3.5f, -0.5f);
    public Vector2Int testCoord = new Vector2Int(0,0);

    public float tickTime = 0.5f;

    public bState[,] gridS;
    public GridEntity[,] gridE;

    public BlockManager bManager;
    public SManager sManager;
    public GridEntity freeState;

    public float curScore = 0;
    public TextMeshPro scoreDisp;

    public GameObject lineClearFX;

    float lastTick = 0f;
    // Use this for initialization
    void Start() {
        curPeak = 0;
        gridE = new GridEntity[gridSize.x, gridSize.y];
        for(int i=0; i<gridSize.x; i++) {
            for(int j=0; j<gridSize.y; j++) {
                gridE[i, j] = freeState;
            }
        }

        bManager = GameObject.FindWithTag("BManager").GetComponent<BlockManager>();
        sManager = GameObject.FindWithTag("SManager").GetComponent<SManager>();
    }

    // Update is called once per frame
    void Update() {
        curScore += Time.deltaTime;
        scoreDisp.SetText(Mathf.RoundToInt(curScore).ToString());

        if (Time.time > lastTick + tickTime) {
            Tick();
            lastTick = Time.time;
        }

        if (curPeak >= maxPeak) {
            GameOver();
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            Time.timeScale = 5f;
        }else{
            Time.timeScale = 1f;
        }

    }

    void Tick() {
        if (!gameOver) {
            bManager.TickUpdate();
            FindAndClear();
            curPeak = GetNewPeak();

            print(gridE[testCoord.x, testCoord.y].state);
        }
    }

    void FindAndClear() {
        bool brokenRow = false;
        for(int iY=0; iY < gridSize.y; iY+=1) {
        brokenRow = false;
            for (int iX=0; iX < gridSize.x; iX+=1) {
                if(gridE[iX, iY].state == bState.free) {
                    brokenRow = true;
                    break;
                }
            }
            if(brokenRow == false) {
                ClearRow(iY);
            }
        }
        
    }

    void ClearRow(int yy) {
        for (int iX=0; iX < gridSize.x; iX+=1) {
            MinoBlock curBlock = gridE[iX, yy].GetComponent<MinoBlock>();
            curBlock.ClearBlock();
        }
        GameObject.Instantiate(lineClearFX, new Vector3(0f, yy + gridZero.y, 0.5f), Quaternion.Euler(new Vector3(0, 0, 90)));
        clearSFO.GetComponent<AudioSource>().Play();
        bManager.TickUpdate();
        curScore += 100;
        GetPeakTimer = 1;
    }

    int GetNewPeak() {
        if(GetPeakTimer > 0) {
            GetPeakTimer -= 1;
            return curPeak;
        }
        int newPeak = 0;
        for(int iY=0; iY < gridSize.y; iY+=1) {
            for (int iX=0; iX < gridSize.x; iX+=1) {
                if(gridE[iX, iY].state == bState.mblock) {
                    newPeak = gridE[iX, iY].targetCoord.y;
                }
            }
        }
        return newPeak;
    }

    void GameOver() {
        if (!gameOver) {
            gameOver = true;
            FinalScoreDisp.text = Mathf.RoundToInt(curScore).ToString();
            failSFO.GetComponent<AudioSource>().Play();
            killTime = Time.time + 1.5f;
        }
        
        GOTop.localPosition = Vector3.MoveTowards(GOTop.localPosition, GOTopTarget, 7f*Time.deltaTime);
        GOBot.localPosition = Vector3.MoveTowards(GOBot.localPosition, GOBotTarget, 7f*Time.deltaTime);

        if (Input.anyKeyDown && Time.time > killTime) {
            Application.Quit();
        }
    }
}
