using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private LoadParameters parameters;
    private Animator _anim;

    private bool dumbwaiter;
    private float timer;
    [SerializeField] private float howlTime = 1.0f;


    private void Start()
    {
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        _anim = GetComponent<Animator>();
        dumbwaiter = false;
        timer = howlTime;
    }


    private void Update()
    {
        Howl();
        Interact();
        Timer();
    }

    private void Timer()
    {
        timer -= Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null && this.tag == "EnemyDamage")
        {
            other.gameObject.GetComponent<Animator>().SetBool("Attacked", true);
            this.GetComponent<Animator>().SetBool("Blooded", true);
        }

        if (other.gameObject.tag == "Wooden" && other.gameObject.layer == 7 && this.tag == "WolfAttack")
        {
            other.gameObject.GetComponent<Animator>().SetBool("Destroy", true);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dumbwaiter")
        {
            dumbwaiter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dumbwaiter")
        {
            dumbwaiter = false;
        }
    }


    private void Interact()
    {
        if (dumbwaiter && Input.GetButtonDown("Submit"))
        {
            _anim.SetBool("Interact", true);
        }
    }


    public void ResetAttack()
    {
        this.GetComponent<Animator>().SetBool("Attack", false);
    }

    private void Howl()
    {
        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F) && parameters.currentCharacterName == "Wolf" && !parameters.howl && _anim.GetBool("isGrounded"))
            {
                parameters.howl = true;
                this.GetComponent<AudioSource>().Play();
                _anim.SetBool("Howl", true);
                timer = howlTime;
            }
        }


        if ((Input.GetKeyUp(KeyCode.F) || parameters.currentCharacterName != "Wolf") && parameters.howl)
        {
            parameters.howl = false;
        }
    }

    public void ResetHowl()
    {
        _anim.SetBool("Howl", false);
    }

    public void ResetInteract()
    {
        _anim.SetBool("Interact", false);
    }
}
