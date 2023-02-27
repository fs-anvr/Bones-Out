using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLife : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    [SerializeField] private GameObject puddle;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Damage" || other.gameObject.tag == "EnemyDamage" || other.gameObject.tag == "WolfDamage" || other.gameObject.tag == "WolfAttack")
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Damage" || other.gameObject.tag == "EnemyDamage" || other.gameObject.tag == "WolfDamage" || other.gameObject.tag == "WolfAttack")
        {
            Death();
        }
    }


    private void Death()
    {
        _anim.SetTrigger("Death");
        this.tag = "Untagged";
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Enemy>().enabled = false;


        //Vector3 spawnPoint = transform.GetChild(0).transform.position;
        //Quaternion rotation = Quaternion.Euler(0, 0, 0);
        //Instantiate(puddle, spawnPoint, rotation);
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
