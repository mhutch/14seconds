using UnityEngine;

public class InputInterpreter : MonoBehaviour
{
	//controls
	const KeyCode KeyArmStretchLeft = KeyCode.Q;
	const KeyCode KeyArmStretchRight = KeyCode.W;
	const KeyCode KeyLegStretchLeft = KeyCode.A;
	const KeyCode KeyLegStretchRight = KeyCode.S;
	const KeyCode KeyArmLeftGrip = KeyCode.Z;
	const KeyCode KeyArmRightGrip = KeyCode.X;

	// constants
	const float StartOxygenTime = 14f; //seconds
	const float StretchSpeed = 1f; // units per second
	const float StretchOxygenUse = 0.5f; // oxygen seconds per stretch second

	//arm/leg state
	float armStretchLeft, armStretchRight, legStretchLeft, legStretchRight;
	bool armGripLeft, armGripRight;

	public float ArmStretchLeft { get { return armStretchLeft; } }
	public float ArmStretchRight { get { return armStretchRight; } }
	public float LegStretchLeft { get { return legStretchLeft; } }
	public float LegStretchRight { get { return legStretchRight; } }
	public bool ArmGripLeft { get { return armGripLeft; } }
	public bool ArmGripRight { get { return armGripRight; } }

	public float OxygenTime { get; private set; }

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
