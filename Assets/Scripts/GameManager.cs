using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Wall :")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform wallParent;

    [Header("Timer")]
    [SerializeField] private Text timerText;
    [SerializeField] private float timer;

    [Header("Completed Panel")]
    [SerializeField] private GameObject panelDead;
    [SerializeField] private GameObject panelCompleted;
    [SerializeField] private Text completed_score_text;
    [SerializeField] private Ball_Script ball;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject[] buttons;

    [Header("Score")]
    [SerializeField] private int targetScore;
    private int scoreIncreaseRate = 200;
    private int currentScore = 0;

    public bool startCoroutine;



    void Start()
    {
        WallSpawner();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (timer > 0 && !ball.isCompleted && !ball.isDead)
        {
            timer -= Time.deltaTime;
            timerText.text = String.Format("00:{0:00}", (int)timer);
        }
        else
        {
            ball.isDead = true;
        }


        if (ball.isCompleted)
        {
            panelCompleted.SetActive(true);
 
            targetScore = Mathf.RoundToInt(timer * ball.bounce_count * 10);

            if (currentScore < targetScore)
            {
                currentScore += Mathf.CeilToInt(scoreIncreaseRate * Time.deltaTime);
                completed_score_text.text = currentScore.ToString();
            }

            else 
            {
                startCoroutine = true;
            }

            StartCoroutine(UITimer());
        }

        


        

    }


    IEnumerator UITimer()
    {
        if(startCoroutine)
        {
            if(targetScore>500)
            {
                stars[0].SetActive(true);
                yield return new WaitForSeconds(1f);

                if(targetScore>1000)
                {
                    stars[1].SetActive(true);
                    yield return new WaitForSeconds(1f);

                    if (targetScore > 1500)
                    {
                        stars[2].SetActive(true);
                        yield return new WaitForSeconds(1f);
                    }
                }
            }

            for(int i=0; i<buttons.Length;i++)
            {
                buttons[i].SetActive(true);
            }
        }
    }






    private void WallSpawner()
    {
        // Get the width and height of the screen
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Convert screen coordinates to world coordinates for spawn positions
        Vector3 leftSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenHeight / 2, -Camera.main.transform.position.z));
        Vector3 rightSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight / 2, -Camera.main.transform.position.z));
        Vector3 bottomSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth / 2, 0f, -Camera.main.transform.position.z));
        Vector3 topSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth / 2, screenHeight, -Camera.main.transform.position.z));

        // Instantiate walls at the calculated spawn positions
        GameObject wall_left = Instantiate(wallPrefab, leftSpawnPosition, Quaternion.identity, wallParent);
        GameObject wall_right = Instantiate(wallPrefab, rightSpawnPosition, Quaternion.identity, wallParent);
        GameObject wall_bottom = Instantiate(wallPrefab, bottomSpawnPosition, Quaternion.identity, wallParent);
        GameObject wall_top = Instantiate(wallPrefab, topSpawnPosition, Quaternion.identity, wallParent);

        // Calculate distances between spawn positions to determine the size of walls
        float distance1 = Vector3.Distance(topSpawnPosition, bottomSpawnPosition);
        float distance2 = Vector3.Distance(rightSpawnPosition, leftSpawnPosition);

        // Set the scale of the walls based on the calculated distances
        wall_left.transform.localScale = new Vector3(0.2f, distance1, 1);
        wall_right.transform.localScale = new Vector3(0.2f, distance1, 1);
        wall_top.transform.localScale = new Vector3(distance2, 0.2f, 1);
        wall_bottom.transform.localScale = new Vector3(distance2, 0.2f, 1);

        wall_bottom.GetComponent<BoxCollider2D>().isTrigger = true;
        wall_bottom.tag = "Wall_Bottom";

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("OpenScene");
    }
    public void NextLevel()
    {

    }

}
