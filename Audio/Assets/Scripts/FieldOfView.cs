using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;


public class FieldOfView : NetworkBehaviour  {



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

	//Other Scripts
	public LevelManager levelManager;
	SwatController swatController;
	PlayerController_TP playerController;
	Footsteps footSteps;

	//Detection 
	NavMeshAgent agent;
	public Transform target;
	[SyncVar]
	public float detection_time = 30;

	public GameObject Spawn;

	//Audio
	AudioSource audioSource;
	public AudioClip detected;




	void Start(){


		Spawn = GameObject.FindWithTag("Spawn");

		playerController = FindObjectOfType<PlayerController_TP> ();
		levelManager = FindObjectOfType<LevelManager>();
		audioSource = GetComponent<AudioSource>();
		agent = GetComponent<NavMeshAgent>();
		footSteps = FindObjectOfType<Footsteps> ();

			
	}

	void Awake()
	{
		
	}

	void Update(){
		FindVisibleTargets ();
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
			debugColour = targetVisible;
			detection_time--;

			if (detection_time == 20) {
				audioSource.PlayOneShot (detected, 0.8f);	
			}
				
			if (detection_time <= 0)
		{
				//target = GameObject.Find ("Player(Clone)").transform;
				footSteps.soundhearing = 100;
				RpcRespawn ();
			}



		} else {
			debugColour = targetNotVisible;
		}
	}



	void RpcRespawn(){
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject player in players) 
			{

				//Vector3 spawnPoint = Vector3.zero;
				//Transform target = (player.transform);
				player.transform.position = Spawn.transform.position;
			}






	}

	//Debug stuff to make it easier to see what is going on
	/* void OnDrawGizmos(){
		Quaternion leftRayRotation = Quaternion.AngleAxis( -viewAngle/2, Vector3.up );
		Quaternion rightRayRotation = Quaternion.AngleAxis( viewAngle/2, Vector3.up );
		Vector3 leftRay = leftRayRotation * transform.forward * viewRadius;
		Vector3 rightRay = rightRayRotation * transform.forward * viewRadius;

		UnityEditor.Handles.color = debugColour;
		UnityEditor.Handles.DrawSolidArc (transform.position, Vector3.up, leftRay, viewAngle, viewRadius);
		Color secondaryColour = new Color (debugColour.r, debugColour.g, debugColour.b, 1);
		UnityEditor.Handles.color = secondaryColour;
		UnityEditor.Handles.DrawWireArc (transform.position, Vector3.up, leftRay, viewAngle, viewRadius);
		Debug.DrawRay (transform.position, leftRay, secondaryColour);
		Debug.DrawRay (transform.position, rightRay, secondaryColour); 
	} */



}