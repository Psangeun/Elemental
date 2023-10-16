using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneController : MonoBehaviour
{
    Rigidbody2D rb;

    void Update()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -4f, 1f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Line")
        {
            gameObject.SetActive(false);
        }
    }

}
