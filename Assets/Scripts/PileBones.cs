using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileBones : MonoBehaviour
{
    private Animator _anim;
    private Animator _skeletonAnim;
    private bool active;

    private LoadParameters parameters;


    private void Start()
    {
        _anim = this.GetComponent<Animator>();
        _skeletonAnim = GameObject.Find("Skeleton").GetComponent<Animator>();  
        active = false; 
        parameters = Resources.Load<LoadParameters>("LoadParameters");
    }


    private void Update()
    {
        Recovery();
    }


    private void Recovery()
    {
        if (active && Input.GetButton("Submit") && parameters.currentCharacterName == "Skeleton")
        {
            bool legs = _skeletonAnim.GetBool("Legs");
            bool arms = _skeletonAnim.GetBool("Arms");
            if (!legs || !arms)
            {
                if (!arms && _anim.GetBool("Arms"))
                {
                    _skeletonAnim.SetBool("Arms", true);
                    _anim.SetBool("Arms", false);
                }
                if (!legs &&  _anim.GetBool("Legs"))
                {
                    _skeletonAnim.SetBool("Legs", true);
                    _anim.SetBool("Legs", false);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Skeleton")
        {
            active = true;
        }        
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Skeleton")
        {
            active = false;
        } 
    }
}
