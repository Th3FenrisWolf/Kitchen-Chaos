using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float pickupDistanceRange;
    public float throwForce = 5;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundToPlay;
    private AudioSource audio;
    public bool touched = false;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, player.position);
        hasPlayer = (distanceFromPlayer <= 2.5f);

        if (hasPlayer && Input.GetKey(KeyCode.E)) {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }

        if (beingCarried) {
            // stops carried object from going through walls
            if (touched) {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }

            // throw carried object
            if (Input.GetMouseButtonDown(0)) {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                // TODO: RandomAudio();
            }
            
            // drop carried object
            if (Input.GetMouseButton(1)) {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
            }
        }
    }

    void RandomAudio() {
        if (audio.isPlaying) {
            return;
        }

        audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        audio.Play();
    }

    void OnTriggerEnter() {
        if (beingCarried) {
            touched = true;
        }
    }
}
