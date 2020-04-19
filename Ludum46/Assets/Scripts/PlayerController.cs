using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;
    public SpriteRenderer sr;
    public Animator anim;
    private int damagetimer = 0;
    public Material sprDef;
    public Material sprWhite;
    public AudioSource aud;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetGameOverStatus() == false)
        {
            Movement();
        }
        else
        {
            sr.material = sprDef;
            aud.Stop();
            rb.velocity = new UnityEngine.Vector2(0, 0);
        }
    }

    void Movement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        UnityEngine.Vector2 moveVelocity = new UnityEngine.Vector2(moveX, moveY);
        moveVelocity = UnityEngine.Vector2.ClampMagnitude(moveVelocity * speed, speed);
        rb.velocity = moveVelocity;
        //Flip Sprite
        if (moveX < 0 && sr.flipX == false)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (moveX > 0 && sr.flipX == true)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (moveX != 0 || moveY != 0)
        {
            anim.SetBool("Running", true);
        }else anim.SetBool("Running", false);
    }
    public void DamagePlayer()
    {
        StartCoroutine(Flash(8));
        sr.material = sprWhite;
    }
    public IEnumerator Flash(int f)
    {
        float duration = .2f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        if (f % 2 == 0 && GameManager.GetGameOverStatus() == false)
        {
            sr.material = sprWhite;
        }
        else
        {
            sr.material = sprDef;
        }
        f--;
        if(f > 0)
        {
            StartCoroutine(Flash(f));
        }
    }
}
