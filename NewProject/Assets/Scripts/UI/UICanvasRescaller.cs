using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasRescaller : MonoBehaviour
{
    private void Start()
    {
        float designAspect = 1080 / 1920f;
            float screenAspect = Screen.width / (float)Screen.height;

            var scaler = gameObject.GetComponent<CanvasScaler>();
            scaler.matchWidthOrHeight = screenAspect <= designAspect ? 0 : 1;
    }

	private void OnEnable()
	{
	    float designAspect = 1080 / 1920f;
        float screenAspect = Screen.width / (float)Screen.height;

        var scaler = gameObject.GetComponent<CanvasScaler>();
        scaler.matchWidthOrHeight = screenAspect <= designAspect ? 0 : 1;	
	}
}
