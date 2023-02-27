using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wolf" || coll.gameObject.tag == "Skeleton")
        {
            
            if (this.GetComponent<PolygonCollider2D>() != null)
            {
                this.GetComponent<PolygonCollider2D>().isTrigger = true;
            }
            else if (this.GetComponent<BoxCollider2D>() != null)
            {
                this.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
    }
}
