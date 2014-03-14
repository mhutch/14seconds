using UnityEngine;
using System;

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

	protected override void Update ()
	{
		switch (Type) {
		case SpasmType.Push:
			Astronaut.Stretch (Limb, (Strength + InputInterpreter.StretchSpeed) * Time.deltaTime);
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

	public override string ToString ()
	{
		return string.Format ("[Spasm {0} {1} s {2} t {3}]", Limb, Type, Strength, Length);
	}
}