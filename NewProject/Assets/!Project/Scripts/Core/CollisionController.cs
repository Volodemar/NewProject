using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
	[SerializeField] public GameObject controllerObject;
	private ICollisionController controller;

	private void Start()
	{
		if(controller == null)
			controller = controllerObject.GetComponent<ICollisionController>();		
	}

	private void OnEnable()
	{
		if(controller == null)
			controller = controllerObject.GetComponent<ICollisionController>();			
	}

	private void OnTriggerEnter(Collider collision)
	{
		controller.onCollisionEnter(collision);			
	}

	private void OnCollisionEnter(Collision collision)
	{
		controller.onCollisionEnter(collision.collider);			
	}
}
