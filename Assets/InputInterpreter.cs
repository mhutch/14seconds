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
		Player.OxygenTime = StartOxygenTime;
	}
	
	static void UpdateGrip (Limb limb)
	{
		Player.SetGrip (limb, Input.GetKey (Controls.GetGrip (limb)));
	}
	
	static void UpdateStretch (Limb limb)
	{
		var value = Player.GetStretch (limb);
		var keyDown = Input.GetKey (Controls.GetStretch (limb));

		if (keyDown) {
			if (value < 1f)
				Player.RemoveOxygen (Player.Stretch (limb, StretchSpeed * Time.deltaTime));
		} else {
			if (value > 0f)
				Player.RemoveOxygen (- Player.Stretch (limb, - StretchSpeed * Time.deltaTime));
		}
	}
	
	void Update ()
	{
		Player.RemoveOxygen (Time.deltaTime);

		if (Player.OxygenTime < 0 && Random.Range (0f, 10f) < (- Player.OxygenTime / 2f) * Time.deltaTime)
			AddDisconnect ();

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

	void AddDisconnect ()
	{
		if (disconnects.Count > 1)
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

static class Player
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

abstract class Disconnect
{
	float length, remaining;

	protected Disconnect (float length)
	{
		this.length = this.remaining = length;
	}

	public float Length { get { return length; } }
	public float Remaining { get { return remaining; } }

	public bool CheckAndUpdate ()
	{
		remaining -= Time.deltaTime;
		if (remaining < 0)
			return false;
		Update ();
		return true;
	}

	protected abstract void Update ();

	public virtual bool StealInput ()
	{
		return false;
	}
}

enum SpasmType
{
	Push,
	Pull,
	Twitch
}

class Spasm : Disconnect
{
	public Spasm (Limb limb, SpasmType type, float strength, float length) : base (length)
	{
		this.Type = type;
		this.Limb = limb;
		this.Strength = strength;
	}

	public Limb Limb { get; private set; }
	public SpasmType Type { get; set; }
	public float Strength { get; private set; }

	protected override void Update ()
	{
		switch (Type) {
		case SpasmType.Push:
			Player.Stretch (Limb, (Strength + InputInterpreter.StretchSpeed) * Time.deltaTime);
			break;
		case SpasmType.Pull:
			Player.Stretch (Limb, - Strength * Time.deltaTime);
			break;
		case SpasmType.Twitch:
			if (Remaining > (Length / 2f))
				goto case SpasmType.Push;
			goto case SpasmType.Pull;
		default:
			throw new ArgumentOutOfRangeException ();
		}
	}

	public override string ToString ()
	{
		return string.Format ("[Spasm {0} {1} s {2} t {3}]", Limb, Type, Strength, Length);
	}
}