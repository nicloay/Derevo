using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour { 	
	public bool isActive=false;
	public string onHoverText="";	
	public GameObject parentObject;
	
	
	private bool mouseOnObject=false;	
	Vector3 mousePos;
	Vector3 mouseDownStartPosition;
	
	public void SetActive(){
		this.isActive=true;
		StartCoroutine(startProduceWater(GameManager.instance.NewWaterBallTimeout));
	}
	
	void OnMouseEnter() {
		mouseOnObject=true;		
	}
	
	void OnMouseExit() {
		mouseOnObject=false;
	}
	
	void Update(){
		mousePos = Input.mousePosition;			
	}
	
	void OnMouseDown() {			
		mouseDownStartPosition = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
		
		if (GameManager.instance.onResourceMouseDown != null)
			GameManager.instance.onResourceMouseDown(mouseDownStartPosition,gameObject);			
    }
	
	/*
	void OnMouseUp() {		
		if (GameManager.instance.onRresourceMouseRelease != null)
			GameManager.instance.onRresourceMouseRelease(gameObject);			
    }
	*/
	void OnGUI(){
		if (mouseOnObject){
			GUI.Box (new Rect (mousePos.x,Screen.height- mousePos.y,120,20), onHoverText);	
		}		
	}	
	
	public void receiveWaterBall(GameObject waterBall){
		if(parentObject==null){
			Debug.Log("tree received a ball");	
			Destroy(waterBall);
		}else{		
			waterBall.GetComponent<WaterBallController>().GoToParent(parentObject);
		}
		
	}
	
	
	IEnumerator startProduceWater(float waitTime){
		yield return new WaitForSeconds(waitTime);	
		
		
		GameObject waterBall=(GameObject)Instantiate(GameManager.instance.waterBallGO, transform.parent.transform.position, Quaternion.identity);
		//waterBall.transform.position=transform.position;
		WaterBallController wbc= waterBall.GetComponent<WaterBallController>();		
		//main root doesn't has a parent
		if(parentObject!=null){
			wbc.GoToParent(parentObject);
		}
		yield return StartCoroutine(startProduceWater(GameManager.instance.NewWaterBallTimeout));		
	}
	
	
}
