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
    [SerializeField] private float bounce_count;
    [SerializeField] private TMP_Text bonus_text;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
        bonus_text.text = "Bonus : x" + bounce_count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("Stick"))
        {
            Vector2 randomForce = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
            rb.AddForce(randomForce * moveSpeed/2, ForceMode2D.Impulse);
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
        if(collision.gameObject.CompareTag("Wall_Bottom"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(collision.gameObject.CompareTag("Hoop"))
        {

        }
    }
}
