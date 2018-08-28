using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SManager : MonoBehaviour {

    public GManager manager;
    public BlockManager bManager;

    public GameObject[] spawners;
    public Mino[] spawnPool;

    public float nextSpawn;
    public float sBaseDelay;
    public float sDelayOffset;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindWithTag("MainCamera").GetComponent<GManager>();

        Spawn();
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time > nextSpawn) {
            Spawn();
        }
	}

    void Spawn() {
        int ii = Random.Range(0, spawnPool.Length);
        Vector3 nextSpawnPos = transform.position;
        int sOffset = Random.Range(0 + spawnPool[ii].minOffsetMod, manager.gridSize.x + spawnPool[ii].maxOffsetMod);
        nextSpawnPos.x += sOffset;
        GameObject.Instantiate(spawnPool[ii], nextSpawnPos, spawnPool[ii].transform.rotation, manager.bManager.transform);

        SetNextSpawn();
    }

    void SetNextSpawn() {
        nextSpawn = Time.time + sBaseDelay + Random.Range(-sDelayOffset, sDelayOffset);
    }
}
