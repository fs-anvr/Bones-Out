using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private GameObject menu;


    void Start()
    {
        menu = GameObject.Find("UI/Canvas/Menu");    
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel")) // Escape
        {
            if (menu.activeInHierarchy)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }
}
