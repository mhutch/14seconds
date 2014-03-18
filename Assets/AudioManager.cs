using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] AudioClips;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}

	public static AudioManager Instance { get; private set; }

	public void PlaySound (int index)
	{
		if (!audio.isPlaying)
			audio.PlayOneShot (AudioClips [index]);
	}

	public static void PlayBreathing ()
	{
		Instance.PlaySound (7);
	}

	public static void PlayScream (int i)
	{
		Instance.PlaySound (i);
	}
}
