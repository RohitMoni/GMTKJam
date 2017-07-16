using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Material[] materials;
	public int tileVal;

    SkinnedMeshRenderer smrComponent;
	bool interpolating;
	float timeToBlend;
	float blendVal; // 0-timeToBlend
	int currentTileVal; 		// this is the val (material + blendshape) we're interpolating towards
	int oldTileVal; 			// this is the val (material + blendshape) we're interpolating from
	int intermediateBestVal; 	// if we're mid-blend, this is the value we're closest to 

	// Use this for initialization
	void Start () {
        smrComponent = GetComponent<SkinnedMeshRenderer>();

		timeToBlend = 0.5f;
		interpolating = false;
		blendVal = timeToBlend;
		currentTileVal = 0;
		oldTileVal = 0;
		intermediateBestVal = 0;

		SetTileTo(0);		
	}
	
	// Update is called once per frame
	void Update () {
		if (interpolating) {
			if (currentTileVal == oldTileVal) {
				interpolating = false;
				blendVal = timeToBlend;
				return;
			}

			blendVal = Mathf.Clamp(blendVal + Time.deltaTime, 0f, timeToBlend);
			intermediateBestVal = blendVal < (0.5f*timeToBlend) ? oldTileVal : currentTileVal;
			
			// Test interpolating the blend shape values
			switch (currentTileVal) {
				case 0:
				break;
				case 1:
                	smrComponent.SetBlendShapeWeight(0, (blendVal/timeToBlend)*100);
				break;
				case 2:
                	smrComponent.SetBlendShapeWeight(1, (blendVal/timeToBlend)*100);
				break;
			}

			switch (oldTileVal) {
				case 0:
				break;
				case 1:
                	smrComponent.SetBlendShapeWeight(0, (1-(blendVal/timeToBlend))*100);
				break;
				case 2:
                	smrComponent.SetBlendShapeWeight(1, (1-(blendVal/timeToBlend))*100);
				break;
			}

			if (blendVal >= timeToBlend) {
				interpolating = false;
				intermediateBestVal = oldTileVal = currentTileVal;
				blendVal = timeToBlend;
			} 
		}
	}

	public void SetTileTo(int val) {
        smrComponent = GetComponent<SkinnedMeshRenderer>();

        switch (val) {
			case 0:
                smrComponent.material = materials[0];
			break;  						
			case 1:
                smrComponent.material = materials[1];
                break;
            case 2:
                smrComponent.material = materials[2];
                break;
            case 3:
			break;
			case 4:
			break;
			default:
			break;
		}

		tileVal = val;

		if (val == oldTileVal) { // We're going back to our old tile value, just reverse blend back
			blendVal = timeToBlend - blendVal;
			oldTileVal = currentTileVal;
		}
		else {
			blendVal = 0;
			oldTileVal = intermediateBestVal; // If we're in the middle of a blend when we start a new blend, we want to pick the best val for our start
		}

		currentTileVal = val;
		interpolating = true;
	}
}
