using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject tilePrefab;
	
	GameObject[,] mTileInstances;  
	GridManager mGridManagerRef;

	// Use this for initialization
	void Start () {
		mGridManagerRef = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().mGridManager;
		mTileInstances = new GameObject[0,0];
		CreateGridTiles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int[] GetTileCountsByPlayer() {
		int[] numberOfTilesOwned = new int[5] {0, 0, 0, 0, 0};

		for (int i = 0; i < mTileInstances.GetLength(0); ++i) {
			for (int j = 0; j < mTileInstances.GetLength(1); ++j) {
				GameObject tile = mTileInstances[i,j];
				TileScript script = tile.GetComponent<TileScript>();
				numberOfTilesOwned[script.tileVal]++;
			}
		}

		return numberOfTilesOwned;
	}

	public bool IsTileAtPosHaveVal(Vector2 gridPos, int tileVal) {
		if (!mGridManagerRef.IsWithinBounds(gridPos)) {
			return false;
		}
		
		GameObject tileObj = mTileInstances[(int)gridPos.x, (int)gridPos.y];
		TileScript script = tileObj.GetComponent<TileScript>();
		
		return script.tileVal == tileVal;
	}

	public void SetTilesAt(Vector2[] gridPositions, int tileVal) {
		for (int i = 0; i < gridPositions.GetLength(0); ++i) {
			if (mGridManagerRef.IsWithinBounds(gridPositions[i])) {
				GameObject tile = mTileInstances[(int)gridPositions[i].x, (int)gridPositions[i].y];
				TileScript script = tile.GetComponent<TileScript>();
				script.SetTileTo(tileVal);
			}
		}
	}

	public void SetTileAt(Vector2 gridPosition, int tileVal) {
		if (mGridManagerRef.IsWithinBounds(gridPosition)) {
			GameObject tile = mTileInstances[(int)gridPosition.x, (int)gridPosition.y];
			TileScript script = tile.GetComponent<TileScript>();
			script.SetTileTo(tileVal);
		}
	}

	void CleanGridTiles() {
		for (int i = 0; i < mTileInstances.GetLength(0); ++i) {
			for (int j = 0; j < mTileInstances.GetLength(1); ++j) {
				Destroy(mTileInstances[i,j]);
			}
		}
	}

	void CreateGridTiles() {
		CleanGridTiles();

		Vector2 gridSize = mGridManagerRef.mGridSize;
		mTileInstances = new GameObject[(int)gridSize.x, (int)gridSize.y];

        const float scaleFactor = 7f/12f;
		Vector2 scaleXZ = new Vector2((gridSize.x-1) * scaleFactor, (gridSize.y-1) * scaleFactor);

		for (int i = 0; i < mTileInstances.GetLength(0); ++i) {
			GameObject tileHolder = new GameObject();
			tileHolder.transform.parent = transform;
			tileHolder.name = i.ToString();

			for (int j = 0; j < mTileInstances.GetLength(1); ++j) {
				Vector2 pos = mGridManagerRef.Grid2World(i, j);
				mTileInstances[i,j] = CreateTileAt(new Vector3(pos.x, 0.01f, pos.y), scaleXZ, tileHolder.transform);
			}
		}
	}

	GameObject CreateTileAt(Vector3 pos, Vector2 scaleXZ, Transform parent) {
		GameObject newTile = Instantiate(tilePrefab) as GameObject;
		newTile.transform.position = pos;
		newTile.transform.localScale = new Vector3(scaleXZ.x, 12, scaleXZ.y);
		newTile.transform.parent = parent;

		return newTile;
	}
}
