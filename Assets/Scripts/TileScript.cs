using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

    public Material[] materials;
    SkinnedMeshRenderer smrComponent;

	public int tileVal;

	// Use this for initialization
	void Start () {
        smrComponent = GetComponent<SkinnedMeshRenderer>();
		SetTileTo(0);
	}
	
	// Update is called once per frame
	void Update () {
		
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
	}
}
