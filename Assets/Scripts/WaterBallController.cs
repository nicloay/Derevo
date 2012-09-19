using UnityEngine;
using System.Collections;

public class WaterBallController : MonoBehaviour {
	public GameObject target;
	
	private Vector3 startPoint;
	private Vector3 endPoint;
	private float startTime;
	private float duration;
	private bool gogo=false;

	void InitMovementParams (GameObject _target)
	{
		if (this.target!=null){
			startPoint=target.transform.position;
			this.target=_target;			
		}else{
			startPoint = transform.position;	
			this.target= _target;	
		}
		
		
		   
		endPoint=target.transform.position;
		startTime = Time.time;
		
		duration=(startPoint-endPoint).magnitude *GameManager.instance.WaterBallSpeed;
		gogo=true;
	}
	
	public void GoToParent(GameObject _target) {
		InitMovementParams (_target);
	}
	
	
	void Update () {
		
		if (gogo){
			float time= (Time.time - startTime) / duration;
	    	transform.position = Vector3.Lerp(startPoint, endPoint, time);
			if (time>=1){
				moveBallToNextParent();
			}
		}
	}
	
	void moveBallToNextParent(){
		ResourceController rc= target.GetComponent<ResourceController>();
		if (rc==null){
			SendMesageWaterComeToTree ();	
			return;
		}
		GameObject parent=rc.parentObject;
		if (parent==null){
			SendMesageWaterComeToTree();	
			return;
		}
		InitMovementParams(parent);
		
	}
	
	
	
	void SendMesageWaterComeToTree ()
	{		
		if (GameManager.instance.onWaterComeToTree!=null)
			GameManager.instance.onWaterComeToTree();
		Destroy(gameObject);
	}
}
