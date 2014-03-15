using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour 
{
	
	public Transform targetObject;
	
	void Start () 
	{
		
	}
	
	void Update () 
	{
		this.transform.LookAt(targetObject, Vector3.forward);
	}
}