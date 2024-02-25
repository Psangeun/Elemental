using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    private static GameManager instance;

    // ���� �Ŵ����� ���� �������� ������ �����ϴ� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ������ ���� ����
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // Scene�� GameManager�� ���� ���
                if (instance == null)
                {
                    // ���ο� GameObject�� �����Ͽ� GameManager ������Ʈ�� �߰��ϰ� �ν��Ͻ��� ����
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    public bool waterHaveKey = false;
    public bool isDoorOpen = false;
    public bool clear = false;
    public bool isGameover = false;

    public bool gainFire = false;
    public bool gainAir = false;
    public bool gainEarth = false;

    public bool isAllClear = false;
    public bool isDevilDie = false;

    public bool isButtonDown = false;

    // Awake���� �ν��Ͻ��� �����ϰ� �ߺ��Ǵ� �ν��Ͻ��� �ִ��� Ȯ��
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene ��ȯ �ÿ��� �����ǵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� ��쿡�� ���� ������ ���� �ı�
        }
    }

    void Update()
    {
        if(clear)
        {
            Clear();
        }
        if (isGameover)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
        }
    }

    void Clear()
    {
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        Invoke("SceneOver", 1f);
    }

    void SceneOver()
    {
        if(SceneManager.GetActiveScene().name.Contains("Battle"))
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            SceneManager.LoadScene(1 + SceneManager.GetActiveScene().buildIndex);
        }
    }

    public IEnumerator GameOver()
    {
        while(isGameover)
        {
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
