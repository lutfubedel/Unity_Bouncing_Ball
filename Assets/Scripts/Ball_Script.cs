using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Ball_Script : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 lastVelocity;

    [SerializeField] private float moveSpeed;
    [SerializeField] public float bounce_count;
    [SerializeField] private Text bonus_text;
    [SerializeField] private GameObject panelDead,panelCompleted;

    public bool isDead,isCompleted,canHoop;

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

        if(isDead)
        {
            rb.simulated = false;
            panelDead.SetActive(true);
        }

        print("CanHoop : " + canHoop);

        if(isCompleted)
        {
            rb.simulated = false;
        }

        if(!canHoop)
        {
            Invoke(nameof(HoopControl), 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            Vector2 randomForce = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
            rb.AddForce(randomForce * moveSpeed / 2, ForceMode2D.Impulse);
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
        if(collision.CompareTag("Wall_Bottom"))
        {
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hoop") && canHoop)
        {
            isCompleted = true;
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
