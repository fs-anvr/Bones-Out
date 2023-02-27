using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFourLevel : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    [SerializeField] private GameObject levelChanger;
    private LoadParameters parameters;

    private void Start()
    {
        parameters = Resources.Load<LoadParameters>("LoadParameters");
    }

    public void Final()
    {
        parameters.nextLevel = levelToLoad;
        levelChanger.GetComponent<Animator>().SetTrigger("Fade");
    }
}
