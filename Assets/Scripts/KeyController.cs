using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    PlayerController playerController;
    Collider2D keyCol;
    Rigidbody2D keyRigid;

    float rotationSpeed = 200;

    // Start is called before the first frame update
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        keyCol = gameObject.GetComponent<Collider2D>();
        keyRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            keyRigid.isKinematic = true;
            keyCol.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water" && playerController.characterNumber == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
