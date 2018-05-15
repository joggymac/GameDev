using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MP_Cam : NetworkBehaviour {
	public Camera cam;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			cam.enabled = false;
			return;
		}
			
	}
}
