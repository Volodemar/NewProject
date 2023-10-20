using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonNextLevel : BaseGameObject
{
    public void NextLevel()
	{
		if(InitScene())
			GM.OnStartNextLevel();
	}
}
