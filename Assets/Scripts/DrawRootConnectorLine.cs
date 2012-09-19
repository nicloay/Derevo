using UnityEngine;
using System.Collections;

public class DrawRootConnectorLine : MonoBehaviour {
	private bool drawLine=false;
	private GameObject originalGameObject;
	private Vector3 startPos;
	private Vector3 mousePos;
	public Material mat;
	
	// Use this for initialization
	void Start () {
		GameManager.instance.onResourceMouseDown+=onResourceMouseDown;
		//GameManager.instance.onRresourceMouseRelease+=onResourceMouseRelease;
		GameManager.instance.onBackgroundClick=onBackgroundClick;
	}
	
	void onBackgroundClick(){
		drawLine=false;	
	}
	
	void onResourceMouseDown(Vector3 v3,GameObject go){
		ResourceController rc=go.GetComponent<ResourceController>();
		if (rc!=null){
			
			//click on 1st object
			if(drawLine==false && rc.isActive==true){
				originalGameObject=go;
				drawLine=true;
				startPos=new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
				return;
			}
			
			
			//click on2d object
			if (drawLine==true && rc.isActive==false && rc!=originalGameObject){
				Debug.Log("finishing draw line");
				if (GameManager.instance.onGrowNewRoot!=null)
						GameManager.instance.onGrowNewRoot(originalGameObject,go);
				drawLine=false;
				originalGameObject=null;	
				return;				
			}
			
			//
			Debug.Log ("wtf????");
		
		}
	}
	
	void onResourceMouseRelease(GameObject go){
		drawLine=false;	
	}
	
	
	
	// Update is called once per frame
	void Update () {
		mousePos = Input.mousePosition;	
	}
	
	
	void OnPostRender() {
		if (!drawLine)
			return;		
        
		if (!mat) {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
		Vector3 mouseCurrenttPosition = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        GL.Color(Color.yellow);
        GL.Vertex(startPos);
        GL.Vertex(mouseCurrenttPosition);
        GL.End();
        GL.PopMatrix();
    }
}
