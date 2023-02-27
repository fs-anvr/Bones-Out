using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private Animator _LevelChangerAnimator;
    private LoadParameters parameters;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _LevelChangerAnimator = GameObject.Find("UI/Canvas/LevelChanger").GetComponent<Animator>();
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        parameters.nextLevel = SceneManager.GetActiveScene().buildIndex;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Damage") ||
            (gameObject.name == "Skeleton" && other.gameObject.tag == "SkeletonDamage")||
            (gameObject.name == "Wolf" && other.gameObject.tag == "WolfDamage"))
        {
            if ((this.tag != "WolfAttack") || (other.gameObject.layer != 8))
            {
                Death();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Damage") ||
            (gameObject.name == "Skeleton" && other.gameObject.tag == "SkeletonDamage")||
            (gameObject.name == "Wolf" && other.gameObject.tag == "WolfDamage"))
        {
            if ((this.tag != "WolfAttack") || (other.gameObject.layer != 8))
            {
                Death();
            }
        }
    }


    void Death()
    {
        _anim.SetBool("Death", true);
        _rb.velocity = new Vector2( 0, 0);
        //GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<ActivePlayerMovement>().enabled = false;
    }


    void ResetLevel()
    {
        //parameters.nextLevel = SceneManager.GetActiveScene().buildIndex;
        _LevelChangerAnimator.SetTrigger("Fade");
        Debug.Log("Player die. Level Reloaded");
    }
}
