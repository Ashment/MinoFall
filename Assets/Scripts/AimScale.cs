using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScale : MonoBehaviour {

    public float minSize = 0.05f;
    public float maxSize = 1f;
    public float shrinkSpd = 0.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localScale.x > minSize) {
            transform.localScale = new Vector3(transform.localScale.x - shrinkSpd * Time.deltaTime, transform.localScale.y, transform.localScale.z);
        }else{
            transform.localScale = new Vector3(maxSize, transform.localScale.y, transform.localScale.z);
        }
	}
}
