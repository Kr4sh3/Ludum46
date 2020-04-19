using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Vector3 target;
    public Rigidbody2D rb;
    public int contactDamage;
    public int health;
    public int firedamage;
    private Animator anim;
    public bool touchingfire;
    public float attackcooldown;
    public float attacktimer;
    public GameObject logItem;
    public float damagesize;
    public SpriteRenderer spr;
    private AudioSource aud;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Fire").transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetGameOverStatus() == false)
        {

            if (health > 0 && touchingfire == false)
            {
                //Move Enemy towards fire
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
                anim.SetBool("Watering", false);
            }
            if (health <= 0)
            {
                anim.SetBool("Dead", true);
                rb.drag = 12;
            }
            if (touchingfire)
            {
                attacktimer += Time.deltaTime;
                anim.SetBool("Watering", true);
            }
            if (attacktimer > attackcooldown && health > 0)
            {
                attacktimer = 0;
                aud.clip = Resources.Load<AudioClip>("Sounds/WaterDrip");
                aud.Play();
                GameManager.DamageFire(firedamage);
            }
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, .7f);
            touchingfire = false;
            foreach (Collider2D c in collisions)
            {
                if (c.gameObject.CompareTag("Fire"))
                {
                    touchingfire = true;
                }
            }
            transform.localScale = new Vector3(1 - damagesize, 1 - damagesize, 1);
            if (damagesize > 0 && health > 0)
            {
                damagesize -= Time.deltaTime;
                spr.color = new Color(255, 0, 0);

            }
            else spr.color = new Color(255, 255, 255);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GetGameOverStatus() == false)
        {
            if (collision.gameObject.CompareTag("Player") && health > 0)
            {
                GameManager.DamagePlayer(1);
            }
            if (collision.gameObject.CompareTag("Fire") && health > 0)
            {
                DamageEnemy(1);
            }
        }
    }
    public void DamageEnemy(int d)
    {
        if (GameManager.GetGameOverStatus() == false && health > 0)
        {
            damagesize = .2f;
            if (health - d <= 0 && health > 0)
            {
                int randint = Random.Range(0, 2);
                if(randint > 0)
                {
                    aud.clip = Resources.Load<AudioClip>("Sounds/TreeDeath2");
                } else aud.clip = Resources.Load<AudioClip>("Sounds/TreeDeath3");
                aud.Play();
                Destroy(gameObject, 1f);
                StartCoroutine(SpawnLog());
                GameManager.AddScore(250);
            }
            else
            {
                aud.clip = Resources.Load<AudioClip>("Sounds/TreeDeath1");
                aud.Play();
            }
            health -= d;
        }
    }
    public IEnumerator SpawnLog()
    {
        float duration = 1f; 
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
            GameObject log = Instantiate(logItem, transform.position, Quaternion.identity);
            log.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(0, 10), Random.Range(0, 10));
    }
}
