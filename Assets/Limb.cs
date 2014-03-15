using System;
using R = UnityEngine.Random;

// Analysis disable CheckNamespace

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

static class LimbHelper
{
	public static Limb Random ()
	{
		switch (R.Range (0, 4)) {
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