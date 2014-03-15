using R = UnityEngine.Random;
using UnityEngine;
using System.Collections.Generic;

// Analysis disable CheckNamespace

class Delay : Disconnect
{
	readonly Queue<InputSet> delayedInput = new Queue<InputSet> ();

	public Delay (float delay, float length) : base (length + delay)
	{
	}

	public float DelayTime { get; private set; }

	public override bool StealInput (InputSet input)
	{
		delayedInput.Enqueue (input);
		return true;
	}

	protected override void Update (InputSet input)
	{
		if ((Length - Remaining) > DelayTime)
			Astronaut.Move (delayedInput.Dequeue ());
	}

	public static Disconnect Random ()
	{
		return new Slowdown (
			R.Range (0.5f, 2f),
			R.Range (6f, 12f)
		);
	}

	public override string ToString ()
	{
		return string.Format ("[Delay x {0} t {1}]", DelayTime, Length - DelayTime);
	}
}