using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;
	GameObject[] players;

	// Use this for initialization
	void Start () {
		players = new GameObject[4];

		for (int i = 0; i < players.GetLength(0); ++i) {
			players[i] = Instantiate(playerPrefab) as GameObject;
			players[i].transform.parent = transform;
			players[i].name = string.Format("Player {0}", i+1);

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
				}
			}
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
	public void EnablePlayer(int playerIndex) {
		players[playerIndex-1].SetActive(true);
		players[playerIndex-1].GetComponent<Renderer>().enabled = false;
		players[playerIndex-1].name = string.Format("Player {0}", playerIndex);
		
		PlayerScript script = players[playerIndex-1].GetComponent<PlayerScript>();
		script.MoveToStartPosition();
		players[playerIndex-1].GetComponent<Renderer>().enabled = true;
	}

	public void DisablePlayer(int playerIndex) {
		players[playerIndex-1].SetActive(false);
		players[playerIndex-1].name = string.Format("Player {0} (Disabled)", playerIndex);
	}
}
