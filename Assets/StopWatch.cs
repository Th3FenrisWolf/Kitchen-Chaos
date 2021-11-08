using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatch : MonoBehaviour
{
    float timer;
    bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (running) {
            timer += Time.deltaTime;
            float seconds = (int)(timer % 60);
            float minutes = (int)(timer / 60);
            float hours = (int)(timer / 3600);

            gameObject.GetComponent<Text>().text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void StopTimer() {
        running = false;
    }
}
