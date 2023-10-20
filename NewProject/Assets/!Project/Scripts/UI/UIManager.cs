using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseGameObject
{
    public static UIManager Instance { get; private set; }

	public Joystick Joystick;
	public UIWindowDebug UIWindowDebug;
	public UIWindowStart UIWindowStart;
	public UIWindowGame UIWindowGame;
	public UIWindowFailed UIWindowFailed;
	public UIWindowWin UIWindowWin;

	private List<UIWindow> _windows = new List<UIWindow>(); 
	public UIWindow currentWindow;

	private void Awake()
	{
        Instance = this;

		//Проходим по всем подобектам в поисках компонентов Panel
        foreach (Transform chield in this.transform) 
		{
            UIWindow window = chield.GetComponent<UIWindow>();

            if (window != null) 
                _windows.Add(window);
        }
	}

	public void CloseAllWindow()
	{
		foreach(UIWindow window in _windows)
			if(window != currentWindow)
				window.Close();
	}

	public void OnShowUIWindowStart(bool isOpen)
	{
		if(isOpen)
		{ 
			UIWindowStart.Open(); 
			CloseAllWindow();
		}
		else
			UIWindowStart.Close();
	}

	public void OnShowUIWindowGame(bool isOpen)
	{
		if(isOpen)
		{ 
			UIWindowGame.Open(); 
			CloseAllWindow();
		}
		else
			UIWindowGame.Close();
	}

	public void OnShowUIWindowFailed(bool isOpen)
	{
		if(isOpen)
		{ 
			UIWindowFailed.Open(); 
			CloseAllWindow();
		}
		else
			UIWindowFailed.Close();
	}

	public void OnShowUIWindowWin(bool isOpen)
	{
		if(isOpen)
		{ 
			UIWindowWin.Open(); 
			CloseAllWindow();
		}
		else
			UIWindowWin.Close();
	}
}
