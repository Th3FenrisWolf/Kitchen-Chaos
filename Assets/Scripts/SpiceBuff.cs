using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceBuff : MonoBehaviour
{
    float timer;
    float buffMultiplier = 1.5f;
    [SerializeField] ParticleSystem spiceEffect = null;
    ThirdPersonMovement playerMovement;

    // Start is called before the first frame update
    void Awake()
	{
        playerMovement = GameObject.Find("Main Character").GetComponent<ThirdPersonMovement>();
    }

    private void OnTriggerStay(Collider other)
	{
        if (Input.GetKey("e")){
            timer = 0;

            if (!playerMovement.isBuffed)
            {
                spiceEffect = GameObject.Find("SpicedPlayer").GetComponent<ParticleSystem>();
                spiceEffect.Play();
                playerMovement.speed *= buffMultiplier;
                playerMovement.jump *= buffMultiplier;
                playerMovement.isBuffed = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isBuffed) {
            timer += Time.deltaTime;


        }
        if (timer >= 10)
		{
            //return the base stats to normal
            ReturnToBaseStats(playerMovement);
            playerMovement.isBuffed = false;
            spiceEffect.Stop();
            timer = 0;
        }
    }

    void ReturnToBaseStats(ThirdPersonMovement playerMovement)
	{
        playerMovement.speed /= buffMultiplier;
        playerMovement.jump /= buffMultiplier;
	}
}
