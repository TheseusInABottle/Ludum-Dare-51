using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClassData", menuName = "ScriptableObjects/SoundData")]
public class SFXNMusicSO : ScriptableObject
{
	public AudioClip[] sfx;
	public AudioClip music;
}
