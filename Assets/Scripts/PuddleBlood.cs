using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleBlood : MonoBehaviour
{
    private GameObject wolf;

    private bool _inBlood;

    private LoadParameters parameters;

    private void Start()
    {
        wolf = GameObject.Find("Wolf");
        _inBlood = false;   
        parameters = Resources.Load<LoadParameters>("LoadParameters"); 
    }

    private void Update()
    {
        if (_inBlood && parameters.currentCharacterName == "Wolf")
        {
            wolf.GetComponent<Animator>().SetBool("Blooded", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Wolf" && !wolf.GetComponent<Animator>().GetBool("Blooded") && !_inBlood && wolf.GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            _inBlood = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Wolf" && !wolf.GetComponent<Animator>().GetBool("Blooded") && _inBlood)
        {
            _inBlood = false;
        }
    }
}
