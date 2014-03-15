using UnityEngine;
using System;
using Random = UnityEngine.Random;

// Analysis disable CheckNamespace

static class Astronaut
{
	// constants
	public const float StartOxygenTime = 14f; //seconds
	public const float StartStretchSpeed = 2f; // units per second
	public const float StretchOxygenUse = 0.5f; // oxygen seconds per stretch second

	static Astronaut ()
	{
		OxygenTime = StartOxygenTime;
		StretchSpeed = StartStretchSpeed;
	}

	public static float StretchSpeed { get; set; }

	public static float OxygenTime { get; set; }

	static float stretchLeftArm, stretchLeftLeg, stretchRightArm, stretchRightLeg;

	public static float StretchLeftArm {
		get { return stretchLeftArm; }
		set { stretchLeftArm = Mathf.Clamp (value, 0f, 1f); }
	}

	public static float StretchLeftLeg {
		get { return stretchLeftLeg; }
		set { stretchLeftLeg = Mathf.Clamp (value, 0f, 1f); }
	}
	public static float StretchRightArm {
		get { return stretchRightArm; }
		set { stretchRightArm = Mathf.Clamp (value, 0f, 1f); }
	}
	public static float StretchRightLeg {
		get { return stretchRightLeg; }
		set { stretchRightLeg = Mathf.Clamp (value, 0f, 1f); }
	}

	public static bool GripLeftArm { get; set; }
	public static bool GripRightArm { get; set; }

	public static float GetStretch (Limb limb)
	{
		switch (limb) {
		case Limb.LeftArm:
			return StretchLeftArm;
		case Limb.LeftLeg:
			return StretchLeftLeg;
		case Limb.RightArm:
			return StretchRightArm;
		case Limb.RightLeg:
			return StretchRightLeg;
		}
		throw new ArgumentOutOfRangeException ();
	}

	public static void SetStretch (Limb limb, float value)
	{
		switch (limb) {
		case Limb.LeftArm:
			StretchLeftArm = value;
			return;
		case Limb.LeftLeg:
			StretchLeftLeg = value;
			return;
		case Limb.RightArm:
			StretchRightArm = value;
			return;
		case Limb.RightLeg:
			StretchRightLeg = value;
			return;
		}
		throw new ArgumentOutOfRangeException ();
	}

	public static float Stretch (Limb limb, float amount)
	{
		float oldVal;
		switch (limb) {
		case Limb.LeftArm:
			oldVal = StretchLeftArm;
			StretchLeftArm += amount;
			return StretchLeftArm - oldVal;
		case Limb.LeftLeg:
			oldVal = StretchLeftLeg;
			StretchLeftLeg += amount;
			return StretchLeftLeg - oldVal;
		case Limb.RightArm:
			oldVal = StretchRightArm;
			StretchRightArm += amount;
			return StretchRightArm - oldVal;
		case Limb.RightLeg:
			oldVal = StretchRightLeg;
			StretchRightLeg += amount;
			return StretchRightLeg - oldVal;
		default:
			throw new ArgumentOutOfRangeException ();
		}
	}

	public static bool GetGrip (Limb limb)
	{
		return (limb & Limb.Left) != 0 ? GripLeftArm : GripRightArm;
	}

	public static void SetGrip (Limb limb, bool value)
	{
		if ((limb & Limb.Left) != 0) {
			GripLeftArm = value;
		} else {
			GripRightArm = value;
		}
	}

	public static void RemoveOxygen (float value)
	{
		OxygenTime -= value;
	}

	public static void Move (InputSet input)
	{
		UpdateGrip (input, Limb.LeftArm);
		UpdateGrip (input, Limb.RightArm);
		UpdateStretch (input, Limb.LeftArm);
		UpdateStretch (input, Limb.LeftLeg);
		UpdateStretch (input, Limb.RightArm);
		UpdateStretch (input, Limb.RightLeg);
	}

	static void UpdateGrip (InputSet input, Limb limb)
	{
		SetGrip (limb, input.GetGrip (limb));
	}

	static void UpdateStretch (InputSet input, Limb limb)
	{
		var keyDown = input.GetStretch (limb);
		var stretch = StretchSpeed * Time.deltaTime;
		RemoveOxygen (Astronaut.Stretch (limb, keyDown? stretch : -stretch));
	}
}
