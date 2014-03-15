using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioClip[] AudioClips;

	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public static AudioManager Instance { get; private set; }

	public void PlaySound (int index)
	{
		if (!audio.isPlaying)
			audio.PlayOneShot (AudioClips [index]);
	}
}
