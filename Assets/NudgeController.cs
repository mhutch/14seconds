using UnityEngine;
using System.Collections;

public class NudgeController : MonoBehaviour 
{
	void Update () 
	{
		
		if(!GameObject.Find("LeftHand").rigidbody2D.isKinematic && 
		   !GameObject.Find("RightHand").rigidbody2D.isKinematic)
		   return;
		
		if(Input.GetKey(KeyCode.UpArrow))
			rigidbody2D.AddForce(Vector2.up * 5f);
		
		if(Input.GetKey(KeyCode.DownArrow))
			rigidbody2D.AddForce(-Vector2.up * 5f);
			
		if(Input.GetKey(KeyCode.LeftArrow))
			rigidbody2D.AddForce(-Vector2.right * 5f);
		
		if(Input.GetKey(KeyCode.RightArrow))
			rigidbody2D.AddForce(Vector2.right * 5f);
	}
}
