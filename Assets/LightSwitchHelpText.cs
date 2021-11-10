using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchHelpText : MonoBehaviour
{
    public GameObject helperText;

    private void OnTriggerEnter(Collider other) {
        helperText.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        helperText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
