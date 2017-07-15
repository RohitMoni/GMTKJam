using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager {

    public Vector2 mGridSize;
    public Vector2 mGridCellSize;

	public GridManager () {
		mGridSize.Set(10, 10);
        mGridCellSize.Set(1, 1);
	}

    public bool IsWithinBounds(int x, int y) {
        return x >= 0 && x <= mGridSize.x && y >= 0 && y <= mGridSize.y;
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
        return new Vector2( Mathf.Floor((x + (mGridSize.x * mGridCellSize.x * 0.5f)) / mGridCellSize.x),
                        Mathf.Floor((y + (mGridSize.y * mGridCellSize.y * 0.5f)) / mGridCellSize.y));
    }

    public Vector2 World2Grid(Vector2 worldPos) {
        return World2Grid((int)Mathf.Floor(worldPos.x), (int)Mathf.Floor(worldPos.y));
    }

    // 0 index
    public Vector2 Grid2World(int x, int y) {
        return new Vector2( (x * mGridCellSize.x) - (mGridSize.x * mGridCellSize.x * 0.5f),
                        (y * mGridCellSize.y) - (mGridSize.y * mGridCellSize.y * 0.5f));
    }

    public Vector2 Grid2World(Vector2 gridPos) {
        return Grid2World((int)Mathf.Floor(gridPos.x), (int)Mathf.Floor(gridPos.y));
    }
}