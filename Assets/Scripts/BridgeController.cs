using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 버튼을 누르면 사라지는 통나무 다리에 관련된 스크립트
public class BridgeController : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if(GameManager.Instance.isButtonDown)
        {
            gameObject.SetActive(false);
        }
    }
}
