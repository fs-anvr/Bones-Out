using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour
{
    Animator animator;
    private LoadParameters parameters;
    [SerializeField] public int LevelToLoad;

    private bool isTriggered = false;

    void Start()
    {
        animator = GameObject.Find("UI/Canvas/LevelChanger").GetComponent<Animator>();
        parameters = Resources.Load<LoadParameters>("LoadParameters");
    }
    void Update()
    {
        LoadLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Skeleton")
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Skeleton")
        {
            isTriggered = false;
        }
    }

    private void LoadLevel()
    {
        if (Input.GetButtonDown("Submit") && isTriggered)
        {
            float distance = Vector2.Distance(GameObject.Find("Wolf").transform.position, GameObject.Find("Skeleton").transform.position);
            if (distance < 12.0f)
            {
                Debug.Log("ffffffffff");
                this.GetComponent<AudioSource>().Play();
                parameters.nextLevel = LevelToLoad;
                animator.SetTrigger("Fade");
            }
        }
    }
}
