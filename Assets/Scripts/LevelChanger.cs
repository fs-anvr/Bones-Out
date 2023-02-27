using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    private LoadParameters parameters;

    
    private void Start()
    {
        animator = GetComponent<Animator>();
        parameters = Resources.Load<LoadParameters>("LoadParameters");
        parameters.currentLevel = PlayerPrefs.GetInt("currentLevel");
        for (int i = 0; i < parameters.existDialog.Count; ++i)
        {
            parameters.existDialog[i] = PlayerPrefs.GetInt("existDialog" + i);
        }
        Debug.Log(PlayerPrefs.GetInt("cutscene1"));
    }


    public void FadeToLevel()
    {
        animator.SetTrigger("Fade");
    }


    public void StartGame()
    {
        if (parameters.currentLevel == 0)
        {
            parameters.currentLevel = 1;
            PlayerPrefs.SetInt("currentLevel", parameters.currentLevel);
            PlayerPrefs.Save();
        }
        parameters.nextLevel = parameters.currentLevel;
        animator.SetTrigger("Fade");
    }

    public void NewGame()
    {
        parameters.currentLevel = 1;
        PlayerPrefs.SetInt("currentLevel", 1);
        PlayerPrefs.SetInt("Death", 0);
        parameters.nextLevel = 1;
        for (int i = 0; i < parameters.existDialog.Count; ++i)
        {
            PlayerPrefs.SetInt("existDialog" + i, 0);
        }
        for (int i = 0; i < parameters.cutscene.Count; ++i)
        {
            PlayerPrefs.SetInt("cutscene" + i, 0);
        }
        PlayerPrefs.Save();
        animator.SetTrigger("Fade");
    }


    public void LoadToLevel(int level)
    {
        parameters.nextLevel = level;
        animator.SetTrigger("Fade");
    }

    public void ReloadLevel()
    {
        parameters.nextLevel = SceneManager.GetActiveScene().buildIndex;
        animator.SetTrigger("Fade");
    }


    public void OnFadeComplete()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (parameters.nextLevel == SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Death", PlayerPrefs.GetInt("Death") + 1);
                PlayerPrefs.Save();
            }
            if (parameters.nextLevel > SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Death", 0);
            }
        }
        if (parameters.nextLevel > parameters.currentLevel)
        {
            parameters.currentLevel = parameters.nextLevel;
            PlayerPrefs.SetInt("currentLevel", parameters.currentLevel);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene(parameters.nextLevel);
        Debug.Log("Level loaded");
    }


    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}