using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;

// Analysis disable CheckNamespace

static class Controls
{
	public const KeyCode ArmStretchLeft = KeyCode.F;
	public const KeyCode ArmLeftGrip = KeyCode.D;
	public const KeyCode LegStretchLeft = KeyCode.V;
	public const KeyCode ArmStretchRight = KeyCode.J;
	public const KeyCode ArmRightGrip = KeyCode.K;
	public const KeyCode LegStretchRight = KeyCode.N;

	public static KeyCode GetStretch (Limb limb)
	{
		switch (limb) {
		case Limb.LeftArm:
			return ArmStretchLeft;
		case Limb.LeftLeg:
			return LegStretchLeft;
		case Limb.RightArm:
			return ArmStretchRight;
		case Limb.RightLeg:
			return LegStretchRight;
		}
		throw new ArgumentOutOfRangeException ();
	}

	public static KeyCode GetGrip (Limb limb)
	{
		switch (limb) {
		case Limb.LeftArm:
			return ArmLeftGrip;
		case Limb.RightArm:
			return ArmRightGrip;
		}
		throw new ArgumentOutOfRangeException ();
	}
}
