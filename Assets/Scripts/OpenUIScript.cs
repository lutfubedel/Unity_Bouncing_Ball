using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenUIScript : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject[] screens;

    private void Start()
    {

    }
    public void OpenLevel(int index)
    {

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
