using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Cutscene2 : MonoBehaviour
{
    [SerializeField] private int number = 2;

    
    private void Start()
    {
        if (PlayerPrefs.GetInt("cutscene" + number) == 0)
        {
            GameObject.Find("Wolf").GetComponent<SpriteRenderer>().enabled = false;
            Resources.Load<LoadParameters>("LoadParameters").currentCharacterName = "freeze";
        }
    }
}
