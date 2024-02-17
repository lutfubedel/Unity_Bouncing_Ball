using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 wall_left;
    [SerializeField] private Vector3 wall_Right;

    void Start()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        rb = GetComponent<Rigidbody2D>();

        Vector3 leftSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenHeight / 2, -Camera.main.transform.position.z));
        Vector3 rightSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight / 2, -Camera.main.transform.position.z));

        wall_left = leftSpawnPosition;
        wall_Right = rightSpawnPosition;
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                moveSpeed = 25;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                rb.velocity = new Vector2(touch.deltaPosition.x * moveSpeed * Time.deltaTime, rb.velocity.y);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                moveSpeed = 0;
            }
            
        }

    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, wall_left.x+0.5f, wall_Right.x-0.5f);
        transform.position = viewPos;
    }
}
