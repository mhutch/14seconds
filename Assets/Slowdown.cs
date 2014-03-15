using R = UnityEngine.Random;
using UnityEngine;

// Analysis disable CheckNamespace

class Slowdown : Disconnect
{
	public Slowdown (float factor, float length) : base (length)
	{
		Factor = factor;
		Astronaut.StretchSpeed /= factor;
	}

	public float Factor { get; private set; }

	public override void Dispose ()
	{
		Astronaut.StretchSpeed *= Factor;
	}

	public static Slowdown Random ()
	{
		return new Slowdown (
			R.Range (1.2f, 4f),
			R.Range (6f, 12f)
		);
	}

	public override string ToString ()
	{
		return string.Format ("[Slowdown x {0} t {1}]", Factor, Length);
	}
}