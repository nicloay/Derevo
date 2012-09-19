using UnityEngine;
using System.Collections;

public class StartGameHUD : MonoBehaviour {
	
	void OnGUI () {
		
		GUI.TextArea(new Rect(10, 10, 150, 100),"WSAD - control camera");
		GUI.TextArea(new Rect(10, 190, 150	, 150),"?????????\n Connect root with blue water boxes, and grow a tree\n" +
			"start game by clicking on root and then on any blue box");
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(200,40,280,180), "Start Game")) {
			Application.LoadLevel("game");
		}
	}
}
