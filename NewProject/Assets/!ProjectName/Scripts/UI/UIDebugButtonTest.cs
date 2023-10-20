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
}
