using UnityEngine;

/// <summary>
/// Скрипт автоматического следования за целью
/// </summary>
public class AutoMoveParent : MonoBehaviour
{
	public Transform parent;
	public Vector3 offcet = Vector3.zero;
	public bool isParentCamera = false;

	private void Update()
	{
		if(isParentCamera)
			this.transform.position = Camera.main.transform.position;

		else if(parent != null)
			this.transform.position = parent.position + offcet;
	}
}
