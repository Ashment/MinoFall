using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    public GameObject flashObj;
    public float flashDelay = 0.8f;

    float nextFlash;
    bool flashStatus;

    float inputDelay = 2f;

	// Use this for initialization
	void Start () {
        inputDelay += Time.time;
        flashStatus = true;
        nextFlash = Time.time + flashDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= nextFlash) {
            flashObj.SetActive(flashStatus);
            flashStatus = !flashStatus;
            nextFlash += flashDelay;
        }

        if (Input.anyKey && Time.time > inputDelay) {
            SceneManager.LoadScene("GameScene");
        }
	}
}
