using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Animator _anim;
    private bool active;

    private void Start()
    {
        active = true;
        _anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Enemy" || coll.gameObject.name == "Wolf") && active)
        {
            _anim.SetBool("Activated", true);
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Enemy" || coll.gameObject.name == "Wolf") && active)
        {
            _anim.SetBool("Activated", false);
        }
    }

    public void ChangeActive()
    {
        active = !active;
    }
}
