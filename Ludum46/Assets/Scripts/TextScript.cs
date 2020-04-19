using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public bool scoreText;
    private float alphalevel;
    // Start is called before the first frame update
    void Start()
    {
        alphalevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText)
        {
            GetComponent<Text>().text = "Score: " + GameManager.GetScore();
        }else if (GameManager.GetGameOverStatus())
        {
            GetComponent<Text>().color = new Color(255, 255, 255, alphalevel);
            alphalevel += Time.deltaTime;
        }
    }
}
