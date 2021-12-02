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
	public GameObject[] lights;
	public AudioClip switchOn;
	public AudioClip switchOff;
	private AudioSource audioSource;
	public GameObject switchObj;

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
				switchObj.GetComponent<Animator>().SetBool("toggle", true);
			} else
			{
				lightOn = true;
				audioSource.clip = switchOn;
				audioSource.Play();
				switchObj.GetComponent<Animator>().SetBool("toggle", false);
			}

			// update score
			float value = int.Parse(scoreText.text.Split(':')[1]);
			scoreText.text = string.Format("Chaos Score: {0}", value + chaosValue);

			foreach (GameObject light in lights) {
				light.SetActive(lightOn);
			}
		}
	}
}
