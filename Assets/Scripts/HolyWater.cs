using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    private Animator _anim;
    private Animator wolf;


    private void Start()
    {
        _anim = GetComponent<Animator>();
        wolf = GameObject.Find("Wolf").GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Wolf" && wolf.GetBool("Blooded"))
        {
            _anim.SetTrigger("WolfInWater");
            wolf.SetBool("Blooded", false);
            gameObject.tag = "Empty";
        }
    }
}
