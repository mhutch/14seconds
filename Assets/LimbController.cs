using UnityEngine;

// Analysis disable CheckNamespace

public class LimbController : MonoBehaviour 
{
	
	public GameObject leftFoot;
	public GameObject rightFoot;
	public GameObject leftHand;
	public GameObject rightHand;
	
	Vector2 handOffStr = new Vector2(1.0f, 1.0f);
	Vector2 handOffEnd = new Vector2(1.5f, 2.25f);
	
	Vector2 footOffStr = new Vector2(0.5f, -1.5f);
	Vector2 footOffEnd = new Vector2(1.5f, -2.25f);
	
	void Start () 
	{
		
	}
	
	void Update () 
	{
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
				
		leftHand.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(-handOffStr.x, handOffStr.y), 
				new Vector2(-handOffEnd.x, handOffEnd.y), 
				Astronaut.StretchLeftArm);
		
		rightHand.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(handOffStr.x, handOffStr.y), 
				new Vector2(handOffEnd.x, handOffEnd.y), 
				Astronaut.StretchRightArm);
				
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
