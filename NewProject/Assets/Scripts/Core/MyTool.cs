using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ���������
/// ����������� �������� ������ � ������������ �� ���
/// ����������� �������� ������� �� ���� ��� ���
/// ����������� �������� ����� �� ���� ����� ����������� ��������
/// ����������� �������� ��������� ���� �� ����������
/// </summary>
public static class MyTool
{
    public static Vector3 Vector3AddZ(Vector3 current, float x)
    {
        return new Vector3(current.x, current.y, current.z + x);
    }

    public static Vector3 Vector3LockX(Vector3 current, Vector3 target)
    {
        return new Vector3(current.x, target.y, target.z);
    }

    public static Vector3 Vector3LockXZ(Vector3 current, Vector3 target)
    {
        return new Vector3(current.x, target.y, current.z);
    }

    public static Vector3 Vector3LockY(Vector3 current, Vector3 target)
    {
        return new Vector3(target.x, current.y, target.z);
    }

    public static Vector3 Vector3LockZ(Vector3 current, Vector3 target)
    {
        return new Vector3(target.x, target.y, current.z);
    }

	/// <summary>
	/// �������� ������� �� �� �� ���� � ������ ������
	/// </summary>
	public static bool CheckLook(Transform current, Transform look, float minAngle = 0)
	{
		Vector3 checkDirection		= (look.position - current.position).normalized;
		Vector3 currentDirection	= current.forward;
		Quaternion currentQ			= Quaternion.LookRotation(currentDirection);
		Quaternion checkQ			= Quaternion.LookRotation(checkDirection);
		float angle	= Quaternion.Angle(currentQ, checkQ);

		if(angle <= minAngle)
			return true;
		else
			return false;
	}

	/// <summary>
	/// �������� ���� �� ���� � ������������ ������ �� ���� ������� LayerMask.GetMask("Default")
	/// </summary>
	public static bool CheckLookBlocked(Transform current, Transform look, LayerMask blockLayerMask)
	{
		Vector3 dirRay = look.position - current.position;
		if(!Physics.Raycast(current.position, dirRay, out RaycastHit hit, dirRay.magnitude, blockLayerMask))
			return true;
		else
			return false;
	}

	/// <summary>
	/// �������� ����� �� �� ���������� �� ���� �� �������� ��������� � ������ ����������� �����
	/// </summary>
	public static bool CheckLookLimit(Transform current, Transform look, float limitH, float limitV)
	{
		Vector3 direction = (look.position - current.position).normalized;

		//��������� ����������� � ���������� �������� ����������
		Quaternion newRotate = Quaternion.LookRotation(direction);
		float gEulerX = Mathf.Repeat(newRotate.eulerAngles.x + 180, 360) - 180;
		float gEulerY = Mathf.Repeat(newRotate.eulerAngles.y + 180, 360) - 180;
		float gEulerZ = Mathf.Repeat(newRotate.eulerAngles.z + 180, 360) - 180;

		if(current.parent != null)
		{ 
			Quaternion parent = current.parent.rotation;
			float p_gEulerX = Mathf.Repeat(parent.eulerAngles.x + 180, 360) - 180;
			float p_gEulerY = Mathf.Repeat(parent.eulerAngles.y + 180, 360) - 180;
			float p_gEulerZ = Mathf.Repeat(parent.eulerAngles.z + 180, 360) - 180;

			float lEulerX = gEulerX - p_gEulerX;
			float lEulerY = gEulerY - p_gEulerY;
			float lEulerZ = gEulerZ - p_gEulerZ;

			if(lEulerX == Mathf.Clamp(lEulerX, -limitV, limitV) && lEulerY == Mathf.Clamp(lEulerY, -limitH, limitH))
				return true;
			else
				return false;
		}
		else
		{
			gEulerX = Mathf.Clamp(gEulerX, -limitV, limitV);
			gEulerY = Mathf.Clamp(gEulerY, -limitH, limitH);

			if(gEulerX == Mathf.Clamp(gEulerX, -limitV, limitV) && gEulerY == Mathf.Clamp(gEulerY, -limitH, limitH))
				return true;
			else
				return false;
		}
	}

	public static bool CheckLookForvard(Transform current, Transform look, float minAngle)
	{
		Vector3 direction = (look.position - current.position).normalized;
		//Debug.Log(Vector3.Dot(current.forward, direction));
		if(Vector3.Dot(current.forward, direction) > minAngle)
			return true;
		else
			return false;
	}

	public static Vector3 GetInspectorAngle(Transform current, Transform look)
	{
		Vector3 direction = (look.position - current.position).normalized;

		//��������� ����������� � ���������� �������� ����������
		Quaternion newRotate = Quaternion.LookRotation(direction);
		float gEulerX = Mathf.Repeat(newRotate.eulerAngles.x + 180, 360) - 180;
		float gEulerY = Mathf.Repeat(newRotate.eulerAngles.y + 180, 360) - 180;
		float gEulerZ = Mathf.Repeat(newRotate.eulerAngles.z + 180, 360) - 180;

		if(current.parent != null)
		{ 
			Quaternion parent = current.parent.rotation;
			float p_gEulerX = Mathf.Repeat(parent.eulerAngles.x + 180, 360) - 180;
			float p_gEulerY = Mathf.Repeat(parent.eulerAngles.y + 180, 360) - 180;
			float p_gEulerZ = Mathf.Repeat(parent.eulerAngles.z + 180, 360) - 180;

			float lEulerX = gEulerX - p_gEulerX;
			float lEulerY = gEulerY - p_gEulerY;
			float lEulerZ = gEulerZ - p_gEulerZ;

			return new Vector3(lEulerX, lEulerY, lEulerZ);
		}
		else
		{
			return new Vector3(gEulerX, gEulerY, gEulerZ);
		}
	}
}
