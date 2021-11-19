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
	public AudioClip switchOn;
	public AudioClip switchOff;
	private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
	{
		nearLight = true;
	}
	private void OnTriggerExit(Collider other)
	{
		nearLight = false;
	}

    private void Start() {
		audioSource = gameObject.GetComponent<AudioSource>();

	}

    void Update()
	{
		if (Input.GetKeyDown("e") && nearLight)
		{
			if (lightOn)
			{
				lightOn = false;
				audioSource.clip = switchOff;
				audioSource.Play();
			}
			else
			{
				lightOn = true;
				audioSource.clip = switchOn;
				audioSource.Play();
			}

			// update score
			float value = int.Parse(scoreText.text.Split(':')[1]);
			scoreText.text = string.Format("Chaos Score: {0}", value + chaosValue);

			light.SetActive(lightOn);
		}
	}
}
