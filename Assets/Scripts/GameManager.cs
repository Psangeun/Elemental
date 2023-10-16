using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public bool waterHaveKey = false;
    public bool isDoorOpen = false;
    public bool clear = false;
    public bool gameOver = false;

    public static bool gainFire = false;
    public static bool gainAir = false;
    public static bool gainEarth = false;

    public bool isAllClear = false;
    public bool isDevilDie = false;

    public bool isButtonDown = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(clear)
        {
            Clear();
        }
        if (gameOver)
        {
            GameOver();

            if(Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }

    void Clear()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        Invoke("SceneOver", 1f);
    }

    void SceneOver()
    {
        if(SceneManager.GetActiveScene().name.Contains("Battle"))
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            SceneManager.LoadScene(1 + SceneManager.GetActiveScene().buildIndex);
        }
    }

    void GameOver()
    {
        GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
