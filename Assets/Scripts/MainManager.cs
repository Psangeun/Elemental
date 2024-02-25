using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainManager : MonoBehaviour
{
    public Text startText;

    bool isStart = false;
    float interval = 0.6f;

    void Start()
    {
        StartCoroutine(Blink());

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStart = true;
        }

        if (isStart)
        {
            startText.text = "";
            SceneOver();
        }
    }

    IEnumerator Blink()
    {
        while (!isStart)
        {
            startText.text = "click to start";
            yield return new WaitForSeconds(interval);
            startText.text = "";
            yield return new WaitForSeconds(interval);
        }
    }

    public void SceneOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
