using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallGenerator : MonoBehaviour
{
    public ObjectManager objectManager;
    string obstacleObj;
    float timer = 0;

    void Awake()
    {
        obstacleObj = "fireBall";
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.15f)
        {
            timer = 0;
            float ranX = Random.Range(-10f, 30f);
            GameObject newObstacle = objectManager.MakeObj(obstacleObj);
            newObstacle.transform.position = new Vector3(ranX, 12f, 0);
        }
    }
}