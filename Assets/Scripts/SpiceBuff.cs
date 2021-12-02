using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceBuff : MonoBehaviour
{
    float timer;
    float buffMultiplier = 1.5f;
    ThirdPersonMovement PlayerMovement;
    // Start is called before the first frame update
    void Awake()
	{
        PlayerMovement = GameObject.Find("Main Character").GetComponent<ThirdPersonMovement>();
    }

    private void OnTriggerStay(Collider other)
	{
        if (Input.GetKey("e")){
            timer = 0;
            Debug.Log("Game object: " + PlayerMovement.isBuffed);

            
            

            if (!PlayerMovement.isBuffed)
            {
                PlayerMovement.speed = PlayerMovement.speed * buffMultiplier;
                PlayerMovement.jump = PlayerMovement.jump * buffMultiplier;
                PlayerMovement.isBuffed = true;
            }
            Debug.Log("Game object: " + PlayerMovement.isBuffed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.isBuffed) {
            timer += Time.deltaTime;
        }
        if (timer >= 10)
		{
            //return the base stats to normal
            ReturnToBaseStats(PlayerMovement);
            PlayerMovement.isBuffed = false;
            timer = 0;
		}
    }

    void ReturnToBaseStats(ThirdPersonMovement PlayerMovement)
	{
        PlayerMovement.speed = PlayerMovement.speed / buffMultiplier;
        PlayerMovement.jump = PlayerMovement.jump / buffMultiplier;
	}
}
