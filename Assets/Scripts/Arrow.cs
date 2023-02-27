using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Arrow : MonoBehaviour
{
    void Start()
	{
		Destroy(gameObject, 5);
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(!coll.isTrigger || coll.gameObject.tag != "Ground" || coll.gameObject.tag == "Shield")
		{
			Destroy(gameObject);
		}
	}
}
