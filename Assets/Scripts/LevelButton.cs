using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelID;
    public Image levelText;
    public Image lockedImage;
    public GameObject[] stars;
    private Button button;

    private void Start()
    {
        for(int i=0;i<=2;i++)
        {
            if(PlayerPrefs.GetInt("Level_" + i.ToString()) < 500)
            {
                PlayerPrefs.SetInt("Level_" + i.ToString(), 501);
            }
        }

        if (PlayerPrefs.GetInt("Level_" + levelID.ToString()) > 502)
        {
            stars[0].SetActive(true);
            if (PlayerPrefs.GetInt("Level_" + levelID.ToString()) > 1000)
            {
                stars[1].SetActive(true);
                if (PlayerPrefs.GetInt("Level_" + levelID.ToString()) > 1500)
                {
                    stars[2].SetActive(true);
                }
            }
        }

        button = GetComponent<Button>();

        if (PlayerPrefs.GetInt("Level_" + (levelID-1).ToString()) < 500)
        {
            lockedImage.gameObject.SetActive(true);
            levelText.gameObject.SetActive(false);
            button.interactable = false;
        }
        else
        {
            lockedImage.gameObject.SetActive(false);
            levelText.gameObject.SetActive(true);
            button.interactable = true;
        }
    }
    public void OpenLevel()
    {
        SceneManager.LoadScene("Level_" + levelID.ToString());
    }
}
