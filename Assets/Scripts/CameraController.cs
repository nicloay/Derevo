using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		  var movement = Vector3.zero;
	
	    if (Input.GetKey("w"))
	        movement.y++;
	    if (Input.GetKey("s"))
	        movement.y--;
	    if (Input.GetKey("a"))
	        movement.x--;
	    if (Input.GetKey("d"))
	        movement.x++;
	 
		
	    transform.Translate(movement * GameManager.instance.cameraSpeed * Time.deltaTime, Space.Self);
	}
}
