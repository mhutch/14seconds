using UnityEngine;

// Analysis disable CheckNamespace

public class CreditsInput : MonoBehaviour
{
	void Update ()
	{
		if (Input.anyKeyDown) {
			Application.LoadLevel ("Splash");
		}
	}
}
