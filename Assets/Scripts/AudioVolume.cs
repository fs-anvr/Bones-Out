using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Transform music = canvas.transform.Find("SettingsBackground/Music/Slider");
        Transform effects = canvas.transform.Find("SettingsBackground/Effects/Slider");

        if (music == null)
        {
            music = canvas.transform.Find("Menu/SettingsBackground/Music/Slider");
        }
        if (effects == null)
        {
            effects = canvas.transform.Find("Menu/SettingsBackground/Effects/Slider");
        }

        music.gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("musicVolume");
        effects.gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("effectsVolume");

        masterMixer.SetFloat ("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        masterMixer.SetFloat ("effectsVolume", PlayerPrefs.GetFloat("effectsVolume"));
    }

    public void SetMusicVolume(float soundLevel)
    {
        masterMixer.SetFloat ("musicVolume", soundLevel);
        PlayerPrefs.SetFloat("musicVolume", soundLevel);
        PlayerPrefs.Save();
    }


    public void SetEffectsVolume(float soundLevel)
    {
        masterMixer.SetFloat ("effectsVolume", soundLevel);
        PlayerPrefs.SetFloat("effectsVolume", soundLevel);
        PlayerPrefs.Save();
    }
}
