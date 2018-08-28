using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour {

    Material tMat;
    public float spd = 0.35f;

	// Use this for initialization
	void Start () {
        tMat = gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        tMat.SetTextureOffset("_MainTex", new Vector2(spd * Time.time, 0));
	}
}
