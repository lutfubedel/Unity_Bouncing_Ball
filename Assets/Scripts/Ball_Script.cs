using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Script : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 lastVelocity;

    public float moveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * moveSpeed;
    }
}
