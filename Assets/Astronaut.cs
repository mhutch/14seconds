using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;

// Analysis disable CheckNamespace

static class Astronaut
{
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
}
