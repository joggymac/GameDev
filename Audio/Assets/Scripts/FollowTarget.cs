using UnityEngine;
using UnityEngine.Networking;

public abstract class FollowTarget : NetworkBehaviour {
	
	[SerializeField] public Transform target;
	[SerializeField] private bool autoTargetPlayer = true;

	virtual protected void Start()
	{
		if (autoTargetPlayer)
		{
			FindTargetPlayer();
			}
		}

	void FixedUpdate () 
	{
	  if (autoTargetPlayer && (target == null || !target.gameObject.activeSelf))
		{
			FindTargetPlayer();
				}
		if (target != null && (target.GetComponent<Rigidbody>() != null && !target.GetComponent<Rigidbody>().isKinematic)) 
		{
			Follow(Time.deltaTime);
				}
	}

	protected abstract void Follow(float deltaTime);
	

	public void FindTargetPlayer()
	{
		
			if (target == null)
			{
				GameObject[] targetObjs = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject targetObj in targetObjs) {
				SetTarget(targetObj.transform);
			}

		}

		}
	public virtual void SetTarget(Transform newTransform)
	{
		target = newTransform;
	}
	public Transform Target{get {return this.target;}}
}
