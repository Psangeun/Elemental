using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1)
        {
            timerText.text = string.Format("0 : {0:00}", 60 - time);
        }
        if (time >= 60)
        {
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
        }

    }
}
