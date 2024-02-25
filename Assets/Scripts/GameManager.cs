using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static GameManager instance;

    // 게임 매니저에 대한 전역적인 접근을 제공하는 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로 생성
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                // Scene에 GameManager가 없을 경우
                if (instance == null)
                {
                    // 새로운 GameObject를 생성하여 GameManager 컴포넌트를 추가하고 인스턴스로 설정
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

    // Awake에서 인스턴스를 설정하고 중복되는 인스턴스가 있는지 확인
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene 전환 시에도 유지되도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 경우에는 새로 생성된 것을 파괴
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
