using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindow : BaseGameObject
{
    [SerializeField] public string WindowName = "noname";
    public bool IsOnPausedGame  = true;
    public bool IsOnPlayGame    = false;
    public bool IsNotClosed     = false;

    public virtual void Open() 
    {
        if(!InitScene())
            return;

        if(IsOnPausedGame)
            StatusManager.SetGameStatus(GameStatus.Paused);
        if(IsOnPlayGame)
            StatusManager.SetGameStatus(GameStatus.Play);

        UI.currentWindow = this;
        this.gameObject.SetActive(true);
    }

    public virtual void Close() 
    {
        if(!InitScene())
            return;

        if(IsNotClosed)
            return;

		if (UI.currentWindow != null && UI.currentWindow == this)
			UI.currentWindow = null;

		this.gameObject.SetActive(false);
    }
}
