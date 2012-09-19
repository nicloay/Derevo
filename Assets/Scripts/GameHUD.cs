using UnityEngine;
using System.Collections;

public class GameHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		//treesize
		GUI.TextArea(new Rect(10, 10, 150, 100), "Branch Goal [" + GameManager.instance.BranchGoal+"]\n"+
			"WaterCollected ["+GameManager.instance.treeWaterCount+"]\n"+
							"Branch count ["+GameManager.instance.branchCount+"]\n"+
							"Water per branch["+GameManager.instance.waterPerBranch+"]");		
	}
}
