using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAutoMoveTarget : MonoBehaviour
{
    public Transform target;
    public float Speed = 10f;

    private Sequence seqMove;

    public void MoveToPos(Vector2 pos)
	{
		if(seqMove != null)
			seqMove.Kill();
		seqMove = DOTween.Sequence();

		seqMove.Append(this.transform.DOMove(pos, 1)
                                     .OnComplete( () => { this.gameObject.SetActive(false);}) );
	}

	public void AnimMoveStop()
	{
		seqMove.Kill();
	}  
}
