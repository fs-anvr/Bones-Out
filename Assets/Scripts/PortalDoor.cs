using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    private bool collised;
    public bool blocked;
    private bool activated;
    [SerializeField]private int number;
    private GameObject skeleton;
    private LoadParameters parameters;
    private GameObject[] doors;


    private void Start()
    {
        collised = false;
        blocked = false;
        activated = false;
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        skeleton = GameObject.Find("Skeleton");
        doors = GameObject.FindGameObjectsWithTag("PortalDoor");
        for (int i = 0; i < doors.Length; ++i)
        {
            if (gameObject.name == doors[i].name)
            {
                number = i;
                break;
            }
        }
    }


    private void Update()
    {
        Activate();
    }


    private void Activate()
    {
        if (!blocked && collised && !activated && parameters.currentCharacterName == "Skeleton" && Input.GetButtonDown("Submit"))
        {
            parameters.currentCharacterName = "no";
            this.GetComponent<Animator>().SetBool("Open", true);
            this.GetComponent<AudioSource>().Play();
            skeleton.GetComponent<SpriteRenderer>().enabled = false;
            int door = (number + 1)  % doors.Length;
            do
            {
                skeleton.transform.position = doors[door].transform.position;

                if (!doors[door].GetComponent<PortalDoor>().Blocked())
                {
                    break;
                }
                door = (door + 1)  % doors.Length;
            }
            while (door != number);
            
            doors[door].GetComponent<Animator>().SetBool("Open", true);
            skeleton.GetComponent<SpriteRenderer>().enabled = true;
            //skeleton.SetActive(true);
            parameters.currentCharacterName = "Skeleton";
        }
        
    }


    public bool Blocked()
    {
        return blocked;
    }

    public void Close()
    {
        this.GetComponent<Animator>().SetBool("Open", false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Skeleton")
        {
            collised = true;
        }
        if (other.gameObject.tag == "Shield")
        {
            blocked = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.gameObject.name == "Skeleton")
        {
            collised = false;
        }
        if (other.gameObject.tag == "Shield")
        {
            blocked = false;
        }
    }
}
