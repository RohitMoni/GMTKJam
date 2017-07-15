using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Starts at 1, 1-4
	public int playerNumber = 1;

	public Vector2 mOldGridPosInt; // This is actually the current pos its on
	Vector2 mGridPosInt;	// These are the pos we're moving to / towards
	Vector2 mGridPos;
	float mSpeedConst;

	GridManager mGridManagerRef;
	TileManager mTileManagerRef;
	PlayerManager mPlayerManagerRef;

	// Use this for initialization
	void Awake () {
		mSpeedConst = 5.0f;

		mGridManagerRef = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().mGridManager;
		mTileManagerRef = GameObject.FindWithTag("TileManager").GetComponent<TileManager>();
		mPlayerManagerRef = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
	}

	public void MoveToStartPosition() {
		mGridPos = mGridManagerRef.GetGridPosForPlayer(playerNumber);
		mGridPosInt.Set((int)mGridPos.x, (int)mGridPos.y);
		mOldGridPosInt = mGridPosInt;

		UpdateWorldPos();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateWithInput();
	}

	void SetTiles() {
		Vector2[] tilesToSet = new Vector2[9];
		// Square pattern around the player
		int n = 0;
		for (int i = (int)mGridPosInt.x-1; i <= (int)mGridPosInt.x+1; ++i) {
			for (int j = (int)mGridPosInt.y-1; j <= (int)mGridPosInt.y+1; ++j) {
				tilesToSet[n++] = new Vector2(i, j);
			}
		}

		mTileManagerRef.SetTilesAt(tilesToSet, playerNumber);
	}

	void UpdateWorldPos () {
		Vector2 worldPos = mGridManagerRef.Grid2World(mGridPosInt);
		transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.y);
		mOldGridPosInt = mGridPosInt;

		SetTiles();
	}

	void UpdateWithInput() {
		string horizAxis = "Horizontal";
		string vertAxis = "Vertical";
		if (playerNumber > 1) {
			horizAxis = string.Format("{0}{1}", horizAxis, playerNumber);
			vertAxis = string.Format("{0}{1}", vertAxis, playerNumber);
		}

		var x = Input.GetAxis(horizAxis) * Time.deltaTime * mSpeedConst;
        var z = Input.GetAxis(vertAxis) * Time.deltaTime * mSpeedConst;

		mGridPos.x = Mathf.Clamp(mGridPos.x + x, 0, mGridManagerRef.mGridSize.x);
		mGridPos.y = Mathf.Clamp(mGridPos.y + z, 0, mGridManagerRef.mGridSize.y);

		mGridPosInt.x = (int)mGridPos.x;
		mGridPosInt.y = (int)mGridPos.y;

		if (mOldGridPosInt != mGridPosInt) {
			
			// Trying to move
			// If its not owned by us or its occupied, can't move there
			if (mTileManagerRef.IsTileAtPosHaveVal(mGridPosInt, playerNumber) &&
				!mPlayerManagerRef.IsGridSpaceOccupiedByAnyPlayer(mGridPosInt)) {
				UpdateWorldPos();

			}
			else {
				mGridPosInt = mOldGridPosInt;
				mGridPos = mGridPosInt;
			}
		}
	}
}
