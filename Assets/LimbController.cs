using UnityEngine;
using System.Collections;

public class LimbController : MonoBehaviour 
{
	
	public GameObject leftFoot;
	public GameObject rightFoot;
	public GameObject leftHand;
	public GameObject rightHand;
	
	private Vector2 handOffStr = new Vector2(1.0f, 1.0f);
	private Vector2 handOffEnd = new Vector2(1.5f, 2.25f);
	
	private Vector2 footOffStr = new Vector2(0.5f, -1.5f);
	private Vector2 footOffEnd = new Vector2(1.5f, -2.25f);
	
	void Start () 
	{
		
	}
	
	void Update () 
	{
		leftFoot.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(-footOffStr.x, footOffStr.y), 
				new Vector2(-footOffEnd.x, footOffEnd.y), 
				InputInterpreter.LegStretchLeft);
		
		rightFoot.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(footOffStr.x, footOffStr.y), 
				new Vector2(footOffEnd.x, footOffEnd.y), 
				InputInterpreter.LegStretchLeft);
				
		leftHand.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(-handOffStr.x, handOffStr.y), 
				new Vector2(-handOffEnd.x, handOffEnd.y), 
				InputInterpreter.ArmStretchLeft);
		
		rightHand.GetComponent<SpringJoint2D>().connectedAnchor = 
			Vector2.Lerp(
				new Vector2(handOffStr.x, handOffStr.y), 
				new Vector2(handOffEnd.x, handOffEnd.y), 
				InputInterpreter.ArmStretchRight);
				
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
