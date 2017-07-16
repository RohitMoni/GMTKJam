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
	float mOpposingTileModifier;

	GridManager mGridManagerRef;
	TileManager mTileManagerRef;
	PlayerManager mPlayerManagerRef;

	// Use this for initialization
	void Awake () {
		mSpeedConst = 5.0f;
		mOpposingTileModifier = 0.5f;

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
		if (mPlayerManagerRef.GetAcceptPlayerInput()) {
			UpdateWithInput();
		}
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

		Vector2 direction = new Vector2(Input.GetAxis(horizAxis), Input.GetAxis(vertAxis));
		Vector2 targetGridPos = mGridPosInt + direction;

		Vector2 movement = direction * Time.deltaTime * mSpeedConst;

		// Move slower on other players tiles
		if (!mTileManagerRef.IsTileAtPosHaveVal(mGridPosInt, playerNumber)) {
			movement *= mOpposingTileModifier;
		}
		
		mGridPos.x = Mathf.Clamp(mGridPos.x + movement.x, 0, mGridManagerRef.mGridSize.x);
		mGridPos.y = Mathf.Clamp(mGridPos.y + movement.y, 0, mGridManagerRef.mGridSize.y);

		mGridPosInt.x = (int)mGridPos.x;
		mGridPosInt.y = (int)mGridPos.y;

		if (mOldGridPosInt != mGridPosInt) {
			
			// Trying to move
			// If its not owned by us or its occupied, can't move there
			if (!mPlayerManagerRef.IsGridSpaceOccupiedByAnyPlayer(mGridPosInt)) {
				UpdateWorldPos();
			}
			else {
				mGridPosInt = mOldGridPosInt;
				mGridPos = mGridPosInt;
			}
		}
	}
}
