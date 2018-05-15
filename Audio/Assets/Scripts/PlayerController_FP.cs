using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController_FP : NetworkBehaviour {

	public float walkSpeed = 3f;
	public bool isWalking;
	public Camera cam;

	// Use this for initialization
	void Start () {
		Camera cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		if (isLocalPlayer)
			return;
		cam.enabled = false;
	}

	// Update is called once per frame
	void Update () {

		Vector3 input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 inputDirection = input.normalized;

        transform.Rotate(0, Input.GetAxis("Rotate") * 60 * Time.deltaTime, 0);
		transform.Translate (inputDirection * walkSpeed * Time.deltaTime);

		if (input.magnitude > 0) {
			isWalking = true;
		} else {
			isWalking = false;
		}



	}
}