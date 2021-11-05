using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
	public Text scoreText;
	public int chaosValue = 10;
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

			// update score
			float value = int.Parse(scoreText.text.Split(':')[1]);
			scoreText.text = string.Format("Chaos Score: {0}", value + chaosValue);

			light.SetActive(lightOn);
		}
	}
}
