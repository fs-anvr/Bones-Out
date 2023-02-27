using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class RollingStone : MonoBehaviour
{
    private float speed = 10.0f;
    private Transform skeleton;
    private Transform wolf;
    private bool moved;


    private void Start()
	{
        skeleton = GameObject.Find("Skeleton").transform;
        wolf = GameObject.Find("Wolf").transform;
        moved = false;
		Destroy(gameObject, 15);
	}

    private void Update()
    {
        if (!moved)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y - transform.lossyScale.y / 2);
            Vector2 size = transform.lossyScale * 0.1f;
            if (Physics2D.OverlapBox(pos, size, 0, LayerMask.GetMask("Ground")))
            {
                moved = true;
                float sign;
                if (Vector3.Distance(transform.position, skeleton.position) < Vector3.Distance(transform.position, wolf.position))
                {
                    sign = Mathf.Sign(skeleton.position.x - transform.position.x);
                }
                else
                {
                    sign = Mathf.Sign(wolf.position.x - transform.position.x);
                }
                GetComponent<Rigidbody2D>().velocity = Vector2.right * speed * sign;
            }
        }
    }

	
	void OnTriggerEnter2D(Collider2D coll)
	{
        if(!coll.isTrigger || coll.gameObject.tag == "Shield" &&  coll.gameObject.tag != "Untagged")
		{
			Destroy(gameObject);
		}
	}


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wooden")
        {
            coll.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
        }
    }
}
