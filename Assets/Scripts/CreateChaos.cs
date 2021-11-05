using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateChaos : MonoBehaviour
{
	public Text scoreText;
	public bool nearChaos = false;
	public bool chaosActive = false;
	public GameObject Chaos;
	public int chaosValue = 100;

	private void OnTriggerEnter(Collider other)
	{
		nearChaos = true;
	}
	private void OnTriggerExit(Collider other)
	{
		nearChaos = false;
	}
	void Update()
	{
		if (Input.GetKeyDown("e") && nearChaos && !chaosActive)
		{
			chaosActive = true;
			Debug.Log("Fire Started");
			Chaos.SetActive(chaosActive);

			// update score
			float value = int.Parse(scoreText.text.Split(':')[1]);
            scoreText.text = string.Format("Chaos Score: {0}", value + chaosValue);
		}
	}
}
