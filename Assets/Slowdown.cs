using UnityEngine;
using System;

// Analysis disable CheckNamespace

class Slowdown : Disconnect
{
	public Slowdown (float length) : base (length)
	{
	}

	protected override void Update ()
	{
	}

	public override string ToString ()
	{
		return string.Format ("[Slowdown]");
	}
}