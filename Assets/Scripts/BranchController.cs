using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BranchController : MonoBehaviour {	
	List<GameObject> children;	
	private static GameObject[] activeLayerMembers;	
	private static GameObject[] nextLayerMembers;
	private static int nextLayerMembersCounter;
	private static int levelCount;
	public int id;
	
	void Awake(){
		children=new List<GameObject>();
	}
	
	
	public Vector3 getGrowPosition(){		
		foreach (Transform t in transform){			
			if ("Cube".Equals(t.name)){
				return new Vector3(0f, (t.transform.localScale.y*transform.localScale.y),0f);			
			}
		}	
		return Vector3.zero;
	}	
	
	public int getChildCount(){
		return children.Count;
	}
	
	
	public GameObject addBranch(int id){
				
		int childCount=getChildCount();
		
		Vector3 growPoint=getGrowPosition();					
		int radPerStep=GameManager.instance.CHILDREN_MAX_ANGLE/(GameManager.instance.CHILDREN_MAX_COUNT-1);
		int radMiddlePoint=GameManager.instance.CHILDREN_MAX_ANGLE/2;
		int angle=childCount * radPerStep - radMiddlePoint;	
		Quaternion rotation=Quaternion.Euler(new Vector3(0, 0,Mathf.Rad2Deg *angle));
		GameObject newBranch=(GameObject)Instantiate(GameManager.instance.branchGO, growPoint, Quaternion.identity);		
		newBranch.transform.parent=transform;
		newBranch.transform.localPosition=growPoint;
		newBranch.transform.localRotation=rotation;
		
		newBranch.transform.GetComponent<BranchController>().id=id;
		children.Add(newBranch);
		if (activeLayerMembers==null){			
			activeLayerMembers=new GameObject[1];
			nextLayerMembers=new GameObject[2];
			activeLayerMembers[0]=this.gameObject;
			nextLayerMembersCounter=0;
			levelCount=1;
		}
		float scale=1f -(0.1f*levelCount);
		newBranch.transform.localScale=new Vector3(scale,scale,scale);
		nextLayerMembers[nextLayerMembersCounter++]=newBranch;
		return newBranch;
	}
	
	
	public bool hasFreeSlotsForChild(){
		if (children.Count < GameManager.instance.CHILDREN_MAX_COUNT){
			return true;
		} else {
			return false;	
		}		
	}

	private BranchController getParentBranchController (){
		Transform parent=transform.parent;
		BranchController parentBC=parent.GetComponent<BranchController>();
		return parentBC;
	}
	
	public GameObject getEmptyNeighbourIfExists(){
		
		for (int i=0;i < activeLayerMembers.Length ;i++){
			BranchController activeBC=activeLayerMembers[i].GetComponent<BranchController>();
			if (activeBC.children.Count < GameManager.instance.CHILDREN_MAX_COUNT){
				return activeLayerMembers[i];	
			}
		}
		
		return null;
	}
	/*
	public GameObject getParentNeighbour(){
		BranchController parentBC=getParentBranchController();
		int id=parentBC.children.IndexOf(this.gameObject);
		return parentBC.children[id+1];
	}*/
	
	public GameObject getFirstChildFromFirstNeighbour(){
		activeLayerMembers=nextLayerMembers;
		nextLayerMembers=new GameObject[ activeLayerMembers.Length*2];
		nextLayerMembersCounter=0;
		levelCount++;
		return children[0];
	}
	
}
