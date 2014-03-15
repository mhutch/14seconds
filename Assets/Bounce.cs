using UnityEngine;

public class Bounce : MonoBehaviour
{
	void Start () 
	{

	}

	float time;

	void Update () 
	{
		time += Time.deltaTime;
		transform.Translate (0.02f * Mathf.Sin (time * 2f * Mathf.PI), 0f, 0f);
	}
}
