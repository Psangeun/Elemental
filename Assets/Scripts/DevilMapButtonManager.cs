using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DevilMapButtonManager : MonoBehaviour
{
    GameManager gameManager;

    public Button elementButton;
    public Button devilButton;
    public Button kingButton;

    public GameObject triangle1;
    public GameObject triangle2;
    public GameObject triangle3;

    public Sprite devilImage;
    public Sprite kingImage;

    float rotationSpeed = 200;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        elementButton.interactable = true;
        devilButton.interactable = false;
        kingButton.interactable = false;

        triangle1.SetActive(true);
        triangle2.SetActive(false);
        triangle3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        triangle1.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        triangle2.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        triangle3.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

        if (gameManager.isAllClear)
        {
            devilButton.GetComponent<Image>().sprite = devilImage;
            devilButton.interactable = true;
            triangle2.SetActive(true);
        }
        if (gameManager.isDevilDie)
        {
            kingButton.GetComponent<Image>().sprite = kingImage;
            kingButton.interactable = true;
            triangle3.SetActive(true);
        }
    }

    public void ScenePuzzleMAp()
    {
        SceneManager.LoadScene("DevilPuzzle1");
    }

    public void SceneDevilBattleMap()
    {
        SceneManager.LoadScene("DevilBattleMap");
    }

    public void SceneKingMap()
    {
        SceneManager.LoadScene("KingMap");
    }
}
