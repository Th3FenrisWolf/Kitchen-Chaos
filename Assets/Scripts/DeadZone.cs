using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public navigation catNav = null;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object is: " + other.tag);
        if (other.tag == "Player")
		{
            other.GetComponent<ThirdPersonMovement>().isEnabled = false;
            catNav = GameObject.Find("cat_Eat").GetComponent<navigation>();
            StartCoroutine(catNav.HandleDead());
        }
    }
}
