using UnityEngine;

// Analysis disable CheckNamespace

public class LimbController : MonoBehaviour 
{
	
	public GameObject leftFoot;
	public GameObject rightFoot;
	public GameObject leftHand;
	public GameObject rightHand;
	
	private GameObject leftHandPivot;
	private GameObject rightHandPivot;
	
	private Vector2 handOffStr = new Vector2(1.0f, 1.0f);
	private float maxHandDistance = 3f;
	private Vector2 handOffEndDefault = new Vector2(1.75f, 1.75f);
	
	private Vector2 footOffStr = new Vector2(0.5f, -1.5f);
	private Vector2 footOffEnd = new Vector2(1.5f, -2.25f);
	
	private Vector2 leftHandTarget = Vector2.zero;
	private Vector2 rightHandTarget = Vector2.zero;
	
	private float prevArmStretchLeft = 0f;
	private float prevArmStretchRight = 0f;
	
	void Awake()
	{
		leftHandPivot = new GameObject("LeftHandGrabHelper");
		leftHandPivot.transform.parent = this.transform;
		leftHandPivot.transform.position = leftHand.transform.position;
		leftHandPivot.transform.rotation = this.transform.rotation;
		
		rightHandPivot = new GameObject("RightHandGrabHelper");
		rightHandPivot.transform.parent = this.transform;
		rightHandPivot.transform.position = rightHand.transform.position;
		rightHandPivot.transform.rotation = this.transform.rotation;
	}
	
	void Start () 
	{
		rigidbody2D.AddForce((Vector3.up + Vector3.right) * 5000f);
	}
	
	void Update () 
	{
		
		//only update the position when the player isn't holding!
		if(Astronaut.StretchLeftArm != prevArmStretchLeft)
		{
			if(leftHandTarget == Vector2.zero)
				leftHandTarget = ClosestGrabFromHand(leftHand);
				
			prevArmStretchLeft = Astronaut.StretchLeftArm;
		}
		else if(Astronaut.StretchLeftArm == 0f)
		{
			leftHandTarget = Vector2.zero;
		}
		
		if(Astronaut.StretchRightArm != prevArmStretchRight)
		{
			if(rightHandTarget == Vector2.zero)
				rightHandTarget = ClosestGrabFromHand(rightHand);
			
			prevArmStretchRight = Astronaut.StretchRightArm;
		}
		else if(Astronaut.StretchRightArm == 0f)
		{
			rightHandTarget = Vector2.zero;
		}
			
		//stretch the arms!
		leftHand.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(-handOffStr.x, handOffStr.y), 
				new Vector2(leftHandTarget.x, leftHandTarget.y), 
				Astronaut.StretchLeftArm);
		
		rightHand.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(handOffStr.x, handOffStr.y), 
				new Vector2(rightHandTarget.x, rightHandTarget.y), 
				Astronaut.StretchRightArm);
		
		leftFoot.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(-footOffStr.x, footOffStr.y), 
				new Vector2(-footOffEnd.x, footOffEnd.y), 
				Astronaut.StretchLeftLeg);
		
		rightFoot.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(footOffStr.x, footOffStr.y), 
				new Vector2(footOffEnd.x, footOffEnd.y), 
				Astronaut.StretchRightLeg);
	}
	
	Vector2 ClosestGrabFromHand(GameObject hand)
	{
		if(leftHandPivot == null)
			return hand.transform.position + Vector3.up * 2.0f;
			
		bool left = hand.name == "LeftHand";
		Transform piv = left ? leftHandPivot.transform : rightHandPivot.transform;
		piv.position = left ? this.transform.TransformPoint(new Vector2(-handOffStr.x, handOffStr.y)) : 
							  this.transform.TransformPoint(new Vector2(handOffStr.x, handOffStr.y));
		piv.rotation = this.transform.rotation;
		
		Vector3? closestHit = null;
		
		if(left) piv.Rotate(Vector3.forward, -20f);
		else piv.Rotate(Vector3.forward, 20f);
		
		float rotSteps = 140f / 20f;
		if(!left) rotSteps *= -1f;
		
		for(int i = 0; i < 20; i++)
		{
			Debug.DrawRay(piv.position, piv.up * maxHandDistance, Color.red);
			RaycastHit2D[] hits = Physics2D.RaycastAll(piv.position, piv.up, maxHandDistance);
			
			foreach(RaycastHit2D hit in hits)
			{
				if (hit.transform.tag != "Player" && hit.transform.tag != "PlayerPart")
				{
					
					if(closestHit == null ||
					   	Vector3.Distance(piv.position, hit.point) <
					   	Vector3.Distance(piv.position, closestHit.Value))
					{
						closestHit = hit.point;
					}
				}
			}
			
			piv.Rotate(Vector3.forward, rotSteps);
		}
		
		if(closestHit.HasValue)
		{
			Vector3 relativeHitPoint = this.transform.InverseTransformPoint(closestHit.Value);
			Ray2D ray = new Ray2D(Vector2.zero, relativeHitPoint);
			Debug.DrawRay(this.transform.position, (closestHit.Value - this.transform.position), Color.red);
			Vector2 distantPoint = ray.GetPoint(maxHandDistance);
			
			return distantPoint;
		}
		else
		{
			if(left) return new Vector2(-handOffEndDefault.x, handOffEndDefault.y);
			else return handOffEndDefault;
		}
	}
	
	void OnDrawGizmos()
	{
		/*
		Vector2 pos = this.transform.TransformPoint(ClosestGrabFromHand(leftHand));
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(pos, 0.5f);
		*/
	}
}
