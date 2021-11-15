using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructionsStart : MonoBehaviour
{
    public GameObject instructions;
    private bool pastInstructions = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        instructions.transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pastInstructions && Input.GetKeyDown(KeyCode.Return)) {
            pastInstructions = true;
            instructions.transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);
            instructions.SetActive(false);
            Time.timeScale = 1;
        }

    }
}
