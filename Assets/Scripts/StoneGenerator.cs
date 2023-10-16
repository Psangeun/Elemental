using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : MonoBehaviour
{
    public ObjectManager objectManager;
    string obstacleObj;
    float timer = 0;

    void Awake()
    {
        obstacleObj = "stone";
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            timer = 0;
            GameObject newObstacle = objectManager.MakeObj(obstacleObj);
            newObstacle.transform.position = new Vector3(-7.5f, 12f, 0);
        }
    }
}