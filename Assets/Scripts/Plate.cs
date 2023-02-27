using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private GameObject _object;
    private Animator _objectAnim;
    private float timer;
    [SerializeField] private bool possibilityDeactivation = true;

    [SerializeField] private string activatedBool = "Activated";

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _objectAnim = _object.GetComponent<Animator>();
        timer = 0;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Wolf" || collision.gameObject.tag == "Shield" || collision.gameObject.tag == "EnemyDamage" || collision.gameObject.layer == 8) && !_anim.GetBool("Activated") && timer <= 0)
        {
            this.GetComponent<AudioSource>().Play();
            _anim.SetBool("Activated", true);
            _objectAnim.SetBool(activatedBool, true);
            timer = 1.0f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (possibilityDeactivation)
        {
            if ((collision.gameObject.tag == "Wolf" || collision.gameObject.tag == "Shield" || collision.gameObject.tag == "EnemyDamage" || collision.gameObject.layer == 8) && _anim.GetBool("Activated"))
            {
                this.GetComponent<AudioSource>().Play();
                _anim.SetBool("Activated", false);
                _objectAnim.SetBool(activatedBool, false);
                timer = 0.7f;
            }
        }
    }
}
