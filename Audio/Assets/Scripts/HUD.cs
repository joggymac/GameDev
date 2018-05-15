using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HUD : MonoBehaviour {


	public Sprite[] DetectionSprites;

	public Sprite[] SightSprites;


	LevelManager lvlmangaer;
	public Image DetectionUI;
	public Image SightUI;
	//public Footsteps footSteps;

	public int detectionlevel;

	public int sightlevel;

	public double clock;

	public Text clockText;

	// Use this for initialization
	void Start () {


		clock = 0.00;


		//footSteps = GameObject.FindWithTag ("Enemy").GetComponent<Footsteps> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		Footsteps[] footSteps = FindObjectsOfType(typeof(Footsteps)) as Footsteps[];
		foreach (Footsteps footStep in footSteps) {
			
			if (footStep.soundhearing >= 50 && footStep.soundhearing <= 75) {
				detectionlevel = 1;

			} else if (footStep.soundhearing >= 25 && footStep.soundhearing <= 50) {
				detectionlevel = 2;

			} else if (footStep.soundhearing <= 0) {
				detectionlevel = 3;

			} 
				



		}


		DetectionUI.sprite = DetectionSprites [detectionlevel];

		clock += Time.deltaTime;
		SetClockText ();

	}

	 void SetClockText(){
		clockText.text = "Time: " + clock.ToString("#,##0.00");
	}
}
