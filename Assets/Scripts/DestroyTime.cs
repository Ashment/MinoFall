using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour {

    public float timeDelay = 0.08f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeDelay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
