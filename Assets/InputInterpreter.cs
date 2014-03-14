using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;

// Analysis disable CheckNamespace

public class InputInterpreter : MonoBehaviour
{
	// constants
	public const float StartOxygenTime = 14f; //seconds
	public const float StretchSpeed = 2f; // units per second
	public const float StretchOxygenUse = 0.5f; // oxygen seconds per stretch second

	readonly List<Disconnect> disconnects = new List<Disconnect> ();
	
	void Start ()
	{
		Astronaut.OxygenTime = StartOxygenTime;
	}
	
	static void UpdateGrip (Limb limb)
	{
		Astronaut.SetGrip (limb, Input.GetKey (Controls.GetGrip (limb)));
	}
	
	static void UpdateStretch (Limb limb)
	{
		var value = Astronaut.GetStretch (limb);
		var keyDown = Input.GetKey (Controls.GetStretch (limb));

		if (keyDown) {
			if (value < 1f)
				Astronaut.RemoveOxygen (Astronaut.Stretch (limb, StretchSpeed * Time.deltaTime));
		} else {
			if (value > 0f)
				Astronaut.RemoveOxygen (- Astronaut.Stretch (limb, - StretchSpeed * Time.deltaTime));
		}
	}
	
	void Update ()
	{
		Astronaut.RemoveOxygen (Time.deltaTime);

		AddDisconnects ();

		if (!disconnects.Any (d => d.StealInput ()))
			HandleInput ();

		for (int i = 0; i < disconnects.Count; i++)
			if (!disconnects [i].CheckAndUpdate ())
				disconnects.RemoveAt (i--);
	}

	static void HandleInput ()
	{
		UpdateGrip (Limb.LeftArm);
		UpdateGrip (Limb.RightArm);
		UpdateStretch (Limb.LeftArm);
		UpdateStretch (Limb.LeftLeg);
		UpdateStretch (Limb.RightArm);
		UpdateStretch (Limb.RightLeg);
	}

	void AddDisconnects ()
	{
		//glitches only start when time runs out
		if (Astronaut.OxygenTime > 0)
			return;

		//only allow 1 at once
		if (disconnects.Count > 1)
			return;

		//glitch probability linearly scaled to 100% at 240 seconds
		var glitchChance = (-Astronaut.OxygenTime / 240f) * Time.deltaTime;
		if (Random.Range (0f, 1f) > glitchChance)
			return;

		var d = RandomSpasm ();
		Debug.Log (d);
		disconnects.Add (d);
	}

	static Spasm RandomSpasm ()
	{
		return new Spasm (
			RandomLimb (),
			(SpasmType)Random.Range (0, 3),
			Random.Range (4f, 10f),
			0.2f
		);
	}

	static Limb RandomLimb ()
	{
		switch (Random.Range (0, 4)) {
		case 0:
			return Limb.LeftArm;
		case 1:
			return Limb.LeftLeg;
		case 2:
			return Limb.RightArm;
		default:
			return Limb.RightLeg;
		}
	}
}

[Flags]
enum Limb
{
	Leg = 1,
	Arm = 1 << 1,
	Left = 1 << 2,
	Right = 1 << 3,
	LeftArm = Left | Arm,
	RightArm = Right | Arm,
	LeftLeg = Left | Leg,
	RightLeg = Right | Leg,
}
