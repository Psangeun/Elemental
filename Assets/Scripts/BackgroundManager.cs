using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main씬에서 배경화면이 바뀌게 하는 스크립트
public class BackgroundManager : MonoBehaviour 
{
    public GameObject[] backgrounds;
    int backgroundCount = 0;
    public float interval = 2.6f;

    void Start()
    {
        StartCoroutine(ChangeBackgroundRoutine());
    }

    IEnumerator ChangeBackgroundRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            backgrounds[backgroundCount].SetActive(false);
            backgroundCount = (backgroundCount + 1) % backgrounds.Length;
            backgrounds[backgroundCount].SetActive(true);
        }
    }
}
