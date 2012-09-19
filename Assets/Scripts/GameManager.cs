
using UnityEngine;
 using System.Collections; 



 public class GameManager : MonoBehaviour {
	public int treeWaterCount=0;
	public int branchCount=1;
	public int MaxWaterCount=1280;
	public int waterPerBranch=5;
	public int BranchGoal=128;
	public float startTime;
	public float gameTime;
	
	public float RootGrowSpeed=10;
	
	public int CHILDREN_MAX_COUNT=2;
	public int CHILDREN_MAX_ANGLE=90;
	public float cameraSpeed=30f;
	public Rect gamesize=new Rect(-50,-20,100,50);
	
	public float NewWaterBallTimeout=0.5f;
	public float WaterBallSpeed=1f;
	
	
	
	
	
	public delegate void ResourceMouseDown(Vector3 startPosition,GameObject gameObject);
	public delegate void ResourceMouseRelease(GameObject gameObject);
	public delegate void BackgroundClick();
	public delegate void GrowNewRoot(GameObject parent, GameObject child);
	public delegate void WaterComeToTree();
	public delegate void GrowNewBranch();
	
	public delegate void GameOver();
	
	
	
	
	
	public ResourceMouseDown onResourceMouseDown;
	public ResourceMouseRelease onRresourceMouseRelease;
	public BackgroundClick onBackgroundClick;
	public GrowNewRoot onGrowNewRoot;
	public WaterComeToTree onWaterComeToTree;
	public GrowNewBranch onGrowNewBranch;
	public GameOver onGameOver;
	
	
	public GameObject branchGO;
	public GameObject rootGO;
	public GameObject waterBallGO;
	
	
	public int INFINITY_CYCLE_COUNT=10000;
	
	
    private static GameManager s_Instance = null;
 
    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static GameManager instance {
        get {
            if (s_Instance == null) {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance =  FindObjectOfType(typeof (GameManager)) as GameManager;
				
            }
 
            // If it is still null, create a new instance
            if (s_Instance == null) {
                GameObject obj = new GameObject("GameManager");
                s_Instance = obj.AddComponent(typeof (GameManager)) as GameManager;
                Debug.Log ("Could not locate an AManager object. \n AManager was Generated Automaticly.");
            }
 
            return s_Instance;
        }
    }
 
    // Ensure that the instance is destroyed when the game is stopped in the editor.
    void OnApplicationQuit() {
        s_Instance = null;
    }
	
	void Start() {
		onWaterComeToTree += onWaterComeToTreeListener;
		onGameOver+=onGameOverListener;
	}
	
	public void onWaterComeToTreeListener(){
		++treeWaterCount;
		
		
			
		if (treeWaterCount%waterPerBranch==0){
			if(onGrowNewBranch!=null)
				onGrowNewBranch();
			branchCount++;
		}
		if (branchCount > BranchGoal){
			if (onGameOver!=null)
				onGameOver();
		}
	}
	
	void onGameOverListener(){
		gameTime=Time.time-startTime;
		Application.LoadLevel("gameOver");		
	}
	
	
	void OnLevelWasLoaded(int level) {
        if (level == 1){
            print("Woohoo");
			startTime=Time.time;
			init ();		
		}
    }
	
	
	void init(){
		treeWaterCount=0;
		branchCount=1;
	}
	
 
}