using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Аудио менеджер, все звуки запускаются через него
/// </summary>	
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

	[SerializeField] private AudioListener	audioListener;
    [SerializeField] private AudioMixer		audioMixer;

    [SerializeField] private AudioSource	audioSourceMusic;
    [SerializeField] private AudioSource	audioSourceAmbient;
    [SerializeField] private AudioSource	audioSourceSound;

    private float volumeMusic		= 0;
	private float volumeAmbient		= 0;
	private float fadeSpeed			= 0.1f;

	private bool isMusicChanging	= false;
	private bool isAmbientChanging	= false;

    private void Awake()
    {       
        Instance = this;     

		ObjectExtension.DontDestroyOnLoad(this.gameObject);

		volumeMusic		= audioSourceMusic.volume;
		volumeAmbient	= audioSourceAmbient.volume;

		float masterValue = PlayerPrefs.GetFloat(Constants.GameMasterVolume, 1);

		OnMusicValueChanged(masterValue);
    }

    private void Start()
    {
        SetAudioSourceVolume(volumeMusic);
    }

	public void PlayMusic(AudioScriptableObject audio)
	{
        if (audio == null)
			return;

		if (isMusicChanging == true)
			return;
			
		isMusicChanging = true;

		StartCoroutine(FadeOut(audioSourceMusic, () =>
		{
			audioSourceMusic.clip = audio.clip;

			audioSourceMusic.Play();

			StartCoroutine(FadeIn(audioSourceMusic, volumeMusic, () => { isMusicChanging = false; }));
		}));
	}

	public void PlayAmbient(AudioScriptableObject audio)
	{
        if (audio == null)
			return;

		if (isAmbientChanging == true)
			return;
			
		isAmbientChanging = true;

		StartCoroutine(FadeOut(audioSourceAmbient, () =>
		{
			audioSourceAmbient.clip = audio.clip;

			audioSourceAmbient.Play();

			StartCoroutine(FadeIn(audioSourceAmbient, volumeAmbient, () => { isAmbientChanging = false; }));
		}));
	}

	public void PlaySound(AudioScriptableObject audio)
	{
        if (audio == null)
			return;

		audioSourceSound.PlayOneShot(audio.clip);
	}

	/// <summary>
	/// Увеличение громкости
	/// </summary>
	IEnumerator FadeIn(AudioSource source, float volumeMax, Action onComplete = null)
	{
		while (source.volume < volumeMax)
		{
			yield return new WaitForSeconds(0.1f);

			source.volume = source.volume + fadeSpeed < volumeMax ? source.volume + fadeSpeed : volumeMax;
		}

		onComplete?.Invoke();
	}

	/// <summary>
	/// Cнижение громкости
	/// </summary>
	IEnumerator FadeOut(AudioSource source, Action onComplete = null)
	{
		while (source.volume > 0)
		{
			yield return new WaitForSeconds(0.1f);

			source.volume = source.volume - fadeSpeed > 0 ? source.volume - fadeSpeed : 0f;
		}

		onComplete?.Invoke();
	}

	private void OnMusicValueChanged(float value)
	{
		PlayerPrefs.SetFloat(Constants.GameMasterVolume, value);

		SetAudioSourceVolume(value);
	}

	/// <summary>
	/// Установка громкости всех источников с конвертацией кромкости к дицибелам 0..1 к -80..20
	/// </summary>
    public void SetAudioSourceVolume(float volume)
    {
		float clampVolume = Mathf.Clamp(volume, 0.0001f, 1f);

		audioMixer.SetFloat("MusicVolume", Mathf.Log10(clampVolume) * 20);	
		audioMixer.SetFloat("AmbientVolume", Mathf.Log10(clampVolume) * 20);	
		audioMixer.SetFloat("SoundVolume", Mathf.Log10(clampVolume) * 20);	
    }
}
