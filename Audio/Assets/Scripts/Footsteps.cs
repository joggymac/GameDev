using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;



public class Footsteps : NetworkBehaviour {

	[Range(0,20)]
	public float viewRadius = 60;
	[Range(0,360)]
	public float viewAngle;

	public Color targetVisible, targetNotVisible;
	Color debugColour = new Color(255,255,255,0.2f);

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	//[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	//Scripts
	PlayerController_TP playerController;

	SwatController swatController;
	HUD hud;

	//Detection
	NavMeshAgent agent;
	private int heardnoise = 0;

	private int i = 0;
	public Transform[] waypoints;
	public Transform currentWaypoint;

	public float soundhearing = 100;
	public Transform target;
	//Audio
	AudioSource audioSource;
	public AudioClip heard;

	//Animation
	Animator anim;

	//HUD
	private Image detectionmeter;

	void Start(){


		agent = GetComponent<NavMeshAgent>();
		audioSource = GetComponent<AudioSource>();
		swatController = GetComponent<SwatController>();
		anim = GetComponent<Animator> ();
		hud = FindObjectOfType<HUD> ();



	}

	void Update(){
		FindVisibleTargets ();
		var dist = Vector3.Distance(waypoints[i].position,transform.position);
		currentWaypoint = waypoints [i];

		if (dist < 5) {
			if (i < waypoints.Length - 1){ //negate targets[0], since it's already set in destination.
				i++; //change next target
				agent.destination = waypoints[i].position; //go to next target by setting it as the new destination

			}


			else if (i >= waypoints.Length - 1){
				i = -1;
				i++;
				agent.destination = waypoints[i].position;
			}
		}




		soundhearing += Time.deltaTime;
		if (soundhearing >= 100) {
			soundhearing = 100;
			hud.detectionlevel = 0;
			//Debug.Log ("HUD working");
		}







		if (soundhearing <= 0) {
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) {
				target = (player.transform);
			}
			//target = GameObject.Find ("Player(Clone)").transform;
			agent.destination = target.position;


		} else if (soundhearing > 0) {
			agent.destination = waypoints[i].position;

		}

	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);
				RaycastHit hit;
				if (!Physics.Raycast (transform.position, dirToTarget, out hit, dstToTarget, obstacleMask)) {
					if (target.name != gameObject.name) {
						visibleTargets.Add (target);
					}
					Debug.DrawLine (transform.position, target.position, Color.green);
				} else {
					Debug.DrawLine (transform.position, hit.point, Color.red);
				}
			}
		}

		if (visibleTargets.Count > 0) {
			playerController = FindObjectOfType<PlayerController_TP> ();
			debugColour = targetVisible;
			if (playerController.moving == true) {
				  if (playerController.moveSpeed == 5f) {
					soundhearing--;


				} else if (playerController.moveSpeed == 8f) {
					soundhearing--;
					soundhearing--;


				}
			} else if (playerController.moving == false) {
				soundhearing++;
				if (soundhearing >= 100) {
					soundhearing = 100;
				}
			}




		} else {
			debugColour = targetNotVisible;
		}


	}

	//Debug stuff to make it easier to see what is going on
//	void OnDrawGizmos(){
//		Quaternion leftRayRotation = Quaternion.AngleAxis( -viewAngle/2, Vector3.up );
//		Quaternion rightRayRotation = Quaternion.AngleAxis( viewAngle/2, Vector3.up );
//		Vector3 leftRay = leftRayRotation * transform.forward * viewRadius;
//		Vector3 rightRay = rightRayRotation * transform.forward * viewRadius;
//
//		UnityEditor.Handles.color = debugColour;
//		UnityEditor.Handles.DrawSolidArc (transform.position, Vector3.up, leftRay, viewAngle, viewRadius);
//		Color secondaryColour = new Color (debugColour.r, debugColour.g, debugColour.b, 1);
//		UnityEditor.Handles.color = secondaryColour;
//		UnityEditor.Handles.DrawWireArc (transform.position, Vector3.up, leftRay, viewAngle, viewRadius);
//		Debug.DrawRay (transform.position, leftRay, secondaryColour);
//		Debug.DrawRay (transform.position, rightRay, secondaryColour);
//	}



}