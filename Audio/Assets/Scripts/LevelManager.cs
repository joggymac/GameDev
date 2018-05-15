using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class LevelManager : NetworkBehaviour {

	public bool destroyOnDeath;
	Footsteps footSteps;
	public GameObject Spawn;
	HUD hud;
	PlayerController_TP playerController;



	private NetworkStartPosition[] spawnPoints;




	void Start(){
		hud = FindObjectOfType<HUD> ();
		footSteps = FindObjectOfType<Footsteps> ();
		Spawn = GameObject.FindWithTag("Spawn");
		playerController = FindObjectOfType<PlayerController_TP> ();

	}





	void FixedUpdate(){
			TakeDamage ();
			
		}
			





	public void TakeDamage()
	{
		if (!isServer) {
			return;
		}
		FieldOfView[] fovs = FindObjectsOfType(typeof(FieldOfView)) as FieldOfView[];
		Footsteps[] footSteps = FindObjectsOfType(typeof(Footsteps)) as Footsteps[];
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");


			foreach (FieldOfView fov in fovs) {
				if (fov.detection_time <= 0) 
				{
					Debug.Log ("Dead");
					if (destroyOnDeath) {
						Destroy (gameObject);
					} else {
						
					}
					fov.detection_time = 30;

					playerController.heldItem.SetActive (false);

					hud.detectionlevel = 0;
					hud.sightlevel = 0;
				}
			}
			foreach (Footsteps footStep in footSteps) {

				footStep.soundhearing = 100;

			}

			
		}
}



			



		
