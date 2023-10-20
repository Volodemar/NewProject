using UnityEngine;

/// <summary>
/// �������� �������� �� ����������� �������
/// ����������� �������� � �������� ����� �� ������� ��� �� ��������
/// ����������� ������� ������ �� ����� ��������
/// ����������� �������� ������ ����� ���������� ��������
/// ����������� ����������� �������� �� �������� 
/// </summary>
public class MyToolMoved : MonoBehaviour
{
    private Vector3 _targetPos = Vector3.zero;
    private bool    isMove    = false;
    private float   _speed     = 0f;
    private float   _time      = 0f;

    public delegate void OnComplete(); 
    public OnComplete onCompleteCallback;

    private System.Diagnostics.Stopwatch watch; 

    private void Update()
    {
        if(!isMove)
            return;

        float distance      = Vector3.Distance(this.transform.position, _targetPos);
        Vector3 direction   = _targetPos - this.transform.position; 
        float speed         = _speed;

        if(_time != 0)
        { 
		    speed = distance / _time;
            _time = _time - Time.deltaTime;

            if(_time <= 0)
			{
                _time = 0;
                Complete();
                return;
			}
        }

        if(speed * Time.deltaTime <= distance)
		{
            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
		}
        else
		{
            Complete();
		}
    }

    private void Complete()
	{
        isMove = false;
        this.transform.position = _targetPos;
        this.onCompleteCallback?.Invoke();

        if(watch != null)
		{
            watch.Stop();
            Debug.Log(watch.ElapsedMilliseconds.ToString());
		}
            
	}

    /// <summary>
    /// ����������� �� ������� ������� � ������ ����������
    /// </summary>
    /// <param name="inTargetPos">����</param>
    /// <param name="inSpeed">�������� �����������</param>
	public void Move(Vector3 inTargetPos, float inSpeed)
	{
        if(this.transform.position == inTargetPos)
            return;

        _speed              = inSpeed;
		_targetPos          = inTargetPos;
        isMove              = true;
	}

    /// <summary>
    /// ����������� �� ������� ������� � ������ � ��������
    /// </summary>
    /// <param name="inTargetPos">����</param>
    /// <param name="inSpeed">�������� �����������</param>
    /// <param name="onComplete">���������� �� ����������</param>
	public void OnMove(Vector3 inTargetPos, float inSpeed, OnComplete onComplete = null)
	{
        if(this.transform.position == inTargetPos)
            return;

        _speed               = inSpeed;
		_targetPos           = inTargetPos;
        onCompleteCallback  = onComplete;
        isMove              = true;
	}

    /// <summary>
    /// ����������� �� ������� ������� � ������ � ��������
    /// </summary>
    /// <param name="inTargetPos">����</param>
    /// <param name="onComplete">���������� �� ����������</param>
	public void OnMoveTime(Vector3 inTargetPos, float inTime, OnComplete onComplete = null)
	{
        if(this.transform.position == inTargetPos)
            return;

        _speed              = 0;
        _time               = inTime;
		_targetPos          = inTargetPos;
        onCompleteCallback  = onComplete;
        isMove              = true;
        
        watch = new System.Diagnostics.Stopwatch();
        watch.Start();
	}
}
