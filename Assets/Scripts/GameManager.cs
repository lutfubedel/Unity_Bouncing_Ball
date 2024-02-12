using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Wall :")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform wallParent;

    [Header("Timer")]
    [SerializeField] private Image bar_yellow;
    [SerializeField] private Image bar_red;
    [SerializeField] private float timer;


    void Start()
    {
        WallSpawner();
        Application.targetFrameRate = 60;

    }

    void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;
            float fillPercentage = timer / 60f;

            bar_yellow.fillAmount = fillPercentage;
            bar_red.fillAmount = fillPercentage;

            if (timer<20)
            {
                bar_yellow.gameObject.SetActive(false);
            }
        }
        else
        {

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
}
