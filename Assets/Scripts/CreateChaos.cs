using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateChaos : MonoBehaviour
{
	public bool nearChaos = false;
	public bool chaosActive = false;
	public GameObject Chaos;

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
		}
	}
}
