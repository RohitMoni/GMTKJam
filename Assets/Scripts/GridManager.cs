using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GridManager {

    public Vector2 mGridSize;
    public Vector2 mGridCellSize;

	public GridManager () {
		mGridSize.Set(13, 13);
        mGridCellSize.Set(10/(mGridSize.x-1),10/(mGridSize.y-1));
	}

    // playerNumber is 1-4
    public Vector2 GetGridPosForPlayer(int playerNumber) {
        Vector2 playerPos = new Vector2();
        switch (playerNumber) {
            case 1: // bottom left
                playerPos.Set(1f, 1f);
                break;
            case 2: // top right
                playerPos.Set(mGridSize.x, mGridSize.y);
                break;
            case 3: // bottom right
                playerPos.Set(mGridSize.x, 1f);
                break;
            case 4: // top left
                playerPos.Set(1f, mGridSize.y);
                break;
            default:
                Debug.Log("Invalid Player Number!");
                Assert.IsTrue(true);
                playerPos.Set(1f, 1f);
                break;
        }

        // This is to convert it from 1-gridsize -> 0-gridSize-1 (0 index)
        playerPos -= new Vector2(1, 1);        
        return playerPos;
    }

    public Vector2 GetDirToCenterForPlayer(int playerNumber) {
        Vector2 dir = new Vector2();
        switch (playerNumber) {
            case 1: // bottom left
                dir.Set(1f, 1f);
                break;
            case 2: // top right
                dir.Set(-1f, -1f);
                break;
            case 3: // bottom right
                dir.Set(-1f, 1f);
                break;
            case 4: // top left
                dir.Set(1f, -1f);
                break;
            default:
                Debug.Log("Invalid Player Number!");
                Assert.IsTrue(true);
                dir.Set(1f, 1f);
                break;
        }

        return dir;
    }

    public bool IsWithinBounds(int x, int y) {
        return x >= 0 && x < mGridSize.x && y >= 0 && y < mGridSize.y;
    }

    public bool IsWithinBounds(Vector2 gridPos) {
        return IsWithinBounds((int)Mathf.Floor(gridPos.x), (int)Mathf.Floor(gridPos.y));
    }
	
    public void SetmGridSize(int x, int y) {
        mGridSize.Set(x, y);
    }

    public void SetmGridCellSize(float x, float y) {
        mGridCellSize.Set(x, y);
    }

    public Vector2 World2Grid(float x, float y) {
        return new Vector2( Mathf.Floor((x + ((mGridSize.x-1) * mGridCellSize.x * 0.5f)) / mGridCellSize.x),
                        Mathf.Floor((y + ((mGridSize.y-1) * mGridCellSize.y * 0.5f)) / mGridCellSize.y));
    }

    public Vector2 World2Grid(Vector2 worldPos) {
        return World2Grid((int)Mathf.Floor(worldPos.x), (int)Mathf.Floor(worldPos.y));
    }

    // 0 index
    public Vector2 Grid2World(int x, int y) {
        return new Vector2( (x * mGridCellSize.x) - ((mGridSize.x-1) * mGridCellSize.x * 0.5f),
                        (y * mGridCellSize.y) - ((mGridSize.y-1) * mGridCellSize.y * 0.5f));
    }

    public Vector2 Grid2World(Vector2 gridPos) {
        return Grid2World((int)Mathf.Floor(gridPos.x), (int)Mathf.Floor(gridPos.y));
    }
}