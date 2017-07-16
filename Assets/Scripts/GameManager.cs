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

	float startTimer = 5.0f;
	float gameTimer = 15.0f;

	float timer;
	// float lowTimeRemainingThreshold = 10.0f;

	bool gamePaused = true; // We use this to pause the game before the game starts
	bool gameEnded = false;
	bool startTimerStarted = false;

	GameManager() {
		timer = startTimer;
		mGridManager = new GridManager();
	}

	void Start () {
		mTileManagerRef = GameObject.FindWithTag("TileManager").GetComponent<TileManager>();
		mPlayerManagerRef = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
		mWinText.transform.parent.gameObject.SetActive(false);
		mTimeLeftText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

		if (startTimerStarted) {
			timer -= Time.deltaTime;
			mTimeLeftText.text = ((int)timer).ToString();
			if (timer <= 0) {
				GameStart();
			}
		}

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
		int totalNumberOfTiles = ((int)mGridManager.mGridSize.x) * ((int)mGridManager.mGridSize.y);

		mScoreBar.value = (float)tilesOwned[1] / (float)totalNumberOfTiles;
	}

	public void BeginStartTimer() {
		startTimerStarted = true;
		mTimeLeftText.text = ((int)timer).ToString();
	}

	void GameStart() {
		mPlayerManagerRef.GameHasStarted();
		startTimerStarted = false;
		timer = gameTimer;
		mTimeLeftText.text = ((int)timer).ToString();
		gamePaused = false;
	}

	void GameEnd() {
		mPlayerManagerRef.GameHasEnded();
		gameEnded = true;
		mTimeLeftText.text = "";
		
		int[] tilesOwned = mTileManagerRef.GetTileCountsByPlayer();
		int winningPlayerIndex = 0;
		int winningPlayerNumTiles = 0;
		for (int i = 1; i < tilesOwned.GetLength(0); ++i) {
			if (tilesOwned[i] > winningPlayerNumTiles) {
				winningPlayerNumTiles = tilesOwned[i];
				winningPlayerIndex = i;
			}
		}

		mWinText.text = string.Format("Player {0} Wins!", winningPlayerIndex);
		mWinText.transform.parent.gameObject.SetActive(true);
	}
}
