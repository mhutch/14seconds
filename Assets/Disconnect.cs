using UnityEngine;
using System;

// Analysis disable CheckNamespace

abstract class Disconnect : IDisposable
{
	float length, remaining;

	protected Disconnect (float length)
	{
		this.length = this.remaining = length;
	}

	public float Length { get { return length; } }
	public float Remaining { get { return remaining; } }

	public bool CheckAndUpdate (InputSet input)
	{
		remaining -= Time.deltaTime;
		if (remaining < 0)
			return false;
		Update (input);
		return true;
	}

	protected virtual void Update (InputSet input)
	{
	}

	public virtual bool StealInput (InputSet input)
	{
		return false;
	}

	public virtual void Dispose ()
	{
	}
}
