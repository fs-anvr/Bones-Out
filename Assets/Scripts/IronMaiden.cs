using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMaiden : MonoBehaviour
{
    private BoxCollider2D _boxColl;
    private Rigidbody2D _rb;
    private Animator _anim;
    private Transform skeleton;
    private LayerMask _groundLayerMask;

    private bool active;
    private bool _isGrounded;

    private LoadParameters parameters;


    private Collider2D _coll;



    private void Start()
    {
        _boxColl = GetComponent<BoxCollider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        skeleton = GameObject.Find("Skeleton").transform;
        _groundLayerMask = LayerMask.GetMask("Ground");
        active = true;
        _isGrounded = true;
        parameters = Resources.Load<LoadParameters>("LoadParameters");
    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - (this.GetComponent<BoxCollider2D>().size.y / 2));
        Vector2 size = transform.lossyScale * 0.2f;
        if (Physics2D.OverlapBox(pos, size, 0, _groundLayerMask))
        {
            if (!_isGrounded && !active)
            {
                _isGrounded = true;
                gameObject.tag = "Shield";
            }
        }
        else if (!active && _isGrounded)
        {
            _isGrounded = false;
        }

        if (!_isGrounded && !active)
        {
            gameObject.tag = "EnemyDamage";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wolf" || other.gameObject.tag == "Skeleton" || other.gameObject.layer == 8 || other.gameObject.tag == "WolfAttack" || other.gameObject.layer == 6)
        {
            if (active && other.gameObject.layer != 9 && other.gameObject.layer != 7 && other.gameObject.layer != 10)
            {
                Debug.Log("Sucks!");
                active = false;
                _anim.SetBool("Activated", true);
                this.GetComponent<AudioSource>().Play();
                other.gameObject.transform.position = this.transform.position;
                _coll = other;
                gameObject.layer = 12;
            }
        }
    }


    public void HideCollision()
    {
        //if (_coll.gameObject.tag != "Skeleton" && _coll.gameObject.tag != "Wolf")
        //{
            _coll.gameObject.SetActive(false);
        //}
        _boxColl.isTrigger = false;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    public void Diactivated()
    {
        gameObject.tag = "Shield";
        _anim.SetBool("Activated", false);
    }
}
