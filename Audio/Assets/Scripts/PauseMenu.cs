using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public GameObject PauseUI;
	private bool paused = false;
	private PlayerController_TP playerController;

	// Use this for initialization
	void Start () {
		PauseUI = GameObject.FindWithTag ("PauseUI");
		PauseUI.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		
			if (Input.GetButtonDown ("Pause")){
				paused = !paused;
			}
			if (paused) {
				PauseUI.SetActive (true);
			Cursor.visible = true;
			}
			if (!paused) {
				PauseUI.SetActive (false);
			Cursor.visible = false;
			}
		}





	public void Resume(){
		paused = false;
	}
	public void Disconnect(){
		MasterServer.UnregisterHost ();
		Network.Disconnect();

	}

	public void Quit(){
		Application.Quit ();
	}
}
