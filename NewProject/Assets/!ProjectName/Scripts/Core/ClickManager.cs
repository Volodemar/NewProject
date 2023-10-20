using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : BaseGameObject
{
	[HideInInspector] public bool isClick;
	[HideInInspector] public bool isClickHold;

	private Vector3 mouseDownPos;	

	private bool isCheckUI		= false;
	private bool isWorldPoint	= false;
	private Vector3 offcet		= new Vector3(0,0,3f); //Расстояние от экрана или смещение

	private void Update () 
	{
		#if UNITY_ANDROID || UNITY_IOS
			Touch[] touches = Input.touches;

			for (int i = 0; i < touches.Length; i++)
			{
				Touch touch = touches[i];

				// Коснулись
				if (touch.phase == TouchPhase.Began)
				{
					if(!isCheckUI || (isCheckUI && !IsPointerOverUIObject()))
						HandleTouchDown(touch.position);
				}

				// Потянули
				if (touch.phase == TouchPhase.Moved)
				{
					if(!isCheckUI || (isCheckUI && !IsPointerOverUIObject()))
						HandleTouch(touch.position);
				}

				// Отпустили
				if (touch.phase == TouchPhase.Ended)
				{
					if(!isCheckUI || (isCheckUI && !IsPointerOverUIObject()))
						HandleTouchUp(touch.position);
				}
			}
		#endif

		#if UNITY_STANDALONE || UNITY_EDITOR
			// Нажали кнопку
			if (Input.GetMouseButtonDown(0))
			{
				if(!isCheckUI || (isCheckUI && !IsPointerOverUIObject()))
					HandleTouchDown(Input.mousePosition);
			}

			// Сдвинули позицию зажав кнопку
			if (Input.GetMouseButton(0))
			{
				if(!isCheckUI || (isCheckUI && !IsPointerOverUIObject()))
					HandleTouch(Input.mousePosition);
			}

			// Отпустили кнопку мышки в приблизительно той-же позиции
			if (Input.GetMouseButtonUp(0))
			{
				if(!isCheckUI || (isCheckUI && !IsPointerOverUIObject()))
					HandleTouchUp(Input.mousePosition);
			}
		#endif
	}

    public void HandleTouchDown(Vector3 position)
    {
		if(isWorldPoint)
		{
			Vector3 pos = position + offcet;
			mouseDownPos = Camera.main.ScreenToWorldPoint(pos);
			Player.HandleTouchDown(mouseDownPos);
		}
		else
		{
			Vector3 pos = position;
			mouseDownPos = pos;
			Player.HandleTouchDown(pos);
		}
	}

    public void HandleTouch(Vector3 position)
    {
		if(isWorldPoint)
		{
			Vector3 pos = position + offcet;
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(pos);
			Player.HandleTouch(mousePos);
		}
		else
		{
			Vector3 pos = position;
			Player.HandleTouch(pos);
			mouseDownPos = pos;
		}
	}

    public void HandleTouchUp(Vector3 position)
    {
		if(isWorldPoint)
		{
			Vector3 pos = position + offcet;
			Vector3 mouseUpPos = Camera.main.ScreenToWorldPoint(pos);
			Player.HandleTouchUp(mouseUpPos);
		}
		else
		{
			Vector3 pos = position;
			Player.HandleTouchUp(pos);
		}
	}

	/// <summary>
	/// Проверка также и для тачей есть ли на пути UI
	/// </summary>
	private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

		#if UNITY_ANDROID || UNITY_IOS
			Touch[] touches = Input.touches;
			if(touches.Length > 0)
			{ 
			eventDataCurrentPosition.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		#endif

		#if UNITY_STANDALONE || UNITY_EDITOR
			eventDataCurrentPosition.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		#endif

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
