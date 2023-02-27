using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigElevator : MonoBehaviour
{
    private void Stopping()
    {
        //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
    }

    private void Moving()
    {
        //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
