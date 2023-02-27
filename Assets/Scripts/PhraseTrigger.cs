using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string phrase;

    [SerializeField] private int number;

    private LoadParameters parameters;


    private void Start()
    {
        parameters = Resources.Load<LoadParameters>("LoadParameters");
    }
    

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Skeleton" || coll.gameObject.tag == "Wolf" ||  coll.gameObject.tag == "WolfAttack")
        {
            if (parameters.existDialog[number] == 0)
            {
                parameters.existDialog[number] = 1;
                PlayerPrefs.SetInt("existDialog" + number, 1);
                PlayerPrefs.Save();
                FindObjectOfType<PhraseManager>().StartPhrase(phrase);
                this.enabled = false;
            }
        }
    }
}
