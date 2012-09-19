using UnityEngine;
using System.Collections;

public class RootGrowController : MonoBehaviour {
	private float magnitude;
	//private bool needToGrow=false;
	
	
	// Use this for initialization
	void Start () {
		GameManager.instance.onGrowNewRoot+=onGrowNewRoot;
	}
	

	
	
	void onGrowNewRoot(GameObject parent, GameObject destination){
		Vector3 growPoint=parent.transform.position;
		float magnitude=(parent.transform.position-destination.transform.position).magnitude;
		Debug.Log(parent.name);
		GameObject root=(GameObject)Instantiate(GameManager.instance.rootGO, growPoint, Quaternion.identity);				
		if(parent.name.Equals("MainRoot")){		
			root.transform.parent=parent.transform.parent.transform;
			root.transform.position=parent.transform.position;
		}else {
			root.transform.parent=parent.transform;
			root.transform.localPosition=new Vector3(0,0,0);	
		}
			
		
		//needToGrow=true;
		root.transform.localScale=new Vector3(1,1,magnitude);
		
		root.transform.LookAt(destination.transform.position);
		destination.GetComponent<ResourceController>().SetActive();
		destination.GetComponent<ResourceController>().parentObject=parent;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
