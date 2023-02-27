using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhraseManager : MonoBehaviour
{
    [SerializeField] private Text phraseText;

    [SerializeField] private Animator windowAnim;


    private void Start()
    {
        //slides = new Queue<Phrase>();
    }

    public void StartPhrase(string phrase)
    {
        windowAnim.SetBool("Activated", true);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(phrase));

    }

    IEnumerator TypeSentence(string phrase)
    {
        phraseText.text = "";
        foreach (char letter in phrase)
        {
            phraseText.text += letter;
            yield return null;
        }
        Invoke("EndDialog", 5.0f);
    }

    public void EndDialog()
    {
        windowAnim.SetBool("Activated", false);
    }
}
