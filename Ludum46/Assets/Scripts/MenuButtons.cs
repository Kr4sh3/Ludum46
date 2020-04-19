using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public bool isStartButton;
    public bool isRestartButton;
    public bool isQuitButton;
    public bool isTitleButton;
    public bool mousedOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseEnter()
    {
        if (isQuitButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumQuit2");
        }
        if (isStartButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumStart2");
        }
        if (isRestartButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumRestart2");
        }
        if (isTitleButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumTitle2");
        }
    }
    private void OnMouseExit()
    {
        if (isQuitButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumQuit1");
        }
        if (isStartButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumStart1");
        }
        if (isRestartButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumRestart1");

        }
        if (isTitleButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumTitle1");
        }
    }
    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        if (isStartButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumStart3");
        }
        if (isRestartButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumRestart3");
        }
        if (isQuitButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumQuit3");
        }
        if (isTitleButton)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LudumTitle3");
        }
        StartCoroutine(NextScene());
    }
    public IEnumerator NextScene()
    {
        float duration = 1f;
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            if(isQuitButton || isStartButton)
            {
                GameObject.FindGameObjectWithTag("MenuLight").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius -= Time.deltaTime * 10;
                GameObject.FindGameObjectWithTag("MenuLight").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius = Mathf.Clamp(GameObject.FindGameObjectWithTag("MenuLight").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius, 0, 10);
            }
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        if (isStartButton)
        {
            SceneManager.LoadScene(1);
        }
        if (isQuitButton)
        {
            Application.Quit();
        }
        if (isTitleButton)
        {
            SceneManager.LoadScene(0);
        }
        if (isRestartButton)
        {
            SceneManager.LoadScene(1);
        }
    }
}

