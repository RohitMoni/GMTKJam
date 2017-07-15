using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject mTileObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateTileAt(Vector3 pos) {
		GameObject newTile = Instantiate(mTileObject) as GameObject;
		newTile.transform.parent = transform;
	}
}
