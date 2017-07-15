using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	Vector2 mGridPos;
	float mSpeedConst;

	GridTileManager mGridTileManagerRef;

	// Use this for initialization
	void Start () {
		mGridPos.Set(0, 0);
		mSpeedConst = 5.0f;

		mGridTileManagerRef = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().mGridTileManager;

		UpdateWorldPos();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateWithKeyboardInput();
	}

	void UpdateWorldPos () {
		Vector2 worldPos = mGridTileManagerRef.Grid2World(mGridPos);
		transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.y);
	}

	void UpdateWithKeyboardInput() {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * mSpeedConst;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * mSpeedConst;

		mGridPos.x = Mathf.Clamp(mGridPos.x + x, 0, mGridTileManagerRef.mGridSize.x);
		mGridPos.y = Mathf.Clamp(mGridPos.y + z, 0, mGridTileManagerRef.mGridSize.y);

		UpdateWorldPos();
	}
}
