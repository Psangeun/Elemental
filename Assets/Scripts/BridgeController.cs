using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ư�� ������ ������� �볪�� �ٸ��� ���õ� ��ũ��Ʈ
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
