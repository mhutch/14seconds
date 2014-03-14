using UnityEngine;

// Analysis disable CheckNamespace

public class Hand : MonoBehaviour 
{
	
	public bool isTouching { get; private set; }
	
	void Start () 
	{
		isTouching = false;
	}
	
	void Update () 
	{
		bool grabbing = gameObject.name == "LeftHand" ? Astronaut.GripLeftArm : Astronaut.GripRightArm;
		
		if(grabbing)
		{
			if(isTouching)
			{
				rigidbody2D.isKinematic = true;
			}
		}
		else
		{
			rigidbody2D.isKinematic = false;
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
		if(collisionInfo.transform != transform.parent)
			isTouching = false;
	}
}
