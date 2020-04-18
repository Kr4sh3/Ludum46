using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Bullet attributes, obtained from gun class
    public float bulletSpeed;
    public float spread;
    public float bulletDamage;
    public float knockbackMultiplier;
    public bool isGunBullet = false;
    //References
    protected Rigidbody2D rb;
    protected GameObject player;
    public GameObject impact;
    protected Vector3 target;
    protected Camera cameraMain;
    private float time;

    protected virtual void Start()
    {
        //Assign Private Variables
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
            cameraMain = Camera.main;
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
            //If we dont hit anything after 5 seconds, we'll destroy the bullet
            Destroy(gameObject, 5f);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void Update()
    {
            time += Time.deltaTime * 1000;
            transform.localRotation = Quaternion.Euler(0, 0, time);
    }
}
