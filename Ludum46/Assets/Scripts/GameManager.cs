using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int health = 5;
    private static float firehealth = 1;
    private static bool invincible;
    private static int score;
    private static bool gameOver = false;
    private bool start = true;
    private static bool invincibleReset = false;
    private void Start()
    {
        health = 5;
        firehealth = 1;
        invincible = false;
        score = 0;
        gameOver = false;
        start = true;
        GameObject.FindGameObjectWithTag("TitleButton").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("RestartButton").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.FindGameObjectWithTag("TitleButton").GetComponent<MenuButtons>().enabled = false;
        GameObject.FindGameObjectWithTag("RestartButton").GetComponent<MenuButtons>().enabled = false;
        GameObject.FindGameObjectWithTag("TitleButton").GetComponent<BoxCollider2D>().enabled = false;
        GameObject.FindGameObjectWithTag("RestartButton").GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Update()
    {
        if(health <= 0 || firehealth <= 0)
        {
            StartCoroutine(NewGame());
        }
        if(firehealth > 80 && start == true)
        {
            start = false;
        }
        if (start == true)
        {
            firehealth += .5f;
        }
        if (invincibleReset)
        {
            invincibleReset = false;
            StartCoroutine(InvincibilityReset());
        }
    }
    public static void SetHealth(int h)
    {
        health = h;
        health = Mathf.Clamp(health, 0, 5);
    }
    public static int GetHealth()
    {
        return health;
    }
    public static void DamagePlayer(int h)
    {
        if (!invincible)
        {
            health -= h;
            health = Mathf.Clamp(health, 0, 5);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().DamagePlayer();
            invincible = true;
            invincibleReset = true;
        }
    }
    public static void SetFireHealth(float h)
    {
        firehealth = h;
        firehealth = Mathf.Clamp(firehealth, 0, 100);
    }
    public static float GetFireHealth()
    {
        return firehealth;
    }
    public static void DamageFire(float h)
    {
        firehealth -= h;
        firehealth = Mathf.Clamp(firehealth, 0, 100);
        if(firehealth == 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Fire"));
        }
    }
    public static void HealFire(float h)
    {
        if (h > 2)
        {
            score += (int)h * 100;
        }
        firehealth += h;
        firehealth = Mathf.Clamp(firehealth, 0, 100);
        GameObject.FindGameObjectWithTag("Fire").GetComponent<FireController>().woodburn = h/15;
        GameObject.FindGameObjectWithTag("Fire").GetComponent<FireController>().SpawnParticles((int)h);
    }
    public static void SetInvincible(bool i)
    {
        invincible = i;
    }
    public static int GetScore()
    {
        return score;
    }
    public static void AddScore(int s)
    {
        score += s;
    }
    public static bool GetGameOverStatus()
    {
        return gameOver;
    }
    public IEnumerator NewGame()
    {
        gameOver = true;
        firehealth -= 1;
        firehealth = Mathf.Clamp(firehealth, 0, 100);
        float duration = 3f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        GameObject.FindGameObjectWithTag("TitleButton").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("RestartButton").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.FindGameObjectWithTag("TitleButton").GetComponent<MenuButtons>().enabled = true;
        GameObject.FindGameObjectWithTag("RestartButton").GetComponent<MenuButtons>().enabled = true;
        GameObject.FindGameObjectWithTag("TitleButton").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.FindGameObjectWithTag("RestartButton").GetComponent<BoxCollider2D>().enabled = true;
    }
    public IEnumerator InvincibilityReset()
    {
        float duration = 3f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        invincible = false;
    }
    }
