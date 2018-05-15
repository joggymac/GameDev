using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatController : MonoBehaviour {

	public Transform head;
	public Transform player;
	Animator anim;

	string state = "patrol";
	public GameObject[] waypoints;
	int currentWP = 0;
	public float rotSpeed = 0.2f;
	public float speed = 1.5f;

	public float accuracyWP = 5.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("Idle", true);
		anim.SetBool ("walking", false);
		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		float angle = Vector3.Angle (direction, head.up);

		if (state == "patrol" && waypoints.Length > 0) {
			anim.SetBool ("Idle", false);
			anim.SetBool ("walking", true);
			if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP){
				currentWP++;
				if (currentWP >= waypoints.Length) {
					currentWP = 0;
				}

			}
			direction = waypoints [currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
			this.transform.Translate (0, 0, Time.deltaTime * speed);


		}

		if (Vector3.Distance (player.position, this.transform.position) < 10 && (angle < 30 || state == "pursuing")) {
			state = "pursuing";
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);

			if (direction.magnitude > 5) {
				this.transform.Translate (0, 0, Time.deltaTime * speed);
				anim.SetBool ("walking", true);
			} else {
				anim.SetBool ("walking", false);
				anim.SetBool ("Idle", true);
			}

		} else {
			anim.SetBool ("walking", true);
			state = "patrol";
		}

	}
}
