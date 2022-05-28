using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Rigidbody2D rb;
    bool jump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();     
        jump = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (jump && rb.velocity.y < 0.0f)
        {
            rb.AddForce(new Vector2(0.0f, 500.0f));
            jump = false;
        }
    }
}
