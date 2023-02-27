using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbwaiter : MonoBehaviour
{
    private bool collisedSkeleton;
    private bool collisedWolf;
    private bool activated;
    private float grScale;
    private Animator _anim;
    private GameObject skeleton;
    private LoadParameters parameters;
    private bool isSkeleton;


    private void Start()
    {
        _anim = GetComponent<Animator>();
        collisedSkeleton = false;
        collisedWolf = false;
        activated = false;
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        skeleton = GameObject.Find("Skeleton");
        grScale = skeleton.GetComponent<Rigidbody2D>().gravityScale;
        isSkeleton = false;
    }


    private void Update()
    {
        Activate();
    }


    private void Activate()
    {
        if (collisedSkeleton && !activated && parameters.currentCharacterName == "Skeleton" && Input.GetButtonDown("Submit"))
        {
            skeleton.GetComponent<Animator>().SetBool("inElevator", true);
            skeleton.transform.SetParent(this.transform);
            skeleton.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            skeleton.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            skeleton.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - this.transform.lossyScale.y/4);
            skeleton.GetComponent<BoxCollider2D>().isTrigger = true;
            parameters.currentCharacterName = "no";
            activated = true;
            _anim.SetBool("Activated", true);
            isSkeleton = true;
            skeleton.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            //skeleton.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else if (collisedWolf && !activated && parameters.currentCharacterName == "Wolf" && Input.GetButtonDown("Submit"))
        {
            isSkeleton = false;
            activated = true;
            _anim.SetBool("Activated", true);
        }
    }


    public void Diactivated()
    {
        skeleton.GetComponent<Animator>().SetBool("inElevator", false);
        skeleton.transform.parent = null;
        skeleton.GetComponent<BoxCollider2D>().isTrigger = false;
        skeleton.GetComponent<Rigidbody2D>().gravityScale = grScale;
        if (isSkeleton)
        {
            parameters.currentCharacterName = "Skeleton";
            skeleton.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            //skeleton.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        activated = false;
        _anim.SetBool("Activated", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Skeleton")
        {
            collisedSkeleton = true;
        }
        if (other.gameObject.name == "Wolf")
        {
            collisedWolf = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.name == "Skeleton")
        {
            collisedSkeleton = false;
        }
        if (other.gameObject.name == "Wolf")
        {
            collisedWolf = false;
        }
    }
}
