using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    bool jump;
    bool flap;

    bool gameOver = false;
    public bool GameOver { get => gameOver; }

    float InitPosY;

    void Awake()
    {
        InitPosY = transform.position.y;    
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();     
        animator = GetComponent<Animator>();
        jump = false;
        flap = false;
    }

    void Update()
    {
        if (gameOver)
        {
            flap = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        animator.SetBool("Flap", flap);
    }

    void FixedUpdate()
    {
        if (jump)
        {
            if (rb.velocity.y < 0.0f)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0.0f, 320.0f));
                flap = true;
            }
            jump = false;
        }
        else if(rb.velocity.y < 0.0f)
        {
            flap = false;
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
    public void ResetBird()
    {
        transform.position = new Vector3(transform.position.x, InitPosY, transform.position.z);
        rb.velocity = Vector2.zero;
        rb.simulated = true;
        gameOver = false;
    }
}
