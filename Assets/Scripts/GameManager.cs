using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GridManager mGridManager;
	public Slider mScoreBar;
	public Text mTimeLeftText;
	public Text mWinText;

	TileManager mTileManagerRef;
	PlayerManager mPlayerManagerRef;

	float timer = 15.0f;
	// float lowTimeRemainingThreshold = 10.0f;

	bool gameEnded = false;
	bool gamePaused = false;

	GameManager() {
		mGridManager = new GridManager();
	}

	void Start () {
		mTileManagerRef = GameObject.FindWithTag("TileManager").GetComponent<TileManager>();
		mPlayerManagerRef = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
		mWinText.transform.parent.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if (gamePaused || gameEnded) {
			return;
		}

		// Update timer
		timer -= Time.deltaTime;
		mTimeLeftText.text = ((int)timer).ToString();

		if (timer <= 0) {
			GameEnd();
		}

		// Update score bar
		int[] tilesOwned = mTileManagerRef.GetTileCountsByPlayer();
		int totalNumberOfTiles = ((int)mGridManager.mGridSize.x+1) * ((int)mGridManager.mGridSize.y+1);

		mScoreBar.value = (float)tilesOwned[0] / (float)totalNumberOfTiles;
	}

	void GameEnd() {
		mPlayerManagerRef.GameHasEnded();
		gameEnded = true;
		
		int[] tilesOwned = mTileManagerRef.GetTileCountsByPlayer();
		int winningPlayerIndex = 0;
		int winningPlayerNumTiles = 0;
		for (int i = 0; i < tilesOwned.GetLength(0); ++i) {
			if (tilesOwned[i] > winningPlayerNumTiles) {
				winningPlayerNumTiles = tilesOwned[i];
				winningPlayerIndex = i+1;
			}
		}

		mWinText.text = string.Format("Player {0} Wins!", winningPlayerIndex);
		mWinText.transform.parent.gameObject.SetActive(true);
	}
}
