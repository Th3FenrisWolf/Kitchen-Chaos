using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
	public bool nearLight = false;
	public bool lightOn = true;
	public GameObject light;

    private void OnTriggerEnter(Collider other)
	{
		nearLight = true;
	}
	private void OnTriggerExit(Collider other)
	{
		nearLight = false;
	}
	void Update()
	{
		if (Input.GetKeyDown("e") && nearLight)
		{
			if (lightOn)
			{
				lightOn = false;
				Debug.Log("Switch has been turned off");
			}
			else
			{
				lightOn = true;
				Debug.Log("Switch has been turned on");
			}
			light.SetActive(lightOn);
		}
	}
}
