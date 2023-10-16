using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    PlayerController playerController;
    GameManager gameManager;
    SpriteRenderer button;

    public Sprite buttonDown;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        button = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water" && playerController.characterNumber == 3)
        {
            gameManager.isButtonDown = true;

            button.sprite = buttonDown;
        }
    }
}
