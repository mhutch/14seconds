using UnityEngine;
using System;

public class InputInterpreter : MonoBehaviour
{
	//controls
	const KeyCode KeyArmStretchLeft = KeyCode.F;
	const KeyCode KeyArmLeftGrip = KeyCode.D;
	const KeyCode KeyLegStretchLeft = KeyCode.V;
	const KeyCode KeyArmStretchRight = KeyCode.J;
	const KeyCode KeyArmRightGrip = KeyCode.K;
	const KeyCode KeyLegStretchRight = KeyCode.N;
	
	// constants
	const float StartOxygenTime = 14f; //seconds
	const float StretchSpeed = 2f; // units per second
	const float StretchOxygenUse = 0.5f; // oxygen seconds per stretch second
	
	//arm/leg state
	static float armStretchLeft, armStretchRight, legStretchLeft, legStretchRight;
	static bool armGripLeft, armGripRight;
	
	public static float ArmStretchLeft { get { return armStretchLeft; } }
	public static float ArmStretchRight { get { return armStretchRight; } }
	public static float LegStretchLeft { get { return legStretchLeft; } }
	public static float LegStretchRight { get { return legStretchRight; } }
	public static bool ArmGripLeft { get { return armGripLeft; } }
	public static bool ArmGripRight { get { return armGripRight; } }
	
	static public float OxygenTime { get; private set; }
	
	void Start ()
	{
		OxygenTime = StartOxygenTime;
	}
	
	void UpdateGrip (KeyCode key, ref bool value)
	{
		value = Input.GetKey (key);
	}
	
	void UpdateStretch (KeyCode key, ref float value)
	{
		if (Input.GetKey (key)) {
			if (value < 1f) {
				var newValue = Mathf.Min (1f, value + StretchSpeed * Time.deltaTime);
				var delta = newValue - value;
				value = newValue;
				
				OxygenTime -= delta * StretchOxygenUse;
			}
		} else {
			if (value > 0f) {
				var newValue = Mathf.Max (0f, value - StretchSpeed * Time.deltaTime);
				var delta = newValue - value;
				value = newValue;
				
				OxygenTime -= delta * StretchOxygenUse;
			}
		}
	}
	
	void Update ()
	{
		UpdateGrip (KeyArmLeftGrip, ref armGripLeft);
		UpdateGrip (KeyArmRightGrip, ref armGripRight);
		
		UpdateStretch (KeyArmStretchLeft, ref armStretchLeft);
		UpdateStretch (KeyArmStretchRight, ref armStretchRight);
		UpdateStretch (KeyLegStretchLeft, ref legStretchLeft);
		UpdateStretch (KeyLegStretchRight, ref legStretchRight);
	}
}