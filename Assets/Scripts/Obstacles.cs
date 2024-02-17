using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;


    [Header("Bools")]
    [SerializeField] private bool canRotate;
    [SerializeField] private bool canMove;

    public Transform targetPoint;


    private Vector3 startPoint; 
    private bool movingToTarget = true;


    private void Start()
    {
        startPoint = transform.position;
    }

    private void FixedUpdate()
    {
        if(canRotate)
        {
            transform.Rotate(new Vector3(0, 0, 1*rotateSpeed*Time.deltaTime));
        }

        if(canMove)
        {
            if (movingToTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPoint.position) < 0.01f)
                {
                    movingToTarget = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPoint) < 0.01f)
                {
                    movingToTarget = true;
                }
            }
        }
    }
}
