﻿using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

// Analysis disable CheckNamespace
using System.Linq;

public class InputInterpreter : MonoBehaviour
{
	readonly List<Disconnect> disconnects = new List<Disconnect> ();
	bool displayConsole;

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
			Astronaut.Move (input);

		for (int i = 0; i < disconnects.Count; i++) {
			if (!disconnects [i].CheckAndUpdate (input)) {
				disconnects [i].Dispose ();
				disconnects.RemoveAt (i--);
			}
		}

		if (Input.GetKeyDown (KeyCode.BackQuote)) {
			displayConsole = !displayConsole;
		}

		//allow for reset
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Astronaut.ResetState ();
			Application.LoadLevel (Application.loadedLevel);
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
		case 2:
			return Delay.Random ();
		}
		throw new Exception ();
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (20, 20, 300, 50), string.Format ("Oxygen: {0:F2}", Astronaut.OxygenTime));

		if (displayConsole) {
			string blinkText = string.Join ("\n", disconnects.Select (s => s.ToString ()).ToArray ());
			if (!string.IsNullOrEmpty (blinkText))
				GUI.Label (new Rect (20, 70, 300, 500), blinkText);
		}
	}
}