using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bone : MonoBehaviour
{
	void Start()
	{
		Destroy(gameObject, 3);
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(!coll.isTrigger && coll.gameObject.tag != "Skeleton")
		{
			Destroy(gameObject);
		}
	}
}
