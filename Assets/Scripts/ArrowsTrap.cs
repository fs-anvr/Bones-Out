using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsTrap : MonoBehaviour
{
    public float speed = 10;
	public Rigidbody2D arrow;


	public void Throw()
	{
        Vector2 myPos = transform.position;
        myPos.x += Mathf.Sign(transform.localScale.x) * (transform.lossyScale.x / 2 + 0.01f);
        Rigidbody2D clone = Instantiate(arrow, myPos, transform.rotation);
        clone.velocity = Vector2.right * speed * Mathf.Sign(transform.localScale.x);
        GetComponent<Animator>().SetBool("Activated", false);
	}
}
