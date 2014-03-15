using UnityEngine;

public class Bounce : MonoBehaviour
{
	void Update () 
	{
		var s = 2f * Mathf.PI;
		if (Astronaut.OxygenTime < 0)
			s *= 2;

		transform.Translate (0.03f * Mathf.Sin (Time.time * s), 0f, 0f);
	}
}
