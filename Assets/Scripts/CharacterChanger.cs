using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterChanger : MonoBehaviour
{
    #region Cinemachine
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] private int activeCharacter = 0;
    private GameObject Skeleton;
    private GameObject Wolf;
    private LoadParameters parameters;

    #endregion


    void Start()
    {
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        Skeleton = GameObject.Find("Skeleton");
        Wolf = GameObject.Find("Wolf");
        Skeleton.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        vcam.m_Follow = Skeleton.transform;
        parameters.currentCharacterName = Skeleton.name;
    }


    void Update()
    {
        ChangeLogic();
    }


    private void ChangeLogic()
    {
        if (parameters.nextLevel != 1)
        {
            if (Input.GetButtonDown("Fire3") && parameters.currentCharacterName != "freeze")
            {
                if ((activeCharacter == 0 && Skeleton.GetComponent<ActivePlayerMovement>()._isGrounded) ||
                    (activeCharacter == 1 && Wolf.GetComponent<ActivePlayerMovement>()._isGrounded))
                {
                    Change((++activeCharacter) % 2);
                    activeCharacter %= 2;
                }
            }
        }
    }


    private void Change(int activateCharacter)
    {
        if (parameters.nextLevel != 1)
        {
            if (activateCharacter == 0)
            {
                    //Wolf.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    //Skeleton.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    vcam.m_Follow = Skeleton.transform;
                    parameters.currentCharacterName = Skeleton.name;
            }
            else
            {
                //Skeleton.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                //Wolf.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                vcam.m_Follow = Wolf.transform;
                parameters.currentCharacterName = Wolf.name;   
            }
            Wolf.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Skeleton.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        Debug.Log("Character changed");
    }
}
