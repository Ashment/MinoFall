using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFX : MonoBehaviour {

    public float startSize = 0.05f;
    public float maxSize = 1f;
    public float shrinkSpd = 0.5f;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(maxSize, transform.localScale.y, transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(transform.localScale.x - shrinkSpd * Time.deltaTime, transform.localScale.y, transform.localScale.z);
        if (transform.localScale.x <= 0){
            Destroy(gameObject);
        }
    }
}
