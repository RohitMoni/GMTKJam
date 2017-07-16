using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;
	
	GameObject[] players;
	float playerScaleFactor = 10f;	
	bool acceptPlayerInput;
	int numberOfEnabledPlayers = 0;
	bool gameStarted = false;

	GameManager mGameManagerRef;
	GridManager mGridManagerRef;
	TileManager mTileManagerRef;

	// Use this for initialization
	void Start () {
		mGameManagerRef = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		mGridManagerRef = mGameManagerRef.mGridManager;
		mTileManagerRef = GameObject.FindWithTag("TileManager").GetComponent<TileManager>();
		
		Vector2 gridSize = mGameManagerRef.mGridManager.mGridSize;
		Vector2 scaleXZ = new Vector2(1.0f/gridSize.x*playerScaleFactor, 1.0f/gridSize.y*playerScaleFactor);

		acceptPlayerInput = false;

		players = new GameObject[4];

		for (int i = 0; i < players.GetLength(0); ++i) {
			players[i] = Instantiate(playerPrefab) as GameObject;
			players[i].transform.parent = transform;
			players[i].name = string.Format("Player {0}", i+1);
			players[i].transform.localScale = new Vector3(scaleXZ.x, scaleXZ.x, scaleXZ.y);

			PlayerScript script = players[i].GetComponent<PlayerScript>();
			script.playerNumber = i+1;
			script.MoveToStartPosition();

			players[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		string horizAxis = "Horizontal";
		string vertAxis = "Vertical";
		for (int i = 1 ; i <= players.GetLength(0); ++i) {
			if (i > 1) {
				horizAxis = string.Format("Horizontal{0}", i);
				vertAxis = string.Format("Vertical{0}", i);
			}

			if (Input.GetAxis(horizAxis) != 0 || Input.GetAxis(vertAxis) != 0) {
				if (!players[i-1].activeSelf) {
					EnablePlayer(i);
					numberOfEnabledPlayers++;
				}
			}
		}
		
		if (numberOfEnabledPlayers > 1 && !gameStarted) {
			mGameManagerRef.BeginStartTimer();
			gameStarted = true;
		}
	}

	public bool IsGridSpaceOccupiedByAnyPlayer(Vector2 gridPos) {
		for (int i = 0; i < players.GetLength(0); ++i) {
			if (players[i].activeSelf) {
				PlayerScript script = players[i].GetComponent<PlayerScript>();
				if (script.mOldGridPosInt == gridPos) {
					return true;
				}
			}
		}

		return false;
	}

	// Player index 1-4
	public void EnablePlayer(int playerNumber) {
		players[playerNumber-1].SetActive(true);
		players[playerNumber-1].GetComponent<Renderer>().enabled = false;
		players[playerNumber-1].name = string.Format("Player {0}", playerNumber);
		
		PlayerScript script = players[playerNumber-1].GetComponent<PlayerScript>();
		script.MoveToStartPosition();
		players[playerNumber-1].GetComponent<Renderer>().enabled = true;

		StartCoroutine(SetPlayerStartingTiles(playerNumber-1, mGameManagerRef.timer));
	}

	public void DisablePlayer(int playerNumber) {
		players[playerNumber-1].SetActive(false);
		players[playerNumber-1].name = string.Format("Player {0} (Disabled)", playerNumber);
	}

	public bool GetAcceptPlayerInput() {
		return acceptPlayerInput;
	}

	public void GameHasEnded() {
		acceptPlayerInput = false;
	}

	public void GameHasStarted() {
		acceptPlayerInput = true;
	}

	// Co-routine to set starting tiles for enabled players
	private IEnumerator SetPlayerStartingTiles(int playerIndex, float timeLeft) {
		// Goal is to set all the players starting tiles in a nice way, interpolated for the time remaining before the game starts
		int playerNumber = playerIndex+1;
		int playerReach = (int)mGridManagerRef.mGridSize.x / 3;
		Vector2 startingPlayerGridPos = mGridManagerRef.GetGridPosForPlayer(playerIndex+1);
		Vector2 playerDirToCenter = mGridManagerRef.GetDirToCenterForPlayer(playerIndex+1);
		float timePerTileFlip = timeLeft / (float)playerReach;
		
		List<Vector2> tilesToSet = new List<Vector2>();
		tilesToSet.Add(startingPlayerGridPos);

		int remainingReach = playerReach;

		do {
			foreach (Vector2 tile in tilesToSet) {
				mTileManagerRef.SetTileAt(tile, playerNumber);
			}

			remainingReach--;
			List<Vector2> newTilesToSet = new List<Vector2>();
			foreach (Vector2 tile in tilesToSet) {
				Vector2 reachedTileX = tile + new Vector2(playerDirToCenter.x, 0);
				Vector2 reachedTileY = tile + new Vector2(0, playerDirToCenter.y);
				newTilesToSet.Add(reachedTileX);
				newTilesToSet.Add(reachedTileY);
			}

			// remove duplicates
			tilesToSet.Clear();
			tilesToSet = newTilesToSet.Distinct().ToList();

			yield return new WaitForSeconds(timePerTileFlip);

		} while (remainingReach > 0);
	}
}
