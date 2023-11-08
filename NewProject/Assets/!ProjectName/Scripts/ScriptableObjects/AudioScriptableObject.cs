using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Аудио
/// </summary>
[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData")]
public class AudioScriptableObject : ScriptableObject
{
	public AudioClip clip;
}
