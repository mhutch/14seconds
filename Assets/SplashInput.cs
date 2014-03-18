using UnityEngine;

// Analysis disable CheckNamespace

public class SplashInput : MonoBehaviour
{
	void Update ()
	{
		if (!Application.isWebPlayer && Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		} else if (Input.GetKeyDown (KeyCode.C)) {
			Application.LoadLevel ("Credits");
		} else if (Input.anyKeyDown) {
			Application.LoadLevel ("Scene");
		}
	}
}
