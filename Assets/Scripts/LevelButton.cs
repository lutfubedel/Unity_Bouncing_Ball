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
    public Button button;
    public LevelManager levelManager;

    private void Start()
    {
        if(levelManager.IsLevelLocked(levelID))
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
