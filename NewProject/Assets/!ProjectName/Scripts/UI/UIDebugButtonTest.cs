using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDebugButtonTest : BaseGameObject
{
    public void OnWin()
	{
		Level.OnLevelWin();
	}

    public void OnFailed()
	{
		Level.OnLevelFailed();
	}

    public void OnPlaySound(AudioScriptableObject audio)
	{
		//Audio.PlaySound(audio, Camera.transform, Vector3.zero);
		Audio.PlayOneSound(audio);
	}
}
