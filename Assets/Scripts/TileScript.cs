using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Material[] materials;
	public int tileVal;

    SkinnedMeshRenderer smrComponent;
	bool interpolating;
	float blendVal; // 0-1
	int currentTileVal; 		// this is the val (material + blendshape) we're interpolating towards
	int oldTileVal; 			// this is the val (material + blendshape) we're interpolating from
	int intermediateBestVal; 	// if we're mid-blend, this is the value we're closest to 

	// Use this for initialization
	void Start () {
        smrComponent = GetComponent<SkinnedMeshRenderer>();

		interpolating = false;
		blendVal = 1f;
		currentTileVal = 0;
		oldTileVal = 0;
		intermediateBestVal = 0;

		SetTileTo(0);		
	}
	
	// Update is called once per frame
	void Update () {
		if (interpolating) {
			blendVal += Time.deltaTime;
			intermediateBestVal = blendVal < 0.5f ? oldTileVal : currentTileVal;
			
			if (blendVal >= 1) {
				interpolating = false;
				intermediateBestVal = oldTileVal = currentTileVal;
				blendVal = 1;
			} 
		}
	}

	public void SetTileTo(int val) {
        smrComponent = GetComponent<SkinnedMeshRenderer>();

        switch (val) {
			case 0:
                smrComponent.SetBlendShapeWeight(0, 0);
                smrComponent.SetBlendShapeWeight(1, 0);
                smrComponent.material = materials[0];
			break;  						
			case 1:
                smrComponent.SetBlendShapeWeight(0, 100);
                smrComponent.SetBlendShapeWeight(1, 0);
                smrComponent.material = materials[1];
                break;
            case 2:
                smrComponent.SetBlendShapeWeight(0, 0);
                smrComponent.SetBlendShapeWeight(1, 100);
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
			blendVal = 1 - blendVal;
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
