using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelLoader : NetworkBehaviour {

    private bool inExitZone;
    public string levelToLoad;


	PlayerController_TP playerController2;

	public List<PlayerController_TP> playerControllers;

	private static bool created = false;

	// Use this for initialization
	void Start () {


        inExitZone = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void DontDestroyPlz(){
		
			if (!created) {
				
			foreach (PlayerController_TP playerController in playerControllers) {
				DontDestroyOnLoad (playerController);


			}

			created = true;
					

		}
	}

    private void OnTriggerEnter(Collider other)
	{playerController2 = FindObjectOfType<PlayerController_TP> ();
		if (playerController2.heldItem.activeSelf)
        {
			Application.LoadLevel (levelToLoad);


        }

    
    }

//	    private void OnTriggerExit(Collider other)
//	    {
//	        if (other.name == "Player")
//	        {
//	            inExitZone = false;
//	        }
//	    } 
}
