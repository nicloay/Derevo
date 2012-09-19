using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {		
	private GameObject LastParent;
	private int branchSize=1;
	
	private GameObject trunkGO;

	void identifyTrunkGO ()
	{
		foreach (Transform t in transform){			
			if ("Branch".Equals(t.name)){				
				trunkGO=t.gameObject;
				return;
			}
		}
	}
	
	void initParams(){		
		identifyTrunkGO ();	
		LastParent=trunkGO;
	}
	
	void Start(){
		initParams();
		GameManager.instance.onGrowNewBranch=onGrowNewBranch;
	}
 
	
	void onGrowNewBranch(){
		addBranch();	
	}
	
		
		
		
	void addBranch(){
		int i=GameManager.instance.INFINITY_CYCLE_COUNT;
		while (i-- >0){
			BranchController lastBC= LastParent.GetComponent<BranchController>();
			
			if(lastBC.hasFreeSlotsForChild()){
				branchSize++;
				lastBC.addBranch(branchSize);					
				return;
			} 
			
		 	GameObject emptyNeigbour=lastBC.getEmptyNeighbourIfExists();
			
			if (emptyNeigbour!=null){
				LastParent=emptyNeigbour;
				continue;
			} 
			LastParent=lastBC.getFirstChildFromFirstNeighbour();	
			
			
			
		}
		throw new System.Exception("infinity cycle exception");
	}
	
	
	void test ()
	{		
		for (int i=0;i<150;i++){		
			addBranch();
		}
	}
}
