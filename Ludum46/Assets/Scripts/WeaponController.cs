using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Camera cameraMain;
    public GameObject bullet;
    public GameObject logBullet;
    public float cooldown;
    private float cooldownTimer = 10;
    private Transform instantiationpoint;
    public int chargecounter;
    public bool isSucking = false;
    public bool clogged = false;
    private Animator anim;
    private AudioSource aud;
    private bool sucstart;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = Camera.main;
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetGameOverStatus() == false)
        {
            RotSelf();
            FlipSelf();
            Controls();
            instantiationpoint = transform.GetChild(0).transform;
            if (clogged)
            {
                anim.SetBool("Clogged", true);
            }
        }
        else aud.Stop();
    }
    void Flip(int xory)//Flips the gun's x or y scale
    {
        //0 = x
        //1 = y
        Vector3 localScalexory = transform.localScale; //Get our scale
        localScalexory[xory] = localScalexory[xory] * -1; //Flip it
        transform.localScale = localScalexory; //Set it
    }
    private void FlipSelf()//Flips the gun if it is facing the wrong direction
    {
        //Flipping if the player is flipped
        if (transform.parent.transform.localScale.x < 0) //If character facing left
        {
            if (transform.localScale.x > 0) //And we're facing right
            {
                Flip(0);
            }
        }
        if (transform.parent.transform.localScale.x > 0) //If character facing right
        {
            if (transform.localScale.x < 0) //And we're facing left
            {
                Flip(0);
            }
        }
        //Flipping if we are rotated the wrong direction
        if (transform.localEulerAngles.z > 90 && transform.localRotation.eulerAngles.z < 270) //If rotated towards left
        {
            if (transform.localScale.y > 0) //And facing right
            {
                Flip(1);
            }
        }
        if (transform.localRotation.eulerAngles.z < 90 || transform.localRotation.eulerAngles.z > 270) //If rotated towards right
        {
            if (transform.localScale.y < 0) //And facing left
            {
                Flip(1);
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
    private void Controls()
    {
        cooldownTimer += Time.deltaTime;
        if (Input.GetAxis("Fire1") == 1 && cooldownTimer >= cooldown && Input.GetAxis("Fire2") == 0)
        {
            chargecounter++;
            cooldownTimer = 0;
            if(chargecounter > 1)
            {
                anim.SetBool("Charged", true);
            }
        }
        if (Input.GetAxis("Fire1") == 0 && chargecounter > 0)
        {
            anim.SetBool("Charged", false);
            if (clogged == false)
            {
                GameObject projectile1 = Instantiate(bullet, instantiationpoint.position, Quaternion.identity);
                if (chargecounter == 1)
                {
                    aud.clip = Resources.Load<AudioClip>("Sounds/Gust2");
                    projectile1.GetComponent<BulletController>().bulletDamage = 1;
                    projectile1.GetComponent<BulletController>().bulletSpeed = 1;
                }
                else if (chargecounter == 2)
                {
                    aud.clip = Resources.Load<AudioClip>("Sounds/Gust1");
                    projectile1.GetComponent<BulletController>().bulletDamage = 2;
                    projectile1.GetComponent<BulletController>().bulletSpeed = 2;
                    projectile1.transform.localScale = projectile1.transform.localScale * 1.25f;
                }
                else if (chargecounter >= 3)
                {
                    aud.clip = Resources.Load<AudioClip>("Sounds/Gust1");
                    projectile1.GetComponent<BulletController>().bulletDamage = 3;
                    projectile1.GetComponent<BulletController>().bulletSpeed = 3;
                    projectile1.transform.localScale = projectile1.transform.localScale * 1.5f;
                }
                projectile1.GetComponent<BulletController>().isMainAttack = true;
                projectile1.GetComponent<BulletController>().DestroySelf();
            }
            else
            {
                GameObject projectile2 = Instantiate(logBullet, instantiationpoint.position, Quaternion.identity);
                if (chargecounter == 1)
                {
                    projectile2.GetComponent<BulletController>().bulletDamage = 2;
                    projectile2.GetComponent<BulletController>().bulletSpeed = 15;
                }
                else if (chargecounter == 2)
                {
                    projectile2.GetComponent<BulletController>().bulletDamage = 4;
                    projectile2.GetComponent<BulletController>().bulletSpeed = 20;
                }
                else if (chargecounter >= 3)
                {
                    projectile2.GetComponent<BulletController>().bulletDamage = 6;
                    projectile2.GetComponent<BulletController>().bulletSpeed = 25;
                }
                projectile2.GetComponent<Rigidbody2D>().drag = 5;
                projectile2.GetComponent<BulletController>().isMainAttack = false;
                clogged = false;
                anim.SetBool("Clogged", false);
            }
            aud.Play();
            aud.volume = 1;
            chargecounter = 0;
        }
        if (Input.GetAxis("Fire2") == 1 && Input.GetAxis("Fire1") == 0 && clogged == false)
        {
            aud.clip = Resources.Load<AudioClip>("Sounds/Suction");
            if (!aud.isPlaying)
            {
                aud.loop = true;
                aud.Play();
            }
            transform.GetChild(0).GetComponent<Animator>().SetBool("Sucking", true);
            isSucking = true;
            anim.SetBool("Sucking", true);
        }
        else
        {
            if(aud.clip == Resources.Load<AudioClip>("Sounds/Suction"))
            {
                aud.Stop();
            }
            aud.loop = false;
            isSucking = false;
            transform.GetChild(0).GetComponent<Animator>().SetBool("Sucking", false);
            anim.SetBool("Sucking", false);
        }
    }
}
