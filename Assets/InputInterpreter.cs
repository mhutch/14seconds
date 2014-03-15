using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

// Analysis disable CheckNamespace

public class InputInterpreter : MonoBehaviour
{
	readonly List<Disconnect> disconnects = new List<Disconnect> ();
	
	void Start ()
	{
	}
	
	void Update ()
	{
		Astronaut.RemoveOxygen (Time.deltaTime);

		AddDisconnects ();

		var input = InputSet.Read ();

		bool handleInput = true;
		foreach (var d in disconnects)
			handleInput &= !d.StealInput (input);

		if (handleInput)
			Astronaut.Move (input, 1f);

		for (int i = 0; i < disconnects.Count; i++) {
			if (!disconnects [i].CheckAndUpdate (input)) {
				disconnects [i].Dispose ();
				disconnects.RemoveAt (i--);
			}
		}
	}

	void AddDisconnects ()
	{
		//glitches only start when time runs out
		if (Astronaut.OxygenTime > 0)
			return;

		//only allow 2 at once
		if (disconnects.Count > 2)
			return;

		//glitch probability linearly scaled to 100% at 240 seconds
		var glitchChance = (-Astronaut.OxygenTime / 240f) * Time.deltaTime;
		if (Random.Range (0f, 1f) > glitchChance)
			return;

		Disconnect d = RandomDisconnect ();
		Debug.Log (d);
		disconnects.Add (d);
	}

	static Disconnect RandomDisconnect ()
	{
		switch (Random.Range (0, 2)) {
		case 0:
			return Spasm.Random ();
		case 1:
			return Slowdown.Random ();
		}
		throw new Exception ();
	}
}