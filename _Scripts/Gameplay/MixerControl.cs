using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerControl : MonoBehaviour
{
	public AudioMixer musicVolume;

	public void SetMusicLevel(float sliderValue)
	{
		musicVolume.SetFloat("Sound", Mathf.Log10(sliderValue) * 20);
	}
}
