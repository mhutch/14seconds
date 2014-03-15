using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

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
