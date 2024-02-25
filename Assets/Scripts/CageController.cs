using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CageController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water" && GameManager.Instance.waterHaveKey)
        {
            gameObject.SetActive(false);
            Invoke("SceneOver", 1f);
        }
    }

    void SceneOver()
    {
        if (SceneManager.GetActiveScene().name.Contains("Fire"))
        {
            GameManager.Instance.gainFire = true;
        }
        else if(SceneManager.GetActiveScene().name.Contains("Air"))
        {
            GameManager.Instance.gainAir = true;
        }
        else if(SceneManager.GetActiveScene().name.Contains("Earth"))
        {
            GameManager.Instance.gainEarth = true;
        }
        SceneManager.LoadScene("BattleMap");
    }
}
