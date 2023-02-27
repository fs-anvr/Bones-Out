using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonAnim : MonoBehaviour
{
    private LoadParameters parameters;
    private GameObject skeleton;


    public void Bones_out()
    {
        this.GetComponent<Animator>().SetBool("Activated", false);
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        skeleton = GameObject.Find("Skeleton");
        parameters.currentCharacterName = "Skeleton";
        skeleton.GetComponent<SpriteRenderer>().enabled = true;
    }
}
