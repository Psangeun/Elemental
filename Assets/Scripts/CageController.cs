using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CageController : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water" && gameManager.waterHaveKey)
        {
            gameObject.SetActive(false);
            Invoke("SceneOver", 1f);
        }
    }

    void SceneOver()
    {
        if (SceneManager.GetActiveScene().name.Contains("Fire"))
        {
            GameManager.gainFire = true;
        }
        else if(SceneManager.GetActiveScene().name.Contains("Air"))
        {
            GameManager.gainAir = true;
        }
        else if(SceneManager.GetActiveScene().name.Contains("Earth"))
        {
            GameManager.gainEarth = true;
        }
        SceneManager.LoadScene("BattleMap");
    }
}
