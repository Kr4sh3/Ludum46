using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SuctionController : MonoBehaviour
{
    private Vector2 target;
    private Transform suctionarea;
    public int speed;
    private void Start()
    {
        suctionarea = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        target = suctionarea.GetChild(0).transform.position;
        if (collision.gameObject.CompareTag("Log"))
        {
            if (transform.parent.GetComponentInParent<WeaponController>().isSucking)
            {
                collision.transform.position = Vector2.MoveTowards(collision.transform.position, target, Time.deltaTime * speed);
            }
        }
    }
}
