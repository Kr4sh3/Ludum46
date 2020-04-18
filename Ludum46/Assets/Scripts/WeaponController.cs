using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Camera cameraMain;
    public GameObject bullet;
    public float cooldown;
    private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RotSelf();
        FlipSelf();
        Controls();
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
       if (Input.GetAxis("Fire1") == 1 && cooldownTimer > cooldown){
            Instantiate(bullet,transform.position,Quaternion.identity);
            cooldownTimer = 0;
        }
        if (Input.GetAxis("Fire2") == 1)
        {
            print("fire2");
        }
    }
}
