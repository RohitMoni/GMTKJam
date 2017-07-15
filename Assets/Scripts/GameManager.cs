using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GridManager mGridManager;
	public Slider mScoreBar;

	TileManager mTileManagerRef;

	GameManager() {
		mGridManager = new GridManager();
	}

	void Start () {
		mTileManagerRef = GameObject.FindWithTag("TileManager").GetComponent<TileManager>();
	}
	
	// Update is called once per frame
	void Update () {
		int[] tilesOwned = mTileManagerRef.GetTileCountsByPlayer();
		int totalNumberOfTiles = ((int)mGridManager.mGridSize.x+1) * ((int)mGridManager.mGridSize.y+1);

		mScoreBar.value = (float)tilesOwned[0] / (float)totalNumberOfTiles;
	}
}
