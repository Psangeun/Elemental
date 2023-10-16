using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    Rigidbody2D rb;

    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(2f, 0, 0));
        Invoke("SetActiveFalse", 5f);
    }

    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "plant")
        {
            gameObject.SetActive(false);
        }
    }
}