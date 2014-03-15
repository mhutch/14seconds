using UnityEngine;

public class SplashInput : MonoBehaviour
{
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Space)) {
			Application.LoadLevel ("Scene");
		}
	}
}
