using UnityEngine;
using System.Collections;

public class GameOverHUD : MonoBehaviour {

	void OnGUI () {
		
		GUI.TextArea(new Rect(10, 10, 150, 100),"game over");
		GUI.TextArea(new Rect(10, 190, 150	, 150),"your score "+GameManager.instance.gameTime);
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(200,40,280,180), "Try Again [DOESNT WORK =) ]")) {
			Application.LoadLevel("game");
		}
	}
}
