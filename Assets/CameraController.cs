using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	
	public Transform target;
	
	private Camera cam;
	
	void Start () 
	{
		cam = this.gameObject.GetComponent<Camera>();
		cam.transform.position = target.position + Vector3.back * 10.0f;
	}
	
	void Update () 
	{
		cam.transform.position = Vector3.Lerp(cam.transform.position, target.position + Vector3.back * 10.0f, Time.deltaTime);
	}
}
