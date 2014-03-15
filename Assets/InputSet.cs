using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;

// Analysis disable CheckNamespace

class InputSet
{
	public bool ArmStretchLeft { get; set; }
	public bool ArmLeftGrip { get; set; }
	public bool LegStretchLeft { get; set; }
	public bool ArmStretchRight { get; set; }
	public bool ArmRightGrip { get; set; }
	public bool LegStretchRight { get; set; }

	public void Read ()
	{
		ArmStretchLeft = Input.GetKey (KeyCode.F);
		ArmLeftGrip = Input.GetKey (KeyCode.D);
		LegStretchLeft = Input.GetKey (KeyCode.V);
		ArmStretchRight = Input.GetKey (KeyCode.J);
		ArmRightGrip = Input.GetKey (KeyCode.K);
		LegStretchRight = Input.GetKey (KeyCode.N);
	}

	public bool GetStretch (Limb limb)
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

	public bool GetGrip (Limb limb)
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
