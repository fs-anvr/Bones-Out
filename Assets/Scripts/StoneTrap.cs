using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrap : MonoBehaviour
{
    public float speed = 3;
	public Rigidbody2D stone;


	public void Throw()
	{
        Vector2 myPos = transform.position;
        myPos.y -= (transform.lossyScale.x / 2 + 0.01f);
        Rigidbody2D clone = Instantiate(stone, myPos, transform.rotation);
        GetComponent<Animator>().SetBool("Activated", false);
	}
}
