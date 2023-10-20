using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Выключаем эффект если он был включен.
/// </summary>
public class AutoDisableVFX : MonoBehaviour
{
	public float ShowSeconds = 1f;
	public bool isDestroy = false;

	private void Start()
	{
		StartCoroutine(WaitForVisibleOff());
	}

	private void OnEnable()
	{
		StartCoroutine(WaitForVisibleOff());
	}

	private IEnumerator WaitForVisibleOff()
	{
		yield return new WaitForSeconds(ShowSeconds);
		this.gameObject.SetActive(false);
		if(isDestroy)
			Destroy(this.gameObject);
	}
}
