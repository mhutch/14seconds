using UnityEngine;
using System;
using R = UnityEngine.Random;

// Analysis disable CheckNamespace

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

	protected override void Update (InputSet input)
	{
		switch (Type) {
		case SpasmType.Push:
			Astronaut.Stretch (Limb, (Strength + Astronaut.StretchSpeed) * Time.deltaTime);
			break;
		case SpasmType.Pull:
			Astronaut.Stretch (Limb, - Strength * Time.deltaTime);
			break;
		case SpasmType.Twitch:
			if (Remaining > (Length / 2f))
				goto case SpasmType.Push;
			goto case SpasmType.Pull;
		default:
			throw new ArgumentOutOfRangeException ();
		}
	}

	public static Spasm Random ()
	{
		return new Spasm (
			LimbHelper.Random (),
			(SpasmType)R.Range (0, 3),
			R.Range (4f, 10f),
			0.2f
		);
	}

	public override string ToString ()
	{
		return string.Format ("[Spasm {0} {1} s {2} t {3}]", Limb, Type, Strength, Length);
	}
}