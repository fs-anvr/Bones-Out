using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    private Animator _anim;


    private void Start()
    {
        _anim = this.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Wolf" || coll.gameObject.tag == "Skeleton") && !_anim.GetBool("Activated"))
        {
            _anim.SetBool("Activated", true);
        }   
    }


    private void OnTriggerExit2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Wolf" || coll.gameObject.tag == "Skeleton") && _anim.GetBool("Activated"))
        {
            _anim.SetBool("Activated", false);
        }  
    }
}
