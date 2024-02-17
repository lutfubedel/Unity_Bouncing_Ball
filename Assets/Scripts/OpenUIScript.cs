using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenUIScript : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject[] screens;

    [Header("Settings")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);

        source.volume = PlayerPrefs.GetFloat("MusicVolume");
    }


    public void OpenLevelScreen()
    {
        OpenScreen(1);
    }
    public void BackButton()
    {
        OpenScreen(0);
    }
    public void OpenSettingScreen()
    {
        OpenScreen(2);
    }



    private void OpenScreen(int activeScreenIndex)
    {
        for(int i=0;i<screens.Length;i++)
        {
            if(i==activeScreenIndex)
            {
                screens[i].SetActive(true);
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }


}
