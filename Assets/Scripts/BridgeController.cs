using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    GameManager gameManager;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        gameObject.SetActive(true);
    }

    void Update()
    {
        if(gameManager.isButtonDown)
        {
            gameObject.SetActive(false);
        }
    }
}
