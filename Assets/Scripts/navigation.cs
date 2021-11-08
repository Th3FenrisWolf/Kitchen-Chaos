using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour
{
    public Transform target;
    public Transform agentTrans;
    public Transform[] patrolAnchors;
    NavMeshAgent agent;
    private int index = 0;
    private Animator anim;
    private bool isStunned = false;
    private int chaosStunValue = 50;
    public Text scoreText;
    public GameObject deadText;
    public GameObject stopwatch;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    IEnumerator HandleDead() {
        yield return new WaitForSeconds(2);

        deadText.SetActive(true);

        deadText.transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);

        stopwatch.GetComponent<StopWatch>().StopTimer();
    }
    IEnumerator timedTurnOffStun() {
        yield return new WaitForSeconds(3);

        isStunned = false;
        anim.SetTrigger("walk");
    }

    public void Stun() {
        // update score
        float value = int.Parse(scoreText.text.Split(':')[1]);
        scoreText.text = string.Format("Chaos Score: {0}", value + chaosStunValue);

        isStunned = true;
        anim.SetTrigger("stun");
        StartCoroutine(timedTurnOffStun());
    }

    // Update is called once per frame
    void Update()
    {
        if (deadText.activeSelf) {
            if (Input.GetKey(KeyCode.E)) {
                // reload scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }

            if (Input.GetKey(KeyCode.Escape)) {
                // go to the main screen
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
            }
        }

        if (isStunned) {
            agent.SetDestination(transform.position);
            return;
        }

        // transition player to dead state
        if (Vector3.Distance(agentTrans.position, target.position) < 2) {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.ResetPath();

            anim.SetTrigger("eat");

            // lock the player movement
            target.GetComponent<ThirdPersonMovement>().isEnabled = false;

            // wait then show dead screen
            StartCoroutine(HandleDead());
        }


        if (Vector3.Distance(agentTrans.position, target.position) >= 2 && Vector3.Distance(agentTrans.position, target.position) <= 15)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(patrolAnchors[index].position);
        }
        
        if (pathComplete())
        {
            if (index < patrolAnchors.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
    }

    protected bool pathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }
}
