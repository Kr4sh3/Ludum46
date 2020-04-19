using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;

public class FireController : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D firelight;
    public float woodburn;

    private void Start()
    {
        firelight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }
    // Update is called once per frame
    void Update()
    {
            GameManager.DamageFire(Time.deltaTime * 4);
            firelight.pointLightOuterRadius = GameManager.GetFireHealth() / 7 + 3 + woodburn;
            transform.localScale = new Vector3(GameManager.GetFireHealth() / 100 + .3f + woodburn, GameManager.GetFireHealth() / 100 + .3f + woodburn, 0);
            if (woodburn > 0)
            {
                woodburn -= Time.deltaTime;
            }
            if (woodburn < 0)
            {
                woodburn += Time.deltaTime;
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.GetGameOverStatus() == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.DamagePlayer(1);
            }
        }
    }
    public void SpawnParticles(int h)
    {
        if (h < 2)
        {
            int firechild = Random.Range(0, 5);
            transform.GetChild(firechild).GetComponent<ParticleSpawner>().SpawnParticle();
        }
        if(h == 2)
        {
            for(int i = 0; i < 2; i++)
            {
                int firechild = Random.Range(0, 5);
                transform.GetChild(firechild).GetComponent<ParticleSpawner>().SpawnParticle();
            }
        }
        if(h > 2)
        {
            for (int i = 0; i < 8; i++)
            {
                int firechild = Random.Range(0, 5);
                transform.GetChild(firechild).GetComponent<ParticleSpawner>().SpawnParticle();
            }
        }
    }
}
