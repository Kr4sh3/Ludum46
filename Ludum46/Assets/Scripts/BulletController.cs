using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Bullet attributes, obtained from gun class
    public float bulletSpeed;
    public float spread;
    public int bulletDamage;
    public float knockbackMultiplier;
    //References
    protected Rigidbody2D rb;
    protected GameObject player;
    public GameObject impact;
    protected Vector3 target;
    protected Camera cameraMain;
    private float time;
    public bool isMainAttack;
    private Animator anim;

    protected virtual void Start()
    {
        if (GameManager.GetGameOverStatus() == false)
        {
        //Assign Private Variables
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
            cameraMain = Camera.main;
            anim = GetComponent<Animator>();
        }


        //Set Velocity Towards Mouse Pos
        {
            //Get Mouse Pos
            Vector3 mousePos = cameraMain.ScreenToWorldPoint(Input.mousePosition);
            //Make It Relative
            mousePos.x -= player.transform.position.x;
            mousePos.y -= player.transform.position.y;
            mousePos.z = 0;
            //Set Base Velocity
            target = mousePos;
            //Normalize And Set Speed Of Velocity
            target = target.normalized * bulletSpeed;
            //Set Velocity
            rb.velocity = target;
        }
        RotSelf();
        if (isMainAttack)
        {
            anim.SetBool("IsMainAttack", isMainAttack);
            anim.SetInteger("Damage", bulletDamage);
        }
    }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GetGameOverStatus() == false)
        {
        if (isMainAttack)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().DamageEnemy(bulletDamage);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = target.normalized * bulletDamage * 10;
                collision.gameObject.GetComponent<EnemyController>().touchingfire = false;
            }
            if (collision.gameObject.CompareTag("Fire"))
            {
                GameManager.HealFire(2);
                Destroy(gameObject);
            }
        }
        else
        {
                if(collision.gameObject != null)
                {
                    if (collision.gameObject.CompareTag("SuctionPoint") && GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>().isSucking)
                    {
                        GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>().clogged = true;
                        GameObject.FindGameObjectWithTag("Weapon").GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/LogPlunk");
                        GameObject.FindGameObjectWithTag("Weapon").GetComponent<AudioSource>().volume = .5f;
                        GameObject.FindGameObjectWithTag("Weapon").GetComponent<AudioSource>().Play();
                        Destroy(gameObject);
                    }
                    if (collision.gameObject.CompareTag("Fire"))
                    {
                        GameManager.HealFire(20);
                        Destroy(gameObject);
                    }
                    if (collision.gameObject.CompareTag("Enemy") && rb.velocity.x > 0 || rb.velocity.y > 0 && collision.gameObject.GetComponent<EnemyController>().health > 0)
                    {
                        collision.gameObject.GetComponent<EnemyController>().DamageEnemy(bulletDamage);
                        collision.gameObject.GetComponent<Rigidbody2D>().velocity = target.normalized * bulletDamage * 12;
                        collision.gameObject.GetComponent<EnemyController>().touchingfire = false;
                        GameManager.AddScore(bulletDamage * 25);
                    }
                }
        }
    }
    }
    public void DestroySelf()
    {
        if (GameManager.GetGameOverStatus() == false)
        {
            if (bulletDamage == 0 && isMainAttack)
            {
                Destroy(gameObject);
            }
            if (bulletDamage == 1 && isMainAttack)
            {
                Destroy(gameObject, .5f);
            }
            if (bulletDamage == 2 && isMainAttack)
            {
                Destroy(gameObject, .6f);
            }
            if (bulletDamage >= 3 && isMainAttack)
            {
                Destroy(gameObject, .7f);
            }
        }
    }
    private void RotSelf()
    {
        //Grab the current mouse position on the screen
        Vector3 mousePosition = cameraMain.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        //Make mouse position relative to the gun
        mousePosition -= transform.position;
        //Convert both lengths to radians
        float targetAngle = Mathf.Atan2(mousePosition.y, mousePosition.x);
        //Convert radians to degrees
        targetAngle *= Mathf.Rad2Deg;
        //Rotate to the target degrees
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }
}
