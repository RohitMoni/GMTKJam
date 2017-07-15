using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	public Material mMaterialA;
	public Material mMaterialB;
	public Material mMaterialC;
	public Material mMaterialD;

	bool flip;

	// Use this for initialization
	void Start () {
		flip = false;
		gameObject.GetComponent<Renderer>().material = mMaterialA;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTileTo(int val) {
		switch (val) {
			case 0:
			gameObject.GetComponent<Renderer>().material = mMaterialA;  			
			break;
			case 1:
			gameObject.GetComponent<Renderer>().material = mMaterialB;  			
			break;
			case 2:
			gameObject.GetComponent<Renderer>().material = mMaterialC;  			
			break;
			case 3:
			gameObject.GetComponent<Renderer>().material = mMaterialD;  			
			break;
			default:
			break;
		}
	}

	public void FlipTile() {
		gameObject.GetComponent<Renderer>().material = flip ? mMaterialA : mMaterialB;  
		flip = !flip;
	}
}
