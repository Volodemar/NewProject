using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTextRotate : BaseGameObject
{
    private void Update()
    {
        if(InitScene())
		{
            this.transform.rotation = Camera.main.transform.rotation;
		}
    }
}
