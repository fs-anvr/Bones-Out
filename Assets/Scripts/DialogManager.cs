using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Image dialogSprite;

    [SerializeField] private Animator windowAnim;
    //[SerializeField] private Animator triggerAnim;

    private LoadParameters parameters; 
    private Queue<Dialog> slides;
    private bool slideFinished;
    private string characterName;
    private GameObject character;


    private void Start()
    {
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        slides = new Queue<Dialog>();
        slideFinished = true;
    }

    public void StartDialog(Dialog[] phrases)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && PlayerPrefs.GetInt("cutscene2") == 0)
        {
            GameObject.Find("WolfPrison").GetComponent<Animator>().SetBool("Activated", true);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && PlayerPrefs.GetInt("cutscene1") == 1)
        {
            GameObject.Find("PrisonAnim").GetComponent<Animator>().SetBool("Activated", true);
        }
        characterName = parameters.currentCharacterName;
        //if (characterName == "freeze" && (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2))
        //{
        //    characterName = "Skeleton";
        //}
        character = GameObject.Find(characterName);
        parameters.currentCharacterName = "freeze";
        Invoke("StopCharacter", 0.3f);
        windowAnim.SetBool("Activated", true);

        slides.Clear();

        foreach(Dialog phrase in phrases)
        {
            slides.Enqueue(phrase);
        }


        slideFinished = true;
        NextPhrase();
    }

    private void StopCharacter()
    {
        if (character != null)
        {
            character.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
        }
    }

    public void NextPhrase()
    {
        if (slideFinished)
        {
            slideFinished = false;
            if (slides.Count == 0)
            {
                EndDialog();
                return;
            }
            Dialog slide = slides.Dequeue();
            if (slide.noSkip == false)
            {
                slideFinished = true;
            }
            nameText.text = "";
            nameText.text = slide.name;
            dialogSprite.sprite = null;
            dialogSprite.sprite = slide.sprite;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(slide.phrase));
        }
    }

    IEnumerator TypeSentence(string phrase)
    {
        Debug.Log("Start");
        dialogText.text = "";
        foreach (char letter in phrase)
        {
            dialogText.text += letter;
            yield return null;
        }
        Debug.Log("End");
        slideFinished = true;
    }

    public void EndDialog()
    {
        windowAnim.SetBool("Activated", false);
        if (SceneManager.GetActiveScene().buildIndex == 2 && PlayerPrefs.GetInt("cutscene2") == 0)
        {
            GameObject.Find("WolfPrison").GetComponent<Animator>().SetBool("Activated", true);
            GameObject.Find("TimelineManager").GetComponent<PlayableDirector>().Play();
            PlayerPrefs.SetInt("cutscene2", 2);
            Debug.Log("YOU FINALLY ABANDONED");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && PlayerPrefs.GetInt("cutscene1") == 1)
        {
            GameObject.Find("TimelineManager").GetComponent<PlayableDirector>().Play();
            PlayerPrefs.SetInt("cutscene1", 2);
            Debug.Log("YOU FINALLY AWAKE");
        }
        else
        {
            parameters.currentCharacterName = characterName;
        }
        PlayerPrefs.Save();
    }
}
