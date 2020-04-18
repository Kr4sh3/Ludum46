using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;
    public SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        UnityEngine.Vector2 moveVelocity = new UnityEngine.Vector2(moveX, moveY);
        moveVelocity = UnityEngine.Vector2.ClampMagnitude(moveVelocity * speed, speed);
        rb.velocity = moveVelocity;
        //Flip Sprite
        if(moveX < 0 && sr.flipX == false)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (moveX > 0 && sr.flipX == true)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
