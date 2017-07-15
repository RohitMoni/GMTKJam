using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	public Material mMaterialA;
	public Material mMaterialB;

	bool flip;

	// Use this for initialization
	void Start () {
		flip = false;
		gameObject.GetComponent<Renderer>().material = mMaterialA;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FlipTile() {
		gameObject.GetComponent<Renderer>().material = flip ? mMaterialA : mMaterialB;  
		flip = !flip;
	}
}
