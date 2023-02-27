using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    private LoadParameters parameters;


    void Start()
    {
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        ActiveLevelMenu();
    }


    private void ActiveLevelMenu()
    {
        for (int i = 0; i < transform.childCount && i <= parameters.currentLevel - 1; ++i)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
