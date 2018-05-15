using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger : MonoBehaviour {


	public AudioMixerSnapshot triggerSnapshot;
	public AudioMixerSnapshot ambientSnapshot;
	public float transitionTime = 0.2f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Player") {
			triggerSnapshot.TransitionTo (transitionTime);
		}


	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Player") {
			ambientSnapshot.TransitionTo (transitionTime);
		}

	}
}
