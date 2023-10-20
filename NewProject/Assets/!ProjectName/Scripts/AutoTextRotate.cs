using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTextRotate : BaseGameObject
{
    private void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
