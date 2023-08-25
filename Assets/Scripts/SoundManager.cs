using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioClip buttonAudioClip;
	public AudioClip dismissalAudioClip;
	public AudioClip diceAudioClip;
	public AudioClip winAudioClip;
	public AudioClip safeHouseAudioClip;
	public AudioClip playerAudioClip;

	public static AudioSource buttonAudioSource;
	public static AudioSource dismissalAudioSource;
	public static AudioSource diceAudioSource;
	public static AudioSource winAudioSource;
	public static AudioSource safeHouseAudioSource;
	public static AudioSource playerAudioSource;

	AudioSource AddAudio(AudioClip clip, bool playOnAwake, bool loop, float volume)
	{
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.playOnAwake = playOnAwake;
		audioSource.loop = loop;
		audioSource.volume = volume;
		return audioSource;
	}

	void Start()
	{
		buttonAudioSource = AddAudio(buttonAudioClip, false, false, 1.0f);
		dismissalAudioSource = AddAudio(dismissalAudioClip, false, false, 1.0f);
		diceAudioSource = AddAudio(diceAudioClip, false, false, 1.0f);
		winAudioSource = AddAudio(winAudioClip, false, false, 1.0f);
		safeHouseAudioSource = AddAudio(safeHouseAudioClip, false, false, 1.0f);
		playerAudioSource = AddAudio(playerAudioClip, false, false, 1.0f);

		if (PlayerPrefs.GetInt("soundState", 0) == 0)
		{
			soundOn();
		}
		else
		{
			soundOff();
		}
	}

	public static void soundOn()
    {
		buttonAudioSource.mute = false;
		dismissalAudioSource.mute = false;
		diceAudioSource.mute = false;
		winAudioSource.mute = false;
		safeHouseAudioSource.mute = false;
		playerAudioSource.mute = false;
	}

	public static void soundOff()
    {
		buttonAudioSource.mute = true;
		dismissalAudioSource.mute = true;
		diceAudioSource.mute = true;
		winAudioSource.mute = true;
		safeHouseAudioSource.mute = true;
		playerAudioSource.mute = true;
	}
}
