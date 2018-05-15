using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAnimator : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetBool ("HeadShake", true);
		anim.SetBool ("Angry", true);

	}
}
