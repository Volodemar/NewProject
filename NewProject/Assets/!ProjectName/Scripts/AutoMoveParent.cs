using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Скрипт автоматического следования за целью
/// </summary>
public class AutoMoveParent : MonoBehaviour
{
	public Transform parent;
	public Vector3 offcet = Vector3.zero;

	private void Update()
	{
		if(parent != null)
			this.transform.position = parent.position + offcet;
	}
}
