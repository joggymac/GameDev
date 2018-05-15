using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController_TP : NetworkBehaviour {

	public GameObject Player;
	public GameObject Item;
	public GameObject heldItem;


    public float moveSpeed = 5f;
    public float turnSpeed = 50f;
    public float runSpeed = 8f;
    public float crawlSpeed = 3f;
    Animator anim;
	public bool moving = false;




	public double clock;

	public Camera cam;



	 void Start()
	{
		cam = Camera.main;
		Item = GameObject.FindGameObjectWithTag ("Item");
		anim = GetComponent<Animator> ();
		anim.SetBool ("Walking", false);
		heldItem.SetActive (false);
		Cursor.visible = false;

	}
		
	


	void Update()
    {

			if (!isLocalPlayer) {
			return;
			}
		FieldOfView[] fovs = FindObjectsOfType(typeof(FieldOfView)) as FieldOfView[];
		foreach (FieldOfView fov in fovs) {
			if (fov.detection_time <= 0) {
				Item.SetActive (true);
			}
		
		}

        if (Input.GetKey(KeyCode.S))
        {
			moving = true;
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
            anim.SetBool("Walking", true);
        }

		else if (Input.GetKeyUp(KeyCode.C))
		{

			moveSpeed = crawlSpeed;

		}

        else if (Input.GetKey(KeyCode.W))
        {
			moving = true;
            if (Input.GetKeyDown(KeyCode.LeftShift)){
                moveSpeed = runSpeed;
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift)){
                moveSpeed = 5f;
            }

            



            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            anim.SetBool("Walking", true);

        }
        else
        {
			moving = false;
            anim.SetBool("Walking", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            anim.SetBool("Walking", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            anim.SetBool("Walking", true);
        }

       

    }

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Item") {
			Item.SetActive (false);
			heldItem.SetActive (true);
		}
	}
}