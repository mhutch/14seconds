using UnityEngine;

// Analysis disable CheckNamespace

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
