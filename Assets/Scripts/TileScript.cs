using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	public Material mMaterialA;
	public Material mMaterialB;
	public Material mMaterialC;
	public Material mMaterialD;

	public int tileVal;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material = mMaterialA;
		SetTileTo(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTileTo(int val) {
		switch (val) {
			case 1:
			gameObject.GetComponent<Renderer>().material = mMaterialA;  			
			break;
			case 2:
			gameObject.GetComponent<Renderer>().material = mMaterialB;  			
			break;
			case 3:
			gameObject.GetComponent<Renderer>().material = mMaterialC;  			
			break;
			case 4:
			gameObject.GetComponent<Renderer>().material = mMaterialD;  			
			break;
			default:
			break;
		}

		tileVal = val;
	}
}
