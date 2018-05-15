using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootstepsManager : MonoBehaviour {

	private PlayerController_FP player;
	public LayerMask groundLayer;
	public float transitionTime = 0.2f;

	public AudioSource grassAudio;
	public AudioClip[] grassSteps;
	public AudioMixerSnapshot grassSnapshot;

	public AudioSource concreteAudio;
	public AudioClip[] concreteSteps;
	public AudioMixerSnapshot concreteSnapshot;

	public AudioSource dirtAudio;
	public AudioClip[] dirtSteps;
	public AudioMixerSnapshot dirtSnapshot;

	public AudioSource gravelAudio;
	public AudioClip[] gravelSteps;
	public AudioMixerSnapshot gravelSnapshot;

	public AudioSource sandAudio;
	public AudioClip[] sandSteps;
	public AudioMixerSnapshot sandSnapshot;


	public AudioSource woodAudio;
	public AudioClip[] woodSteps;
	public AudioMixerSnapshot woodSnapshot;

	public AudioSource triggerAudio;
	public AudioClip[] triggerColide;
	public AudioMixerSnapshot triggerSnapshot;


	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerController_FP> ();
	}
	
	// Update is called once per frame
	void Update () {
		PlayFootstepSFX (grassAudio, grassSteps);
		PlayFootstepSFX (concreteAudio, concreteSteps);
		PlayFootstepSFX (dirtAudio, dirtSteps);
		PlayFootstepSFX (gravelAudio, gravelSteps);
		PlayFootstepSFX (sandAudio, sandSteps);
		PlayFootstepSFX (woodAudio, woodSteps);
		PlayFootstepSFX (triggerAudio, triggerColide);


		RaycastHit hit;

		if (Physics.Raycast (transform.position, -Vector3.up, out hit, 5f, groundLayer)) {
			if (hit.collider.tag == "Grass") {
				grassSnapshot.TransitionTo (transitionTime);
			} else if (hit.collider.tag == "Concrete") {
				concreteSnapshot.TransitionTo (transitionTime);
			} else if (hit.collider.tag == "Dirt") {
				dirtSnapshot.TransitionTo (transitionTime);
			} else if (hit.collider.tag == "Gravel") {
				gravelSnapshot.TransitionTo (transitionTime);
			} else if (hit.collider.tag == "Sand") {
				sandSnapshot.TransitionTo (transitionTime);
			} else if (hit.collider.tag == "Wood") {
				woodSnapshot.TransitionTo (transitionTime);
			} 
		
				
				


		}
	}
		

	void PlayFootstepSFX (AudioSource aSource, AudioClip[] aClips)
	{
		if (player.isWalking && !aSource.isPlaying) {
			int stepCount = Random.Range (0, aClips.Length);

			aSource.clip = aClips [stepCount];
			aSource.Play ();


		} 
	}
}