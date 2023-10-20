using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FPS : MonoBehaviour
{
	private int fps = 0;
	private int fpsAverage = 0;
	private int frameRange = 60;
	private int[] fpsBuffer;
	private int fpsIndex;

	private Text textFPS;

	private void Awake()
	{
		textFPS = this.GetComponent<Text>();
	}

	private void Update()
	{
		if(fpsBuffer == null || frameRange != fpsBuffer.Length)
			InitBuffer();


		fps = (int)(1f / Time.deltaTime);
		fpsAverage = AverageFPS();
		textFPS.text = fpsAverage.ToString();
	}

	private void InitBuffer()
	{
		if(frameRange <= 0)
			frameRange = 1;

		fpsBuffer = new int[frameRange];
		fpsIndex = 0;
	}

	private int AverageFPS()
	{
		fpsBuffer[fpsIndex++] = (int)(1f / Time.deltaTime);
		if(fpsIndex >= frameRange)
			fpsIndex = 0;

		int sum = 0;
		for(int i=0; i<frameRange; i++)
			sum += fpsBuffer[i];

		return sum / frameRange;
	}
}
