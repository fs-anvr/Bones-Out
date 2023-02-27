using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonWolfAnim : MonoBehaviour
{
    private LoadParameters parameters;
    private GameObject wolf;


    public void Wolf_out()
    {
        this.GetComponent<Animator>().SetBool("Activated", false);
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        wolf = GameObject.Find("Wolf");
        parameters.currentCharacterName = "Skeleton";
        wolf.GetComponent<SpriteRenderer>().enabled = true;
    }
}
