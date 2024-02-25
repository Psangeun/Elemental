using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    PlayerController playerController;

    public GameObject top;
    public GameObject bottom;

    public Sprite openTop;
    public Sprite openBottom;

    SpriteRenderer rendTop;
    SpriteRenderer rendBottom;


    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        rendTop = top.GetComponentInChildren<SpriteRenderer>();
        rendBottom = bottom.GetComponentInChildren<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water" && playerController.haveKey)
        {
            rendTop.sprite = openTop;
            rendBottom.sprite = openBottom;
            GameManager.Instance.isDoorOpen = true;
        }
    }
}
