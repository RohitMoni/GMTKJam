using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	public Material mMaterialA;
	public Material mMaterialB;
	public Material mMaterialC;
	public Material mMaterialD;
	public Material mMaterialNone;

	public int tileVal;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material = mMaterialA;
		SetTileTo(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTileTo(int val) {
		switch (val) {
			case 0:
			gameObject.GetComponent<Renderer>().material = mMaterialNone;
			break;  						
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
			gameObject.GetComponent<Renderer>().material = mMaterialNone;
			break;
		}

		tileVal = val;
	}
}
