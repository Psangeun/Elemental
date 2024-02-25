using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapButtonManager : MonoBehaviour
{
    public Button waterButton;
    public Button fireButton;
    public Button airButton;
    public Button earthButton;

    public GameObject triangle1;
    public GameObject triangle2;
    public GameObject triangle3;
    public GameObject triangle4;

    public Sprite fireImage;
    public Sprite airImage;
    public Sprite earthImage;

    float rotationSpeed = 200;

    void Start()
    {
        waterButton.interactable = false;
        fireButton.interactable = true;
        airButton.interactable = true;
        earthButton.interactable = true;

        triangle1.SetActive(false);
        triangle2.SetActive(true);
        triangle3.SetActive(true);
        triangle4.SetActive(true);
    }

    void Update()
    {
        triangle1.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        triangle2.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        triangle3.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        triangle4.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

        if (GameManager.Instance.gainFire)
        {
            fireButton.GetComponent<Image>().sprite = fireImage;
            fireButton.interactable = false;
            triangle2.SetActive(false);
        }
        if(GameManager.Instance.gainAir)
        {
            airButton.GetComponent<Image>().sprite = airImage;
            airButton.interactable = false;
            triangle3.SetActive(false);
        }
        if (GameManager.Instance.gainEarth)
        {
            earthButton.GetComponent<Image>().sprite = earthImage;
            earthButton.interactable = false;
            triangle4.SetActive(false);
        }

        if(GameManager.Instance.gainEarth && GameManager.Instance.gainAir && GameManager.Instance.gainFire)
        {
            SceneManager.LoadScene("DevilMap");
        }
    }

    public void SceneFireMap()
    {
        SceneManager.LoadScene("BattleMap_Fire");
    }

    public void SceneAirMap()
    {
        SceneManager.LoadScene("BattleMap_Air");
    }

    public void SceneEarthMap()
    {
        SceneManager.LoadScene("BattleMap_Earth");
    }
}
