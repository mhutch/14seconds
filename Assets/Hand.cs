using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour 
{
	
	public bool isTouching { get; private set; }
	
	void Start () 
	{
		isTouching = false;
	}
	
	void Update () 
	{
		bool grabbing = this.gameObject.name == "LeftHand" ? 
			InputInterpreter.ArmGripLeft : InputInterpreter.ArmGripRight;
		
		if(grabbing)
		{
			if(isTouching)
			{
				this.rigidbody2D.isKinematic = true;
			}
		}
		else
		{
			this.rigidbody2D.isKinematic = false;
		}
	}
	
	void OnCollisionEnter2D(Collision2D collisionInfo) 
	{
		if(collisionInfo.transform != this.transform.parent)
			isTouching = true;
	}
	
	void OnCollisionStay2D(Collision2D collisionInfo) 
	{
		
	}
	
	void OnCollisionExit2D(Collision2D collisionInfo) 
	{
		if(collisionInfo.transform != this.transform.parent)
			isTouching = false;
	}
}
