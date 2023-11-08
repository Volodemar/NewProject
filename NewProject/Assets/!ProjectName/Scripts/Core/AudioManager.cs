using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляет всеми звуками игры
/// </summary>
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	public AudioSource audioSourceMusic;
	public AudioSource audioSourceAmbient;
	public AudioSource audioSourceSound;
	public Transform   soundsParent;

	private float volumeMusic	= 0;
	private float volumeAmbient = 0;
	private float volumeSound	= 0;

	private void Awake()
	{
		volumeMusic		= audioSourceMusic.volume;
		volumeAmbient	= audioSourceAmbient.volume;
		volumeSound		= audioSourceSound.volume;

		Instance = this;
	}

	public void PlayMusic(AudioScriptableObject audio)
	{
		StartCoroutine(FadeOut(() => 
		{ 
			audioSourceMusic.Play();
			StartCoroutine(FadeIn(() => 
			{ 
				audioSourceMusic.clip = audio.clip;
				audioSourceMusic.Play();
			}));
		}));

		//Увеличение громкости
		IEnumerator FadeIn(Action onComplete = null)
		{
			while(audioSourceMusic.volume < volumeMusic)
			{
				yield return null;
				audioSourceMusic.volume = audioSourceMusic.volume + 0.1f < volumeMusic ? audioSourceMusic.volume + 0.1f : volumeMusic;
			}

			onComplete?.Invoke();
		}

		//Cнижение громкости
		IEnumerator FadeOut(Action onComplete = null)
		{
			while(audioSourceMusic.volume > 0)
			{
				yield return null;
				audioSourceMusic.volume = audioSourceMusic.volume - 0.1f > 0 ? audioSourceMusic.volume - 0.1f : 0f;
			}

			onComplete?.Invoke();
		}
	}

	public void PlayAmbient(AudioScriptableObject audio)
	{
		StartCoroutine(FadeOut(() => 
		{ 
			audioSourceAmbient.clip = audio.clip;
			audioSourceAmbient.Play();
			StartCoroutine(FadeIn(() => 
			{ 
				audioSourceAmbient.Play();
			}));
		}));

		//Увеличение громкости
		IEnumerator FadeIn(Action onComplete = null)
		{
			while(audioSourceAmbient.volume < volumeAmbient)
			{
				yield return null;
				audioSourceAmbient.volume = audioSourceAmbient.volume + 0.1f < volumeMusic ? audioSourceAmbient.volume + 0.1f : volumeAmbient;
			}

			onComplete?.Invoke();
		}

		//Cнижение громкости
		IEnumerator FadeOut(Action onComplete = null)
		{
			while(audioSourceAmbient.volume > 0)
			{
				yield return null;
				audioSourceAmbient.volume = audioSourceAmbient.volume - 0.1f > 0 ? audioSourceAmbient.volume - 0.1f : 0f;
			}

			onComplete?.Invoke();
		}
	}

	public void PlaySound(AudioScriptableObject audio, Transform parentObject, Vector3 offcet)
	{
		//Создание звука
		AudioSource sound = Instantiate(audioSourceSound.gameObject, parentObject.position, Quaternion.identity, soundsParent).GetComponent<AudioSource>();
		sound.clip = audio.clip;	
		sound.Play();

		//Прикрепление звука
		AutoMoveParent autoMove = sound.GetComponent<AutoMoveParent>();
		autoMove.parent = parentObject;
		autoMove.offcet = offcet;

		//Удаление звука
		AutoDisable autoDisable = sound.gameObject.AddComponent<AutoDisable>();
		autoDisable.ShowSeconds = audio.clip.length;	
		autoDisable.isDestroy = true;		
	}
}
