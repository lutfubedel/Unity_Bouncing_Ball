using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Ball_Script : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 lastVelocity;

    [SerializeField] private float moveSpeed;
    [SerializeField] public float bounce_count;
    [SerializeField] private Text bonus_text;
    [SerializeField] private GameObject panelDead;

    public bool isDead, isCompleted, canHoop;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
        isCompleted = false;
        canHoop = true;
    }

    void Update()
    {
        lastVelocity = rb.velocity;
        bonus_text.text = "x" + bounce_count.ToString();

        if (isDead)
        {
            rb.simulated = false;
            panelDead.SetActive(true);
        }

        if (isCompleted)
        {
            rb.simulated = false;
        }

        if (!canHoop)
        {
            Invoke(nameof(HoopControl), 0.001f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            Vector2 randomForce = new Vector2(Random.Range(-2f, 2f), 2f).normalized;
            rb.AddForce(randomForce * moveSpeed, ForceMode2D.Impulse);
            bounce_count = 0;
        }
        else
        {
            Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * moveSpeed;
            bounce_count++;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall_Bottom"))
        {
            isDead = true;
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hoop") && canHoop)
        {
            Vector3 hoopTopPosition = collision.ClosestPoint(transform.position);
            if (transform.position.y > hoopTopPosition.y)
            {
                isCompleted = true;
            }
        }

        if (collision.CompareTag("Bottom_Hoop"))
        {
            canHoop = false;
        }
    }

    private void HoopControl()
    {
        canHoop = true;
    }

}
