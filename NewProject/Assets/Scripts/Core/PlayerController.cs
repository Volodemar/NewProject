using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using AndroidNativeCore;

/// <summary>
/// Управление игроком
/// </summary>
public class PlayerController : BaseGameObject
{
    public static PlayerController Instance { get; private set; }

    public Animator animator;
    public Transform ModelMount;
    public List<GameObject> models; 

    private bool isActive = false;
    private Rigidbody _rb;
    private int currentModelIndex = 0;

    private long _watchMax = 0;

	private void Awake()
	{
        Instance = this;
        _rb = this.GetComponent<Rigidbody>();
	}

	public void Init()
	{
		ChangePlayerModel(true, 0);
	}

	public void SetPlayerActive(bool value)
	{
        isActive = value;
        _rb.velocity = Vector3.zero;
	}

	public void FixedUpdate()
    {
        if(isActive)
        { 
            if(InitScene())
            {             
                MoveJoystikDirection();
            }
        }
    }

    public void HandleTouchDown(Vector3 position)
    {
    }

    public void HandleTouch(Vector3 position)
    {
    }

    public void HandleTouchUp(Vector3 position)
    { 
    }

    /// <summary>
    /// Движение игрока джойстиком, когда камера смотрит сверху под каким-то углом
    /// </summary>
	private void MoveJoystikDirection()
	{
        if(J.Horizontal != 0 || J.Vertical != 0)
        { 
            Vector3 direction = Camera.transform.forward * J.Vertical + Camera.transform.right * J.Horizontal;
            direction = new Vector3(direction.x, 0, direction.z);
            float speed = 5f;

            _rb.velocity = direction * speed; 

			if (_rb.velocity != Vector3.zero)
            { 
                Quaternion newRotate = Quaternion.LookRotation(_rb.velocity);

                ModelMount.rotation = Quaternion.Slerp(ModelMount.rotation, newRotate, 0.1f);
            }
        }
        else
		{
            _rb.velocity = Vector3.zero;
		}

        if(_rb.velocity != Vector3.zero)
        { 
            //animator.SetTrigger("walk");
        }
        else
        { 
            //animator.ResetTrigger("walk");
        }
	}

    public void ChangePlayerModel(bool value, int newIndex)
	{
		if (InitScene() && models.Count > 0)
		{
            for (int i = 0; i < models.Count; i++)
			    if(models[i].activeSelf) currentModelIndex = i;                 

            if(!value)
            { 
                for (int i = 0; i < models.Count; i++)
			        models[i].SetActive(false);
            }
            else
		    {
                if(newIndex > models.Count-1)
                    return;

                if(newIndex != currentModelIndex)
				{
                    // Анимация смены модельки
				    ModelMount.localScale = Vector3.one;
				    ModelMount.DOScale(0.5f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
				    {
                        //Отключаем прошлые модели
                        for (int i = 0; i < models.Count; i++)
                            if(i != newIndex)
			                    models[i].SetActive(false);

                        //Включаем текущую модельку
				        models[newIndex].SetActive(true);
                        animator = models[newIndex].GetComponent<Animator>();

                        //Текущий материал, лучше искать скрипт на модельке
                        //foreach(MeshRenderer mr in models[newModelIndex].transform.GetComponentsInChildren<MeshRenderer>())
                        //{
                        //    currentMaterial = mr.material;                        
                        //}

                        //foreach(SkinnedMeshRenderer mr in models[newModelIndex].transform.GetComponentsInChildren<SkinnedMeshRenderer>())
                        //{
                        //    currentMaterial = mr.material;                        
                        //}

					    ModelMount.DOScale(1f, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
					    {
						    ModelMount.localScale = Vector3.one;
					    });

                        currentModelIndex = newIndex;
				    });
				}
		    }
        }
	}

    /// <summary>
    /// Игнорирование коллайдеров игрока с объектом
    /// </summary>
	public void SetIgnoreCollisionsPlayer(Collider collider,  bool isNotCollision)
	{ 
        foreach(Collider playerCollider in Player.transform.GetComponentsInChildren<Collider>())
            Physics.IgnoreCollision(playerCollider, collider, isNotCollision);
	}

    /// <summary>
    /// Анимация сбор предмета
    /// </summary>
    public IEnumerator AnimBubble()
	{
        ModelMount.localScale = Vector3.one;
        ModelMount.DOScale(1.2f, 0.1f).OnComplete(() => {
            ModelMount.DOScale(1f, 0.1f).OnComplete(() => {
                ModelMount.localScale = Vector3.one;
            });                
        });
        yield return new WaitForSeconds(0.2f);
	}

    /// <summary>
    /// Анимация мигание
    /// </summary>
    private IEnumerator AnimBlinced()
	{
        GameObject currentModel = models[currentModelIndex];

        currentModel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        currentModel.SetActive(true);

        yield return null;
	}

    private void Vibration()
	{
        //Vibrator.Vibrate(100);
	}

    private void DebugTimerExample()
	{
        //Замер и оптимизация для слабых устройств
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        watch.Stop();

        if(_watchMax < watch.ElapsedMilliseconds)
            _watchMax = watch.ElapsedMilliseconds;

        UI.UIWindowDebug.DebugText.SetText("CutTime: " + watch.ElapsedMilliseconds.ToString() + " MAX: " + _watchMax.ToString());
	}
}
