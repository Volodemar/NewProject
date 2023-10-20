using UnityEngine;

/// <summary>
/// Механизм движения не физического объекта
/// Возможность движения в заданную точку по времени или по скорости
/// Возможность удалять объект во время движения
/// Возможность получить фитбек после завершения движения
/// Возможность постоянного слежения за объектом 
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
    /// Перемещение из текущей позиции в таргет постоянное
    /// </summary>
    /// <param name="inTargetPos">Цель</param>
    /// <param name="inSpeed">Скорость перемещения</param>
	public void Move(Vector3 inTargetPos, float inSpeed)
	{
        if(this.transform.position == inTargetPos)
            return;

        _speed              = inSpeed;
		_targetPos          = inTargetPos;
        isMove              = true;
	}

    /// <summary>
    /// Перемещение из текущей позиции в таргет с событием
    /// </summary>
    /// <param name="inTargetPos">Цель</param>
    /// <param name="inSpeed">Скорость перемещения</param>
    /// <param name="onComplete">Выполнение по завершении</param>
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
    /// Перемещение из текущей позиции в таргет с событием
    /// </summary>
    /// <param name="inTargetPos">Цель</param>
    /// <param name="onComplete">Выполнение по завершении</param>
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
