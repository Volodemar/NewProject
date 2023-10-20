using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionController
{ 
	/// <summary>
	/// С чем-то столкнулись
	/// </summary>
	public void onCollisionEnter(Collider collision);
}
