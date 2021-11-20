using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float minStunVelocity = 0.5f;
    [SerializeField] ParticleSystem hitParticle = null;
    private ParticleSystem tempParticle = null;

    // Start is called before the first frame update
    void Start()
    {
        audio =  GameObject.Find("Main Character").GetComponent<AudioSource>();

        if (gameObject.name.Contains("Carrot")) {
            hitParticle = GameObject.Find("CarrotHit").GetComponent<ParticleSystem>();
        } else if (gameObject.name.Contains("Lemon")) {
            hitParticle = GameObject.Find("LemonHit").GetComponent<ParticleSystem>();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name.Equals("cat") && gameObject.GetComponent<Rigidbody>().velocity.magnitude >= minStunVelocity) {
            collision.gameObject.GetComponentInParent<navigation>().Stun();
        }

        if (audio != null && gameObject.GetComponent<Rigidbody>().velocity.magnitude >= 0.5f) {
            Debug.Log(gameObject.name);
            Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
            audio.PlayOneShot(audio.clip);
            audio.volume = 0.1f / Vector3.Distance(gameObject.transform.position, collision.transform.position) + 0.3f;
            if (hitParticle != null) {
                if (tempParticle != null) {
                    Destroy(tempParticle);
                }
                tempParticle = Instantiate(hitParticle, transform.position, Quaternion.identity);
                tempParticle.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(gameObject.transform.position, player.position);
        hasPlayer = (distanceFromPlayer <= 2.5f);

        if (hasPlayer && Input.GetKey(KeyCode.E)) {
            transform.position += new Vector3(0f, 0.1f, 0f);
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
