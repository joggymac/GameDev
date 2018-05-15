using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelLoader : NetworkBehaviour {

    private bool inExitZone;
    public string levelToLoad;
	PlayerController_TP playerController;

	// Use this for initialization
	void Start () {
		
        inExitZone = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
		



    private void OnTriggerEnter(Collider other)
	{playerController = FindObjectOfType<PlayerController_TP> ();
		if (playerController.heldItem.activeSelf)
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
