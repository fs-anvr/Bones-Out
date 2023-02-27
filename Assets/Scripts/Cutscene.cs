using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Cutscene : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private Transform skeleton;
    //[SerializeField] private int number = 1;

    
    private void Start()
    {
        if (PlayerPrefs.GetInt("cutscene1") == 0)
        {
            skeleton.position = point.position;
            PlayerPrefs.SetInt("cutscene1", 1);
        }
        else if (PlayerPrefs.GetInt("cutscene1") == 1)
        {
            GameObject.Find("Skeleton").GetComponent<SpriteRenderer>().enabled = false;
            Resources.Load<LoadParameters>("LoadParameters").currentCharacterName = "freeze";
        }
    }
}
