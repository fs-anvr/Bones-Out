using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonThrow : MonoBehaviour {

	public float speed = 10;
	public Rigidbody2D bone;
	//public Transform spawnPoint;
	private Animator _anim;
	private LoadParameters parameters;
	private float reload = 1.0f;
	private float timer;
	private bool interact;
	private bool pressed;

	private float curTimeout;
	
	private void Start()
	{
		_anim = GetComponent<Animator>();
		parameters = Resources.Load<LoadParameters>("LoadParameters");
		timer = reload;
		interact = false;
		pressed = false;
		Physics2D.IgnoreLayerCollision(6, 12, true);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1) && parameters.currentCharacterName == "Skeleton" && (_anim.GetBool("Arms") || _anim.GetBool("Legs")) && timer <= 0)
		{
			Throw();
		}
		timer -= Time.deltaTime;

		isInteract();
		InteractLogic();
	}

	void Throw()
	{
		timer = reload;
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 myPos = transform.position;
        Vector2 direction = target - myPos;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
        Rigidbody2D clone = Instantiate(bone, myPos, rotation);
		if (_anim.GetBool("Legs"))
		{
			_anim.SetBool("Legs", false);
		}
		else
		{
			_anim.SetBool("Arms", false);
		}
        clone.velocity = direction * speed;
		
	}


	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Shield")
		{
			_anim.SetBool("Push", true);
		}
		if (coll.gameObject.tag == "Interact")
		{
			interact = true;
		}
	}


	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Shield")
		{
			_anim.SetBool("Push", true);
		}
		if (coll.gameObject.tag == "Interact")
		{
			interact = true;
		}
	}


	private void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Shield" || coll.gameObject.tag == "EnemyDamage")
		{
			_anim.SetBool("Push", false);
		}
		if (coll.gameObject.tag == "Interact")
		{
			interact = false;
		}
	}


	private void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Shield" || coll.gameObject.tag == "EnemyDamage")
		{
			_anim.SetBool("Push", false);
		}
		if (coll.gameObject.tag == "Interact")
		{
			interact = false;
		}
	}


	private void isInteract()
	{
		if (interact && Input.GetButtonDown("Submit") && _anim.GetBool("Arms") && parameters.currentCharacterName == "Skeleton")
		{
			_anim.SetBool("Interact", true);
		}
	}


	public void NoInteract()
	{
		_anim.SetBool("Interact", false);
	}

	private void InteractLogic()
	{
		if (Input.GetButtonDown("Fire2") && parameters.currentCharacterName == "Skeleton" && _anim.GetBool("Legs") && !pressed)
		{
			Physics2D.IgnoreLayerCollision(6, 12, false);
			pressed = true;
		}
		else if (Input.GetButtonUp("Fire2") || parameters.currentCharacterName != "Skeleton" && pressed)
		{
			Physics2D.IgnoreLayerCollision(6, 12, true);
			pressed = false;
		}
	}
}
