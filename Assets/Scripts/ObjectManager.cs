using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject fireBallPrefab;
    public GameObject stonePrefab;
    public GameObject skillPrefab;


    GameObject[] fireBall;
    GameObject[] stone;
    GameObject[] skill;

    GameObject[] targetPool;

    void Awake()
    {
        fireBall = new GameObject[20];
        stone = new GameObject[10];
        skill = new GameObject[3];

        Generate();
    }
    void Generate()
    {
        for (int index = 0; index < fireBall.Length; index++)
        {
            fireBall[index] = Instantiate(fireBallPrefab);
            fireBall[index].SetActive(false);
        }

        for (int index = 0; index < stone.Length; index++)
        {
            stone[index] = Instantiate(stonePrefab);
            stone[index].SetActive(false);
        }

        for (int index = 0; index < skill.Length; index++)
        {
            skill[index] = Instantiate(skillPrefab);
            skill[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "fireBall":
                targetPool = fireBall;
                break;
            case "stone":
                targetPool = stone;
                break;
            case "skill":
                targetPool = skill;
                break;
        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "fireBall":
                targetPool = fireBall;
                break;
            case "stone":
                targetPool = stone;
                break;
            case "skill":
                targetPool = skill;
                break;
        }
        return targetPool;
    }
}
