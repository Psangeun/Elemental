using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    PlayerController playerController;
    SpriteRenderer button;

    public Sprite buttonDown;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        button = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water" && playerController.characterNumber == 3)
        {
            GameManager.Instance.isButtonDown = true;

            button.sprite = buttonDown;
        }
    }
}
