using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Rigidbody2D rb;
    bool jump;

    bool gameOver = false;
    public bool GameOver { get => gameOver; }
    public float BirdPositionX { get => transform.position.x; }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();     
        jump = false;
    }

    void Update()
    {
        if (!gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            if (rb.velocity.y < 0.0f)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0.0f, 320.0f));
            }
            jump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe")
        {
            gameOver = true;
            rb.simulated = false;
        }
    }
}
