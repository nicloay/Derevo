using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	void OnMouseDown() {	
		
		if (GameManager.instance.onBackgroundClick != null)
			GameManager.instance.onBackgroundClick();
    }
}
